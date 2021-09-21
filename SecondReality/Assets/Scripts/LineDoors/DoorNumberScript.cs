using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorNumberScript : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _frameLineRenderer;
    [SerializeField]
    private TMP_Text _numberText;

    private LineRenderer _lineRenderer;
    private Vector3[] _doorCornerPoints = new Vector3[4];

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void DrawNumberDoor(Vector3 startPoint, DoorScriptableObject doorScriptableObject)
    {
        _doorCornerPoints[0] = new Vector3(startPoint.x + doorScriptableObject.DeltaFromQR.XLeft, startPoint.y + doorScriptableObject.DeltaFromQR.YBottom, startPoint.z + doorScriptableObject.DeltaFromQR.Z);
        _doorCornerPoints[1] = new Vector3(startPoint.x + doorScriptableObject.DeltaFromQR.XLeft, startPoint.y + doorScriptableObject.DeltaFromQR.YTop, startPoint.z + doorScriptableObject.DeltaFromQR.Z);
        _doorCornerPoints[2] = new Vector3(startPoint.x + doorScriptableObject.DeltaFromQR.XRight, startPoint.y + doorScriptableObject.DeltaFromQR.YTop, startPoint.z + doorScriptableObject.DeltaFromQR.Z);
        _doorCornerPoints[3] = new Vector3(startPoint.x + doorScriptableObject.DeltaFromQR.XRight, startPoint.y + doorScriptableObject.DeltaFromQR.YBottom, startPoint.z + doorScriptableObject.DeltaFromQR.Z);

        int multiplerWidth = 4;
        _lineRenderer.startWidth = doorScriptableObject.WidthLine;
        _lineRenderer.endWidth = doorScriptableObject.WidthLine;

        _frameLineRenderer.startWidth = doorScriptableObject.WidthLine;
        _frameLineRenderer.endWidth = doorScriptableObject.WidthLine;

        _lineRenderer.positionCount = 4;
        _frameLineRenderer.positionCount = 5;
        //рисовка линии до номер
        Vector3 P1 = (_doorCornerPoints[1] + _doorCornerPoints[0]) / 2;
        Vector3 P2 = new Vector3(P1.x - doorScriptableObject.WidthLine * multiplerWidth, P1.y, P1.z);
        Vector3 P3 = new Vector3(P1.x - doorScriptableObject.WidthLine * multiplerWidth, _doorCornerPoints[1].y + doorScriptableObject.WidthLine * multiplerWidth * 2, P1.z);
        Vector3 P4 = new Vector3((_doorCornerPoints[1].x + _doorCornerPoints[2].x) / 2, _doorCornerPoints[1].y + doorScriptableObject.WidthLine * multiplerWidth * 2, P1.z);
        //Vector3 P4 = new Vector3(_doorCornerPoints[2].x, _doorCornerPoints[1].y + doorScriptableObject.WidthLine * multiplerWidth * 2, P1.z);

        //рамка номера
        float lengthNumberFrame = 0.2f * doorScriptableObject.Number.Length;
        if (lengthNumberFrame == 0)
            lengthNumberFrame = 0.2f * 5;
        float deltaFrame = doorScriptableObject.WidthLine * 4;
        float heightNumberFrame = 0.5f;
        Vector3 P5 = new Vector3((_doorCornerPoints[1].x + _doorCornerPoints[2].x) / 2, _doorCornerPoints[1].y + doorScriptableObject.WidthLine * multiplerWidth * 2, P1.z);

        Vector3 P6 = new Vector3((_doorCornerPoints[1].x + _doorCornerPoints[2].x) / 2, _doorCornerPoints[1].y + doorScriptableObject.WidthLine * multiplerWidth * 2, P1.z - lengthNumberFrame - deltaFrame);
        Vector3 P7 = new Vector3((_doorCornerPoints[1].x + _doorCornerPoints[2].x) / 2, _doorCornerPoints[1].y + doorScriptableObject.WidthLine * multiplerWidth * 2 + heightNumberFrame, P1.z - lengthNumberFrame - deltaFrame);
        Vector3 P8 = new Vector3((_doorCornerPoints[1].x + _doorCornerPoints[2].x) / 2, _doorCornerPoints[1].y + doorScriptableObject.WidthLine * multiplerWidth * 2 + heightNumberFrame, P1.z - deltaFrame);
        Vector3 P9 = new Vector3((_doorCornerPoints[1].x + _doorCornerPoints[2].x) / 2, _doorCornerPoints[1].y + doorScriptableObject.WidthLine * multiplerWidth * 2, P1.z - deltaFrame);

        _lineRenderer.SetPositions(new Vector3[] { P1, P2, P3, P4});
        _frameLineRenderer.SetPositions(new Vector3[] { P5, P6, P7, P8, P9 });


        _numberText.transform.position = (P6 + P8) / 2;
        _numberText.rectTransform.sizeDelta = new Vector2(P8.z-P7.z, P7.y - P6.y);
        
        Debug.Log(new Vector2(P8.z - P7.z, P7.y - P6.y));

    }
}
