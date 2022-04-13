using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

public class ARObjectManagerNew : MonoBehaviour
{
    private TrackedImageRuntimeManager _trackedImageRuntimeManager;
    private AssetsBundleLoader _assetsBundleLoader;

    private QrInfoSubstitution qrInfoSubstitution;

    private void Start()
    {
        _trackedImageRuntimeManager = FindObjectOfType<TrackedImageRuntimeManager>();
        _assetsBundleLoader = gameObject.AddComponent<AssetsBundleLoader>();
    }

    public void LoadContent(QrInfoSubstitution qrInfo)
    {
        qrInfoSubstitution = qrInfo;
        AssetBundleLoad(qrInfo);
    }

    private void AssetBundleLoad(QrInfoSubstitution qrInfo)
    {
        try
        {
            _assetsBundleLoader.DownloadBundle(qrInfo, OnSuccess, OnFail, 0);
        }
        catch (Exception e)
        {
            ViewManager.ShowLast();
            ViewManager.Show<MessageView>((object)(e.Message + " " + e.Source + " " + e.ToString()), hideLast:false);

            Debug.LogError(e.Message + " " + e.Source + " " + e.ToString());
        }
    }

    private void OnSuccess(AssetBundle assetBundle)
    {
        StartCoroutine(LoadAssets(assetBundle));
    }

    private void OnFail()
    {
        Debug.LogError("Error load Bundle");

        ViewManager.ShowLast();
        ViewManager.Show<MessageView>((object)("Error load Bundle"), hideLast: false);
    }

    IEnumerator LoadAssets(AssetBundle assetBundle)
    {
        string imagePattern = @"^.*\.(jpg|JPG|png|PNG)$";
        string prefabPattern = @"^.*\.(prefab)$";

        Texture2D qrImage = null;
        GameObject prefabForTracking = null;

        foreach (var fileName in assetBundle.GetAllAssetNames())
        {
            if (Regex.Matches(fileName, imagePattern).Count > 0)
            {
                var pictureRequest = assetBundle.LoadAssetAsync(fileName, typeof(Texture2D));
                yield return pictureRequest;
                qrImage = pictureRequest.asset as Texture2D;
                continue;
            }
            else if (Regex.Matches(fileName, prefabPattern).Count > 0)
            {
                var prefabRequest = assetBundle.LoadAssetAsync(fileName, typeof(GameObject));
                yield return prefabRequest;
                prefabForTracking = prefabRequest.asset as GameObject;
                continue;
            }
            else
            {
                Debug.LogError("Error asset load");
            }
        }

        if (qrImage == null)
            qrImage = GenerateQRImage();

        if(qrImage!=null && prefabForTracking != null)
        {   
            StartTracking(qrImage, prefabForTracking);

            //??????
            assetBundle.Unload(false);
        }
        
    }

    private void StartTracking(Texture2D qrImage, GameObject prefab)
    {
        _trackedImageRuntimeManager.NewTracking(qrImage, prefab);
    }

    private Texture2D GenerateQRImage()
    {
        Texture2D texture = new Texture2D(256, 256, TextureFormat.RGB24, false);
        string jsonStr = JsonUtility.ToJson(qrInfoSubstitution);
        texture.SetPixels32(Encode(jsonStr, texture.width, texture.height));
        texture.Apply();

        return texture;
    }

    private Color32[] Encode(string text, int width, int height)
    {
        BarcodeWriter writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(text);
    }

   
}
