using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecodedQRInfo
{
    //�������� �������������� ���� �� qr ����

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
    public string URL { get; set; }
}
