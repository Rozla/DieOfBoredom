using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

public class GearsManager : MonoBehaviour
{
    public static List<GearInList> _allGears = new List<GearInList>();

    private void OnDrawGizmos()
    {
        Handles.zTest = CompareFunction.LessEqual;

        foreach(GearInList gear in _allGears)
        {
            Vector3 managerPos = transform.position;
            Vector3 playerPos = gear.transform.position;
            float halfHeight = (managerPos.y - playerPos.y) * .5f;
            Vector3 offset = Vector3.up * halfHeight;


            Handles.DrawBezier(managerPos, playerPos, managerPos - offset, playerPos + offset, Color.white, EditorGUIUtility.whiteTexture, 2f);
        }
    }
}
