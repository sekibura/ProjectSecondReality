using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AssetsBundleLoader : MonoBehaviour
{
    //использовать ли локальные пути
    private bool useLocalPaths = false;

    public void DownloadBundle(QrInfoSubstitution qrInfo, Action<AssetBundle> onSuccesAction, Action onFailAction, int ver = 0)
    {
        StartCoroutine(DownloadAndCache(qrInfo, ver, onSuccesAction, onFailAction));
    }

    IEnumerator DownloadAndCache(QrInfoSubstitution qrInfo, int ver, Action<AssetBundle> onSuccesAction, Action onFailAction)
    {
        Debug.Log("start download bundle");


        while (!Caching.ready)
            yield return null;

        string url = qrInfo.url;

        if (useLocalPaths)
            url = LocalBundlePath.GetLocalPath(qrInfo.ID.ToString());

        var uwr = UnityWebRequestAssetBundle.GetAssetBundle(url);



        yield return uwr.SendWebRequest();

        if (!string.IsNullOrEmpty(uwr.error))
        {
            Debug.Log("Error! " + uwr.error);
            onFailAction();
            yield break;
        }
        Debug.Log("Bundle loading done!");

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
        if (bundle == null)
        {
            Debug.Log("Error GetContent! ");
            onFailAction();
            yield break;
        }

        var names = bundle.GetAllAssetNames();
        foreach (var item in names)
        {
            Debug.Log("Asset name: " + item);
        }
        //var loadAsset = bundle.LoadAssetAsync<GameObject>("Assets/Players/MainPlayer.prefab");
        //yield return loadAsset;

        onSuccesAction(bundle);
        
        
    }
}
