using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;

    private void Update()
    {
        if (inputActions.FindAction("Pause").WasPressedThisFrame())
        {
            Debug.Log("izi");
        }
    }

    public void LoadScene(int builtIndex)
    {
        SceneManager.LoadScene(builtIndex);
    }

//    public void ExitGame()
//    {
//#if UNITY_STANDALONE
//        Application.Quit();
//#endif

//#if UNITY_EDITOR
//        UnityEditor.EditorApplication.isPlaying = false;
//#endif
//    }
}
