using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FileManager
{
    /// <summary>
    /// Called via: var loadedPrefabResource = LoadPrefabFromFile("Cube");
    /// Instantiate(loadedPrefabResource, Vector3.zero, Quaternion.identity);
    /// 
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    private static bool LoadPrefabFromFile(string path, out UnityEngine.Object loadedObject)
    {
        //Debug.Log("Trying to load LevelPrefab from file (" + filename + ")...");
        loadedObject = Resources.Load(path);

        if (loadedObject == null)
        {
            return false;
            //throw new FileNotFoundException("...no file found - please check the configuration");
        }
        return true;
    }

    private static bool IsFolderExistAndNotEmpty(string path, List<string> expectedFilesNames)
    {
        if (Directory.Exists(path))
        {
            foreach (var file in expectedFilesNames)
            {
                if (!File.Exists(file))
                    return false;
            }
        }
        else
            return false;

        return true;
    }
}
