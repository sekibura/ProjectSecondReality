using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TrackedImageRuntimeManager : MonoBehaviour
{
    private GameObject prefabOnTrack;
    public GameObject PrefabOnTrack 
    { 
        get 
        {
            return prefabOnTrack; 
        } 
        set 
        {
            prefabOnTrack = value;
        } 
    }

    [SerializeField]
    private Vector3 scaleFactor = new Vector3(1f, 1f, 1f);

    [SerializeField]
    private XRReferenceImageLibrary runtimeImageLibrary;

    private ARTrackedImageManager trackImageManager;

    [SerializeField]
    private Texture2D dynamicTexture;

    private void Start()
    {
        trackImageManager = gameObject.AddComponent<ARTrackedImageManager>();
        trackImageManager.referenceLibrary = trackImageManager.CreateRuntimeLibrary(runtimeImageLibrary);
        trackImageManager.requestedMaxNumberOfMovingImages = 1;
        trackImageManager.trackedImagePrefab = prefabOnTrack;

        trackImageManager.enabled = true;

        //trackImageManager.trackedImagesChanged += OnTrackedImagesChanged;

    }

  
    void OnDisable()
    {
       // trackImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    public void AddImage(Texture2D texture2D)
    {
        StartCoroutine(AddImageJob(texture2D));
    }

    private IEnumerator AddImageJob(Texture2D texture2D)
    {
        yield return null;


        //как стереть нынешнюю библеотеку? без создавания новой
        trackImageManager.referenceLibrary = trackImageManager.CreateRuntimeLibrary(runtimeImageLibrary);

        var firstGuid = new SerializableGuid(0, 0);
        var secondGuid = new SerializableGuid(0, 0);

        
        XRReferenceImage newImage = new XRReferenceImage(firstGuid, secondGuid, new Vector2(0.1f, 0.1f), Guid.NewGuid().ToString(), texture2D);

        try
        {
            Debug.Log(newImage.ToString());

            // MutableRuntimeReferenceImageLibrary - может давать проблемы на каких-то устройствах.
            MutableRuntimeReferenceImageLibrary mutableRuntimeReferenceImageLibrary = trackImageManager.referenceLibrary as MutableRuntimeReferenceImageLibrary;

      

            var jobHandle = mutableRuntimeReferenceImageLibrary.ScheduleAddImageJob(texture2D, Guid.NewGuid().ToString(), 0.1f);

            //while (!jobHandle.IsCompleted)
            //{
            //    jobLog.text = "Job Running...";
            //}
            Debug.Log("Image adding to tracking library done!");
        }
        catch (Exception e)
        {
            Debug.LogError("Error adding image to tracking library" + e.ToString());
        }
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            // Display the name of the tracked image in the canvas
            trackedImage.transform.Rotate(Vector3.up, 180);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            // Display the name of the tracked image in the canvas
            trackedImage.transform.Rotate(Vector3.up, 180);
        }
    }
}
