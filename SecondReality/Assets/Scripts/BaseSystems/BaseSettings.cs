using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseSettings : MonoBehaviour
{
    [SerializeField]
    private GameObject _debugFPSCounter;
    private float _time;
    private bool _isAnimationFinished = false;
    [SerializeField]
    private bool _useFpsCounter;


    private void Awake()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

#if DEVELOPMENT_BUILD || UNITY_EDITOR
        if (_useFpsCounter)
        {
            var fpsCounter = Instantiate(_debugFPSCounter);
            DontDestroyOnLoad(fpsCounter);
        }
#endif

    }
    private void Start()
    {
        _time = Time.time;
        try
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            StartCoroutine(LoadLevelAfterDelay(1.8f));
            //StartCoroutine(LoadAsyncScene());
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }


    }

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator LoadAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scene2");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        asyncLoad.allowSceneActivation = false;
        // Wait until the asynchronous scene fully loads

        while (!_isAnimationFinished)
        {
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
    }

    public void FinishAnimation()
    {
        Debug.Log("Time"+(Time.time-_time).ToString());
        _isAnimationFinished = true;
    }


}
