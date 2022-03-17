using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessageView : View
{
    [SerializeField]
    private TMP_Text _textMessage;
    [SerializeField]
    private Button _button;

    public override void Initialize()
    {
        Vibration.Init();
        _button.onClick.AddListener(()=> { ViewManager.ShowLast(); });
    }

    public override void Show(object parameter = null)
    {
        base.Show(parameter);
        _textMessage.text = (string)parameter;

    }
}
