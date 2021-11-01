using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARObjectsManager : MonoBehaviour
{
    private GameObject _currentSpawnedGameobject;
    private QRInfo _currentSpawnedQRinfo;
    private TrackedImageRuntimeManager trackedImageRuntimeManager;
    private GameObject _mainCamera;

    [SerializeField]
    private GameObject _plugPrefab;

    private void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        InitARSessionOrigin();
    }

    private void InitARSessionOrigin()
    {
        var ARSession = GameObject.FindGameObjectWithTag("ARSessionOrigin");
        if (ARSession == null)
        {
            Debug.LogError("ARSession not found!");
            return;
        }
        trackedImageRuntimeManager = ARSession.GetComponent<TrackedImageRuntimeManager>();
    }

    public void AddARObject(QRInfo qrInfo)
    {
        Debug.Log("Add ARObject invoked");
        if (qrInfo == null || trackedImageRuntimeManager == null)
            return;
        try
        {   
            if (qrInfo.ID != _currentSpawnedQRinfo?.ID)
            {
                if(_currentSpawnedGameobject!=null)
                    Destroy(_currentSpawnedGameobject);

                _currentSpawnedGameobject = (GameObject)Instantiate(_plugPrefab, _mainCamera.transform.position, Quaternion.identity);
                
                var baseArObject = _currentSpawnedGameobject.GetComponent<BaseARObject>();
                baseArObject.TrackedImageRuntimeManager = trackedImageRuntimeManager;
                baseArObject.Setup(qrInfo);
                _currentSpawnedQRinfo = qrInfo;
                trackedImageRuntimeManager.PrefabOnTrack = _currentSpawnedGameobject;
                Debug.Log("ArObject spawned");
            }
        }
        catch(Exception error)
        {
            Debug.LogError("Error add ArObject: "+error.Message);
        }

    }
}
