using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    int _builtIndex;

    private void Update()
    {
        if (inputActions != null)
        {
            if (inputActions.FindAction("Pause").WasPressedThisFrame())
            {
                Pause();
            }
        }
    }

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void TryAgain()
    {
        _builtIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(_builtIndex);

    }
    public void Continue()
    {
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void ExitGame()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
