using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoObject : MonoBehaviour
{
    [SerializeField]
    private BaseSettings _baseSettings;

    public void SetAnimationIsFinished()
    {
        Debug.Log("Anim finished");
        _baseSettings.FinishAnimation();
    }
}
