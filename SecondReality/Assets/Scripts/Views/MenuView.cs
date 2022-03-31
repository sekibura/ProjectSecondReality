using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : View
{
    [SerializeField]
    private Button _leftMenuBtn;
    [SerializeField]
    private Button _industrial;
    public override void Initialize()
    {
        _leftMenuBtn.onClick.AddListener(()=>ViewManager.Show<LeftMenuView>(remember:true, hideLast:false));
        _industrial.onClick.AddListener(()=> { ViewManager.Show<CategoriesIndustrial>(); });
    }
}
