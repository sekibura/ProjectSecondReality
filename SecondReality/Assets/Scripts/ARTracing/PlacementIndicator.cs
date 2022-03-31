using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{

    [SerializeField] 
    private ARRaycastManager rayManager;
    [SerializeField]
    private GameObject visual;

    void Start()
    {
        // get the components
        //rayManager = FindObjectOfType<ARRaycastManager>();
        //visual = transform.GetChild(0).gameObject;

        // hide the placement indicator visual
        visual.SetActive(false);
        //Debug.Log("start finished");
    }

    void Update()
    {
        // shoot a raycast from the center of the screen
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
        //Debug.Log("raycast");
        // if we hit an AR plane surface, update the position and rotation
        if (hits.Count > 0)
        {
            //Debug.Log("count > 0");
            visual.transform.position = hits[0].pose.position;
            visual.transform.rotation = hits[0].pose.rotation;
            //Debug.Log(visual.transform.position);

            // enable the visual if it's disabled
            if (!visual.activeInHierarchy)
            {
                //Debug.Log("set active visual");
                visual.SetActive(true);
                //Debug.Log("after set active visual");
            }
                
        }
    }
}