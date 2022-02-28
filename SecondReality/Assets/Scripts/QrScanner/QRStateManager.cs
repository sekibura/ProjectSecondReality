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

    private void Awake()
    {
        Instance = this;
    }

    public void CaptureStart()
    {
        captureStart?.Invoke();
    }

    public void CapturePause()
    {
        capturePause?.Invoke();
    }

    public void OnQRCodeReadSuccess()
    {
        QRCodeReadSuccess?.Invoke();
    }


}
