using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARInteractions : MonoBehaviour
{

    [SerializeField]
    private Camera _arCamera;


    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;
            if (touch.phase == TouchPhase.Began)
                return true;
        }

        touchPosition = default;
        return false;
    }

    private void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        Ray ray = _arCamera.ScreenPointToRay(touchPosition);
        RaycastHit hitObject;
        if(Physics.Raycast(ray, out hitObject))
        {
            GameObject placedObject = hitObject.transform.gameObject;
            if (placedObject != null)
            {
                DoWithObject(placedObject);
            }
        }

    }

    private void DoWithObject(GameObject placedObject)
    {
        Debug.Log("Hit object: "+placedObject.name);
        InteractionTranslater.Translate(placedObject);
    }
}

