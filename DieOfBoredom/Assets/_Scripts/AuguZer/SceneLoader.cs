using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class SceneLoader : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject looseMenu;
    [SerializeField] GameObject UIplayer;
    int _builtIndex;

    [SerializeField] BoxBehaviour boxBehaviour;
    [SerializeField] LoseTimer loseTimer;

    UIGameBehaviour _uiGameScript;


    private void Awake()
    {

        _uiGameScript = pauseMenu.transform.parent.GetComponent<Transform>().parent.GetComponent<UIGameBehaviour>();

        GameManager.GameWin = false;
        GameManager.GameLost = false;

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        if (UIplayer != null)
        {
            UIplayer.SetActive(true);
        }

        if (winMenu != null)
        {
            winMenu.SetActive(false);
        }

        if (looseMenu != null)
        {
            looseMenu.SetActive(false);
        }
    }

    private void Start()
    {
        if (boxBehaviour != null)
        {
            boxBehaviour._winEvent.AddListener(() =>
            {
                StartCoroutine(WinPanelCoroutine());
            });
        }

        if (loseTimer != null)
        {
            loseTimer._loseEvent.AddListener(() =>
            {
                StartCoroutine(LoosePanelCoroutine());
            });
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

        //Win();
        //Loose();
    }

    private void Win()
    {
        if (GameManager.GameWin)
        {
            winMenu.SetActive(true);
            UIplayer.SetActive(false);
        }
    }

    private void Loose()
    {
        if (GameManager.GameLost)
        {
            looseMenu.SetActive(true);
            UIplayer.SetActive(false);
        }
    }

    public void LoadScene(int buildIndex)
    {
        StartCoroutine(LoadSceneCoroutine(buildIndex));
    }
    public void NextLevel()
    {
        Debug.Log("NextLevel");
        //SceneManager.LoadScene(_builtIndex + 1);
    }

    public void TryAgain()
    {
        StartCoroutine(TryAgainCoroutine());
    }
    public void Continue()
    {
        StartCoroutine(ContinueCoroutine());
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
        StartCoroutine(ExitCoroutine());
#endif

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    IEnumerator LoadSceneCoroutine(int index)
    {
        yield return new WaitForSeconds(1.2f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(index);
    }

    IEnumerator TryAgainCoroutine()
    {
        yield return new WaitForSeconds(1.2f);
        Time.timeScale = 1.0f;
        looseMenu.SetActive(false);
        _builtIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(_builtIndex);
        UIplayer.SetActive(true);
    }

    IEnumerator ContinueCoroutine()
    {
        yield return new WaitForSeconds(1.2f);
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

    IEnumerator ExitCoroutine()
    {
        yield return new WaitForSeconds(1.2f);
        Application.Quit();
    }

    IEnumerator WinPanelCoroutine()
    {
        yield return new WaitForSeconds(6f);
        Win();
    }
    IEnumerator LoosePanelCoroutine()
    {
        yield return new WaitForSeconds(3f);
        Loose();
    }
}
