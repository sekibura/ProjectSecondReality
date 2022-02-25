using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchWaveScript : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        //FrameCapturer frameCapturer = FindObjectOfType<FrameCapturer>();
        QRStateManager.Instance.captureStart += CaptureStart;
        QRStateManager.Instance.capturePause += CapturePause;
        _animator = GetComponent<Animator>();
    }

    private void CaptureStart()
    {
        _animator.enabled = true;
    }

    private void CapturePause()
    {
        _animator.enabled = false;
    }

}
