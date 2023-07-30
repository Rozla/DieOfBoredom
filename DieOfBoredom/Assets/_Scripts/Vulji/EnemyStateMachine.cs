using System.Collections;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    enum EnemyState
    {
        LookBoard,
        Turn,
        LookClass,
        Angry
    }

    [SerializeField] EnemyState currentState;
    public float minTimeToTurn = 5f;
    public float maxTimeToTurn = 15f;

    public float timeToTurn;
    public float turningTime =0.5f;
    public float timeBeforeLookingBack = 5f;

    private PlayerMovement playerMovement;
    private BoxCollider detectionZone;

    private bool previousBoard;
    private bool previousClass;
    private bool isRotating;

     private Quaternion targetRotation;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Start()
    {
        TransitionToState(EnemyState.LookBoard);
    }

    private IEnumerator WaitForTurn()
    {
        if (!isRotating)
        {
            while (true)
            {
                timeToTurn = Random.Range(minTimeToTurn, maxTimeToTurn);
                yield return new WaitForSeconds(timeToTurn);
                TransitionToState(EnemyState.Turn);
            }
        }

        yield return new WaitForSeconds(5f);

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

        yield return new WaitForSeconds(5f);
    }

    private IEnumerator LookClassCoroutine()
    {

        if (isRotating) yield break;

        if (!playerMovement._isSitting)
        {
            TransitionToState(EnemyState.Angry);
        }
        yield return new WaitForSeconds(timeBeforeLookingBack);

        TransitionToState(EnemyState.Turn);

        yield return new WaitForSeconds(5f);

    }

    private void Update()
    {
        OnStateUpdate();
    }

    void OnStateEnter()
    {
        switch (currentState)
        {
            case EnemyState.LookBoard:
                StartCoroutine(WaitForTurn());
                break;
            case EnemyState.Turn:
                targetRotation = transform.rotation * Quaternion.Euler(0, 180, 0);
                turningTime = 0.5f;
                StartCoroutine(RotateTowardsTarget(targetRotation, turningTime));
                break;
            case EnemyState.LookClass:
                timeBeforeLookingBack = 5f;
                StartCoroutine(LookClassCoroutine());
                break;
            case EnemyState.Angry:
                Debug.Log("You Lose");
                StopAllCoroutines();
                break;
            default:
                break;
        }
    }

    void OnStateUpdate()
    {
        switch (currentState)
        {
            case EnemyState.LookBoard:
                break;
            case EnemyState.Turn:
                if (isRotating == false)
                {
                    if (previousBoard)
                    {
                        TransitionToState(EnemyState.LookClass);
                    }
                    else if (previousClass)
                    {
                        TransitionToState(EnemyState.LookBoard);
                    }
                }
                break;
            case EnemyState.LookClass:
                break;
            case EnemyState.Angry:
                break;
            default:
                break;
        }
    }

    void OnStateExit()
    {
        switch (currentState)
        {
            case EnemyState.LookBoard:
                previousBoard = true;
                break;
            case EnemyState.Turn:
                previousBoard = false;
                previousClass = false;
                break;
            case EnemyState.LookClass:
                previousClass = true;
                break;
            case EnemyState.Angry:
                break;
            default:
                break;
        }
    }

    void TransitionToState(EnemyState nextState)
    {
        OnStateExit();
        currentState = nextState;
        OnStateEnter();
    }
}
