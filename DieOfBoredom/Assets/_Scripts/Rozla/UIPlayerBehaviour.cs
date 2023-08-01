using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerBehaviour : MonoBehaviour
{
    public static UIPlayerBehaviour Instance { get; set; }

    [SerializeField] public Animator _gearsAnimator;

    [SerializeField] GameObject _teacherDisplay;


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

    private void Start()
    {
        NewEnemyState.Instance._angryEvent.AddListener(() =>
        {
            StartCoroutine(TeacherCamCor());
        });
    }

    IEnumerator TeacherCamCor()
    {
        yield return new WaitForSeconds(.1f);
        _teacherDisplay.SetActive(true);
        yield return new WaitForSeconds(3f);
        _teacherDisplay.SetActive(false);
    }
}
