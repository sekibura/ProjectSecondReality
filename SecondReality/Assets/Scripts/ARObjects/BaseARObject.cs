using System;
using System.Collections;
using System.Collections.Generic;
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
            assetsBundleLoader.DownloadBundle(DecodedQRInfo.URL, OnSuccess, OnFail, 0);
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
        string pictureName = "qrCode";
        string prefabName = "prefab";

        //typeof(????)
        var pictureRequest = assetBundle.LoadAssetAsync(pictureName, typeof(Texture2D));
        QRCodeImage = pictureRequest.asset as Texture2D;
        AddQRImageToLibrary(QRCodeImage);
        yield return pictureRequest;

        //typeof(????)
        var prefabRequest = assetBundle.LoadAssetAsync(prefabName, typeof(GameObject));
        GameObject prefabGameObject = prefabRequest.asset as GameObject;
        SpawnARObject(prefabGameObject);
        yield return prefabRequest;
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
        //remove 
        _arPlugObject.SetActive(false);

        ARObject = (GameObject)Instantiate(prefab);
        ARObject.transform.parent = gameObject.transform;
    }

}
