using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GetPlayerInputs : MonoBehaviour
{
    [SerializeField] InputActionAsset _playerInputs;

    static Vector2 _moveInputs;

    public static Vector2 MoveInputs
    {
        get
        {
            return _moveInputs;
        }
    }

    static bool _crouchInput;

    public static bool CrouchInput
    {
        get
        {
            return (_crouchInput);
        }
    }

    private void OnEnable() => _playerInputs.Enable();
    private void OnDisable() => _playerInputs.Disable();


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _moveInputs = _playerInputs.FindAction("Move").ReadValue<Vector2>();

        if (_playerInputs.FindAction("Crouch").WasPerformedThisFrame())
        {
            _crouchInput = !_crouchInput;
        }
    }
}
