using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTracingManager : MonoBehaviour
{
    
    

    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedPrefab;

    [SerializeField]
    GameObject visualObject;

    [SerializeField]
    ARRaycastManager m_RaycastManager;

    [SerializeField]
    private GameObject _placeButton;
    [SerializeField]
    private GameObject _removeBtn;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    private bool _toPlaceObject = false;
    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set 
        { 
            if(value!=null)
                m_PlacedPrefab = value;
        }
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }


    private void Start()
    {
        visualObject.SetActive(true);
    }

    //bool TryGetTouchPosition(out Vector2 touchPosition)
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        touchPosition = Input.GetTouch(0).position;
    //        return true;
    //    }

    //    touchPosition = default;
    //    return false;
    //}

    void Update()
    {

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        m_RaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
        if (hits.Count > 0)
        {
            visualObject.transform.position = hits[0].pose.position;
            visualObject.transform.rotation = hits[0].pose.rotation;
        }


        //if (!TryGetTouchPosition(out Vector2 touchPosition))
        //    return;

        if (!_toPlaceObject)
            return;


        if (m_RaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), s_Hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = s_Hits[0].pose;

            if (spawnedObject == null)
            {
                //spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                spawnedObject = Instantiate(m_PlacedPrefab, visualObject.transform.position, visualObject.transform.rotation);

                _placeButton.SetActive(false);
                _removeBtn.SetActive(true);
                visualObject.SetActive(false);
                _toPlaceObject = false;
            }
            //else
            //{
            //    //repositioning of the object 
            //    spawnedObject.transform.position = hitPose.position;
            //}
            
        }
    }

   

    public void DeleteObject()
    {
        Destroy(spawnedObject);
        spawnedObject = null;
        visualObject.SetActive(true);
        _placeButton.SetActive(true);
        _removeBtn.SetActive(false);
    }

    public void PlaceObject()
    {
        _toPlaceObject = true;
    }
}
