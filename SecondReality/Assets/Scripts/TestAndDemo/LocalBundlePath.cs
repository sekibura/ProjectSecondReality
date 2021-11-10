using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class LocalBundlePath 
{
    private static string _localFolder = "http://192.168.1.70:8080/Unity/assetbundles/";
    public static string GetLocalPath(string id)
    {
        string path = _localFolder+id;
        Debug.Log("Local file: " + path);
        return path;
    }
}
