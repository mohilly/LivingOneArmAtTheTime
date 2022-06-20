using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{

    public GameObject pauseMenu;
    public static bool isPaused = false;

    public GameObject loadingScreen;
    public Slider slider;

    public GameObject endGameScreen;
    public  bool isEnd = false;
    public  bool isCompleted = false;

    // Start is called before the first frame update
    void Start()
    {
        loadingScreen.SetActive(false);
        pauseMenu.SetActive(false);
        endGameScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (isPaused || isEnd) { BacktoMenu(0); }
            else if (!isPaused) { PauseGame(); }

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isPaused) { ResumeGame(); }
            if(isEnd) { ReloadGame(); }
        }

        if (TriggerObjectScript.allObjectsTOS == true) { isCompleted = true; }
        if(isCompleted) { endGameScreen.SetActive(true); isEnd = true; }
        
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ReloadGame()
    {
        endGameScreen.SetActive(false);
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
        isPaused = false;
        isEnd = false;
    }

    public void BacktoMenu(int sceneId)
    {
        Time.timeScale = 1f;
        isPaused = false;
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        loadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progressValue;
            //loadingBarFill.fillAmount = progressValue;
            operation.allowSceneActivation = true;
            yield return null;
        }
    }

}
