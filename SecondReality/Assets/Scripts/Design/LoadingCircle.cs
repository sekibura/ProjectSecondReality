using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCircle : MonoBehaviour
{
    [SerializeField]
    private RectTransform _rectTransform;

    [SerializeField]
    private float _timeStep;

    [SerializeField]
    private float _stepAngle;

    private float _time;
    private Vector3 _iconAngle;

    void Start()
    {
        _time = Time.time;
    }

    
    void Update()
    {
        if (Time.time > _time + _timeStep)
        {
            _iconAngle = _rectTransform.localEulerAngles;
            _iconAngle.z += _stepAngle;
            _rectTransform.localEulerAngles = _iconAngle;
            _time += _timeStep;
        }
    }
}
