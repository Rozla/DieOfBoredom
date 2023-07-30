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

    private bool checkSitting;
    private bool previousBoard;
    private bool previousClass;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        checkSitting = playerMovement._isSitting;
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

    private void Update()
    {
        OnStateUpdate();
    }

    void OnStateEnter()
    {
        switch (currentState)
        {
            case EnemyState.LookBoard:
                StartCoroutine("WaitForTurn");
                break;
            case EnemyState.Turn:
                transform.Rotate(0, 180, 0);
                timeBeforeMoving1 = 5f;
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
                    if (previousBoard == true)
                    {
                        TransitionToState(EnemyState.LookClass);
                    }
                    if (previousClass == true)
                    {
                        TransitionToState(EnemyState.LookBoard);
                    }
                }
                break;
            case EnemyState.LookClass:
                timeBeforeMoving2 -= Time.deltaTime;
                if (timeBeforeMoving2 <= 0)
                {
                    if (!checkSitting)
                    {
                        TransitionToState(EnemyState.Angry);
                    }
                    else
                    {
                        TransitionToState(EnemyState.Turn);
                    }
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
                timeBeforeMoving1 = 0;
                break;
            case EnemyState.LookClass:
                previousClass = true;
                timeBeforeMoving2 = 0;
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

