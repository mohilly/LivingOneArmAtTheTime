using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;

    void Update()
    {
        //Pressing ENTER calls the same function as clicking the button
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playGame(1);
        }

        //Pressing ESC calls the same function as clicking the button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitGame();
        }
    }

    public void playGame (int sceneId) 
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    public void quitGame ()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        loadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        operation.allowSceneActivation = false;
        
        while(!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progressValue;
            //loadingBarFill.fillAmount = progressValue;
            operation.allowSceneActivation = true;
            yield return null;
        }
    }
}
