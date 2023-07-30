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
    public GameObject player;
    public float minTimeToTurn = 5f;
    public float maxTimeToTurn = 15f;

    private float timeToTurn;
    private float timeBeforeMoving;

    private PlayerMovement playerMovement;
    private bool previousBoard;
    private bool previousClass;

    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        currentState = EnemyState.LookBoard;
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
                timeBeforeMoving = 5f;
                break;
            case EnemyState.LookClass:
                timeBeforeMoving = 5f;
                break;
            case EnemyState.Angry:
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
                timeBeforeMoving -= Time.deltaTime;
                if (timeBeforeMoving <= 0)
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
                timeBeforeMoving = 5f;
                if (timeBeforeMoving <= 0)
                {
                    if (!playerMovement._currentState.Equals(PlayerMovement.PlayerState.SIT))
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
                // GameManager.Instance.YouLose();
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
                timeBeforeMoving = 0;
                break;
            case EnemyState.LookClass:
                previousClass = true;
                timeBeforeMoving = 0;
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

