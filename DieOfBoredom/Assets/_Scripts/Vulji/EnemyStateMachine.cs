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
    public float timeBeforeMoving1;
    public float timeBeforeMoving2;

    private PlayerMovement playerMovement;

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
        while (true)
        {
            timeToTurn = Random.Range(minTimeToTurn, maxTimeToTurn);
            yield return new WaitForSeconds(timeToTurn);
            TransitionToState(EnemyState.Turn);
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


        TransitionToState(EnemyState.LookClass);
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
                timeBeforeMoving1 = 0.5f;
                StartCoroutine(RotateTowardsTarget(targetRotation, timeBeforeMoving1));
                break;
            case EnemyState.LookClass:
                timeBeforeMoving2 = 5f;
                break;
            case EnemyState.Angry:
                Debug.Log("You Lose");
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
                timeBeforeMoving1 -= Time.deltaTime;
                if (timeBeforeMoving1 <= 0)
                {
                    if (previousBoard)
                    {
                        TransitionToState(EnemyState.LookClass);
                        if (timeBeforeMoving1 < 0)
                            timeBeforeMoving1 = 0.5f;
                    }
                    else if (previousClass)
                    {
                        TransitionToState(EnemyState.LookBoard);
                        if (timeBeforeMoving1 < 0)
                            timeBeforeMoving1 = 0.5f;
                    }
                }
                break;
            case EnemyState.LookClass:
                timeBeforeMoving2 -= Time.deltaTime;
                if (!playerMovement._isSitting)
                {
                    TransitionToState(EnemyState.Angry);
                }
                if (timeBeforeMoving2 <= 0)
                {
                    TransitionToState(EnemyState.Turn);
                }
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
                timeBeforeMoving2 = 5;
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
