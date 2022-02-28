using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using ZXing;

public class QRFrameCapturer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The ARCameraManager which will produce frame events.")]
    private ARCameraManager m_CameraManager;

    [SerializeField]
    private GameObject _arObjectOnQRCode;

    //[SerializeField]
    //[Tooltip("The FrameViewer")]
    //private Image _frameViewer;

    public ARCameraManager cameraManager
    {
        get => m_CameraManager;
        set => m_CameraManager = value;
    }

    //private Texture2D m_CameraTexture;
    private Texture2D m_Texture;
    private bool onlyonce = false;
    //private bool doOnce=false;
    private BarcodeReader barCodeReader;
    private ARRaycastManager arRaycastManager;
    

    #region FrameViewCoordinates
    private int _rectSize;
    private Vector2Int _posRect;
    #endregion

    private bool _toScan = true;

    private void Start()
    {
        //InitRect();
        Vibration.Init();
        barCodeReader = new BarcodeReader();

        QRStateManager.Instance.captureStart += CaptureStart;
        QRStateManager.Instance.capturePause += CapturePause;
        QRStateManager.Instance.QRCodeReadSuccess += OnQRCodeReadSuccess;

        QRStateManager.Instance.CaptureStart();

    }
    void OnEnable()
    {
        if (m_CameraManager != null)
        {
            Debug.Log("Enable");
            m_CameraManager.frameReceived += OnCameraFrameReceived;
        }

        QRStateManager.Instance.CaptureStart();
    }

    void OnDisable()
    {
        
        if (m_CameraManager != null)
        {
            m_CameraManager.frameReceived -= OnCameraFrameReceived;
        }
    }

    void OnCameraFrameReceived(ARCameraFrameEventArgs eventArgs)
    {
        if(_toScan)
            ReadQR();
    }

     void ReadQR()
     {

        if ((Time.frameCount % 15) != 0)
            return;

         //You can set this number based on the frequency to scan the QRCode

        if (!cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
        {
            Debug.LogWarning("No last cpu image");
            return;
        }

        StartCoroutine(ProcessQRCode(image));
        image.Dispose();

     }

    private IEnumerator ProcessQRCode(XRCpuImage image)
    {
        // Create the async conversion request
        var request = image.ConvertAsync(new XRCpuImage.ConversionParams
        {
            // crop to frameView size
            inputRect = new RectInt(0, 0, image.width, image.height),

            // Downsample by 2
            outputDimensions = new Vector2Int(image.width / 2, image.height / 2),

            // Color image format
            outputFormat = TextureFormat.RGB24,

        });

        // Wait for it to complete
        while (!request.status.IsDone())
            yield return null;

        // Check status to see if it completed successfully.
        if (request.status != XRCpuImage.AsyncConversionStatus.Ready)
        {
            // Something went wrong
            Debug.LogErrorFormat("Request failed with status {0}", request.status);

            // Dispose even if there is an error.
            request.Dispose();
            yield break;
        }

        // Image data is ready. Let's apply it to a Texture2D.
        var rawData = request.GetData<byte>();
        // Create a texture if necessary
        if (m_Texture == null)
        {
            m_Texture = new Texture2D(
                request.conversionParams.outputDimensions.x,
                request.conversionParams.outputDimensions.y,
                request.conversionParams.outputFormat,
                false);
        }

        // Copy the image data into the texture
        m_Texture.LoadRawTextureData(rawData);
        m_Texture.Apply();

        byte[] barcodeBitmap = m_Texture.GetRawTextureData();
        LuminanceSource source = new RGBLuminanceSource(barcodeBitmap, m_Texture.width, m_Texture.height);


     
        Result decodedInfo = barCodeReader.Decode(source);
        if (decodedInfo != null && decodedInfo.Text != "")
        {
            QRStateManager.Instance.OnQRCodeReadSuccess();    
            string QRContents = decodedInfo.Text;
            Debug.Log(QRContents);

            var resultComparing = ProcessQRCode(QRContents);
            if(resultComparing != null)
            {
                Vibration.VibratePop();
                DoAfterReadRightCode();
            }
            else
            {
                //ignore this qrCode
            }
           
            

            //// Get the resultsPoints of each qr code contain the following points in the following order: index 0: bottomLeft index 1: topLeft index 2: topRight
            ////Note this depends on the oreintation of the QRCode. The below part is mainly finding the mid of the QRCode using result points and making a raycast hit from that pose.
            //ResultPoint[] resultPoints = decodedInfo.ResultPoints;
            //ResultPoint a = resultPoints[1];
            //ResultPoint b = resultPoints[2];
            //ResultPoint c = resultPoints[0];
            //Vector2 pos1 = new Vector2((float)a.X, (float)a.Y);
            //Vector2 pos2 = new Vector2((float)b.X, (float)b.Y);
            //Vector2 pos3 = new Vector2((float)c.X, (float)c.Y);
            //Vector2 pos4 = new Vector2(((float)b.X - (float)a.X) / 2.0f, ((float)c.Y - (float)a.Y) / 2.0f);
            //List<ARRaycastHit> aRRaycastHits = new List<ARRaycastHit>();

            ////Make a raycast hit to get the pose of the QRCode detected to place an object around it.
            //if (arRaycastManager.Raycast(new Vector2(pos4.x, pos4.y), aRRaycastHits, TrackableType.FeaturePoint) && aRRaycastHits.Count > 0)
            //{
            //    //To shift the object to a relative position by adding/subtracting a delta value, uncomment the below line.
            //    //Instantiate an object at Hitpose found on the QRCode


            //    //GameObject NewObjectToPlace = Instantiate(_arObjectOnQRCode, aRRaycastHits[0].pose.position, Quaternion.identity);


            //    //OR
            //    // Use default position to place the object in front of the camera if the Hit Pose is not found. //You can uncomment the below code for this default behaviour
            //    //defaultObjectPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, //Screen.height / 2, Camera.main.nearClipPlane));
            //    //OR
            //    //Reposition the Augmented Object by adding some delta
            //    //NewObjectToPlace.transform.position = new //Vector3(NewObjectToPlace.transform.position.x + xDelta, //NewObjectToPlace.transform.position.y, NewObjectToPlace.transform.position.z);
            //}
        }
      
        request.Dispose();
    }

    //private void InitRect()
    //{
    //    Debug.Log("initRect");
    //    //_rectSize = _frameViewer.rectTransform.sizeDelta;
    //    _rectSize = Mathf.FloorToInt(_frameViewer.rectTransform.sizeDelta.x);
    //    int x = Mathf.FloorToInt(Screen.width / 2 - _rectSize / 2);
    //    int y = Mathf.FloorToInt(Screen.height / 2 - _rectSize / 2);
    //    _posRect = new Vector2Int(x, y);
        
    //}

    
    private bool ProcessQRCode(string result)
    {
        return true;
    }

    private void DoAfterReadRightCode()
    {

    }

    private void CaptureStart()
    {
        _toScan = true;
    }

    private void CapturePause()
    {
        _toScan = false;
    }
    private void OnQRCodeReadSuccess()
    {
        _toScan = false;
    }
}
