using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerBehaviour : MonoBehaviour
{
    public static UIPlayerBehaviour Instance { get; set; }

    [SerializeField] public Animator _gearsAnimator;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
