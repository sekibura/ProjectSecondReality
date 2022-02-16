using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftMenuView : View
{
    [SerializeField]
    private Transform _menuPanel;

    [SerializeField]
    private CanvasGroup _background;

    [SerializeField]
    private Button _backgroundBtn;

    private float _x;

    [Header("Menu btns")]
    [SerializeField]
    private Button _aboutUsBtn;

    public override void Initialize()
    {
        _backgroundBtn.onClick.AddListener(() => ViewManager.ShowLast());
        _aboutUsBtn.onClick.AddListener(() => ViewManager.Show<AboutUsView>());
        _x = _menuPanel.localPosition.x;
    }

    public override void Show()
    {
        base.Show();
        _background.interactable = true;
        OnShowAnimation();
    }

    public override void Hide()
    {
        _background.interactable = false;
        OnHideAnimation();
        this.Invoke(() => { base.Hide(); }, 0.5f);
        
    }

    private void OnShowAnimation()
    {
        _background.alpha = 0;
        _background.LeanAlpha(1, 0.5f);

        _menuPanel.localPosition = new Vector2(-Screen.width, 0);
        _menuPanel.LeanMoveLocalX(_x, 0.5f).setEaseOutExpo().delay = 0.1f;
    }
    private void OnHideAnimation()
    {
        _background.LeanAlpha(0, 0.5f);
        _menuPanel.LeanMoveLocalX(-Screen.width, 0.5f).setEaseOutExpo().delay = 0.1f;
    }



}
