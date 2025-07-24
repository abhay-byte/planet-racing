using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class FriendsUpdator : MonoBehaviour
{
    private GPG_LF GPG_FL;

    // Start is called before the first frame update
    private userData userData;

    public GameObject scrollViewParent;
    public GameObject clone;
    void Start()
    {
        GPG_FL = GameObject.Find("/UserData").GetComponent<GPG_LF>();
        userData = GameObject.Find("/UserData").GetComponent<userData>();
        CreateFriendsList(GPG_FL.friendsList);
    }

    private void CreateFriendsList(List<IUserProfile> friendsList)
    {
        int i = 0;
            foreach (IUserProfile data in friendsList)
            {
                i++;

                GameObject friendObject = Instantiate(clone);
                friendObject.SetActive(true);
                friendObject.transform.SetParent(scrollViewParent.transform, false);
                FriendsObjects friendObjects = friendObject.GetComponent<FriendsObjects>();



            //leaderboardObjects.playerAvatorImage.sprite = ConvertToSprite(data.UserProfile.image);
                friendObjects.friendName.text = data.userName;
                friendObjects.friendScore.text = "---";
                friendObjects.serialNo.text = i+".";

            }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
