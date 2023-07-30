using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GearInList : MonoBehaviour
{
    private void OnEnable() => GearsManager._allGears.Add(this);

    private void OnDisable() => GearsManager._allGears.Remove(this);

}
