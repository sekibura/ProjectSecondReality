using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutUsView : View
{
    [SerializeField]
    private Button _backBtn;
    public override void Initialize()
    {
        _backBtn.onClick.AddListener(() => ViewManager.ShowLast()); ;
    }

}
