using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;

public class CircleSlider : MonoBehaviour
{
    [SerializeField]
    private Transform _handle;
    [SerializeField]
    private Image _fill;
    [SerializeField]
    private TMP_Text _valueText;
    private Vector3 _touchPosition;
    [SerializeField]
    private float _divider;
    [SerializeField]
    private string _prefix;
    [SerializeField]
    private string _postfix;

    public UnityEvent<float> onValueChange;

  

    public void onHandleDrag()
    {
        //Debug.Log("on drag");
        if (Input.touchCount > 0)
        {
            _touchPosition = Input.GetTouch(0).position;
            Vector2 dir = _touchPosition - _handle.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle = (angle <= 0) ? (360 + angle) : angle;
            if(angle<=225 || angle >= 315)
            {
                Quaternion r = Quaternion.AngleAxis(angle + 135f, Vector3.forward);
                _handle.rotation = r;
                angle = ((angle >= 315) ? (angle - 360) : angle) + 45;
                _fill.fillAmount = 0.75f - (angle / 360f);
                float value = Mathf.Round((_fill.fillAmount * 100) / 0.75f); //0-100
                value = value / _divider;
                _valueText.text = _prefix+value.ToString()+_postfix;

                if(onValueChange!=null)
                    onValueChange.Invoke(value);
                
            }
        }
    }

    [Serializable]
    public class onValueChangeEvent : UnityEvent<float> { }

}
