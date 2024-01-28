using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [Header("Scene Managment")]
    [SerializeField]
    private string mainMenuScene;
    [SerializeField]
    private List<string> listScenes;

    private string errorScene = "ErrorScene";

    private string currentScene;
    private int currentSceneIndex;

    private void OnEnable()
    {
        EventsManager_PersistenceScene.ReloadSceneEvent += ReloadScene;
        EventsManager_PersistenceScene.LoadNextSceneEvent += LoadNextScene;

    }
    private void OnDisable()
    {
        EventsManager_PersistenceScene.ReloadSceneEvent -= ReloadScene;
        EventsManager_PersistenceScene.LoadNextSceneEvent -= LoadNextScene;
    }
    void Awake()
    {
        LoadMainMenuScene();
    }

    void LoadMainMenuScene()
    {
        // -1, because "LoadNextScene" has "currentSceneIndex++;"
        currentSceneIndex = -1;
        StartCoroutine(LoadScene(mainMenuScene));
    }

    void ReloadScene()
    {
        StartCoroutine(LoadScene(currentScene));
    }

    void LoadNextScene()
    {
        currentSceneIndex++;

        string sceneToLoad = "";

        if(currentSceneIndex < listScenes.Count)
        {
            sceneToLoad += listScenes[currentSceneIndex].ToString();
        }

        StartCoroutine(LoadScene(sceneToLoad));
    }

    IEnumerator LoadScene(string _scene)
    {
        // Start Unloading previous Scene
        if (currentScene != null)
        {
            AsyncOperation unloadSceneAsync =
                SceneManager.UnloadSceneAsync(currentScene);

            while (!unloadSceneAsync.isDone)
            {
                yield return new WaitForEndOfFrame();
            }
        }

        // Start Loading New Scene
        AsyncOperation loadSceneAsync =
            SceneManager.LoadSceneAsync(_scene, LoadSceneMode.Additive);

        if (loadSceneAsync == null)
        {
            loadSceneAsync =
                SceneManager.LoadSceneAsync(errorScene, LoadSceneMode.Additive);
        }

        while (!loadSceneAsync.isDone)
        {
            yield return new WaitForEndOfFrame();
        }

        currentScene = _scene;
    }
}
