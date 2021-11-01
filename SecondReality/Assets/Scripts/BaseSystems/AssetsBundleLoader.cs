using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsBundleLoader : MonoBehaviour
{
    //использовать ли локальные пути
    private bool useLocalPaths = true;

    public void DownloadBundle(string url, Action<AssetBundle> onSuccesAction, Action onFailAction, int ver = 0)
    {
        StartCoroutine(DownloadAndCache(url, ver, onSuccesAction, onFailAction));
    }

    IEnumerator DownloadAndCache(string url, int ver, Action<AssetBundle> onSuccesAction, Action onFailAction)
    {
        Debug.Log("start download bundle");

        while (!Caching.ready)
            yield return null;

        var www = WWW.LoadFromCacheOrDownload(url, ver);
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
            onFailAction();
            yield break;
        }
        Debug.Log("Bundle loading done!");
        var assetBundle = www.assetBundle;
        onSuccesAction(assetBundle);
    }
}
