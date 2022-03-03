using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QRInfo
{
    //�������� �������������� ���� �� qr ����
    // http//rcrjkfr.com/hbjbhjb/1#0;0;0;0;0;0;1;1;1
    private string url;

    /// <summary>
    /// �������� 1,2,3 ����� � ������
    /// </summary>
    public Vector3 StartPosition { get; set; } // ��������� ���������� ��� ������ ��������.

    /// <summary>
    /// 4,5,6 ����� 
    /// </summary>
    public Vector3 Offset { get; set; } // �������� ������� ������������?????????

    /// <summary>
    /// 7,8,9 �����
    /// </summary>
    public Vector3 Dimensions { get; set; } // �������� ��������.

    /// <summary>
    /// 0 �����
    /// </summary>
    public string URL
    {
        get { return url; }
        set 
        { 
            url = value;
            //TODO
            //        //���������� ������������ 
            var a = URL.Split('/');
            ID = a[a.Length - 1];
        }
    }

    /// <summary>
    /// 0 �����
    /// </summary>
    public string ID { get; private set; }

}
