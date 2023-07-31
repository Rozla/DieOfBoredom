using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIGameBehaviour : MonoBehaviour
{
    [SerializeField] EventSystem _eventSystem;
    PlayerInput _playerInput;

    [Header("Pause Menu")]
    [SerializeField] Button _continueButton;

    [Header("Win Menu")]
    [SerializeField] Button _nextLevelButton;

    [Header("Lose Menu")]
    [SerializeField] Button _tryAgainButton;

    [Header("Objects in scene")]
    [SerializeField] LoseTimer _loseTimerScript;
    [SerializeField] BoxBehaviour _boxBehaviourScript;

    string _knmScheme = "Keyboard&Mouse";
    string _gamepadScheme = "Gamepad";

    public static string _currentScheme;

    // Start is called before the first frame update
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.onControlsChanged += OnControlsChanged;
        _eventSystem.SetSelectedGameObject(_continueButton.gameObject);


        _loseTimerScript._loseEvent.AddListener(() =>
        {
            _eventSystem.SetSelectedGameObject(_tryAgainButton.gameObject);
        });

        _boxBehaviourScript._winEvent.AddListener(() =>
        {
            _eventSystem.SetSelectedGameObject(_nextLevelButton.gameObject);
        });
    }

    private void Update()
    {
        Debug.Log(_currentScheme);
    }

    void OnControlsChanged(UnityEngine.InputSystem.PlayerInput obj)
    {
        if(_playerInput.currentControlScheme == _knmScheme) _currentScheme = _knmScheme;
        if (_playerInput.currentControlScheme == _gamepadScheme) _currentScheme = _gamepadScheme;

        if (_continueButton.isActiveAndEnabled)
        {
            if (_playerInput.currentControlScheme == _knmScheme)
            {
                _eventSystem.SetSelectedGameObject(null);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }

            if (_playerInput.currentControlScheme == _gamepadScheme)
            {
                _eventSystem.SetSelectedGameObject(_continueButton.gameObject);
                Cursor.visible = false;
            }
        }

        if (_nextLevelButton.isActiveAndEnabled)
        {

            if (_playerInput.currentControlScheme == _knmScheme)
            {
                _eventSystem.SetSelectedGameObject(null);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }

            if (_playerInput.currentControlScheme == _gamepadScheme)
            {
                _eventSystem.SetSelectedGameObject(_nextLevelButton.gameObject);
                Cursor.visible = false;
            }
        }

        if (_tryAgainButton.isActiveAndEnabled)
        {
            if (_eventSystem.firstSelectedGameObject != _tryAgainButton.gameObject)
            {
                _eventSystem.firstSelectedGameObject = _tryAgainButton.gameObject;
            }

            if (_playerInput.currentControlScheme == _knmScheme)
            {
                _eventSystem.SetSelectedGameObject(null);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }

            if (_playerInput.currentControlScheme == _gamepadScheme)
            {
                _eventSystem.SetSelectedGameObject(_tryAgainButton.gameObject);
                Cursor.visible = false;
            }
        }
    }
}
