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

    // Start is called before the first frame update
    void Start()
    {
        loadingScreen.SetActive(false);
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if(isPaused) { BacktoMenu(0); }
            else if (!isPaused) { PauseGame(); }

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isPaused) { ResumeGame(); }
        }
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

    public void BacktoMenu(int sceneId)
    {
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
