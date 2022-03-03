using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using ZXing;

public class ARObjectsManager : MonoBehaviour
{
    private GameObject _currentSpawnedGameobject;
    private QrInfoSubstitution _currentSpawnedQRinfo;
    
   

    //префаб для спавна
    [SerializeField]
    private GameObject _plugPrefab;
    
    static List<ARRaycastHit> _hits = new List<ARRaycastHit>();
    private ARRaycastManager _arRaycastManager;
    private ARSessionOrigin _arSessionOrigin;

    private void Start()
    {
        InitARSessionOrigin();
    }

    private void InitARSessionOrigin()
    {
         GameObject ARSession = GameObject.FindGameObjectWithTag("ARSessionOrigin");
        if (ARSession == null)
        {
            Debug.LogError("ARSession not found!");
            return;
        }
        _arRaycastManager = ARSession.GetComponent<ARRaycastManager>();
        _arSessionOrigin = ARSession.GetComponent<ARSessionOrigin>();
    }

    public void AddARObject(QrInfoSubstitution qrInfo)
    {
        try
        {
            Debug.Log("Add ARObject");
            if (qrInfo == null)
            {
                Debug.Log("Empty qrInfo");
                return;
            }

            if (qrInfo.ID != _currentSpawnedQRinfo?.ID)
            {
                Debug.Log("New ARObject");

                if(_currentSpawnedGameobject!=null)
                    Destroy(_currentSpawnedGameobject);

                //var centerOfScreen = new Vector2(Screen.width/2, Screen.height/2);
                //var ray = _arSessionOrigin.camera.ScreenPointToRay(centerOfScreen);

                ////заменить на нормальный рейкаст по координатам qr-кода
                //if(_arRaycastManager.Raycast(ray, _hits, TrackableType.All))
                //{
                //    var hitPose = _hits[0].pose;
                //    _currentSpawnedGameobject = (GameObject)Instantiate(_plugPrefab, hitPose.position, hitPose.rotation);
                //}
                //else
                //{
                //    Debug.LogError("Wrong ARRaycst hit!");
                //    return;
                //}

                
                var baseArObject = _currentSpawnedGameobject.GetComponent<BaseARObject>();
                baseArObject.Setup(qrInfo);
                _currentSpawnedQRinfo = qrInfo;
            }
            else
            {
                Debug.Log("Ar object already exist!");
            }

        }
        catch(Exception error)
        {
            Debug.LogError("Error add ArObject: "+error.Message);
        }

    }
}
