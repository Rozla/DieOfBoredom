using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckGearLeft()
    {
        if (GearsManager._allGears.Count > 0)
        {
            Debug.Log("Il reste des engrenages");
        }

        if (GearsManager._allGears.Count == 0)
        {
            Debug.Log("C'est gagné");
        }
    }
}
