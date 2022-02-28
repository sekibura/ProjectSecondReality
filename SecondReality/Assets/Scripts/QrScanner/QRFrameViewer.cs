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
        QRStateManager.Instance.captureStart += CaptureStart;
        QRStateManager.Instance.capturePause += CapturePause;
        QRStateManager.Instance.QRCodeReadSuccess += QRReadedSuccess;
        _animator = GetComponent<Animator>();


        _frame.GetComponent<RectTransform>().sizeDelta = CalculateSizeViewFrame();
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

    private Vector2Int CalculateSizeViewFrame()
    {
        Vector2Int size;

        if (Screen.width <= Screen.height)
            size = new Vector2Int (Screen.width / 3, Screen.width / 3);
        else
            size = new Vector2Int(Screen.height / 3, Screen.height / 3);
        Debug.Log(Screen.height + "x" + Screen.width + " - " + size);
        return size;
    }
}
