using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpriteColorSwitcher : EditorWindow
{
    Texture2D txt;
    
    

    Color oldColor = Color.black;
    Color newColor = Color.white;

    [MenuItem("Window/SpriteColorChanger")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<SpriteColorSwitcher>("SpriteColorChanger");
    }

    private void OnGUI()
    {
        GUILayout.Label("AssetBundle files", EditorStyles.boldLabel);
        var texture = (Texture2D)EditorGUILayout.ObjectField("texture ", txt, typeof(Texture2D), true, GUILayout.Height(EditorGUIUtility.singleLineHeight));
        oldColor = EditorGUILayout.ColorField("Old Color", oldColor);
        newColor = EditorGUILayout.ColorField("New Color", newColor);

        if (GUILayout.Button("Repaint"))
        {
            ChangeColor(texture);
        }

        //GUILayout.Label(texture);
    }

    public void ChangeColor(Texture2D texture)
    {
        

        var pixels = texture.GetPixels32();
        Color32[] newPixels = new Color32[pixels.Length];
        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i] == oldColor)
            {
                newPixels[i] = newColor;
            }
        }

        texture.SetPixels32(newPixels);
        texture.Apply();
        
    }

}
