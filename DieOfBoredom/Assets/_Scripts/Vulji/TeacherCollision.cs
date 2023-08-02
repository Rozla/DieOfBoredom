using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.GameLost = true;
            LoseTimer.Instance._loseEvent?.Invoke();
            NewEnemyState.Instance._angryEvent?.Invoke();
        }
    }
}
