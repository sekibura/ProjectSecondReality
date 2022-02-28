using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QrScannerBottomBar : MonoBehaviour
{
    [SerializeField]
    private Button _qrButton;

    [SerializeField]
    private GameObject _buttons;

    private void Start()
    {
        QRStateManager.Instance.captureStart += CaptureStart;
        QRStateManager.Instance.capturePause += CapturePause;
        QRStateManager.Instance.QRCodeReadSuccess += QRReadedSuccess;

        _qrButton.onClick.AddListener(()=> { PressQRButton(); });
        _buttons.SetActive(false);
    }

    private void CaptureStart()
    {
        _buttons.SetActive(false);
    }

    private void CapturePause()
    {

    }

    private void QRReadedSuccess()
    {
        _buttons.SetActive(true);
    }

    private void PressQRButton()
    {
        QRStateManager.Instance.CaptureStart();
    }
}
