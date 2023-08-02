using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuietZoneBehaviour : MonoBehaviour
{

    BoxCollider _boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            NewEnemyState.Instance.playerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            NewEnemyState.Instance.playerInZone = false;
        }
    }
}
