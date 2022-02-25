using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QRFrameViewer : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]
    private Image _frame;

    private void Start()
    {
        FrameCapturer frameCapturer = FindObjectOfType<FrameCapturer>();
        QRStateManager.Instance.captureStart += CaptureStart;
        QRStateManager.Instance.capturePause += CapturePause;
        QRStateManager.Instance.QRCodeReadSuccess += QRReadedSuccess;
        _animator = GetComponent<Animator>();

        _frame.GetComponent<RectTransform>().sizeDelta = new Vector2(frameCapturer.AnalizedPictureWidth - 100, frameCapturer.AnalizedPictureHeight - 100);
    }

    private void CaptureStart()
    {
        _animator.Play("QRScanerFrameApear");
    }

    private void CapturePause()
    {
        _animator.Play("QRScanerFrameSuccessDissapear");
    }

    private void QRReadedSuccess()
    {
        _animator.Play("Success");
    }
}
