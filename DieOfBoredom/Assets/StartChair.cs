using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartChair : MonoBehaviour
{
    [SerializeField] GameObject _gregPrefab;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(_gregPrefab, transform.position, Quaternion.identity);
    }
}
