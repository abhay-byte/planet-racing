using CI.QuickSave;
using System;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using TMPro;
using GooglePlayGames;

public class userData : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerCompleteData playerData;
    public bool saveFileExists;
    private LoadScene loadScene;

    private int hallOfFameScore;
    private int reputationScore;

    public GameObject AuthGB;
    public Animator accountAlreadyExits;
    public Button newAccount;
    public Button existingAccount;
    public TMP_Text existingAccountText;

    [SerializeField] public List<List<int>> playerEngine; int engineIndex = 0; int engineHealthIndex = 1;
    [SerializeField] public List<List<int>> playerTires; int tireIndex = 0; int tireHealthIndex = 1;
    [SerializeField] public List<List<int>> playerNitros; int nitrosIndex = 0; int nitrosHealthIndex = 1;

    [SerializeField] public List<List<float>> playerBody;
    int bodyIndex = 0; int bodyHealthIndex = 1; int spoilerIndex = 2; int vinylIndex = 3; int bodyModIndex = 4;
    int bodyMetallic = 5; int bodySmoothness = 6; int vinylMetallic = 7; int vinylSmoothness = 8;

    [SerializeField] public List<List<Color32>> playerBodyColor;
    int bodyColor = 0; int vinylColor = 1; int glassColor = 2; int spoilerColor = 3; int interiorColor = 4;

    [SerializeField] public List<Color32> playerTireColor;
    [SerializeField] public int playerCoins;
    [SerializeField] public List<int> reputation; int reputationLevel = 0; int reputationExperience = 1;

    [SerializeField] public List<int> currentSelected;
    int engine = 0; int tire = 1; int nitros = 2; int body = 3;
    [SerializeField] public List<int> kingDefeated;

    [SerializeField] public List<float> playerStats;
    int noOfRacesWon = 0; int noOfRacesLose = 1; int distanceTravelled = 2;
    int carTotalled = 3; int noOfCarsWon = 4; int amountOfMoneyWon = 5; int carbattleWon = 6;
    int carEscapeLevel = 7;
    
 /*    
 
    Hall of Fame = no of wins - no of loss - car totalled + carbattlewon+ carEscape level won;
        
 */
    [SerializeField] public List<float> playerStatsSeasonal;

    [SerializeField] public List<float> trackTimeCompleted;

    [SerializeField] public List<float> trackTimeCompletedSeasonal;

    [SerializeField] public string uniqueId;

    [SerializeField] public List<bool> playerAchievements;

    public string playerName;
    public int storyIndex;
    public bool storyTold;
    public string playerPlanetName;
    public int playerCharIcon;

    public int graphicSetting;
    public int gameVolume;
    public int currentPlanet;
    QuickSaveSettings settings = new QuickSaveSettings();

    //Device Specs
    private string graphicVendor;
    private string apiCurrentlyInUse;
    private string gpuName;
    private int ram;
    private int vRam;
    private double GpuScore;
    private PlayGames playGames;
    private AuthSystem authSystem;

    //new values

    public bool showFps;
    public int currentEscapeLevel;
    public int currentColosseumLevel;

    public List<int> kingLevels;

    public string PG_currentId;
    public bool musicEnabled;
    public int languageIndex;
    public int renderRes;
    public int frameRate;
    public int controlMode;

    List<int> carStructure;//upto lvl 50, 4 index
    List<List<int>> currentSelectedCards; //4 index
    List<int> cardsInventory;
    List<int> accessories;
    List<int> accessoriesInventory;

    // new new items 

    List<List<int>> specialSkinsInv; //index 0: bodyindex index1: customSkinIndex

    



    void Start()
    {
        writingDataTimeout = true;
        playGames = GetComponent<PlayGames>();
        authSystem = gameObject.GetComponent<AuthSystem>();
        uniqueId = SystemInfo.deviceUniqueIdentifier;
        settings.SecurityMode = SecurityMode.Aes;
        settings.Password = "adZ5HsQEppwFF*EjiTw^yN#8oaSikA%v";
        settings.CompressionMode = CompressionMode.Gzip;
        heightScreen = Screen.height;
        widthScreen = Screen.width;
        loadScene = GameObject.Find("/LevelLoader").GetComponent<LoadScene>();

        DontDestroyOnLoad(gameObject);

        try
        {
            ReadUserData();
            saveFileExists = true;
        }
        catch(System.Exception Error)
        {
            // First Run
            Debug.Log(Error);
            saveFileExists = false;

        }
        
    }

    public void CheckDeviceSpecs()
    {
        graphicVendor = SystemInfo.graphicsDeviceVendor;
        apiCurrentlyInUse = SystemInfo.graphicsDeviceType.ToString();
        ram = SystemInfo.graphicsMemorySize;
        vRam = SystemInfo.systemMemorySize;
        gpuName = SystemInfo.graphicsDeviceName;
        var PhoneData = new Dictionary<string, double>
            {
                {"Adreno (TM) 640", 69.73800018310547},{
                "Adreno (TM) 506", 10.356000015735626},{
                "Adreno (TM) 650" , 76.362001247406},{
                "Mali-G72 MP3", 15.239999842643737},{
                "Mali-G78", 61.47000137329101},{
                "Mali-G51", 13.955999865531922},{
                "Adreno (TM) 509", 13.72199987411499},{
                "PowerVR Rogue GE8320", 10.518000118732452},{
                "Adreno (TM) 618", 30.12399963378906},{
                "Adreno (TM) 620", 33.45599950790405},{
                "Mali-T830", 9.311999895572662},{
                "Mali-T760", 10.344000070095062},{
                "Adreno (TM) 612", 16.019999742507935},{
                "Adreno (TM) 512", 25.230000014305116},{
                "Adreno (TM) 505", 11.838000512123108},{
                "Mali-G72", 16.061999917030334},{
                "Mali-G71", 21.491999888420104},{
                "Mali-T720", 7.48199990272522},{
                "Mali-G76", 20.285999579429628},{
                "Adreno (TM) 610", 14.652000274658203},{
                "Mali-G52", 16.1880003118515},{
                "Mali-T860", 9.318000147342682},{
                "Adreno (TM) 630", 53.86199953079224},{
                "Adreno (TM) 660", 101.63400032043457},{
                "Mali-G77", 24.534000549316406},{
                "Adreno (TM) 530", 30.264000191688538},{
                "Adreno (TM) 330", 9.594000055789948},{
                "Mali-G52 MC2", 15.900000157356262},{
                "Adreno (TM) 540", 32.2140003490448},{
                "Mali-G76 MC4", 30.726000394821167},{
                "Adreno (TM) 619", 30.726000394821167},{
                "Mali-G57 MC2", 17.531999802589418},{
                "PowerVR Rogue GE8300", 9.042000153064727},{
                "Adreno (TM) 420", 12.617999939918517},{
                "Adreno (TM) 616", 23.423999891281127},{
                "Mali-T880", 9.636000058650971},{
                "Mali-400 MP", 8.249999964237213},{
                "Adreno (TM) 405", 9.090000085830688},{
                "Adreno (TM) 430", 17.346000216007234},{
                "Mali-G57", 25.193999576568604},{
                "Adreno (TM) 510", 14.346000180244445},{
                "Mali-G57 MC3", 17.748000111579895},{
                "PowerVR Rogue GE8100", 7.745999886989593},{
                "Mali-G57 MC4", 21.08400023460388},{
                "Mali-450 MP", 8.778000168800354},{
                "PowerVR Rogue GM9446", 16.157999782562257},{
                "Adreno (TM) 306", 7.48199990272522},{
                "Adreno (TM) 308", 7.794000163078308},{
                "Adreno (TM) 508", 11.981999859809875},{
                "Intel(R) HD Graphics 520", 53.88399999999999},{
                "Adreno (TM) 642", 50.885998764038085},{
                "Mali-G77 MC9", 25.644000434875487},{
                "PowerVR Rogue GE8322", 9.636000058650971},{
                "Mali-T624", 8.316000137329102},{
                "Mali-T820", 8.652000052928924},{
                "Adreno (TM) 304", 7.415999965667725},{
                "virgl", 38.201999530792236},{
                "Mali-G31", 9.528000075817108},{
                "Adreno (TM) 320", 8.520000178813934},{
                "Mesa DRI Intel(R) UHD Graphics 600 (Geminilake 2x6) ", 17.51400046348572},{
                "Mali-G68 MC4", 23.783999676704408},{
                "PowerVR Rogue G6200", 8.448000011444092},{
                "Android Emulator OpenGL ES Translator (GeForce RTX 2060/PCIe/SSE2)", 46.740000443458555},{
                "PowerVR SGX 544MP", 15.299999957084657},{
                "Intel(R) UHD Graphics 605", 40.299999957084657},{
                "NVIDIA Tegra", 32.07599970817566},{
                "Adreno (TM) 615", 20.466000394821165},{
                "Mesa DRI Intel(R) HD Graphics 400 (Braswell) ", 10.290000078678132},{
                "Adreno (TM) 504", 10.032000153064727},{
                "PowerVR Rogue GX6250", 8.459999785423278},{
                "Adreno (TM) 642L", 44.91599982261658},{
                "Mali-T628", 9.227999975681305},{
                "Mesa DRI Intel(R) HD Graphics (Comet Lake 3x8 GT2) ", 49.65599959373474},{
                "Mali-G71 MP2", 10.158000268936156},{
                "Adreno (TM) 418", 10.062000102996826},{
                "Mali-G52 MC1", 11.321999845504761},{
                "Intel(R) HD Graphics for Atom(TM) x5/x7", 11.124000034332276},{
                "Adreno (TM) 305", 7.415999965667725},{
                "FD618", 27.51000062942505},{
                "PowerVR Rogue G6430", 8.699999771118165}
            };

        try
        {
            GpuScore = PhoneData[gpuName];
            if (GpuScore > 50) graphicSetting = 0;
            else if (GpuScore > 40) graphicSetting = 1;
            else if (GpuScore > 30) graphicSetting = 2;
            else if (GpuScore > 20) graphicSetting = 3;
            else if (GpuScore > 10) graphicSetting = 4;
        }
        catch
        {
            graphicSetting = 3;
        } 
        


    }

    public void PushFirstData(string name)
    {
        List<float> listOfInt = new List<float>() { 0, 100, -1, 3, 0, 0, .9f, 0, .9f };
        List<int> listOfIntC = new List<int>() { 0, 100 };
        List<int> listOfIntR = new List<int>() { 1, 0 };
        List<Color32> listOfColor = new List<Color32>() { Color.red, Color.white, new Color(0, 0, 0, .7f), Color.black, Color.grey };

        playerEngine = new List<List<int>>() { listOfIntC };
        playerTires = new List<List<int>> { listOfIntC };
        playerNitros = new List<List<int>>() { listOfIntC };
        playerBody = new List<List<float>>() { listOfInt };
        currentSelected = new List<int>() { 0, 0, 0, 0 };
        playerBodyColor = new List<List<Color32>>() { listOfColor };
        playerTireColor = new List<Color32>() { Color.white };
        reputation = listOfIntR;
        kingDefeated = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        playerCoins = 15000;
        playerStats = new List<float>()
        {
            0,0,0,0,0,0,0,0
        };

        trackTimeCompleted = new List<float>()
        {
            0,0,0,0,0,0,0,0,0,0
        };
        Debug.Log("Reached 1");
        /*        playerAchievements.SetValue(false,0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,
                    32,33,34,35,36,37,38,39);*/
        playerAchievements = new List<bool>()
        {
            false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,
            false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,
            false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,
            false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,
        };

        try { playerName = name; } catch(Exception error) { Debug.Log("Error: " + error); playerName = "Abhay Raj"; }
        playerName = name;
        storyIndex = 0;
        playerGems = 100;
        kingPass = 0;
        eventPass = 1;
        dailyReward = 0;
        storyTold = true;

        Debug.Log("Reached 2");
        playerPlanetName = "Earth"; 
        CheckDeviceSpecs();
        gameVolume = 100;
        gameVolumeEngine = 75;
        gameVolumeUI = 90;
        gameVolumeMusic = 60;
        currentPlanet = 0;

        Debug.Log("Reached 3");
        if (playGames.playerAuthenticated)
        {
            //Journey Begins
            Social.ReportProgress("CgkIk4emifEBEAIQAQ", 100.0f, (bool success) => {
                // handle success or failure
                Debug.Log("Journey Begins : Unlocked");
            });
        }

        showFps = false;
        currentEscapeLevel = 0;
        currentColosseumLevel = 0;
        kingLevels = new List<int>() {0,0,0,0};
        PG_currentId = Social.localUser.id;
        musicEnabled = true;
        languageIndex = 0;
        renderRes = 75;
        frameRate = 60;
        controlMode = 0;
        carStructure = new List<int>() {0,0,0,0};
        currentSelectedCards = new List<List<int>>() {
        new List<int>(){0,-1,-1,-1},
        new List<int>(){-1,-1,-1,-1},
        new List<int>(){-1,-1,-1,-1},
        new List<int>(){-1,-1,-1,-1}
        };
        cardsInventory = new List<int>()
        { 
            0
        };
        accessories = new List<int>()
        {
            -1,-1,-1,-1
        };
        accessoriesInventory = new List<int>()
        {
            0
        };
        playerCharIcon = 0;
        playerData = new PlayerCompleteData(playerEngine, playerTires, playerNitros, playerBody, playerBodyColor
            , playerTireColor, playerCoins, reputation, currentSelected, kingDefeated, uniqueId, playerName, storyIndex
            , storyTold, playerPlanetName, playerCharIcon, graphicSetting, gameVolume, playerStats, currentPlanet, playerGems,
            kingPass, eventPass, dailyReward, trackTimeCompleted, gameVolumeEngine, gameVolumeUI, gameVolumeMusic, playerAchievements,

            showFps, currentEscapeLevel, currentColosseumLevel, kingLevels, PG_currentId, musicEnabled,
            languageIndex, renderRes, frameRate, controlMode, carStructure, currentSelectedCards, cardsInventory,accessories, accessoriesInventory);
        WriteUserData();
    }

    // Update is called once per frame

    public int gameVolumeEngine;
    public int gameVolumeUI;
    public int gameVolumeMusic;

    public int playerGems;
    public int kingPass;
    public int eventPass;
    public int dailyReward;


    public void WriteUserData()
    {
        QuickSaveWriter saveWriter = QuickSaveWriter.Create("PlayerData", settings);
        saveWriter.Write("PlayerEngine", playerEngine);
        saveWriter.Write("PlayerTires", playerTires);
        saveWriter.Write("PlayerNitros", playerNitros);
        saveWriter.Write("PlayerBody", playerBody);
        saveWriter.Write("PlayerBodyColor", playerBodyColor);
        saveWriter.Write("PlayerTireColor", playerTireColor);
        saveWriter.Write("Coins", playerCoins);
        saveWriter.Write("Reputation", reputation);
        saveWriter.Write("CurrentSelected", currentSelected);
        saveWriter.Write("UniqueId", uniqueId);
        saveWriter.Write("KingDefeated", kingDefeated);

        saveWriter.Write("PlayerName", playerName);
        saveWriter.Write("StoryIndex", storyIndex);
        saveWriter.Write("StoryTold", storyTold);
        saveWriter.Write("PlayerPlanetName", playerPlanetName);
        saveWriter.Write("PlayerCharacterIcon", playerCharIcon);
        saveWriter.Write("GameVolumeEngine", gameVolumeEngine);
        saveWriter.Write("GameVolumeUI", gameVolumeUI);
        saveWriter.Write("GameVolumeMusic", gameVolumeMusic);
        saveWriter.Write("GameVolumeMaster", gameVolume);
        saveWriter.Write("GraphicSetting", graphicSetting);
        saveWriter.Write("PlayerStatistics", playerStats);
        saveWriter.Write("CurrentPlanet", currentPlanet);

        saveWriter.Write("Gems", playerGems);
        saveWriter.Write("KingPass", kingPass);
        saveWriter.Write("EventPass", eventPass);
        saveWriter.Write("DailyRewardIndex", dailyReward);
        saveWriter.Write("TrackTimeCompleted", trackTimeCompleted);
        saveWriter.Write("PlayerAchievements", playerAchievements);

        saveWriter.Write("ShowFps", showFps);
        saveWriter.Write("CurrentEscapeLevel", currentEscapeLevel);
        saveWriter.Write("CurrentColosseumLevel", currentColosseumLevel);
        saveWriter.Write("KingLevels", kingLevels);
        saveWriter.Write("PG_currentId", PG_currentId);
        saveWriter.Write("MusicEnabled", musicEnabled);
        saveWriter.Write("LanguageIndex", languageIndex);
        saveWriter.Write("RenderRes", renderRes);
        saveWriter.Write("FrameRate", frameRate);
        saveWriter.Write("ControlMode", controlMode);
        saveWriter.Write("CarStructure", carStructure);
        saveWriter.Write("CardsInventory", cardsInventory);
        saveWriter.Write("CurrentSelectedCards", currentSelectedCards); 
        saveWriter.Write("Accessories", accessories);
        saveWriter.Write("AccessoriesInventory", accessoriesInventory);


        bool commit = saveWriter.TryCommit(); Debug.Log("SavedData " + commit);
        if(writingDataTimeout)
        {
            StartCoroutine(UpdateAchievementsLeaderBoard());
        }


    }
    bool writingDataTimeout;
    IEnumerator UpdateAchievementsLeaderBoard()
    {
        writingDataTimeout = false;
        yield return new WaitForSeconds(30f);
        writingDataTimeout = true;
        if (playGames.playerAuthenticated)
        {
            Debug.Log("Saving data to play games: Started");
            StartCoroutine(PlayGamesWriteData());
            //Journey Begins
            Social.ReportProgress("CgkIk4emifEBEAIQAQ", 100.0f, (bool success) => {
                // handle success or failure
                Debug.Log("Journey Begins : Unlocked");
            });
             
            if (playerCoins >= 100000)
            {
                //100,000 Star Coins
                Social.ReportProgress("CgkIk4emifEBEAIQFw", 100.0f, (bool success) => {
                    // handle success or failure
                    Debug.Log("100,000 Star Coins : Unlocked");
                });
            }
            if (playerCoins >= 1000000)
            {
                //Millionaire Racer
                Social.ReportProgress("CgkIk4emifEBEAIQGA", 100.0f, (bool success) => {
                    // handle success or failure
                    Debug.Log("Millionaire Racer : Unlocked");
                });
            }
            if (playerCoins >= 1000000000)
            {
                //Billionaire Racer
                Social.ReportProgress("CgkIk4emifEBEAIQGQ", 100.0f, (bool success) => {
                    // handle success or failure
                    Debug.Log("Billionaire Racer : Unlocked");
                });
            }
            if (reputation[reputationLevel] >= 8)
            {
                //Interplanetary Citizen
                PlayGamesPlatform.Instance.IncrementAchievement(
                "CgkIk4emifEBEAIQIA", 8, (bool success) => {
                    Debug.Log("++ Interplanetary Citizen Racer racer");
                });
            }
            if (reputation[reputationLevel] >= 12)
            {
                //King Reputation
                PlayGamesPlatform.Instance.IncrementAchievement(
                "CgkIk4emifEBEAIQHw", 12, (bool success) => {
                    Debug.Log("++ King Reputation Racer racer");
                });
            }
            hallOfFameScore = (int)(playerStats[noOfRacesWon] - playerStats[noOfRacesLose] - playerStats[carTotalled] + playerStats[carbattleWon] + playerStats[carEscapeLevel]);
            reputationScore = reputation[reputationExperience];

            //TimeSpan time = SecondsToTimespan((int)trackTimeCompleted[0]);
            if(hallOfFameScore<1) hallOfFameScore = 1;
            
            Social.ReportScore(hallOfFameScore, "CgkIk4emifEBEAIQIw", (bool success) => {
                Debug.Log("LeaderBoard data Send.");
            });

            Social.ReportScore(reputationScore, "CgkIk4emifEBEAIQJA", (bool success) => {
                Debug.Log("LeaderBoard data Send.");
            });
            float time = trackTimeCompleted[0];
            if(time < 10)
            {
                time = 250;
            }

            Social.ReportScore((long)time, "CgkIk4emifEBEAIQJg", (bool success) => {
                Debug.Log("LeaderBoard data Send.");
            });


        }

    }

    private TimeSpan SecondsToTimespan(int seconds)
    {
        int minutes = seconds / 60;
        int hours = minutes / 60;
        return new TimeSpan(hours,minutes,seconds%60);
    }
    IEnumerator PlayGamesWriteData()
    {
        CreateByteData();
        yield return new WaitForSeconds(1.95f);
        playGames.savedata(userBytedata);
    }
    public void save()
    {
        StartCoroutine(PlayGamesWriteData());
    }
    public void show()
    {
        playGames.ShowSelectUI();
    }
    public void WriteThenRead()
    {
        WriteUserData();
        ReadUserData();
        
    }

    private void ReadUserData()
    {
        QuickSaveReader saveReader = QuickSaveReader.Create("PlayerData", settings);
        playerEngine = saveReader.Read<List<List<int>>>("PlayerEngine");
        playerTires = saveReader.Read<List<List<int>>>("PlayerTires");
        playerNitros = saveReader.Read<List<List<int>>>("PlayerNitros");
        playerBody = saveReader.Read<List<List<float>>>("PlayerBody");
        playerBodyColor = saveReader.Read<List<List<Color32>>>("PlayerBodyColor");
        playerTireColor = saveReader.Read<List<Color32>>("PlayerTireColor");
        playerCoins = saveReader.Read<int>("Coins");
        reputation = saveReader.Read<List<int>>("Reputation");
        currentSelected = saveReader.Read<List<int>>("CurrentSelected");
        uniqueId = saveReader.Read<string>("UniqueId");
        kingDefeated = saveReader.Read<List<int>>("KingDefeated");

        playerName = saveReader.Read<string>("PlayerName");
        storyIndex = saveReader.Read<int>("StoryIndex");
        storyTold = saveReader.Read<bool>("StoryTold");
        playerPlanetName = saveReader.Read<string>("PlayerPlanetName");
        playerCharIcon = saveReader.Read<int>("PlayerCharacterIcon");
        graphicSetting = saveReader.Read<int>("GraphicSetting");
        playerStats = saveReader.Read<List<float>>("PlayerStatistics");
        currentPlanet = saveReader.Read<int>("CurrentPlanet");

        playerGems = saveReader.Read<int>("Gems");
        kingPass = saveReader.Read<int>("KingPass");
        eventPass = saveReader.Read<int>("EventPass");
        dailyReward = saveReader.Read<int>("DailyRewardIndex");
        trackTimeCompleted = saveReader.Read<List<float>>("TrackTimeCompleted");

        gameVolumeEngine = saveReader.Read<int>("GameVolumeEngine");
        gameVolumeUI = saveReader.Read<int>("GameVolumeUI");
        gameVolumeMusic = saveReader.Read<int>("GameVolumeMusic");
        gameVolume = saveReader.Read<int>("GameVolumeMaster");
        playerAchievements = saveReader.Read<List<bool>>("PlayerAchievements");

        showFps = saveReader.Read <bool>("ShowFps");
        currentEscapeLevel = saveReader.Read<int>("CurrentEscapeLevel");
        currentColosseumLevel = saveReader.Read<int>("CurrentColosseumLevel");
        kingLevels = saveReader.Read<List<int>>("KingLevels");
        PG_currentId = saveReader.Read<string>("PG_currentId");
        musicEnabled = saveReader.Read<bool>("MusicEnabled");
        languageIndex = saveReader.Read<int>("LanguageIndex");
        renderRes = saveReader.Read<int>("RenderRes");
        frameRate = saveReader.Read<int>("FrameRate");
        controlMode = saveReader.Read<int>("ControlMode");
        carStructure = saveReader.Read<List<int>>("CarStructure");
        cardsInventory = saveReader.Read<List<int>>("CardsInventory");
        currentSelectedCards = saveReader.Read<List<List<int>>>("CurrentSelectedCards");
        accessories = saveReader.Read<List<int>>("Accessories"); 
        accessoriesInventory = saveReader.Read<List<int>>("AccessoriesInventory");

        playerData = new PlayerCompleteData(playerEngine, playerTires, playerNitros, playerBody, playerBodyColor
            , playerTireColor, playerCoins, reputation, currentSelected, kingDefeated, uniqueId, playerName, storyIndex
            , storyTold, playerPlanetName, playerCharIcon, graphicSetting, gameVolume, playerStats, currentPlanet, playerGems,
            kingPass,eventPass,dailyReward,trackTimeCompleted,gameVolumeEngine,gameVolumeUI,gameVolumeMusic,playerAchievements,showFps,currentEscapeLevel,currentColosseumLevel,kingLevels
            ,PG_currentId,musicEnabled,languageIndex,renderRes,frameRate,controlMode,carStructure,currentSelectedCards, cardsInventory,accessories, accessoriesInventory);


    }

    public byte[] userBytedata;
    public Dictionary<string, object> someJson;

    public void CreateByteData()
    {
        try
        {
/*            someJson = playerData.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, ConvertValue);*/
           string playerdata = JsonConvert.SerializeObject(playerData);
           userBytedata =  Encoding.ASCII.GetBytes(playerdata);
           Debug.Log("Converted to byte data : Success");

        }
        catch (System.Exception error)
        {

            Debug.Log(error);
        }
    }

    class JsonUtils
    {
        public static string ToJson(IEnumerable input)
        {
            string json = "{";
            foreach (var item in input)
            {
                if (item is KeyValuePair<string, object> pair)
                {
                    var key = pair.Key;
                    var value = pair.Value;
                    json += "\"" + key + "\":";
                    if (IsList(value))
                    {

                        json += ToJsonArray((IEnumerable)value);
                    }
                    else if (IsDictionary(value))
                    {
                        json += ToJson((IEnumerable)value);
                    }
                    else
                    {
                        json += ToJsonValue(value);
                    }
                    json += ",";
                }
            }
            if (json.Last() == ',')
            {
                json = json.Substring(0, json.Length - 1);
            }
            json += "}";
            return json;
        }

        public static string ToJsonArray(IEnumerable input)
        {
            string json = "[";
            foreach (var item in input)
            {
                json += ToJsonValue(item);
                json += ",";
            }

            if (json.Last() == ',')
            {
                json = json.Substring(0, json.Length - 1);
            }
            json += "]";
            return json;
        }

        public static string ToJsonValue(object value)
        {
            string json = "";
            if (value == null)
            {
                json += "null";
            }
            else if (value is string)
            {
                json += "\"" + value + "\"";
            }
            else if (value is int)
            {
                json += value.ToString();
            }
            else if (value is float)
            {
                json += value.ToString();
            }
            else if (value is byte)
            {
                json += value.ToString();
            }
            if (IsList(value))
            {

                json += ToJsonArray((IEnumerable)value);
            }

            return json;
        }

        public static bool IsList(object o)
        {
            if (o == null) return false;
            return o is IList &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }

        public static bool IsDictionary(object o)
        {
            if (o == null) return false;
            return o is IDictionary &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>));
        }
    }
    private object ConvertValue(PropertyInfo info)
    {
        var value = info.GetValue(playerData, null);
        if (value is List<Color32> list)
        {
            return Color32AsByteList(list);
        }
        else if (value is List<List<Color32>> list1)
        {
            List<List<List<byte>>> data = new List<List<List<byte>>>();
            for (int i = 0; i < list1.Count; i++)
            {
                data.Add(Color32AsByteList(list1[i]));
            }
            return data;
        }
        else if (value is List<List<int>> list3)
        {
            List<string> data = new List<string>();
            for (int i = 0; i < list3.Count; i++)
            {
                string temp = "";
                temp = list3[i][0] + ",";
                temp += list3[i][1] + ":";
                data.Add(temp);

            }
            return data;
        }
        else if (value is List<List<float>> list4)
        {
            List<string> data = new List<string>();
            for (int i = 0; i < list4.Count; i++)
            {
                string temp = "";
                temp = list4[i][0] + ",";
                temp += list4[i][1] + ":";
                data.Add(temp);

            }
            return data;
        }
        return value;
    }

    private static List<List<byte>> Color32AsByteList(List<Color32> colorList)
    {
        return colorList.ConvertAll(Color32AsByteList);
    }


    private static List<byte> Color32AsByteList(Color32 input)
    {
        return new List<byte> {
            input.r,
            input.g,
            input.b,
            input.a
        };
    }

    private static string ListAsString(List<object> list)
    {
        return string.Join(DELIMITER, list);
    }
    private static string DELIMITER = ",";
    public int heightScreen;
    public int widthScreen;

    public int NoOfRacesWon { get => noOfRacesWon; set => noOfRacesWon = value; }
    public int NoOfRacesLose { get => noOfRacesLose; set => noOfRacesLose = value; }
    public int DistanceTravelled { get => distanceTravelled; set => distanceTravelled = value; }
    public int CarTotalled { get => carTotalled; set => carTotalled = value; }
    public int NoOfCarsWon { get => noOfCarsWon; set => noOfCarsWon = value; }
    public int AmountOfMoneyWon { get => amountOfMoneyWon; set => amountOfMoneyWon = value; }
    public int CarbattleWon { get => carbattleWon; set => carbattleWon = value; }
    public int CarEscapeLevel { get => carEscapeLevel; set => carEscapeLevel = value; }

    public void SaveDataFromPlayGames(PlayerCompleteData playerSaveFile)
    {
        Debug.Log("Reading Play Games Data.");
        loadScene.GoToFirst();
        playerEngine = playerSaveFile.Engines;
        playerTires = playerSaveFile.Tires;
        playerNitros = playerSaveFile.Nitros;
        playerBody = playerSaveFile.Body;
        currentSelected = playerSaveFile.CurrentSelected;
        playerBodyColor = playerSaveFile.BodyColor;
        playerTireColor = playerSaveFile.TireColor;
        reputation = playerSaveFile.Reputation;
        kingDefeated = playerSaveFile.KingDefeated;
        playerCoins = playerSaveFile.Coins;
        playerStats = playerSaveFile.PlayerStats;
        trackTimeCompleted = playerSaveFile.TrackTimeCompleted;
        playerName = playerSaveFile.PlayerName;
        storyIndex = playerSaveFile.StoryIndex;
        playerGems = playerSaveFile.PlayerGems;
        kingPass = playerSaveFile.KingPass;
        eventPass = playerSaveFile.EventPass;
        dailyReward = playerSaveFile.DailyReward;
        storyTold = playerSaveFile.StoryTold;
        playerPlanetName = playerSaveFile.PlayerPlanetName;
        gameVolume = playerSaveFile.Gamevolume;
        playerCharIcon = playerSaveFile.PlayerCharIcon;
        gameVolumeEngine = playerSaveFile.GameVolumeEngine;
        gameVolumeUI = playerSaveFile.GameVolumeUI;
        gameVolumeMusic = playerSaveFile.GameVolumeMusic;
        graphicSetting = playerSaveFile.GraphicSetting;
        uniqueId = uniqueId;
        playerAchievements = playerSaveFile.PlayerAchievement;
        currentPlanet = playerSaveFile.CurrentPlanet;

        showFps = playerSaveFile.ShowFps;
        currentEscapeLevel = playerSaveFile.CurrentEscapeLevel;
        currentColosseumLevel = playerSaveFile.CurrentColosseumLevel;

        kingLevels = playerSaveFile.KingLevels;

        PG_currentId = playerSaveFile.PG_currentId1;
        musicEnabled = playerSaveFile.MusicEnabled;
        languageIndex = playerSaveFile.LanguageIndex;
        renderRes = playerSaveFile.RenderRes;
        frameRate = playerSaveFile.FrameRate;
        controlMode = playerSaveFile.ControlMode;

        carStructure = playerSaveFile.CarStructure;//upto lvl 50, 4 index
        currentSelectedCards = playerSaveFile.CurrentSelectedCards; //4 index
        cardsInventory = playerSaveFile.CardsInventory;
        accessories = playerSaveFile.Accessories;
        accessoriesInventory = playerSaveFile.AccessoriesInventory;

        AuthGB.SetActive(true);
        accountAlreadyExits.SetTrigger("on");

        newAccount.onClick.AddListener(NewGame);
        existingAccount.onClick.AddListener(LoadExisting);

        existingAccountText.text = "Name: "+playerSaveFile.PlayerName + "\n\nReputation: " + playerSaveFile.Reputation[reputationLevel] + "\n\n Coins: " + playerSaveFile.Coins;



    }

    public void LoadExisting()
    {
        SavePlayGamesData();
        loadScene.GoToMain();
        accountAlreadyExits.SetTrigger("off");
    }
    public void NewGame()
    {
        loadScene.StartGame();
        accountAlreadyExits.SetTrigger("off");
    }
    public void First()
    {
        loadScene.GoToFirst();
        
        accountAlreadyExits.SetTrigger("off");
    }
    private void SavePlayGamesData()
    {
        QuickSaveWriter saveWriter = QuickSaveWriter.Create("PlayerData", settings);
        saveWriter.Write("PlayerEngine", playerEngine);
        saveWriter.Write("PlayerTires", playerTires);
        saveWriter.Write("PlayerNitros", playerNitros);
        saveWriter.Write("PlayerBody", playerBody);
        saveWriter.Write("PlayerBodyColor", playerBodyColor);
        saveWriter.Write("PlayerTireColor", playerTireColor);
        saveWriter.Write("Coins", playerCoins);
        saveWriter.Write("Reputation", reputation);
        saveWriter.Write("CurrentSelected", currentSelected);
        saveWriter.Write("UniqueId", uniqueId);
        saveWriter.Write("KingDefeated", kingDefeated);

        saveWriter.Write("PlayerName", playerName);
        saveWriter.Write("StoryIndex", storyIndex);
        saveWriter.Write("StoryTold", storyTold);
        saveWriter.Write("PlayerPlanetName", playerPlanetName);
        saveWriter.Write("PlayerCharacterIcon", playerCharIcon);
        saveWriter.Write("GameVolumeEngine", gameVolumeEngine);
        saveWriter.Write("GameVolumeUI", gameVolumeUI);
        saveWriter.Write("GameVolumeMusic", gameVolumeMusic);
        saveWriter.Write("GameVolumeMaster", gameVolume);
        saveWriter.Write("GraphicSetting", graphicSetting);
        saveWriter.Write("PlayerStatistics", playerStats);
        saveWriter.Write("CurrentPlanet", currentPlanet);

        saveWriter.Write("Gems", playerGems);
        saveWriter.Write("KingPass", kingPass);
        saveWriter.Write("EventPass", eventPass);
        saveWriter.Write("DailyRewardIndex", dailyReward);
        saveWriter.Write("TrackTimeCompleted", trackTimeCompleted);
        saveWriter.Write("PlayerAchievements", playerAchievements);

        saveWriter.Write("ShowFps", showFps);
        saveWriter.Write("CurrentEscapeLevel", currentEscapeLevel);
        saveWriter.Write("CurrentColosseumLevel", currentColosseumLevel);
        saveWriter.Write("KingLevels", kingLevels);
        saveWriter.Write("PG_currentId", PG_currentId);
        saveWriter.Write("MusicEnabled", musicEnabled);
        saveWriter.Write("LanguageIndex", languageIndex);
        saveWriter.Write("RenderRes", renderRes);
        saveWriter.Write("FrameRate", frameRate);
        saveWriter.Write("ControlMode", controlMode);
        saveWriter.Write("CarStructure", carStructure);
        saveWriter.Write("CardsInventory", cardsInventory);
        saveWriter.Write("CurrentSelectedCards", currentSelectedCards);
        saveWriter.Write("Accessories", accessories);
        saveWriter.Write("AccessoriesInventory", accessoriesInventory);


        bool commit = saveWriter.TryCommit(); Debug.Log("SavedData " + commit);
        loadScene.GoToMain();
    }
    public class PlayerCompleteData
    {
        List<List<int>> engines;
        List<List<int>> tires;
        List<List<int>> nitros;
        List<List<float>> body;
        List<List<Color32>> bodyColor;
        List<Color32> tireColor;
        int coins;
        List<int> reputation;
        List<int> currentSelected;
        List<int> kingDefeated;
        string uniqueId;
        private string playerName;
        int storyIndex;
        bool storyTold;
        string playerPlanetName;
        int playerCharIcon;
        int graphicSetting;
        int gamevolume;
        int currentPlanet;
        List<float> playerStats;

        int playerGems;
        int kingPass;
        int eventPass;
        int dailyReward ;
        List<float> trackTimeCompleted;

        int gameVolumeEngine;
        int gameVolumeUI;
        int gameVolumeMusic;
        List<bool> playerAchievement;

        bool showFps;
        int currentEscapeLevel;
        int currentColosseumLevel;

        List<int> kingLevels;

        string PG_currentId;
        bool musicEnabled;
        int languageIndex;
        int renderRes;
        int frameRate;
        int controlMode;

        List<int> carStructure;//upto lvl 50, 4 index
        List<List<int>> currentSelectedCards; //4 index
        List<int> cardsInventory;
        List<int> accessories;
        List<int> accessoriesInventory;



        public PlayerCompleteData(List<List<int>> engines, List<List<int>> tires, List<List<int>> nitros, List<List<float>> body, List<List<Color32>> bodyColor, List<Color32> tireColor, int coins, List<int> reputation, List<int> currentSelected, List<int> kingDefeated, string uniqueId, string playerName, int storyIndex, bool storyTold, string playerPlanetName, int playerCharIcon, int graphicSetting, int gamevolume, List<float> playerStats, int currentPlanet, int playerGems, int kingPass, int eventPass, int dailyReward, List<float> trackTimeCompleted, int gameVolumeEngine, int gameVolumeUI, int gameVolumeMusic, List<bool> playerAchievement, bool showFps, int currentEscapeLevel, int currentColosseumLevel, List<int> kingLevels, string pG_currentId, bool musicEnabled, int languageIndex, int renderRes, int frameRate, int controlMode, List<int> carStructure, List<List<int>> currentSelectedCards, List<int> cardsInventory, List<int> accessories, List<int> accessoriesInventory)
        {
            this.engines = engines;
            this.tires = tires;
            this.nitros = nitros;
            this.body = body;
            this.bodyColor = bodyColor;
            this.tireColor = tireColor;
            this.coins = coins;
            this.reputation = reputation;
            this.currentSelected = currentSelected;
            this.kingDefeated = kingDefeated;
            this.uniqueId = uniqueId;
            this.playerName = playerName;
            this.storyIndex = storyIndex;
            this.storyTold = storyTold;
            this.playerPlanetName = playerPlanetName;
            this.playerCharIcon = playerCharIcon;
            this.graphicSetting = graphicSetting;
            this.gamevolume = gamevolume;
            this.playerStats = playerStats;
            this.currentPlanet = currentPlanet;
            this.playerGems = playerGems;
            this.kingPass = kingPass;
            this.eventPass = eventPass;
            this.dailyReward = dailyReward;
            this.trackTimeCompleted = trackTimeCompleted;
            this.gameVolumeEngine = gameVolumeEngine;
            this.gameVolumeUI = gameVolumeUI;
            this.gameVolumeMusic = gameVolumeMusic;
            this.playerAchievement = playerAchievement;
            this.showFps = showFps;
            this.currentEscapeLevel = currentEscapeLevel;
            this.currentColosseumLevel = currentColosseumLevel;
            this.kingLevels = kingLevels;
            PG_currentId = pG_currentId;
            this.musicEnabled = musicEnabled;
            this.languageIndex = languageIndex;
            this.renderRes = renderRes;
            this.frameRate = frameRate;
            this.controlMode = controlMode;
            this.carStructure = carStructure;
            this.currentSelectedCards = currentSelectedCards;
            this.cardsInventory = cardsInventory;
            this.accessories = accessories;
            this.accessoriesInventory = accessoriesInventory;
        }

        public List<List<int>> Engines { get => engines; set => engines = value; }
        public List<List<int>> Tires { get => tires; set => tires = value; }
        public List<List<int>> Nitros { get => nitros; set => nitros = value; }
        public List<List<float>> Body { get => body; set => body = value; }
        public List<List<Color32>> BodyColor { get => bodyColor; set => bodyColor = value; }
        public List<Color32> TireColor { get => tireColor; set => tireColor = value; }
        public int Coins { get => coins; set => coins = value; }
        public List<int> Reputation { get => reputation; set => reputation = value; }
        public List<int> CurrentSelected { get => currentSelected; set => currentSelected = value; }
        public List<int> KingDefeated { get => kingDefeated; set => kingDefeated = value; }
        public string UniqueId { get => uniqueId; set => uniqueId = value; }
        public string PlayerName { get => playerName; set => playerName = value; }
        public int StoryIndex { get => storyIndex; set => storyIndex = value; }
        public bool StoryTold { get => storyTold; set => storyTold = value; }
        public string PlayerPlanetName { get => playerPlanetName; set => playerPlanetName = value; }
        public int PlayerCharIcon { get => playerCharIcon; set => playerCharIcon = value; }
        public int GraphicSetting { get => graphicSetting; set => graphicSetting = value; }
        public int Gamevolume { get => gamevolume; set => gamevolume = value; }
        public List<float> PlayerStats { get => playerStats; set => playerStats = value; }
        public int CurrentPlanet { get => currentPlanet; set => currentPlanet = value; }
        public int PlayerGems { get => playerGems; set => playerGems = value; }
        public int KingPass { get => kingPass; set => kingPass = value; }
        public int EventPass { get => eventPass; set => eventPass = value; }
        public int DailyReward { get => dailyReward; set => dailyReward = value; }
        public List<float> TrackTimeCompleted { get => trackTimeCompleted; set => trackTimeCompleted = value; }
        public int GameVolumeEngine { get => gameVolumeEngine; set => gameVolumeEngine = value; }
        public int GameVolumeUI { get => gameVolumeUI; set => gameVolumeUI = value; }
        public int GameVolumeMusic { get => gameVolumeMusic; set => gameVolumeMusic = value; }
        public List<bool> PlayerAchievement { get => playerAchievement; set => playerAchievement = value; }
        public bool ShowFps { get => showFps; set => showFps = value; }
        public int CurrentEscapeLevel { get => currentEscapeLevel; set => currentEscapeLevel = value; }
        public int CurrentColosseumLevel { get => currentColosseumLevel; set => currentColosseumLevel = value; }
        public List<int> KingLevels { get => kingLevels; set => kingLevels = value; }
        public string PG_currentId1 { get => PG_currentId; set => PG_currentId = value; }
        public bool MusicEnabled { get => musicEnabled; set => musicEnabled = value; }
        public int LanguageIndex { get => languageIndex; set => languageIndex = value; }
        public int RenderRes { get => renderRes; set => renderRes = value; }
        public int FrameRate { get => frameRate; set => frameRate = value; }
        public int ControlMode { get => controlMode; set => controlMode = value; }
        public List<int> CarStructure { get => carStructure; set => carStructure = value; }
        public List<List<int>> CurrentSelectedCards { get => currentSelectedCards; set => currentSelectedCards = value; }
        public List<int> CardsInventory { get => cardsInventory; set => cardsInventory = value; }
        public List<int> Accessories { get => accessories; set => accessories = value; }
        public List<int> AccessoriesInventory { get => accessoriesInventory; set => accessoriesInventory = value; }
    }

}
