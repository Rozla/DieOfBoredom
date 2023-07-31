using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;

public class MainMenuBehavior : MonoBehaviour
{

    [SerializeField] EventSystem _eventSystem;
    [SerializeField] Button _playButton;
    [SerializeField] Button _quitButton;

    PlayerInput _playerInput;

    string _knmScheme = "Keyboard&Mouse";
    string _gamepadScheme = "Gamepad";

    // Start is called before the first frame update
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.onControlsChanged += OnControlsChanged;

        if (_playerInput.currentControlScheme == _knmScheme)
        {
            _eventSystem.SetSelectedGameObject(null);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        if (_playerInput.currentControlScheme == _gamepadScheme)
        {
            _eventSystem.SetSelectedGameObject(_playButton.gameObject);
            Cursor.visible = false;
        }
    }

    void OnControlsChanged(UnityEngine.InputSystem.PlayerInput obj)
    {
        if (_playerInput.currentControlScheme == _knmScheme)
        {
            _eventSystem.SetSelectedGameObject(null);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        if (_playerInput.currentControlScheme == _gamepadScheme)
        {
            _eventSystem.SetSelectedGameObject(_playButton.gameObject);
            Cursor.visible = false;
        }
    }
}
