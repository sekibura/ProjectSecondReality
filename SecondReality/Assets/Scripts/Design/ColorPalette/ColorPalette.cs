using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorPaletteType : byte
{
    Day_1,
    Night_1
}

public enum UiColorType : byte
{
    NONE = 0,

    /// <summary>
    /// ÷вет текста и пиктограмм панелей меню
    /// </summary>
    Menu_txt_icon = 1,

}
[CreateAssetMenu(fileName = "ColorPalette", menuName = "Create Color Palette")]
public class ColorPalette : ScriptableObject
{
    public ColorPaletteType PaletteType;

    public Color Menu_txt_icon;
    
    public Color GetColor(UiColorType uiType)
    {
        switch (uiType)
        {
            case UiColorType.Menu_txt_icon:
                return Menu_txt_icon;
        }
        return Color.black;
    }


    [ContextMenu("Set random colors")]
    void ResetColors()
    {
        var types = this.GetType().GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        if (types.Length == 0) { Debug.LogError("No fields"); }
        foreach (var t in types)
        {
            Color currentColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
            t.SetValue(this, currentColor);
        }
    }
}

