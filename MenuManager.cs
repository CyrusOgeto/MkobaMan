using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenuButtons;
    public void LoadScene(string sceneName)
    {
        MainMenuButtons.gameObject.SetActive(false);
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            Debug.Log($"Loading {sceneName}: {asyncLoad.progress * 100}%");
            yield return null;
        }

        Debug.Log($"Scene {sceneName} loaded successfully.");
    }
    public void QuitGame()
    {
        Debug.Log("This will work on actual Phone");
        Handheld.Vibrate();
        Application.Quit();
    }
}
