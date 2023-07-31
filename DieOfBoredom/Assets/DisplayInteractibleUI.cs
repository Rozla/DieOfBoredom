using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DisplayInteractibleUI : MonoBehaviour
{
    [SerializeField] float _offsetX = 0f;
    [SerializeField] float _offsetY = 1f;
    [SerializeField] GameObject _knmImage;
    [SerializeField] GameObject _gamePadImage;

    SphereCollider _sphereCollider;
    GameObject _canva;
    GameObject _interactText;
    bool _canDisplay;

    Vector3 _currentPos;
    Vector3 _displayPos;

    Camera _mainCam;

    string _knm = "Keyboard&Mouse";
    string _gamePad = "Gamepad";

    // Start is called before the first frame update
    void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _canva = transform.GetChild(0).gameObject;
        _interactText = _canva.transform.GetChild(0).transform.GetChild(0).gameObject;
        _mainCam = Camera.main;
        _currentPos = new Vector3(transform.position.x + _offsetX, transform.position.y + _offsetY, transform.position.z);
        _displayPos = _mainCam.WorldToScreenPoint(_currentPos);
        _interactText.transform.position = _displayPos;
    }

    void DisplayText(bool value)
    {
        _interactText.SetActive(value);
    }

    private void Update()
    {
        if(_canDisplay)
        {
            if (UIGameBehaviour._currentScheme == _knm || UIGameBehaviour._currentScheme == null)
            {
                _knmImage.SetActive(true);
                _gamePadImage.SetActive(false);
            }

            if (UIGameBehaviour._currentScheme == _gamePad)
            {
                _knmImage.SetActive(false);
                _gamePadImage.SetActive(true);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            DisplayText(true);
            _canDisplay = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            _canDisplay = false;
            DisplayText(false);
        }
    }


}
