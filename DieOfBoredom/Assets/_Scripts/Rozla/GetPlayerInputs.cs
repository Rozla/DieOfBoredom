using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GetPlayerInputs : MonoBehaviour
{
    [SerializeField] InputActionAsset _playerInputs;

    Vector2 moveInputs;

    private void OnEnable() => _playerInputs.Enable();
    private void OnDisable() => _playerInputs.Disable();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInputs = _playerInputs.FindAction("Move").ReadValue<Vector2>();

        Debug.Log(moveInputs);
    }
}
