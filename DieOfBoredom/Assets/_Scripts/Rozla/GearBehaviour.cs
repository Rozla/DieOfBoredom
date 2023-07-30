using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearBehaviour : MonoBehaviour
{

    public bool _hasBeenPicked;

    // Start is called before the first frame update
    void Start()
    {
        _hasBeenPicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasBeenPicked)
        {
            gameObject.SetActive(false);
        }
    }
}
