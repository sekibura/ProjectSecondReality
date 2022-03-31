using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class AssetBundleBuilderTool :EditorWindow
{
    private AssetBundleTemplate _assetBundleTemplate;
    private BuildTarget _buildPlatform;

    [MenuItem("Window/AssetBundleBuilder")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<AssetBundleBuilderTool>("AssetBundleBuilderTool");
    }

    private void OnGUI()
    {
        GUILayout.Label("AssetBundle files", EditorStyles.boldLabel);
        _buildPlatform = (BuildTarget)EditorGUILayout.EnumPopup("Choose platform:", _buildPlatform);
        EditorInspector.Show(_assetBundleTemplate);

        if (GUILayout.Button("Repaint"))
        {
            Repaint();
        }

        if (GUILayout.Button("Build"))
        {
            BuildAssetBundle(); 
        }

        //GUILayout.Label(jsonStr, EditorStyles.boldLabel);
        //GUILayout.Label(_qrCodeTexture);

    }

    private void BuildAssetBundle()
    {
        AssetBundleBuild[] buildMap = new AssetBundleBuild[1];

        buildMap[0].assetBundleName = _assetBundleTemplate.NameAssetBundle;



        

        string[] assets = new string[4];
        //Descrpition
        assets[0] = CreateTextFile(_assetBundleTemplate.Description, _assetBundleTemplate.NameAssetBundle);
        //PrevieImage
        assets[1] = AssetDatabase.GetAssetPath(_assetBundleTemplate.PreviewImage);
        //Marker
        assets[2] = AssetDatabase.GetAssetPath(_assetBundleTemplate.Marker);
        //AR_Object
        assets[3] = AssetDatabase.GetAssetPath(_assetBundleTemplate.AR_Object);

        buildMap[0].assetNames = assets;

        //Debug.Log(AssetDatabase.GetAssetPath(_assetBundleTemplate.Marker));
        //Debug.Log(AssetDatabase.GetAssetPath(_assetBundleTemplate.PreviewImage));
        string pathForBuild = "";
        if(_buildPlatform==BuildTarget.Android)
            pathForBuild = "Assets/AssetBundles/Android";
        else if (_buildPlatform == BuildTarget.iOS)
        {
            pathForBuild = "Assets/AssetBundles/Ios";
        }
        else
        {
            pathForBuild = "Assets/AssetBundles/Other";
        }
        BuildPipeline.BuildAssetBundles(pathForBuild, buildMap, BuildAssetBundleOptions.None, _buildPlatform);
    }

    private string CreateTextFile(string text, string filename)
    {
        string folderPath = Application.dataPath + "/../AssetBundleTemplateFiles/";
        string path = folderPath+filename+".txt";

        //System.IO.DirectoryInfo di = new DirectoryInfo(folderPath);

        //foreach (FileInfo file in di.GetFiles())
        //{
        //    file.Delete();
        //}
        //foreach (DirectoryInfo dir in di.GetDirectories())
        //{
        //    dir.Delete(true);
        //}

        if (!File.Exists(path))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(text);
            }
        }

        return path;
    }


}


