using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyState : MonoBehaviour
{
    enum EnemyState
    {
        LookBoard,
        Turn,
        LookClass,
        Angry,
        Sad
    }

    [SerializeField] EnemyState currentState;
    public float minTimeToTurn = 5f;
    public float maxTimeToTurn = 15f;
    public float cooldownTime = 5f;

    public float timeToTurn;
    public float turningTime = 0.5f;
    public float timeBeforeLookingBack = 5f;
    private float lastTurnTime;

    //private PlayerMovement playerMovement;
    private BoxCollider detectionZone;
    private Animator animator;

    private bool isRotating;
    private Quaternion targetRotation;
    private EnemyState previousState;
    private bool isWaitingForTurn = false;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        //playerMovement = FindObjectOfType<PlayerMovement>();
        detectionZone = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        TransitionToState(EnemyState.LookBoard);
    }

    private IEnumerator WaitForTurn()
    {
        isWaitingForTurn = true;

        while (true)
        {
            if (!isRotating && Time.time - lastTurnTime >= cooldownTime)
            {
                timeToTurn = Random.Range(minTimeToTurn, maxTimeToTurn);
                yield return new WaitForSeconds(timeToTurn);
                lastTurnTime = Time.time;
                TransitionToState(EnemyState.Turn);
            }
            else
            {
                yield return null;
            }
        }
    }

    private IEnumerator RotateTowardsTarget(Quaternion targetRotation, float rotationTime)
    {
        if (isRotating) yield break;

        isRotating = true;
        float elapsedTime = 0;
        Quaternion startRotation = transform.rotation;

        while (elapsedTime < rotationTime)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rotationTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;

        isRotating = false;
    }

    private IEnumerator LookClassCoroutine()
    {
        if (isRotating) yield break;

        if (!PlayerMovement.Instance._isSitting)
        {
            TransitionToState(EnemyState.Angry);
            yield break;
        }

        yield return new WaitForSeconds(timeBeforeLookingBack);

        TransitionToState(EnemyState.Turn);
    }

    IEnumerator SadCoroutine()
    {
        yield return new WaitForSeconds(3.2f);
        Quaternion targetRotation = Quaternion.Euler(0, 180, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1f);
        animator.SetTrigger("Sad");
    }

    private void Update()
    {
        OnStateUpdate();
        if (GameManager.GameWin)
        {
            TransitionToState(EnemyState.Sad);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!PlayerMovement.Instance._isCrouching)
            {
                TransitionToState(EnemyState.Angry);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!PlayerMovement.Instance._isCrouching)
            {
                TransitionToState(EnemyState.Angry);
            }
        }
    }

    private void OnStateEnter()
    {
        switch (currentState)
        {
            case EnemyState.LookBoard:
                detectionZone.enabled = true;
                if (!isWaitingForTurn)
                {
                    StartCoroutine(WaitForTurn());
                }
                break;
            case EnemyState.Turn:
                detectionZone.enabled = false;
                targetRotation = transform.rotation * Quaternion.Euler(0, 180, 0);
                turningTime = 0.5f;
                StartCoroutine(RotateTowardsTarget(targetRotation, turningTime));
                break;
            case EnemyState.LookClass:
                detectionZone.enabled = false;
                StartCoroutine(LookClassCoroutine());
                animator.SetBool("NoClass", true);
                break;
            case EnemyState.Angry:
                GameManager.GameLost = true;
                LoseTimer.Instance._loseEvent?.Invoke();
                animator.SetTrigger("Angry");
                StopAllCoroutines();
                break;
            case EnemyState.Sad:
                StartCoroutine(SadCoroutine());
                break;
            default:
                break;
        }
    }

    private void OnStateUpdate()
    {
        switch (currentState)
        {
            case EnemyState.LookBoard:

                break;
            case EnemyState.Turn:
                if (!isRotating)
                {
                    if (previousState == EnemyState.LookBoard)
                    {
                        TransitionToState(EnemyState.LookClass);
                    }
                    else
                    {
                        TransitionToState(EnemyState.LookBoard);
                    }
                }
                break;
            case EnemyState.LookClass:
                if (!PlayerMovement.Instance._isSitting)
                {
                    TransitionToState(EnemyState.Angry);
                }
                break;
            case EnemyState.Angry:
                break;
            case EnemyState.Sad:
                break;
            default:
                break;
        }
    }

    private void OnStateExit()
    {
        switch (currentState)
        {
            case EnemyState.LookBoard:
                break;
            case EnemyState.Turn:
                break;
            case EnemyState.LookClass:
                animator.SetBool("NoClass", false);
                break;
            case EnemyState.Angry:
                break;
            case EnemyState.Sad:
                break;
            default:
                break;
        }
    }

    private void TransitionToState(EnemyState nextState)
    {
        OnStateExit();
        previousState = currentState;
        currentState = nextState;
        OnStateEnter();
    }
}
