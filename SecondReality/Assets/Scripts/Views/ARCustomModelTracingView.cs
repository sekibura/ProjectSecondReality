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

    [SerializeField]
    private Button _scaleBtn;
    [SerializeField]
    private GameObject _scaleControl;
    [SerializeField]
    private Button _rotationBtn;
    [SerializeField]
    private GameObject _rotationControl;



    public override void Initialize()
    {
        _homeBtn.onClick.AddListener(()=> { ViewManager.ShowLast(); });
        _helpBtn.onClick.AddListener(()=> { ViewManager.Show<RayCastingInfoView>();});
        _toPlaceBtn.onClick.AddListener(()=> { _aRTracingManager.PlaceObject(); });
        _toRemoveBtn.onClick.AddListener(() => { _aRTracingManager.DeleteObject(); });

        _scaleBtn.onClick.AddListener(()=> 
        { 
            _scaleControl.SetActive(!_scaleControl.activeSelf);
            _rotationControl.SetActive(false);
        });
        _rotationBtn.onClick.AddListener(() => 
        { 
            _rotationControl.SetActive(!_rotationControl.activeSelf);
            _scaleControl.SetActive(false);
        });
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
