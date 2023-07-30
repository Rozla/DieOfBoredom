using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoseTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timerText;


    float _timer;
    float _minutes;
    float _seconds;

    // Start is called before the first frame update
    void Start()
    {

        _timer = 300f;
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        _minutes = Mathf.FloorToInt(_timer / 60f);
        _seconds = Mathf.FloorToInt(_timer % 60f);

        _timerText.text = string.Format("{0:00}.{1:00}", _minutes, _seconds);

        if (_timer <= 0f)
        {
            GameManager.GameLost = true;
        }
    }
}
