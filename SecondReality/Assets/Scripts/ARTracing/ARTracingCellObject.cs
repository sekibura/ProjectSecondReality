using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARTracingCellObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;

    private Button _cellButton;
    private void Start()
    {
        _cellButton = GetComponent<Button>();
        _cellButton.onClick.AddListener(()=> { ViewManager.Show<ARCustomModelTracingView>(_prefab); });
    }
}
