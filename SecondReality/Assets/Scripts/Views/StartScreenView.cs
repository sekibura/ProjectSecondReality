using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenView : View
{
    [SerializeField]
    private CanvasGroup _fadeOut;
    public override void Initialize()
    {}

    public override void Show(object parameter = null)
    {
        base.Show();
        _fadeOut.alpha = 1;
        _fadeOut.LeanAlpha(0, 1f);
        this.Invoke(() =>
        {
            ViewManager.Show<QRView>(remember:false);
        }, 3f);
    }
}
