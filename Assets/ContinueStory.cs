using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueStory : MonoBehaviour
{
    private LoadScene loadScene;

    // Start is called before the first frame update
    private void Start()
    {
        loadScene = GameObject.Find("/LevelLoader").GetComponent<LoadScene>();
        StartCoroutine(Continue());
    }
    IEnumerator Continue()
    {
        yield return new WaitForSeconds(12f);
        loadScene.GoToMain();
    }
}
