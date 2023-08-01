using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController _playerCC;
    Transform _playerGraphics;
    Animator _playerAnimController;


    [Header("State Machine")]
    [SerializeField] PlayerState _currentState;

    [Space]
    [Space]

    [Header("OverLap Settings")]
    [SerializeField] LayerMask _interactibleMask;
    [SerializeField] Transform _overlapCenter;
    [SerializeField] float _overlapRadius = .4f;

    [Space]
    [Space]

    [Header("Move Settings")]
    [Range(3f, 10f)][SerializeField] float _walkSpeed = 7f;
    [Range(1f, 3f)][SerializeField] float _crouchSpeed = 3f;
    float _currentSpeed;
    Vector3 _move;
    float _rotationSpeed = 15f;
    float _lastXValue;

    [Space]
    [Space]

    [Header("Sit Settings")]
    [SerializeField] GameObject _leftArrow;
    [SerializeField] GameObject _rightArrow;
    public bool _isSitting;
    public bool _canCheckArrow;
    public bool _canStand;

    [Space]
    [Space]

    [Header("Interact Settings")]
    bool _isInteracting;
    float _interactDuration;




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
        _currentState = PlayerState.SIT;
        _isSitting = true;
        _canCheckArrow = true;
        _lastXValue = 1f;
        _playerCC = GetComponent<CharacterController>();
        _playerGraphics = transform.GetChild(0).transform;
        _playerAnimController = _playerGraphics.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        OnStateUpdate();

        if(!_isSitting && !_isInteracting)
        {
            RotatePlayer();
        }

        if(MoveVector().x != 0f)
        {
            _lastXValue = MoveVector().x;
        }
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
        if(!_isSitting && !_isInteracting)
        {
            _playerCC.Move(MoveVector().normalized * _currentSpeed * Time.deltaTime);
        }
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
                _playerAnimController.SetTrigger("SIT");
                _playerAnimController.SetBool("WALK", false);

                break;
            case PlayerState.PICKUP:

                _currentSpeed = 0f;

                _playerAnimController.SetBool("WALK", false);

                StartCoroutine(InteractCor(_interactDuration));

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

                SetDirArrow(_lastXValue);

                if (_canStand)
                {
                    SitToStand(_lastXValue);
                }

                break;
            case PlayerState.PICKUP:

                if (_isInteracting) return;

                if (GetPlayerInputs.CrouchInput)
                {
                    TransitionToState(PlayerState.CROUCHIDLE);
                }

                if (!GetPlayerInputs.CrouchInput)
                {
                    TransitionToState(PlayerState.IDLE);
                }

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

    public void SphereOverlap()
    {
        if(_isSitting) return;

        Collider[] colliders = Physics.OverlapSphere(_overlapCenter.position, _overlapRadius, _interactibleMask);

        foreach (Collider collider in colliders)
        {
            if(collider.gameObject != null)
            {
                CheckGOTag(collider.gameObject);
            }
        }
    }

    void CheckGOTag(GameObject go)
    {
        if(go.tag == "Chair")
        {
            StartCoroutine(SitOnChairCor(go));
        }
        else if(go.tag == "Gear")
        {
            go.GetComponent<GearBehaviour>()._hasBeenPicked = true;
            _playerAnimController.SetTrigger("PICKUP");
            _interactDuration = .4f;
            TransitionToState(PlayerState.PICKUP);
        }
        else if(go.tag == "RingBox")
        {
            go.GetComponent<BoxBehaviour>().CheckGearLeft();
            _playerAnimController.SetTrigger("INTERACT");
            _interactDuration = 1.5f;
            TransitionToState(PlayerState.PICKUP);
        }
        else
        {
            Debug.LogError("Pas d'objet");
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_overlapCenter.position, _overlapRadius);
    }

    IEnumerator SitOnChairCor(GameObject go)
    {
        _isSitting = true;
        _playerCC.enabled = false;
        TransitionToState(PlayerState.SIT);

        Vector3 offset = new Vector3(0f, .1f, 0f);
        Vector3 currentPos = gameObject.transform.position;
        Vector3 targetPos = go.transform.position + offset;

        Quaternion currentRota = transform.rotation;
        Quaternion targetRota = go.transform.rotation;
        float timer = 0f;
        float maxTimer = 1f;

        while(timer < maxTimer)
        {
            transform.position = Vector3.Lerp(currentPos, targetPos, timer / maxTimer);
            transform.rotation = Quaternion.Slerp(currentRota, targetRota, timer / maxTimer);

            timer += Time.deltaTime;
            yield return null;
        }

        _canCheckArrow = true;
    }

    public void SitToStand(float moveXValue)
    {
        StartCoroutine(SitToStandCor(moveXValue));
    }

    IEnumerator SitToStandCor(float moveXValue)
    {
        _canCheckArrow = false;
        _canStand = false;

        _leftArrow.SetActive(false);
        _rightArrow.SetActive(false);

        Vector3 currentPos = transform.position;
        float offset = moveXValue > 0f ? ( 1f * 1f) : (- 1f * 1f);
        Vector3 targetPos = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);

        Quaternion currentRota = Quaternion.Euler(0f, 0, 0f);
        float offsetRota = moveXValue > 0f ? 90f : -90f;
        Quaternion targetRota = Quaternion.Euler(0f, offsetRota, 0f);

        float timer = 0f;
        float maxTimer = 1f;

        _playerAnimController.SetTrigger("STAND");

        while( timer < maxTimer)
        {
            transform.position = Vector3.Lerp(currentPos, targetPos , timer / maxTimer);
            transform.rotation = Quaternion.Slerp(currentRota, targetRota, timer / maxTimer);

            timer += Time.deltaTime;
            yield return null;
        }

        _isSitting = false;
        _playerCC.enabled = true;

        TransitionToState(PlayerState.IDLE);

    }

    void SetDirArrow(float moveX)
    {
        if(!_canCheckArrow) return;

        if(moveX < 0f)
        {
            _leftArrow.SetActive(true);
            _rightArrow.SetActive(false);
        }
        else if(moveX > 0f)
        {
            _leftArrow.SetActive(false);
            _rightArrow.SetActive(true);
        }
    }

    IEnumerator InteractCor(float interactCorDuration)
    {
        _isInteracting = true;
        yield return new WaitForSeconds(interactCorDuration);
        _isInteracting = false;
    }
}