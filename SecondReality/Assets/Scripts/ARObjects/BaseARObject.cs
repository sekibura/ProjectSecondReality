using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class BaseARObject : MonoBehaviour
{
    public QRInfo DecodedQRInfo { get; private set; }
    public Texture2D QRCodeImage { get; private set; }
    public GameObject ARObject { get; private set; }
    public TrackedImageRuntimeManager TrackedImageRuntimeManager { get; set; }

    [SerializeField]
    private GameObject _arPlugPrefab;
    private GameObject _arPlugObject;

    [SerializeField]
    private GameObject _arERRORPlugPrefab;
    private GameObject _arERRORPlugObject;

    AssetsBundleLoader assetsBundleLoader;

    private void Start()
    {
        Debug.Log(name + " created!\n"+transform.position);
    }

    public void Setup(QRInfo qrInfo)
    {
        DecodedQRInfo = qrInfo;
        Init();
        AssetBundleInit();
    }

    private void Init()
    {
        //TODO
        //доделать инициализацию, применить другие параметры к префабу.

        //gameObject.transform.position = DecodedQRInfo.StartPosition;
        //gameObject.transform.localScale = DecodedQRInfo.Dimensions;

        //spawn plug
        _arPlugObject = (GameObject)Instantiate(_arPlugPrefab);
        _arPlugObject.transform.parent = gameObject.transform;
        assetsBundleLoader = gameObject.AddComponent<AssetsBundleLoader>();
    }

    private void AssetBundleInit()
    {
        try
        { 
            assetsBundleLoader.DownloadBundle(DecodedQRInfo, OnSuccess, OnFail, 0);
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message + " " + e.Source+" "+e.ToString());
        }
        
    }

    private void OnSuccess(AssetBundle assetBundle)
    {
        StartCoroutine(LoadAssets(assetBundle));
    }

    IEnumerator LoadAssets(AssetBundle assetBundle)
    {
        //string pictureName = "qrCode";
        //string prefabName = "prefab";
        string imagePattern = @"^.*\.(jpg|JPG|png|PNG)$";
        string prefabPattern = @"^.*\.(prefab)$";



        //var pictureRequest = assetBundle.LoadAssetAsync(pictureName, typeof(Texture2D));
        //QRCodeImage = pictureRequest.asset as Texture2D;
        //AddQRImageToLibrary(QRCodeImage);
        //yield return pictureRequest;


        //var prefabRequest = assetBundle.LoadAssetAsync(prefabName, typeof(GameObject));
        //GameObject prefabGameObject = prefabRequest.asset as GameObject;
        //SpawnARObject(prefabGameObject);
        //yield return prefabRequest;

        foreach (var fileName in assetBundle.GetAllAssetNames())
        {
            if (Regex.Matches(fileName, imagePattern).Count > 0)
            {
                var pictureRequest = assetBundle.LoadAssetAsync(fileName, typeof(Texture2D));
                yield return pictureRequest;
                QRCodeImage = pictureRequest.asset as Texture2D;
                AddQRImageToLibrary(QRCodeImage);
                continue;
            }
            else if(Regex.Matches(fileName, prefabPattern).Count > 0)
            {
                var prefabRequest = assetBundle.LoadAssetAsync(fileName, typeof(GameObject));
                yield return prefabRequest;
                GameObject prefabGameObject = prefabRequest.asset as GameObject;
                SpawnARObject(prefabGameObject);
                continue;
            }
            else
            {
                Debug.LogError("Error asset load");
            }
        }
    }


    private void OnFail()
    {
        //Show error message
        Debug.Log(gameObject.name + "Fail load bundle");
        _arPlugObject.SetActive(false);

        _arERRORPlugObject = (GameObject)Instantiate(_arERRORPlugPrefab);
        _arERRORPlugObject.transform.parent = gameObject.transform;

    }

    private void AddQRImageToLibrary(Texture2D QRImage)
    {
        // 
        TrackedImageRuntimeManager.AddImage(QRImage);
    }

    private void SpawnARObject(GameObject prefab)
    {
        ARObject = (GameObject)Instantiate(prefab);
        ARObject.transform.parent = gameObject.transform;

        //remove 
        _arPlugObject.SetActive(false);
    }

}
