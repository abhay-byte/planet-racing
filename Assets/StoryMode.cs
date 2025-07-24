using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

public class StoryMode : MonoBehaviour
{
    public GameObject loadingScreen;
    private userData userData;
    public Slider slider;
    [SerializeField] List<GameObject> parts = new List<GameObject>();

    //0 
    public TMP_InputField nameInputField;
    public TMP_InputField planetNameInputField;
    public TMP_Dropdown iconDropDown;

    //1


    void Start()
    {
        userData = GameObject.Find("/UserData").GetComponent<userData>();
        if (userData.playerEngine == null) parts[0].SetActive(true);
        else parts[userData.storyIndex].SetActive(true);
    }

    public void StartGame()
    {
        if (!(nameInputField.text == "") && (nameInputField.text.Length>3))
        {
            userData.PushFirstData(nameInputField.text);
            StartCoroutine(LoadAsynchronously(1));
            parts[userData.storyIndex].SetActive(false);
        }
    }

    public IEnumerator LoadAsynchronously(int sceneIndex)
    {
        loadingScreen.SetActive(true);
        loadingScreen.GetComponent<Animator>().SetTrigger("on");
        yield return new WaitForSeconds(2f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            int val = Convert.ToInt32(progress * 100);
            //percent.text = val + "%";

            yield return null;
        }
        loadingScreen.GetComponent<Animator>().SetTrigger("off");
        slider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
