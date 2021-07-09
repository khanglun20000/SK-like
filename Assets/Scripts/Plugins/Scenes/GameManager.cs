using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool gameIsPause =false;
    public static bool canPaused = false;
    public GameObject loadingScreen;
    public GameObject pauseMenu;

    private void Awake()
    {
        instance = this;

        SceneManager.LoadSceneAsync((int)SceneIndexes.CHARACTER_SELECTION, LoadSceneMode.Additive);
    }

    List<AsyncOperation> sceneLoading = new List<AsyncOperation>();

    public void LoadMap1()
    {
        canPaused = true;
        loadingScreen.SetActive(true);
        sceneLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.CHARACTER_SELECTION));
        sceneLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.MAP1,LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    public IEnumerator GetSceneLoadProgress()
    {
        for(int i = 0; i < sceneLoading.Count; i++)
        {
            while (sceneLoading[i].isDone != true)
            {
                yield return null;
            }
        }

        loadingScreen.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canPaused)
        {
            if (gameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        gameIsPause = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    void Pause()
    {
        gameIsPause = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
