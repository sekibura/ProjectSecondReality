using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoorData", menuName = "AR/Door")]
public class DoorScriptableObject : ScriptableObject
{
    public Material Material;
    public Vector3[] Points;
    public DeltaFromQR DeltaFromQR;
    public float WidthLine = 1;
    public string Number="";
    public string DoorInfo="";
    
}

[System.Serializable]
public struct DeltaFromQR
{
    public float XLeft, XRight, YTop, YBottom, Z;
}
