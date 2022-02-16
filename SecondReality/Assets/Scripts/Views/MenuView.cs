using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : View
{
    [SerializeField]
    private Button _leftMenuBtn;
    public override void Initialize()
    {
        _leftMenuBtn.onClick.AddListener(()=>ViewManager.Show<LeftMenuView>(true, false));
    }
}
