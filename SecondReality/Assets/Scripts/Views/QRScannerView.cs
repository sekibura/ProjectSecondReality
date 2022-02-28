using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QRScannerView : View
{
    [SerializeField]
    private Button _homeBtn;
    [SerializeField]
    private Button _infoBtn;
    public override void Initialize()
    {
        _homeBtn.onClick.AddListener(() => ViewManager.ShowLast());
        _infoBtn.onClick.AddListener(() => ViewManager.Show<QRScannerInfoView>());

    }

    public override void Show()
    {
        base.Show();
    }
}
