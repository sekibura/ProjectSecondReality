using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARCustomModelTracingView : View
{
    [SerializeField]
    private Button _homeBtn;
    [SerializeField]
    private Button _helpBtn;
    [SerializeField]
    private Button _toPlaceBtn;
    [SerializeField]
    private Button _toRemoveBtn;

    [SerializeField]
    private ARTracingManager _aRTracingManager;

    public override void Initialize()
    {
        _homeBtn.onClick.AddListener(()=> { ViewManager.ShowLast(); });
        _helpBtn.onClick.AddListener(()=> { ViewManager.Show<QRScannerInfoView>();});
        _toPlaceBtn.onClick.AddListener(()=> { _aRTracingManager.PlaceObject(); });
        _toRemoveBtn.onClick.AddListener(() => { _aRTracingManager.DeleteObject(); });
    }

    public override void Show(object parameter)
    {
        base.Show(null);
        _aRTracingManager.placedPrefab = (GameObject)parameter;

    }

    public override void Hide()
    {
        base.Hide();
        _aRTracingManager.DeleteObject();
    }


}
