using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GearCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _gearCountText;

    int _currentCount;
    int _totalCount;

    // Start is called before the first frame update
    void Start()
    {
        _totalCount = GearsManager._allGears.Count;
    }

    // Update is called once per frame
    void Update()
    {
        _currentCount = _totalCount - GearsManager._allGears.Count;

        _gearCountText.text = _currentCount.ToString() + " of  " + _totalCount.ToString();
    }
}
