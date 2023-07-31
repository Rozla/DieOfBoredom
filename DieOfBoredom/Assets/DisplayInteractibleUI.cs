using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInteractibleUI : MonoBehaviour
{
    [SerializeField] float _offsetX = 0f;
    [SerializeField] float _offsetY = 1f;

    GameObject _canva;
    TextMeshProUGUI _interactText;

    Vector3 _currentPos;
    Vector3 _displayPos;

    Camera _mainCam;

    // Start is called before the first frame update
    void Start()
    {
        _canva = transform.GetChild(0).gameObject;
        _interactText = _canva.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>();
        _mainCam = Camera.main;
        _currentPos = new Vector3(transform.position.x + _offsetX, transform.position.y + _offsetY, transform.position.z);
        _displayPos = _mainCam.WorldToScreenPoint(_currentPos);
        _interactText.transform.position = _displayPos;
    }

    void DisplayText()
    {
        _canva.SetActive(true);
    }
}
