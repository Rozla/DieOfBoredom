using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJBehavior : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameWin)
        {
            StartCoroutine(WinWaitCoroutine());
        }

        if (GameManager.GameLost)
        {
            StartCoroutine(LooseWaitCoroutine());
        }
    }


    IEnumerator WinWaitCoroutine()
    {
        yield return new WaitForSeconds(2.6f);
        animator.SetTrigger("WIN");

    }
    IEnumerator LooseWaitCoroutine()
    {
        yield return new WaitForSeconds(3f);
        animator.SetTrigger("LOOSE");

    }
}
