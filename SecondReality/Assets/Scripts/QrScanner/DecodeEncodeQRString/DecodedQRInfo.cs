using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecodedQRInfo
{
    //содержит декодированную инфу из qr кода

    /// <summary>
    /// «анимают 1,2,3 места в ссылке
    /// </summary>
    public Vector3 StartPosition { get; set; } // начальные координаты дл€ спавна заглушки.

    /// <summary>
    /// 4,5,6 место 
    /// </summary>
    public Vector3 Offset { get; set; } // смещение префаба относительно?????????

    /// <summary>
    /// 7,8,9 место
    /// </summary>
    public Vector3 Dimensions { get; set; } // габариты заглушки.

    /// <summary>
    /// 0 место
    /// </summary>
    public string URL { get; set; }
}
