using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QRInfo
{
    //содержит декодированную инфу из qr кода
    // http//rcrjkfr.com/hbjbhjb/1#0;0;0;0;0;0;1;1;1
    private string url;

    /// <summary>
    /// Занимают 1,2,3 места в ссылке
    /// </summary>
    public Vector3 StartPosition { get; set; } // начальные координаты для спавна заглушки.

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
    public string URL
    {
        get { return url; }
        set 
        { 
            url = value;
            //TODO
            //        //переделать наименование 
            var a = URL.Split('/');
            ID = a[a.Length - 1];
        }
    }

    /// <summary>
    /// 0 место
    /// </summary>
    public string ID { get; private set; }

}
