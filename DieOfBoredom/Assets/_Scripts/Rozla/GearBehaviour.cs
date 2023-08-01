using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearBehaviour : MonoBehaviour
{

    public bool _hasBeenPicked;
    [SerializeField] private GameObject psGear;

    // Start is called before the first frame update
    void Start()
    {
        _hasBeenPicked = false;
        psGear.SetActive(false);
        StartCoroutine(StartPSCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasBeenPicked)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator StartPSCoroutine()
    {
        float index = Random.Range(2f, 6f);
        yield return new WaitForSeconds(index);
        psGear.SetActive(true);
    }
}
