using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReputationIncrease : MonoBehaviour
{


    private userData userData;

    [SerializeField] List<GameObject> parts = new List<GameObject>();


    void Start()
    {
        userData = GameObject.Find("/UserData").GetComponent<userData>();
        if (!userData.storyTold)
        {
            parts[userData.storyIndex - 1].SetActive(true);
            userData.storyTold = true;
        }
    }
}
