using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOutLineScript : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Vector3[] _doorCornerPoints = new Vector3[4];

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    
   
    public void DrawOutLineByPivotPoint(Vector3 startPoint, float XLeft, float XRight, float YTop, float YBottom, float Z, float widthLine)
    {
        _lineRenderer.startWidth = widthLine;
        _lineRenderer.endWidth = widthLine;

        _lineRenderer.positionCount = 6;

        Vector3 P1 = new Vector3(startPoint.x, startPoint.y + YBottom, startPoint.z + Z);
        Vector3 P2 = new Vector3(startPoint.x + XLeft, startPoint.y + YBottom, startPoint.z + Z);
        _doorCornerPoints[0] = P2;
        Vector3 P3 = new Vector3(startPoint.x + XLeft, startPoint.y + YTop, startPoint.z + Z);
        _doorCornerPoints[1] = P3;
        Vector3 P4 = new Vector3(startPoint.x + XRight, startPoint.y + YTop, startPoint.z + Z);
        _doorCornerPoints[2] = P4;
        Vector3 P5 = new Vector3(startPoint.x + XRight, startPoint.y + YBottom, startPoint.z + Z);
        _doorCornerPoints[3] = P5;
        Vector3 P6 = new Vector3(startPoint.x, startPoint.y + YBottom, startPoint.z + Z);

        _lineRenderer.SetPositions(new Vector3[] { P1, P2, P3, P4, P5, P6 });
    }
    public void DrawOutLineByPivotPoint(Vector3 startPoint, DoorScriptableObject doorScriptableObject)
    {
        DrawOutLineByPivotPoint(startPoint, doorScriptableObject.DeltaFromQR.XLeft, doorScriptableObject.DeltaFromQR.XRight, doorScriptableObject.DeltaFromQR.YTop, doorScriptableObject.DeltaFromQR.YBottom, doorScriptableObject.DeltaFromQR.Z, doorScriptableObject.WidthLine);
    }

    /// <summary>
    /// Построение рамки по координатам точек
    /// </summary>
    //public void BuildByDoorDimensions(Vector3[] point)
    //{
    //    if (point.Length > 0)
    //    {
    //        _lineRenderer.positionCount = point.Length;
    //        for (int i = 0; i < _lineRenderer.positionCount; i++)
    //        {
    //            _lineRenderer.SetPosition(i, point[i]);
    //        }
    //        _lineRenderer.SetPosition(_lineRenderer.positionCount, point[0]);
    //    }
    //}
}
