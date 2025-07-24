using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPG_LF : MonoBehaviour
{
    // Start is called before the first frame update
    public List<IUserProfile> friendsList = new List<IUserProfile>();
    void Start()
    {
        
    }

    public void LoadFriends()
    {
        Social.localUser.LoadFriends((success) =>
        {
            Debug.Log("Friends loaded OK: ");
            foreach (IUserProfile p in Social.localUser.friends)
            {
                friendsList.Add(p);
                Debug.Log(p.userName + " is a friend");
            }
        });

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
