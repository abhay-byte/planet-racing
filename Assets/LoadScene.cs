using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public GameObject loadingScreen;
    userData userData;

    private void Start()
    {
        userData = GameObject.Find("/UserData").GetComponent<userData>();
        DontDestroyOnLoad(gameObject);

        //Screen.SetResolution((int)(Screen.width/1.5f), (int)(Screen.height/1.5f), true,0);
/*        if(userData.saveFileExists)
        {
            StartCoroutine(LoadAsynchronously("StartPage"));
        }*/

    }

/*    public void FirstRun()
    {
        if (!userData.saveFileExists)
        { 
            slider.gameObject.SetActive(false);

        }
        else
        {
            slider.gameObject.SetActive(true);
            if (userData.storyTold) StartCoroutine(LoadAsynchronously(1));
            else StartCoroutine(LoadAsynchronously(1));
        }
    }*/
    public void StartGame()
    {
        gameObject.SetActive(true);
        loadingScreen.GetComponent<Animator>().SetTrigger("on");
        StartCoroutine(LoadAsynchronously("StoryMode"));
    }

    public void GoToMain()
    {
        gameObject.SetActive(true);
        loadingScreen.GetComponent<Animator>().SetTrigger("on");
        StartCoroutine(LoadAsynchronously("Main"));
    }
    public void GoToFirst()
    {
        gameObject.SetActive(true);
        loadingScreen.GetComponent<Animator>().SetTrigger("on");
        StartCoroutine(LoadAsynchronously("StartPage"));
    }
    IEnumerator LoadAsynchronously(string sceneNmae)
    {

        yield return new WaitForSeconds(2f);



        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneNmae);


        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            int val = Convert.ToInt32(progress * 100);
            //percent.text = val + "%";

            yield return null;
        }
        loadingScreen.GetComponent<Animator>().SetTrigger("off");
    }

    IEnumerator SetActiveFalse()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);

    }
}
