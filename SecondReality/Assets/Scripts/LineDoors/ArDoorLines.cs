using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArDoorLines : MonoBehaviour, ARObject
{
    [SerializeField] 
    private DoorOutLineScript _doorOutLine;
    [SerializeField] 
    private DoorNumberScript _doorNumber;
    [SerializeField]
    private DoorInfoPlane _doorInfoPlane;


    [SerializeField]
    private GameObject _qrCode;

    private Vector3[] _doorCornerPoints = new Vector3[4];


    [SerializeField]
    private DoorScriptableObject _doorScriptable;

    private void Start()
    {
        


#if UNITY_EDITOR
        Test();
#endif
    }

    private void Test()
    {
        //DrawOutLineByPivotPoint(_qrCode.transform.position, _doorScriptable);
        //DrawNumberDoor(_qrCode.transform.position, _doorScriptable);
        _doorOutLine.DrawOutLineByPivotPoint(_qrCode.transform.position, _doorScriptable);
        _doorNumber.DrawNumberDoor(_qrCode.transform.position, _doorScriptable);
    }

    public void DoSmth(Object obj)
    {
        //throw new System.NotImplementedException();
    }

}
