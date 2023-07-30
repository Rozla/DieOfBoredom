using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController _playerCC;
    Transform _playerGraphics;
    Animator _playerAnimController;


    [Header("State Machine")]
    public PlayerState _currentState;

    float _currentSpeed;
    Vector3 _move;
    float _rotationSpeed = 15f;

    [Range(3f, 10f)][SerializeField] float _walkSpeed = 7f;

    [Range(1f, 3f)][SerializeField] float _crouchSpeed = 3f;


   public enum PlayerState
    {
        IDLE,
        WALK,
        CROUCHIDLE,
        CROUCH,
        SIT,
        PICKUP
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentState = PlayerState.IDLE;
        _playerCC = GetComponent<CharacterController>();
        _playerGraphics = transform.GetChild(0).transform;
        _playerAnimController = _playerGraphics.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        OnStateUpdate();

        RotatePlayer();
    }

    Vector3 MoveVector()
    {
        Vector2 moveInput = GetPlayerInputs.MoveInputs;
        _move = new Vector3(moveInput.x, 0f, moveInput.y);
        return _move;
    }

    void RotatePlayer()
    {
        if(_move != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(GetPlayerInputs.MoveInputs.x, GetPlayerInputs.MoveInputs.y) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);
        }
    }

    private void FixedUpdate()
    {
        _playerCC.Move(MoveVector().normalized * _currentSpeed * Time.deltaTime);
    }



    void OnStateEnter()
    {
        switch (_currentState)
        {
            case PlayerState.IDLE:

                _currentSpeed = 0f;

                _playerAnimController.SetBool("WALK", false);
                _playerAnimController.SetBool("CROUCH", false);

                break;
            case PlayerState.WALK:

                _currentSpeed = _walkSpeed;

                _playerAnimController.SetBool("WALK", true);
                _playerAnimController.SetBool("CROUCH", false);

                break;
            case PlayerState.CROUCHIDLE:

                _currentSpeed = 0f;

                _playerAnimController.SetBool("WALK", false);
                _playerAnimController.SetBool("CROUCH", true);

                break;
            case PlayerState.CROUCH:

                _currentSpeed = _crouchSpeed;

                _playerAnimController.SetBool("WALK", true);
                _playerAnimController.SetBool("CROUCH", true);

                break;
            case PlayerState.SIT:

                _currentSpeed = 0f;

                break;
            case PlayerState.PICKUP:

                _currentSpeed = 0f;

                break;
        }
    }

    void OnStateUpdate()
    {
        switch (_currentState)
        {
            case PlayerState.IDLE:

                if(GetPlayerInputs.MoveInputs != Vector2.zero && !GetPlayerInputs.CrouchInput)
                {
                    TransitionToState(PlayerState.WALK);
                }

                if(GetPlayerInputs.MoveInputs != Vector2.zero && GetPlayerInputs.CrouchInput)
                {
                    TransitionToState(PlayerState.CROUCH);
                }

                if(GetPlayerInputs.MoveInputs == Vector2.zero && GetPlayerInputs.CrouchInput)
                {
                    TransitionToState(PlayerState.CROUCHIDLE);
                }


                break;
            case PlayerState.WALK:

                if(GetPlayerInputs.MoveInputs == Vector2.zero && !GetPlayerInputs.CrouchInput)
                {
                    TransitionToState(PlayerState.IDLE);
                }

                if (GetPlayerInputs.MoveInputs == Vector2.zero && GetPlayerInputs.CrouchInput)
                {
                    TransitionToState(PlayerState.CROUCHIDLE);
                }

                if(GetPlayerInputs.MoveInputs != Vector2.zero && GetPlayerInputs.CrouchInput)
                {
                    TransitionToState(PlayerState.CROUCH);
                }

                break;
            case PlayerState.CROUCHIDLE:

                if (GetPlayerInputs.MoveInputs == Vector2.zero && !GetPlayerInputs.CrouchInput)
                {
                    TransitionToState(PlayerState.IDLE);
                }

                if (GetPlayerInputs.MoveInputs != Vector2.zero && GetPlayerInputs.CrouchInput)
                {
                    TransitionToState(PlayerState.CROUCH);
                }

                if (GetPlayerInputs.MoveInputs != Vector2.zero && !GetPlayerInputs.CrouchInput)
                {
                    TransitionToState(PlayerState.WALK);
                }

                break;
            case PlayerState.CROUCH:

                if (GetPlayerInputs.MoveInputs == Vector2.zero && !GetPlayerInputs.CrouchInput)
                {
                    TransitionToState(PlayerState.IDLE);
                }

                if (GetPlayerInputs.MoveInputs == Vector2.zero && GetPlayerInputs.CrouchInput)
                {
                    TransitionToState(PlayerState.CROUCHIDLE);
                }

                if (GetPlayerInputs.MoveInputs != Vector2.zero && !GetPlayerInputs.CrouchInput)
                {
                    TransitionToState(PlayerState.WALK);
                }

                break;
            case PlayerState.SIT:
                break;
            case PlayerState.PICKUP:
                break;
        }
    }

    void OnStateExit()
    {
        switch (_currentState)
        {
            case PlayerState.IDLE:
                break;
            case PlayerState.WALK:
                break;
            case PlayerState.CROUCHIDLE:
                break;
            case PlayerState.CROUCH:
                break;
            case PlayerState.SIT:
                break;
            case PlayerState.PICKUP:
                break;
        }
    }

    void TransitionToState(PlayerState nextState)
    {
        OnStateExit();
        _currentState = nextState;
        OnStateEnter();
    }



}