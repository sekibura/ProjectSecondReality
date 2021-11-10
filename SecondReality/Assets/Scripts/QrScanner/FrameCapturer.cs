using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameCapturer : MonoBehaviour
{

    public RecorderState State { get; set; }
    private int _maxFrameCount = 5;
    private float _time;
    private float _timePerFrame;
    private Queue<RenderTexture> _frames;
    private RenderTexture _recycledRenderTexture;
    public int AnalizedPictureWidth { get; private set; }
    public int AnalizedPictureHeight { get; private set; }
    private float _bufferSize = 2f;
    private int _framePerSecond = 10;
    private Color32[] _frame;
    private Color32[] _pixels;
    public Queue<Color32[]> Frames;
    private RenderTexture _rt;
    private DeviceOrientation _lastDeviceOrientation;

    [SerializeField]
    private GameObject _frameViewer;
    public Image _frameViewerImage;

    [SerializeField]
    private SearchWaveScript _searchWave;
    [SerializeField]
    private bool _isSearchWaveEnable;

    //[SerializeField]
    //bool m_AutoAspect = false;
 

    void Start()
    {
        Init();
        StartCoroutine(PreProcess());
    }

    public enum RecorderState
    {
        Recording,
        Paused,
        PreProcessing
    }
  
    void Init()
    {
        State = RecorderState.Paused;
        _lastDeviceOrientation = Input.deviceOrientation;
        _frames = new Queue<RenderTexture>();
        AnalizedPictureWidth = AnalizedPictureHeight = CalculateSizeViewFrame();
        Debug.Log(Screen.currentResolution.ToString() + " " + AnalizedPictureWidth);
        _frameViewerImage.GetComponent<RectTransform>().sizeDelta = new Vector2(AnalizedPictureWidth-100, AnalizedPictureHeight-100);

        
        //ComputeHeight();
        _maxFrameCount = Mathf.RoundToInt(_bufferSize * _framePerSecond);
        _timePerFrame = 1f / _framePerSecond;
        _time = 0f;
        State = RecorderState.Recording;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (State != RecorderState.Recording)
        {
            Graphics.Blit(source, destination);
            if (_isSearchWaveEnable)
            {
                _searchWave.CurrentState = SearchWaveScript.State.Paused;
                _frameViewer.SetActive(false);
            }
            return;
        }
        else
        {
            if (_isSearchWaveEnable)
            {
                _searchWave.CurrentState = SearchWaveScript.State.Play;
                _frameViewer.SetActive(true);
            }
                
        }

        if (IsDeviceOrientationChanged())
        {
            Init();
            Debug.Log("DeviceWasRotated - " + Input.deviceOrientation.ToString());
        }

        _time += Time.unscaledDeltaTime;

        if (_time >= _timePerFrame)
        {
            // Limit the amount of frames stored in memory
            if (_frames.Count >= _maxFrameCount)
            {
                _recycledRenderTexture = _frames.Dequeue();
                _recycledRenderTexture.Release();
            }
            _time -= _timePerFrame;

            // Frame data
            _rt = _recycledRenderTexture;
            _recycledRenderTexture = null;

            if (_rt == null)
            {
                _rt = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB4444);
                _rt.wrapMode = TextureWrapMode.Clamp;
                _rt.filterMode = FilterMode.Point;
                _rt.anisoLevel = 0;

            }

            Graphics.Blit(source, _rt);

            _frames.Enqueue(_rt);
        }

        Graphics.Blit(source, destination);
    }


    IEnumerator PreProcess()
    {
        Frames = new Queue<Color32[]>();

        // Get a temporary texture to read RenderTexture data
        Texture2D temp = new Texture2D(AnalizedPictureWidth, AnalizedPictureHeight, TextureFormat.ARGB4444, false);
        temp.hideFlags = HideFlags.HideAndDontSave;
        temp.wrapMode = TextureWrapMode.Clamp;
        temp.filterMode = FilterMode.Bilinear;
        temp.anisoLevel = 0;

        // Process the frame queue
        while (State == RecorderState.Recording)
        {
            if (_frames.Count > 0)
            {
                if (Frames.Count >= _maxFrameCount)
                {
                    Frames.Dequeue();
                }
                var realesedFrame = _frames.Dequeue();
                _frame = ToColorFrame(realesedFrame, temp);
                realesedFrame.Release();
                Frames.Enqueue(_frame);

                //Debug.LogWarning("frame added");
            }
            yield return null;
        }
    }

    Color32[] ToColorFrame(RenderTexture source, Texture2D target)
    {
        RenderTexture.active = source;
        target.ReadPixels(new Rect(Screen.width/2 - AnalizedPictureWidth/2, Screen.height / 2 - AnalizedPictureHeight / 2, AnalizedPictureWidth, AnalizedPictureHeight), 0, 0);
        target.Apply();
        _pixels = target.GetPixels32();
        RenderTexture.active = null;
        return _pixels; 
    }

    //public void ComputeHeight()
    //{
    //    if (!m_AutoAspect)
    //        return;

    //    m_Height = Mathf.RoundToInt(m_Width / GetComponent<Camera>().aspect);
    //}

    private bool IsDeviceOrientationChanged()
    {
        return _lastDeviceOrientation != Input.deviceOrientation;
    }

    private int CalculateSizeViewFrame()
    {
        //switch (Input.deviceOrientation)
        //{
        //    case DeviceOrientation.Portrait:
        //        return Screen.width / 3;
        //    case DeviceOrientation.LandscapeLeft:
        //        return Screen.height / 4;
        //    case DeviceOrientation.LandscapeRight:
        //        return Screen.height / 4;
        //    default:
        //        return Screen.width / 3;
        //}

        if (Screen.width <= Screen.height)
            return Screen.width / 3;
        else
            return Screen.height / 3;
    }

    public void EnableQRScanner()
    {
        State = RecorderState.Recording;
    }

}