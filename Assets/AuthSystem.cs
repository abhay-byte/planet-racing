using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AuthSystem : MonoBehaviour
{
    public FirebaseAuth auth;
    public FirebaseUser user;
    private string displayName;
    private string emailAddress;
    private Uri photoUrl;
    [SerializeField] Animator Loading;

    public Animator accountAlreadyExits;
    public Button newAccount;
    public Button existingAccount;
    public TMP_Text existingAccountText;

    public Button syncData;


    // sign in
    [SerializeField] private Animator signUpAnimator;
    [SerializeField] private TMP_InputField signUpUserEmail;
    [SerializeField] private TMP_InputField signUpUserPassword;
    [SerializeField] private TMP_InputField signUpUserName;

    [SerializeField] private Button signUpButton;
    [SerializeField] private Animator signUpErrorsAnimator;
    [SerializeField] private TMP_Text signUpErrorsText;
    [SerializeField] private Button signUpErrorsButton;

    [SerializeField] private Animator SignUpSuccessfull;

    // Start is called before the first frame update
    void Start()
    {
        userData = GameObject.Find("/UserData").GetComponent<userData>();
        DontDestroyOnLoad(gameObject);


        InitializeFirebase();
        signUpButton.onClick.AddListener(CreateNewUsers);
    }
    // Update is called once per frame
    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                displayName = user.DisplayName ?? "";

                emailAddress = user.Email ?? "";
                //photoUrl = user.PhotoUrl ?? "";
            }
        }
    }
    string errors;
    private userData userData;

    public Animator AccountAlreadyExits { get => accountAlreadyExits; set => accountAlreadyExits = value; }

    private void SignIncall()
    {
        signUpErrorsText.text = errors;
        signUpErrorsButton.onClick.RemoveAllListeners();
        signUpErrorsButton.onClick.AddListener(RevertErrorToSignin);

        Loading.SetTrigger("off");
        signUpAnimator.SetTrigger("off");
        signUpErrorsAnimator.SetTrigger("on");
    }
    private void CreateNewUsers()
    {
        errors = "Errors : ";
        Loading.SetTrigger("on");
        if (signUpUserEmail.text.Length < 5 || signUpUserPassword.text.Length < 8
            || signUpUserName.text.Length <= 3)
        {
            if (signUpUserEmail.text.Length < 5) { errors += "\nEnter valid email address."; }
            if (signUpUserName.text.Length <= 3) { errors += "\nEnter username with more than 3 letters."; }
            if (signUpUserPassword.text.Length < 8) { errors += "\nEnter password with more than 8 letters."; }
            SignIncall();

        }
        else
        {
            auth.CreateUserWithEmailAndPasswordAsync(signUpUserEmail.text, signUpUserPassword.text).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Loading.SetTrigger("off");
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    errors = "\nCreation of account was canceled.";
                    SignIncall();
                    return;
                }
                if (task.IsFaulted)
                {
                    Loading.SetTrigger("off");
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    errors = "\nCreation of account was encountered an error: " + task.Exception;
                    SignIncall();
                    return;
                }

                Loading.SetTrigger("off");
                signUpAnimator.SetTrigger("off");
                SignUpSuccessfull.SetTrigger("on");
                // Firebase user has been created.
                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
            });
        }

    }



    private void RevertErrorToSignin()
    {
        signUpAnimator.SetTrigger("on");
        signUpErrorsAnimator.SetTrigger("off");
    }
}
