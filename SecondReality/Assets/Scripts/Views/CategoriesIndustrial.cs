using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoriesIndustrial : View
{
    [SerializeField]
    private Button _backBtn;
    //[SerializeField]
    //private Button _toARRaycastMode;
    public override void Initialize()
    {
        _backBtn.onClick.AddListener(() => { ViewManager.ShowLast(); });
        //_toARRaycastMode.onClick.AddListener(()=> { ViewManager.Show<ARCustomModelTracingView>(); });
    }
}
