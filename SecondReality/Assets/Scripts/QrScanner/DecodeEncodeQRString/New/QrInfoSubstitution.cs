using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����������� ����� ��� �������� ���� �� ���� � ���������� json
[Serializable]
public class QrInfoSubstitution
{
    //������ �� ������� �� �������
    public string url;
    //������ ��������
    public int ID;

    //public Vector3 Offset;
    //public Vector3 Dimensions;

    public override string ToString() 
    {
        return "url: " + url + "\nID:" + ID;
    }
}


