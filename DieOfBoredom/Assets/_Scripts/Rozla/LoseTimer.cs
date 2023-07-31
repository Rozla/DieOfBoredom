using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;


public class LoseTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timerText;

    //[Tooltip("Default value = 300f")]


    [Tooltip("Default value = 300f")]
    [SerializeField] float _timer = 300f;
    float _minutes;
    float _seconds;

    // Start is called before the first frame update
    void Start()
    {

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
            if(!GameManager.GameWin) GameManager.GameLost = true;
        }
    }
}
