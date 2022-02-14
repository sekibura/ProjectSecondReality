using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARObjectsManager : MonoBehaviour
{
    private GameObject _currentSpawnedGameobject;
    private QRInfo _currentSpawnedQRinfo;
    private TrackedImageRuntimeManager _trackedImageRuntimeManager;
    private GameObject _mainCamera;
   

    [SerializeField]
    private GameObject _plugPrefab;
    
    static List<ARRaycastHit> _hits = new List<ARRaycastHit>();
    private ARRaycastManager _arRaycastManager;
    private ARSessionOrigin _arSessionOrigin;

    private void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
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
        _trackedImageRuntimeManager = ARSession.GetComponent<TrackedImageRuntimeManager>();
        _arRaycastManager = ARSession.GetComponent<ARRaycastManager>();
        _arSessionOrigin = ARSession.GetComponent<ARSessionOrigin>();
    }

    public void AddARObject(QRInfo qrInfo)
    {
        try
        {
            Debug.Log("Add ARObject invoked");
            if (qrInfo == null || _trackedImageRuntimeManager == null)
            {
                Debug.Log("Empty qrInfo");
                return;
            }

            if (qrInfo.ID != _currentSpawnedQRinfo?.ID)
            {
                Debug.Log("New ARObject");
                if(_currentSpawnedGameobject!=null)
                    Destroy(_currentSpawnedGameobject);
                var centerOfScreen = new Vector2(Screen.width/2, Screen.height/2);
                var ray = _arSessionOrigin.camera.ScreenPointToRay(centerOfScreen);
                if(_arRaycastManager.Raycast(ray, _hits, TrackableType.All))
                {
                    var hitPose = _hits[0].pose;
                    _currentSpawnedGameobject = (GameObject)Instantiate(_plugPrefab, hitPose.position, hitPose.rotation);
                }
                else
                {
                    Debug.LogError("Wrong ARRaycst hit!");
                    return;
                }

               // _currentSpawnedGameobject = (GameObject)Instantiate(_plugPrefab, _mainCamera.transform.position, Quaternion.identity);
                
                var baseArObject = _currentSpawnedGameobject.GetComponent<BaseARObject>();
                baseArObject.TrackedImageRuntimeManager = _trackedImageRuntimeManager;
                baseArObject.Setup(qrInfo);
                _currentSpawnedQRinfo = qrInfo;
                _trackedImageRuntimeManager.PrefabOnTrack = _currentSpawnedGameobject;
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
