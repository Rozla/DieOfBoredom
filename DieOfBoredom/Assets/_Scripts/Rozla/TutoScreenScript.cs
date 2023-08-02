using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TutoScreenScript : MonoBehaviour
{
    [SerializeField] GameObject _continueButton;
    [SerializeField] EventSystem _eventSystem;

    PlayerInput _playerInput;

    string _gamePad = "Gamepad";
    string _knm = "Keyboard&Mouse";

    bool _canCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.onControlsChanged += OnControlsChanged;

        StartCoroutine(ScreenCor());

        Cursor.lockState = CursorLockMode.Confined;
    }


    void OnControlsChanged(PlayerInput obj)
    {
        

        if (!_canCheck) return;


        if (_playerInput.currentControlScheme == _gamePad)
        {
            Cursor.visible = false;
            _eventSystem.SetSelectedGameObject(_continueButton.gameObject);
        }

        if (_playerInput.currentControlScheme == _knm)
        {
            Cursor.visible = true;
            _eventSystem.SetSelectedGameObject(null);
        }
    }

    IEnumerator ScreenCor()
    {
        yield return new WaitForSeconds(3f);

        _continueButton.SetActive(true);
        _canCheck = true;
        _eventSystem.SetSelectedGameObject(_continueButton.gameObject);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
}
