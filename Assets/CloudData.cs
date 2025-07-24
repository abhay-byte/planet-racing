using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using UnityEngine;

public class CloudData : MonoBehaviour
{
    // Start is called before the first frame update
    FirebaseFirestore db;
    private userData userData;
    private AuthSystem authSystem;

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        DontDestroyOnLoad(gameObject);
        userData = GameObject.Find("/UserData").GetComponent<userData>();
        authSystem = GameObject.Find("/AuthSystem").GetComponent<AuthSystem>();
        authSystem.syncData.onClick.AddListener(SaveDataFirebase);


    }

    IEnumerator CreateCollection()
    {
        yield return new WaitForSeconds(2f);
        if (authSystem.auth.CurrentUser != null)
        {
            DocumentReference docRef = db.Collection("userdata").Document(authSystem.user.UserId);
            Dictionary<string, object> data = new Dictionary<string, object>
                        {
                                { "First", "Ada" },
                                { "Last", "Lovelace" },
                                { "Born", 1815 },
                        };
            docRef.SetAsync(data).ContinueWithOnMainThread(task =>
            {
                Debug.Log("Added data to the alovelace document in the users collection.");
            });
        }

    }

    private void SaveDataFirebase()
    {
        if (authSystem.auth.CurrentUser != null)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();
            DocumentReference docRef = db.Collection("userdata").Document(authSystem.user.UserId);

            docRef.SetAsync(userData.someJson).ContinueWithOnMainThread(task =>
            {
                Debug.Log("Userdata Saved.");
            });
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
