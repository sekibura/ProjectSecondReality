using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TrackedImageRuntimeManager : MonoBehaviour
{
    [SerializeField]
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

    //[SerializeField]
    private XRReferenceImageLibrary runtimeImageLibrary;

    private ARTrackedImageManager trackImageManager;

    private void Start()
    {
        trackImageManager = gameObject.AddComponent<ARTrackedImageManager>();
        trackImageManager.referenceLibrary = trackImageManager.CreateRuntimeLibrary(runtimeImageLibrary);
        trackImageManager.requestedMaxNumberOfMovingImages = 1;
        trackImageManager.trackedImagePrefab = prefabOnTrack;

        trackImageManager.enabled = true;

        trackImageManager.trackedImagesChanged += OnTrackedImagesChanged;

        QRStateManager.Instance.captureStart += OnCaptureStart;
    }


    void OnDisable()
    {
        trackImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    public void NewTracking(Texture2D texture2D, GameObject prefabOnTrack = null)
    {
        if (prefabOnTrack != null)
        {
            this.prefabOnTrack = prefabOnTrack;
            trackImageManager.trackedImagePrefab = prefabOnTrack;
        }

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

            var jobHandle = mutableRuntimeReferenceImageLibrary.ScheduleAddImageJob(texture2D, Guid.NewGuid().ToString(), 0.1f /* 0.5f= 50 cm */ );

            while (!jobHandle.IsCompleted)
            {
                Debug.Log("Job Running...");
            }
            Debug.Log("Image adding to tracking library done!");

            trackImageManager.enabled = true;
            //off loading view
            ViewManager.ShowLast();
        }
        catch (Exception e)
        {
            Debug.LogError("Error adding image to tracking library" + e.ToString());
            ViewManager.ShowLast();
            ViewManager.Show<MessageView>((object)("Error adding image to tracking library" + e.ToString()), hideLast: false);
        }
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
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

    private void OnCaptureStart()
    {
        //на запуск режима сканирования qr кодов - остановить трекинг и убрать объект
        trackImageManager.enabled = false;

        var trackedImagePrefab = trackImageManager.trackedImagePrefab;
        if (trackedImagePrefab != null)
        {
            trackedImagePrefab.SetActive(false);
            Destroy(trackedImagePrefab);
        }
    }
}
