using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QROrientationCalculator
{
    // a - ���� �������� QR ������ x`
    // A - ����� ������� ��������
    // p` - �����(CCx, CCy) QR ���� �� ����������� � ������� (u,v)
    // V� � V� - ����� �������� ������� �� ��� oy`


    /// <summary>
    /// ���������� ���� ������� ����� �������������� � ������ (������ - �����)
    /// </summary>
    /// <returns></returns>
    public static float CalculateOXRotation(QrPoints points, float A )
    {
        float k=0;

        //���� �������� QR ������ x`
        float _a=0;


        float Z=0;

        float CCy=CalculateCCy(points);

        float result = 0;
        //Vn (V�) - �������� ������ ������� ��������
        float Vn = (k* Mathf.Cos(_a)*A/2) / (Z - Mathf.Sin(_a)*A/2) + CCy;

        return result;

    }

    /// <summary>
    /// ���������� ���������� ������ p` ���� �� ����������� � ������� (u,v)
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    private static float CalculateCCx(QrPoints points)
    {
        //�� � ������� �� �����
        float CCx = points.LeftUp.x+(Mathf.Abs(points.LeftUp.x - points.LeftDown.x)/2)+((points.LeftDown.y+(Mathf.Abs(points.RightUp.x - points.RightDown.x)/2) - (points.LeftUp.x+(Mathf.Abs(points.LeftUp.x-points.LeftDown.x)/2)))/2);
        return CCx;
    }

    private static float CalculateCCy(QrPoints points)
    {
        //�� � ������� �� �����(2)
        float CCx = points.LeftUp.y + (Mathf.Abs(points.LeftUp.y - points.RightUp.y) / 2) + ((points.LeftDown.y + (Mathf.Abs(points.LeftDown.y - points.RightDown.y) / 2) - (points.LeftUp.y + (Mathf.Abs(points.LeftUp.y - points.RightUp.y) / 2))) / 2);
        return CCx;
    }




}
