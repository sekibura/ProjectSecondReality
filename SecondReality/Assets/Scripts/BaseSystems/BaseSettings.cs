using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseSettings : MonoBehaviour
{
    [SerializeField]
    private GameObject debugFPSCounter;


    private void Awake()
    {
         Application.targetFrameRate = 60;

#if DEVELOPMENT_BUILD || UNITY_EDITOR
      var fpsCounter = Instantiate(debugFPSCounter);
      DontDestroyOnLoad(fpsCounter);
#endif

    }
    private void Start()
    {
        try
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }


    }
}
