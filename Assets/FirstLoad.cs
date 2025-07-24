using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstLoad : MonoBehaviour
{
    public Slider slider;
    public GameObject startNew;
    public GameObject LoadingScreem;
    public Slider loadingSlider;

    public GameObject progress;

    userData userData;

    private void Start()
    {
        userData = GameObject.Find("/UserData").GetComponent<userData>();


        if (userData.saveFileExists)
        {
            if ((userData.PG_currentId == Social.localUser.id) && !userData.AuthGB.activeSelf)
            {
                Debug.Log("Enter MAIN scene.");
                progress.SetActive(true);
                StartCoroutine(LoadAsynchronously("Main"));
            }
            else
            {
                progress.SetActive(false);
                startNew.SetActive(true);
            }
        }
        else
        {
            progress.SetActive(false);
            startNew.SetActive(true);
        }

    }

    public void LoadIntoGame()
    {
        progress.SetActive(true);
        StartCoroutine(LoadAsynchronously("Main"));
    }

    public void StartGame()
    {
        StartCoroutine(LoadAsynchronouslySM("StoryMode"));
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(LoadAsynchronously(scene));
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
        //loadingScreen.GetComponent<Animator>().SetTrigger("off");
    }

    IEnumerator LoadAsynchronouslySM(string sceneNmae)
    {
        LoadingScreem.SetActive(true);
        
        yield return new WaitForSeconds(2f);



        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneNmae);


        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingSlider.value = progress;
            int val = Convert.ToInt32(progress * 100);
            //percent.text = val + "%";

            yield return null;
        }
        //loadingScreen.GetComponent<Animator>().SetTrigger("off");
    }
}
