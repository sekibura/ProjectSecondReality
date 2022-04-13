using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRStateManager : MonoBehaviour
{
    public static QRStateManager Instance;
    public event Action captureStart;
    public event Action capturePause;
    public event Action QRCodeReadSuccess;
    public event Action OnCloseWindow;

    private void Awake()
    {
        Instance = this;
    }

    public void CaptureStart()
    {
        Debug.Log("Capture start");
        captureStart?.Invoke();
    }

    public void CapturePause()
    {
        Debug.Log("Capture pause");
        capturePause?.Invoke();
    }

    public void OnQRCodeReadSuccess()
    {
        QRCodeReadSuccess?.Invoke();
    }

    public void CloseWindow()
    {
        OnCloseWindow?.Invoke();
    }
}
