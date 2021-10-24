using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QROrientationCalculator
{
    // a - угол поворота QR вокруг x`
    // A - длина стороны квадрата
    // p` - центр(CCx, CCy) QR кода на изображении в системе (u,v)
    // Vв и Vн - точки квадрата лежащие на оси oy`


    /// <summary>
    /// Возвращает угол наклона вдоль перпендикуляра к камере (дальше - ближе)
    /// </summary>
    /// <returns></returns>
    public static float CalculateOXRotation(QrPoints points, float A )
    {
        float k=0;

        //угол поворота QR вокруг x`
        float _a=0;


        float Z=0;

        float CCy=CalculateCCy(points);

        float result = 0;
        //Vn (Vн) - середина нижней стороны квадрата
        float Vn = (k* Mathf.Cos(_a)*A/2) / (Z - Mathf.Sin(_a)*A/2) + CCy;

        return result;

    }

    /// <summary>
    /// Определяет координату центра p` кода на изображении в системе (u,v)
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    private static float CalculateCCx(QrPoints points)
    {
        //ну и длинное же гавно
        float CCx = points.LeftUp.x+(Mathf.Abs(points.LeftUp.x - points.LeftDown.x)/2)+((points.LeftDown.y+(Mathf.Abs(points.RightUp.x - points.RightDown.x)/2) - (points.LeftUp.x+(Mathf.Abs(points.LeftUp.x-points.LeftDown.x)/2)))/2);
        return CCx;
    }

    private static float CalculateCCy(QrPoints points)
    {
        //ну и длинное же гавно(2)
        float CCx = points.LeftUp.y + (Mathf.Abs(points.LeftUp.y - points.RightUp.y) / 2) + ((points.LeftDown.y + (Mathf.Abs(points.LeftDown.y - points.RightDown.y) / 2) - (points.LeftUp.y + (Mathf.Abs(points.LeftUp.y - points.RightUp.y) / 2))) / 2);
        return CCx;
    }




}
