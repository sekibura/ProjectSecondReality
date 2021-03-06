using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchWaveScript : MonoBehaviour
{

    [SerializeField]
    private GameObject _wave;

    private void Start()
    {
        //FrameCapturer frameCapturer = FindObjectOfType<FrameCapturer>();
        QRStateManager.Instance.captureStart += CaptureStart;
        QRStateManager.Instance.capturePause += CapturePause;
        QRStateManager.Instance.QRCodeReadSuccess += QRReadedSuccess;
        _wave.SetActive(true);
    }

    private void CaptureStart()
    {
        _wave.SetActive(true);
    }

    private void CapturePause()
    {
        _wave.SetActive(false);
    }

    private void QRReadedSuccess()
    {
        _wave.SetActive(false);
    }

}
