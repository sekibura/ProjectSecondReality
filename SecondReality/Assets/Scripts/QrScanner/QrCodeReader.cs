using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using ZXing;

public class QrCodeReader : MonoBehaviour
{
    
    private BarcodeReader barCodeReader;
    private FrameCapturer _frameCapturer;
    private Animator _frameAnimator;


    [SerializeField]
    [Tooltip("Метод должен принимать параметр типа QRInfo")]
    private UnityEvent<QRInfo> _eventsOnSuccess;
    [SerializeField]
    private bool _doOnce;
    private bool _isEventsDone = false;



#if UNITY_EDITOR
    public bool test;
    public Texture2D QrCodeForTest;
#endif



    private float _time;
    private float _delayBetweenSameCode = 2f;

    private string _lastQR = "";
    Result frameDecodeData = null;

    void Start()
    {
        Vibration.Init();
        _time = Time.time;
        barCodeReader = new BarcodeReader();
        Resolution currentResolution = Screen.currentResolution;
        _frameCapturer = gameObject.GetComponent<FrameCapturer>();
        _frameAnimator = _frameCapturer._frameViewerImage.GetComponent<Animator>();
    }

    void Update()
    {
        if (_frameCapturer.State == FrameCapturer.RecorderState.Recording)
        {
            Resolution currentResolution = Screen.currentResolution;
            try
            {
                if (_frameCapturer.Frames == null)
                {
                    return;
                }
                if (_frameCapturer.Frames.Count == 0)
                    return;

                var frame = _frameCapturer.Frames.Dequeue();
                var height = _frameCapturer.AnalizedPictureHeight;
                var width = _frameCapturer.AnalizedPictureWidth;
                frameDecodeData = null;
#if UNITY_EDITOR
                if (test)
                {
                    frameDecodeData = barCodeReader.Decode(QrCodeForTest.GetPixels32(), QrCodeForTest.width, QrCodeForTest.height);
                    test = false;
                    Debug.Log("test!");
                }
                    
#else
    frameDecodeData = barCodeReader.Decode(frame, width, height);
#endif
                //Result data = barCodeReader.Decode(frame, width, height);

                if (frameDecodeData != null && ((_lastQR == frameDecodeData.Text && Time.time> _time) ||(_lastQR != frameDecodeData.Text)))
                {
                    _lastQR = frameDecodeData.Text;
                    _frameAnimator.Play("Success");
                    _time = Time.time + _delayBetweenSameCode;
                    Vibration.VibratePop();
                    QRInfo qrInfo = new QRInfo();

                    if (!QRInfoManager.Decode(frameDecodeData.Text, ref qrInfo))
                        return;

                    InvokeAct(qrInfo);
                    _frameCapturer.State = FrameCapturer.RecorderState.Paused;
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error reading QR");
                Debug.LogError(e.Message);
            }
        }
    }

    private void InvokeAct(QRInfo qrInfo)
    {
        if (_eventsOnSuccess != null && !(_isEventsDone && _doOnce))
        {
            //Debug.Log("Invoked");
            _eventsOnSuccess?.Invoke(qrInfo);
        }
        _isEventsDone = true;
    }


}