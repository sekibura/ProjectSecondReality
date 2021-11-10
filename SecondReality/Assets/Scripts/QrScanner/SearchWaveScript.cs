using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchWaveScript : MonoBehaviour
{
    private State _currentState;

    private Vector2Int _screenResolution;
    private Vector2 _searchWaveDimensions;

    private Vector3 _startPosition;
    private Vector3 _finishPosition;
    private Vector3 _currentTargetPosition;

    private float _step;
    private float _speed = 200f;

    private Coroutine _coroutine;
    private bool _isCoroutineRunning = false;

    private RectTransform _rectTransform;

    public enum State
    {
        Play,
        Paused
    }


    private void Start()
    {
        Init();
    }

    public  void Init()
    {
        _screenResolution = new Vector2Int(Screen.width, Screen.height);
        _searchWaveDimensions = gameObject.GetComponent<RectTransform>().sizeDelta;

        _startPosition = new Vector3(0 + Screen.width / 2, (_screenResolution.y + _searchWaveDimensions.y + 10));
        _finishPosition = new Vector3(0 + Screen.width / 2, -(0 + _searchWaveDimensions.y + 10));

        _rectTransform = gameObject.GetComponent<RectTransform>();
        _rectTransform.position = _startPosition;
        _speed = Screen.height / 3;

        Debug.Log("searchWavePositions: " + _startPosition + " " + _finishPosition);
    }

    private void Update()
    {
        if (_currentState == State.Play)
        {
            if (gameObject.transform.position == _finishPosition)
            {
                _coroutine = StartCoroutine(Vector3LerpCoroutine( _startPosition, _speed));
                _rectTransform.localScale = new Vector3(1, -1, 1);
                _isCoroutineRunning = true;
            }
            if (gameObject.transform.position == _startPosition)
            {
                _coroutine = StartCoroutine(Vector3LerpCoroutine( _finishPosition, _speed));
                _rectTransform.localScale = new Vector3(1, 1, 1);
                _isCoroutineRunning = true;
            }
        }
        else if (_isCoroutineRunning)
        {
            StopCoroutine(_coroutine);
            _isCoroutineRunning = false;
        }
            
    }

    IEnumerator Vector3LerpCoroutine( Vector3 target, float speed)
    {
        Vector3 startPosition = _rectTransform.position;
        float time = 0f;

        while (_rectTransform.position  != target)
        {
            _rectTransform.position = Vector3.Lerp(startPosition, target, (time / Vector3.Distance(startPosition, target)) * speed);
            time += Time.deltaTime;
            yield return null;
        }
    }


    public State CurrentState
    {
        get { return _currentState; }
        set 
        { 
            if(value == State.Play && _currentState!= State.Play)
            {
                Init();
                _coroutine = StartCoroutine(Vector3LerpCoroutine( _finishPosition, _speed));
                _isCoroutineRunning = true;
            }
            else if(value == State.Paused && _currentState != State.Paused)
            {

            }
            _currentState = value; 
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color  = Color.red;
    //    Gizmos.DrawSphere(_startPosition, 1f);
    //    Gizmos.DrawSphere(_finishPosition, 1f);
    //}
}
