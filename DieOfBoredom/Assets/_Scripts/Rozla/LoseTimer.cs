using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LoseTimer : MonoBehaviour
{

    public static LoseTimer Instance { get; private set; }  

    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] GameObject _timerDisplayObject;

    //[Tooltip("Default value = 300f")]

    public UnityEvent _loseEvent;

    [Tooltip("Default value = 300f")]
    [SerializeField] float _timer = 300f;
    [SerializeField] float _displayTime = 30f;
    float _minutes;
    float _seconds;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        _timerDisplayObject.SetActive(false);
    }

    private void Start()
    {
        _loseEvent.AddListener(() =>
        {
            _timerDisplayObject.SetActive(false);
        });

        BoxBehaviour.BoxInstance._winEvent.AddListener(() =>
        {
            _timerDisplayObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameLost || GameManager.GameWin) return;

        _timer -= Time.deltaTime;
        //_minutes = Mathf.FloorToInt(_timer / 60f);
        //_seconds = Mathf.FloorToInt(_timer % 60f);

        //_timerText.text = string.Format("{0:00}.{1:00}", _minutes, _seconds);

        if(_timer <= _displayTime + 2f)
        {
            _timerDisplayObject.SetActive(true);
            _timerText.text = "HURRY  UP";
        }

        if(_timer <= _displayTime)
        {
            _timerText.text = _timer.ToString("0");
        }

        if (_timer <= 0f)
        {
            _loseEvent?.Invoke();
            if(!GameManager.GameWin) GameManager.GameLost = true;
        }
    }
}
