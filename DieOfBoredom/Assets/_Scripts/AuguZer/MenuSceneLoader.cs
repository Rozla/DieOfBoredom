using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneLoader : MonoBehaviour
{
    public void LoadScene(int buildIndex)
    {
        StartCoroutine(LoadSceneCoroutine(buildIndex));
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

    IEnumerator ExitCoroutine()
    {
        yield return new WaitForSeconds(1.2f);
        Application.Quit();
    }
}
