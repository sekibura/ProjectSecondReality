using UnityEngine;
using UnityEditor;
using ZXing;
using ZXing.QrCode;
using System.IO;
using System;

[System.Serializable]
public class QrCodeGenerator : EditorWindow
{
    public QrInfoSubstitution qrInfoSubstitution;
    private Texture2D _qrCodeTexture;
    private string jsonStr;

    [MenuItem("Window/QRGenerator")] 
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<QrCodeGenerator>("QRGenerator");
    }
    private void OnGUI()
    {
        GUILayout.Label("QR fields", EditorStyles.boldLabel);   
        EditorInspector.Show(qrInfoSubstitution);

        if (GUILayout.Button("Generate"))
        {
            GenerateQR();
        }

        GUILayout.Label(jsonStr, EditorStyles.boldLabel);
        GUILayout.Label(_qrCodeTexture);

        if (GUILayout.Button("Save"))
        {
            Save();
        }
    }

    private void GenerateQR()
    {
        jsonStr = JsonUtility.ToJson(qrInfoSubstitution);
        if (jsonStr == null || string.IsNullOrEmpty(jsonStr))
            return;


        _qrCodeTexture = new Texture2D(256, 256);
        _qrCodeTexture.SetPixels32(Encode(jsonStr,_qrCodeTexture.width, _qrCodeTexture.height));
        _qrCodeTexture.Apply();
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

    private void Save()
    {
        if (_qrCodeTexture == null)
            return;
        var a = _qrCodeTexture.EncodeToPNG();

        var dirPath = Application.dataPath + "/../GeneratedQR/";
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        var name = DateTime.Now.ToString().Replace(' ', '_').Replace('.','_').Replace(':','_');
        File.WriteAllBytes(dirPath + name + ".png", a);
    }
}
