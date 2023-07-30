using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static bool _gameLost;

    public static bool GameLost
    {
        get
        {
            return _gameLost;
        }
        set
        {
            _gameLost = value;
        }
    }

    static bool _gameWin;

    public static bool GameWin
    {
        get
        {
            return _gameWin;
        }
        set
        {
            _gameWin = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
