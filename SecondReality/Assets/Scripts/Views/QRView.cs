using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QRView : View
{
    [SerializeField]
    private Button _startQRScannerBtn;
    public override void Initialize()
    {
        _startQRScannerBtn.onClick.AddListener(() => ViewManager.Show<QRScannerView>());
    }
    public override void Show(object parameter = null)
    {
        base.Show();
        Application.targetFrameRate = 360;
    }
}
