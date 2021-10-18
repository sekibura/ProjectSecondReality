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
    private UnityEvent _eventsOnSuccess;
    [SerializeField]
    private bool _doOnce;
    private bool _isEventsDone = false;



    private float _time;
    private float _delayBetweenSameCode = 2f;

    private string _lastQR = "";

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
                var data = barCodeReader.Decode(frame, width, height);
                if (data != null && ((_lastQR == data.Text && Time.time> _time) ||(_lastQR != data.Text)))
                {
                    _lastQR = data.Text;
                    _frameAnimator.Play("Success");
                    _time = Time.time + _delayBetweenSameCode;
                    Vibration.VibratePop();
                    InvokeAct();
                    Debug.Log("QR: " + data.Text);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error reading QR");
                Debug.LogError(e.Message);
            }
        }
    }

    private void InvokeAct()
    {
        if (_eventsOnSuccess != null && !(_isEventsDone && _doOnce))
        {
            Debug.Log("Invoked");
            _eventsOnSuccess?.Invoke();
        }
        _isEventsDone = true;
    }


}