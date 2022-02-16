using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomBar : MonoBehaviour
{
    [SerializeField]
    private Button _qrMenu;
    [SerializeField]
    private Button _menu;

    [SerializeField]
    private Image _qrMenuImage;
    [SerializeField]
    private Image _menuImage;

    [Header("Color btns")]
    public Color Active;
    public Color Deactive;

    private void Start()
    {
        _qrMenuImage.color = Active;
        _qrMenu.onClick.AddListener(() => 
        {
            ViewManager.Show<QRView>();
            _qrMenuImage.color = Active;
            _menuImage.color = Deactive;
            
        });
        _menu.onClick.AddListener(() => 
        {
            ViewManager.Show<MenuView>();
            _qrMenuImage.color = Deactive;
            _menuImage.color = Active;
        });
    }


}
