using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class SceneLoader : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject UIplayer;
    int _builtIndex;

    private void Awake()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        if (UIplayer != null)
        {
            UIplayer.SetActive(true);
        }
    }
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
        Time.timeScale = 1f;
        SceneManager.LoadScene(buildIndex);
    }

    public void TryAgain()
    {
        Time.timeScale = 1.0f;
        _builtIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(_builtIndex);

    }
    public void Continue()
    {
        Time.timeScale = 1f;
        if (pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(false);
        }
        if (!UIplayer.activeInHierarchy)
        {
            UIplayer.SetActive(true);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        if (UIplayer != null) 
        {
            UIplayer.SetActive(false);
        }
        if (pauseMenu != null && !pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(true);
        }
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
