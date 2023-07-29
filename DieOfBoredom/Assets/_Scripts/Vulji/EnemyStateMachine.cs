using System.Collections;
using UnityEngine;

//public class EnemyStateMachine : MonoBehaviour
//{
//    enum EnemyState
//    {
//        LookBoard,
//        Turn,
//        LookClass,
//        Angry
//    }

//    [SerializeField] EnemyState currentState;

//    public GameObject player;
//    public float minTimeToTurn = 5f;
//    public float maxTimeToTurn = 15f;

//    //private PlayerStateMachine playerStateMachine;
//    private float timeToTurn;

//    private bool previousBoard;
//    private bool previousClass;

//    private void Start()
//    {
//        playerStateMachine = playerStateMachine.Instance.currentState;
//        currentState = EnemyState.LookBoard;
//    }

//    private IEnumerator WaitForTurn()
//    {
//        while (true)
//        {
//            timeToTurn = Random.Range(minTimeToTurn, maxTimeToTurn);
//            yield return new WaitForSeconds(timeToTurn);
//            TransitionToState(EnemyState.Turn);
//        }
//    }

//    void OnStateEnter()
//    {
//        switch (currentState)
//        {
//            case EnemyState.LookBoard:
//                StartCoroutine("WaitForTurn");
//                break;
//            case EnemyState.Turn:
//                transform.Rotate(0, 180, 0);
//                break;
//            case EnemyState.LookClass:
//                break;
//            case EnemyState.Angry:
//                break;
//            default:
//                break;
//        }
//    }

//    void OnStateUpdate()
//    {
//        switch (currentState)
//        {
//            case EnemyState.LookBoard:
//                break;
//            case EnemyState.Turn:
//                if(previousBoard == true)
//                {
//                    TransitionToState(EnemyState.LookClass);
//                }
//                if(previousClass == true)
//                {
//                    TransitionToState()
//                }
//                break;
//            case EnemyState.LookClass:
//                if(playerStateMachine.Instance.currentState = !playerStateMachine.Sit)
//                {
//                    TransitionToState(EnemyState.Angry);
//                }
//                else
//                {
//                    TransitionToState(EnemyState.Turn);
//                }
//                break;
//            case EnemyState.Angry:
//                //GameManager.Instance.YouLose
//                break;
//            default:
//                break;
//        }
//    }

//    void OnStateExit()
//    {
//        switch (currentState)
//        {
//            case EnemyState.LookBoard:
//                previousBoard = true;
//                break;
//            case EnemyState.Turn:
//                previousBoard = false;
//                previousClass = false;
//                break;
//            case EnemyState.LookClass:
//                previousClass = true;
//                break;
//            case EnemyState.Angry:
//                break;
//            default:
//                break;
//        }
//    }

//    void TransitionToState(EnemyState nextState)
//    {
//        OnStateExit();
//        currentState = nextState;
//        OnStateEnter();
//    }
//}
