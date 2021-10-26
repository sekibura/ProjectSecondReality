using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QRInfoManager
{
  //���������� ���� ��� qr �����, ����������� ������, ����������.

    public static DecodedQRInfo Decode(string str)
    {
        DecodedQRInfo qrInfo = new DecodedQRInfo();
        var parts = str.Split('#',';');

        //TODO
        // ��������� ����� ������� ������� ��������� ���������� �� ������.


        qrInfo.URL = parts[0];
        qrInfo.StartPosition = new Vector3(float.Parse(parts[1]), float.Parse(parts[2]), float.Parse(parts[3]));
        qrInfo.Offset = new Vector3(float.Parse(parts[4]), float.Parse(parts[5]), float.Parse(parts[6]));
        qrInfo.Dimensions= new Vector3(float.Parse(parts[7]), float.Parse(parts[8]), float.Parse(parts[9]));



        return qrInfo;
    }

    public static string Encode(DecodedQRInfo qrInfo)
    {
        string encodedInfo = "";

        return encodedInfo;
    }
    
}
