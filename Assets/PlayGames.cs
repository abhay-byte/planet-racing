using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using TMPro;
using Newtonsoft.Json;
using System.Text;
using System.Collections;

public class PlayGames : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button playGamesLoginButton;
    [SerializeField] private Button playGamesSignInButton;
    [SerializeField] private TMP_Text playGamesLoginButtonText;
    userData userdata;
    PlayerStats PlayerStats;
    public string Token;
    public string Error;
    public Texture2D saveFile;
    public GameObject startGame;
    private string mSavedGameFilename;
    private ConflictResolutionStrategy strategy;
    private ISavedGameMetadata mCurrentSavedGame;
    private AuthSystem authSystem;
    public LoadScene loadScene;
    public bool playerAuthenticated;
    private GPG_A_A_LB gPG_A_A_LB;
    private GPG_LF gPG_LF;

    public void Start()
    {
/*        playGamesLoginButton.onClick.AddListener(PlayGamesLogin);*/

        authSystem = gameObject.GetComponent<AuthSystem>();
/*        playGamesSignInButton.onClick.AddListener(delegate { LoadGameData(mCurrentSavedGame); });*/
        userdata = GetComponent<userData>();
        mSavedGameFilename = "SaveSlot_00";
        strategy = ConflictResolutionStrategy.UseLongestPlaytime;
        if (Application.isEditor)
        {
            print("Editor Mode.");
            loadScene.GoToFirst();
        }
        else
        {
            StartCoroutine(CheckLocalSaveDataExist());
            gPG_A_A_LB = GetComponent<GPG_A_A_LB>();
            gPG_LF = GetComponent<GPG_LF>();
        }

    }

    IEnumerator CheckLocalSaveDataExist()
    {

        yield return new WaitForSeconds(2f);
        if(!userdata.saveFileExists)
        {
            if(Social.localUser.authenticated)
            {

                OpenSavedGameLoad(mSavedGameFilename);
            }
            else
            {
                
                loadScene.GoToFirst();
                startGame.SetActive(true);
            }
        }
    }

        void Awake()
    {
        //Initialize PlayGamesPlatform
        PlayGamesPlatform.Activate();
        LoginGooglePlayGames();
    }

    public void LoginGooglePlayGames()
    {
        PlayGamesPlatform.Instance.Authenticate((success) =>
        {
            if (success == SignInStatus.Success)
            {
                Debug.Log("Login with Google Play games successful.");
                playerAuthenticated = true;
                Debug.Log("Player Authenticated: "+ playerAuthenticated);
                OpenSavedGame(mSavedGameFilename);
                if(userdata.saveFileExists)
                {
                    if (userdata.PG_currentId == Social.localUser.id)
                    {
                        loadScene.GoToFirst();
                    }
                    else
                    {
                        OpenSavedGameLoad(mSavedGameFilename);
                    }
                }


                gPG_A_A_LB.LoadLeaderBoardData();
                gPG_LF.LoadFriends();

                ((PlayGamesLocalUser)Social.localUser).GetStats((rc, stats) =>
                {
                    // -1 means cached stats, 0 is succeess
                    // see  CommonStatusCodes for all values.
                    if (rc <= 0 && stats.HasDaysSinceLastPlayed())
                    {

                        Debug.Log("It has been " + stats.DaysSinceLastPlayed + " days");
                        PlayerStats = stats;
                        playGamesLoginButtonText.text = Social.localUser.userName;
                    }
                });

            }
            else
            {

                playerAuthenticated = false;
                Error = "Failed to retrieve Google play games authorization code";
                Debug.Log("Login Unsuccessful");
            }
        });
    }

    void PlayGamesLogin()
    {

        LoginGooglePlayGames();
    }

    //showing Save games ui
    public void ShowSelectUI()
    {
        uint maxNumToDisplay = 5;
        bool allowCreateNew = false;
        bool allowDelete = true;

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ShowSelectSavedGameUI("Select saved game",
            maxNumToDisplay,
            allowCreateNew,
            allowDelete,
            OnSavedGameSelected);
    }


    public void OnSavedGameSelected(SelectUIStatus status, ISavedGameMetadata game)
    {
        if (status == SelectUIStatus.SavedGameSelected)
        {


        }
        else
        {
            // handle cancel or error
        }
    }

    void OpenSavedGame(string filename)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadNetworkOnly,
            strategy, OnSavedGameOpened);
    }
    void OpenSavedGameLoad(string filename)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadNetworkOnly,
            strategy, OnSavedGameOpenedLoadGame);
    }

    public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Save file opened, Metadata created.");
            mCurrentSavedGame = game;
        }
        else
        {
            Debug.Log("Save file failed to open.");
        }
    }
    public void OnSavedGameOpenedLoadGame(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Save file opened, Metadata created.");
            mCurrentSavedGame = game;
            LoadGameData(mCurrentSavedGame);
        }
        else
        {
            Debug.Log("Save file failed to open.");
        }
    }


    // Writing Save game

    public void savedata(byte[] data)
    {
        

        try
        {
            TimeSpan totaltimeplayed;
            if (PlayerStats != null)
            {
                float minutes = (PlayerStats.NumberOfSessions * PlayerStats.AvgSessionLength);
                Debug.Log(minutes);

                totaltimeplayed = new TimeSpan((int)minutes / 60, (int)minutes % 60, 0);
                Debug.Log(totaltimeplayed);
            }
            else
            {
                float minutes = 0;
                Debug.Log(minutes);

                totaltimeplayed = new TimeSpan((int)minutes / 60, (int)minutes % 60, 0);
                Debug.Log(totaltimeplayed);
            }


            string imageUrl = "https://ivarnagames.web.app/img/1.png";

            //ISavedGameMetadata gameMetaData = new metadata(true, "SaveSlot00", "Save data of fantasy racer.", null, totaltimeplayed, DateTime.Now);

            PlayGamesPlatform.Instance.SavedGame.OpenWithAutomaticConflictResolution(
                mSavedGameFilename,
                DataSource.ReadNetworkOnly,
                strategy,
                (status, openedFile) =>
                {
                    /*                    string Status = "Open status for file " + mSavedGameFilename + ": " + status + "\n";*/
                    if (openedFile != null)
                    {
                        /*                        Status += "Successfully opened file: " + openedFile;
                                                GooglePlayGames.OurUtils.Logger.d("Opened file: " + openedFile);*/
                        mCurrentSavedGame = openedFile;
                        SaveGame(mCurrentSavedGame, data, totaltimeplayed);
                    }

                });
            Debug.Log("Write Data Start...");
        }
        catch (Exception error)
        {
            Debug.Log("Encountered error: " + error);
        }


    }

    void SaveGame(ISavedGameMetadata game, byte[] savedData, TimeSpan totalPlaytime)
    {
        try
        {
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

            SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();


            Debug.Log(builder);
            Texture2D savedImage = saveFile;
            
            if (savedImage != null)
            {
                // This assumes that savedImage is an instance of Texture2D
                // and that you have already called a function equivalent to
                // getScreenshot() to set savedImage
                // NOTE: see sample definition of getScreenshot() method below
                byte[] pngData = savedImage.EncodeToPNG();
                builder = builder.WithUpdatedPngCoverImage(pngData);
            }
            SavedGameMetadataUpdate updatedMetadata = builder.Build();
            PlayGamesPlatform.Instance.SavedGame.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
            Debug.Log("Play Games: Data Saved I think");
        }
        catch(Exception error)
        {
            Debug.Log("Encountered error: "+error);
        }

    }

    public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log(status);
            Debug.Log("Data Saved");
        }
        else
        {
            Debug.Log(status);
            Debug.Log("Not Data Saved");
        }
    }

    void LoadGameData(ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
    }

    public void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            try
            {
                Debug.Log("Entered");
                string playerData = Encoding.ASCII.GetString(data);
                Debug.Log("Converted to string");

                userData.PlayerCompleteData saveFileData = JsonConvert.DeserializeObject<userData.PlayerCompleteData>(playerData);
                Debug.Log("Read Data Complete " + saveFileData.PlayerName);
                userdata.SaveDataFromPlayGames(saveFileData);
/*                loadScene.GoToMain();
                authSystem.AuthUIObjects.SetActive(false);
                authSystem.Options.SetActive(false);*/
            }
            catch(System.Exception error)
            {
                Debug.Log("Error Occured in reading: "+ error);

                userdata.First();
                startGame.SetActive(true);

            }

           
        }
        else
        {
            Debug.Log("Player Data Read Failed.");
            userdata.First();
            startGame.SetActive(true);
        }
    }

}
