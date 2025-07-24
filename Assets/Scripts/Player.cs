using Assets.Scripts.Models;
using Assets.Scripts.Utils;
using CI.QuickSave;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    userData UserData;
    /*    [SerializeField] private List<List<int>> playerEngine;*/
    readonly int engineIndex = 0;
    readonly int engineHealthIndex = 1;
    /*    [SerializeField] private List<List<int>> playerTires;*/
    readonly int tireIndex = 0;
    readonly int tireHealthIndex = 1;
    /*    [SerializeField] private List<List<int>> playerNitros;*/
    readonly int nitrosIndex = 0;
    readonly int nitrosHealthIndex = 1;

    /*    [SerializeField] private List<List<float>> playerBody; */
    readonly int bodyIndex = 0;
    readonly int bodyHealthIndex = 1;
    readonly int spoilerIndex = 2;
    readonly int vinylIndex = 3;
    readonly int bodyModIndex = 4;
    readonly int bodyMetallic = 5;
    readonly int bodySmoothness = 6;
    readonly int vinylMetallic = 7;
    readonly int vinylSmoothness = 8;

    /*    [SerializeField] private List<List<Color32>> playerBodyColor;*/
    readonly int bodyColor = 0;
    readonly int vinylColor = 1;
    readonly int glassColor = 2;
    readonly int spoilerColor = 3;
    readonly int interiorColor = 4;
    /*
        [SerializeField] private List<Color32> playerTireColor;
        [SerializeField] private int playerCoins;
        [SerializeField] private List<int> reputation;*/
    readonly int reputationLevel = 0;
    readonly int reputationExperience = 1;

    /*    [SerializeField] private List<int> currentSelected;*/
    readonly int engine = 0;
    readonly int tire = 1;
    readonly int nitros = 2;
    readonly int body = 3;
/*    [SerializeField] private List<int> kingDefeated;*/

    [SerializeField] private string uniqueId ;
    [SerializeField] private Vector3 carPosition;

/*    private string playerName;
    int storyIndex;
    bool storyTold;
    string playerPlanetName;
    int playerCharIcon;

    int graphicSetting;
    int gameVolume;

    [SerializeField] private List<float> playerStats;
    int noOfRacesWon = 0; int noOfRacesLose = 1; int distanceTravelled = 2;
    int carTotalled = 3; int noOfCarsWon = 4; int amountOfMoneyWon = 5; */

    private GameObject currentcar;

    private CarObjects objects;
    private MeshRenderer carMeshRender;
    private MeshRenderer carSpoilRender;
    private MeshFilter carMeshFilter;
    private MeshFilter spoilerMeshFilter;

    private readonly int carMeshRenderBody = 0;
    private readonly int carMeshRenderVinyl = 1;
    private readonly int carMeshRenderGlass = 2;

    private CarData carData ;
    private UIDataMain uIData;
    private ColorTheme carPaint;
    private Car playerCarData;
    readonly QuickSaveSettings settings = new QuickSaveSettings();

    [SerializeField] private Slider playerTopSpeed;
    [SerializeField] private Slider playerMaxTorque;
    [SerializeField] private Slider playerCarHandling;
    void Start()
    {
/*        uniqueId = SystemInfo.deviceUniqueIdentifier;
        settings.SecurityMode = SecurityMode.Aes;
        settings.Password = "adZ5HsQEppwFF*EjiTw^yN#8oaSikA%v";
        settings.CompressionMode = CompressionMode.Gzip;*/
        carData = GetComponent<CarData>();
        uIData = GetComponent<UIDataMain>();
        UserData = GameObject.Find("/UserData").GetComponent<userData>();

        StartCoroutine(Loading());

        
    }

    void UpdateUI()
    {
        UserCurrentCarStats();
        uIData.playerCoinsText.text = PRUtils.CurrencyFormater( UserData.playerCoins.ToString() );
        uIData.playerGemsText.text = UserData.playerGems.ToString();
        uIData.playerNameText.text = UserData.playerName;
        try { uIData.playerPlayGamesName.text = "("+Social.localUser.userName+")"; } 
        catch(Exception error) { Debug.Log("Error in reteriving player name from play games: "+error); }

        playerTopSpeed.value = (float)playerCarData.CarTopSpeed/300f;
        playerMaxTorque.value = (float)playerCarData.CarFullTorque / 20000f;
        playerCarHandling.value = ((float)playerCarData.CarSteerHelper) / 1f;
        uIData.playerReputationText.text = "Rep."+ UserData.reputation[reputationLevel].ToString();
        float scaleValue;
        float value;
        if (UserData.reputation[reputationLevel] == 1)
        {
            scaleValue = ReputationLevelScaling(UserData.reputation[reputationLevel]);
            value = UserData.reputation[reputationExperience] / scaleValue;
        }
        else
        {
            scaleValue = ReputationLevelScaling(UserData.reputation[reputationLevel]) -
    ReputationLevelScaling(UserData.reputation[reputationLevel] - 1);
            value = (UserData.reputation[reputationExperience] - ReputationLevelScaling(UserData.reputation[reputationLevel] - 1)) / scaleValue;
        }
        uIData.playerReputationSlider.value = value;
    }

    int ReputationLevelScaling(int lvl)
    {
        int Lvl = 10 + 10 * (lvl - 1) + 25 * (lvl - 1);

        return Lvl;
    }
/*    public void SetValues()
    {
        playerEngine = UserData.playerEngine;
        playerTires = UserData.playerTires;
        playerNitros = UserData.playerNitros;
        playerBody = UserData.playerBody;
        currentSelected = UserData.currentSelected;
        playerBodyColor = UserData.playerBodyColor;
        playerTireColor = UserData.playerTireColor;
        reputation = UserData.reputation;
        kingDefeated = UserData.kingDefeated;
        playerCoins = UserData.playerCoins;

        playerName = UserData.playerName;
        storyIndex = UserData.storyIndex;
        storyTold = UserData.storyTold;
        playerPlanetName = UserData.playerPlanetName;
        gameVolume = UserData.gameVolume;
        playerCharIcon = UserData.playerCharIcon;
    }*/
    IEnumerator Loading()
    {
/*        try
        {*/
/*            ReadUserData();*/
            
            CreateCar();
        UserCurrentCarStats();
            if (ReputationLevelScaling(UserData.reputation[reputationLevel]) < UserData.reputation[reputationExperience])
            {
                UserData.reputation[reputationLevel]++;
                UserData.storyIndex++;
                UserData.storyTold = false;
            }
            UpdateUI();
/*        }
        catch (System.Exception Error)
        {
            Debug.Log(Error);
            List<float> listOfInt = new List<float>() { 0, 100, -1, -1, 0, 0, .5f, 0, .5f};
            List<int> listOfIntC = new List<int>() { 0, 100};
            List<int> listOfIntR = new List<int>() { 1, 0 };
            List<Color32> listOfColor = new List<Color32>() {Color.red, Color.white, new Color(0,0,0,.7f), Color.black , Color.grey  };

            List<List<int>> engine = new List<List<int>>() { listOfIntC };
            List<List<int>> tire = new List<List<int>> { listOfIntC };
            List<List<int>> nitros = new List<List<int>>() { listOfIntC };
            List<List<float>> body = new List<List<float>>() { listOfInt };
            List<int> selected = new List<int>() { 0, 0, 0, 0 };
            List<List<Color32>> bodyColors = new List<List<Color32>>() { listOfColor };
            List<Color32> tireColors = new List<Color32>() { Color.white };
            List<int> reputation = listOfIntR;
            List<int >KingDefeated = new List<int>() { 0,0,0,0,0,0,0,0,0,0,0,0,0 };

            WriteUserData(engine,tire,nitros,body, bodyColors, tireColors,10000000
                ,reputation, selected,KingDefeated);
*//*            WritePlayerName("Abhay Raj");*//*
            ReadUserData();
            CreateCar();
            UpdateUI();
        }*/
        yield return new WaitForSeconds(0.5f);
    }

/*    private void WritePlayerName(string name)
    {
        QuickSaveWriter saveWriter = QuickSaveWriter.Create("PlayerData", settings);
        saveWriter.Write("PlayerName", name);
    }*/

/*    private void WriteUserData(List<List<int>> playerEngine , List<List<int>>
    playerTires , List<List<int>> playerNitros , List<List<float>> playerBody ,
    List<List<Color32>> playerBodyColor , List<Color32> playerTireColor ,
    int playerCoins , List<int> reputation , List<int> currentSelected, List<int> kingDefeated)
    {
        QuickSaveWriter saveWriter =  QuickSaveWriter.Create("PlayerData", settings);
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


        bool commit = saveWriter.TryCommit(); Debug.Log("SavedData "+commit);
    }*/
     
/*    private void ReadUserData()
    {
        QuickSaveReader saveReader = QuickSaveReader.Create("PlayerData", settings);
        playerEngine = saveReader.Read<List<List<int>>>("PlayerEngine");
        playerTires = saveReader.Read<List<List<int>>>("PlayerTires");
        playerNitros = saveReader.Read<List<List<int>>>("PlayerNitros");
        playerBody = saveReader.Read<List<List<float>>>("PlayerBody");
        playerBodyColor = saveReader.Read<List<List<Color32>>>("PlayerBodyColor");
        playerTireColor = saveReader.Read<List<Color32>>("PlayerTireColor");
        playerCoins = saveReader.Read<int>("Coins");
        reputation = saveReader.Read<List<int>> ("Reputation");
        currentSelected = saveReader.Read<List<int>>("CurrentSelected");
        uniqueId = saveReader.Read<string>("UniqueId");
        kingDefeated = saveReader.Read<List<int>>("KingDefeated");

    }*/


    private void SetWheels(Color32 wheelColor, Mesh wheelMesh)
    {
        for (int i = 0; i < 4; i++)
        {
            MeshRenderer wheelMeshRender = objects.wheel[i].GetComponent<MeshRenderer>();
            wheelMeshRender.material.color = wheelColor;

            MeshFilter wheelMeshFilter = objects.wheel[i].GetComponent<MeshFilter>();
            wheelMeshFilter.mesh = wheelMesh;
        }
    }
    int carRating;
    private void UserCurrentCarStats()
    {
        carRating = 0;
        int bodyindex = (int)UserData.playerBody[UserData.currentSelected[this.body]][bodyIndex];
        int engineindex = UserData.playerEngine[UserData.currentSelected[this.engine]][engineIndex];
        int tireindex = UserData.playerTires[UserData.currentSelected[tire]][tireIndex];
        int nitrosindex = UserData.playerNitros[UserData.currentSelected[nitros]][nitrosIndex];

        carRating += Constants.bodyData[bodyindex].Rating;
        carRating += Constants.enginesData[engineindex].Rating;
        carRating += Constants.tiresData[tireindex].Rating;
        carRating += Constants.nitrosData[nitrosindex].Rating;

        uIData.playerRatingText.text = (carRating+UserData.reputation[reputationExperience]).ToString();
        int CarBodyHealth = Constants.bodyData[bodyindex].CarPartHealth;
        int CarEngineHealth = Constants.enginesData[engineindex].CarPartHealth;
        int CarTireHealth = Constants.tiresData[tireindex].CarPartHealth;
        int CarNitroHealth = Constants.nitrosData[nitrosindex].CarPartHealth;

        decimal BodyHealth = decimal.Divide(
            (decimal)(UserData.playerBody[UserData.currentSelected[this.body]][bodyHealthIndex] * CarBodyHealth),
            100);
        decimal EngineHealth = decimal.Divide(
            (decimal)(UserData.playerEngine[UserData.currentSelected[this.engine]][engineHealthIndex] * CarEngineHealth),
            100);
        decimal TireHealth = decimal.Divide(
            (decimal)(UserData.playerTires[UserData.currentSelected[tire]][tireHealthIndex] * CarTireHealth),
            100);
        decimal NitroHealth = decimal.Divide(
            (decimal)(UserData.playerNitros[UserData.currentSelected[nitros]][nitrosHealthIndex] * CarNitroHealth),
            100);

        int SpoilerIndex = (int)UserData.playerBody[UserData.currentSelected[this.body]][spoilerIndex];
        int BodySkitIndex = (int)UserData.playerBody[UserData.currentSelected[this.body]][bodyModIndex];
        int VinylIndex = (int)UserData.playerBody[UserData.currentSelected[this.body]][vinylIndex];

        int carMass = Constants.enginesData[engineindex].CarPartMass
            + Constants.bodyData[bodyindex].CarPartMass
            + Constants.tiresData[tireindex].CarPartMass
            + Constants.nitrosData[nitrosindex].CarPartMass;

        int carTopSpeed = Constants.enginesData[engineindex].CarPartTopSpeed
            + Constants.bodyData[bodyindex].CarPartTopSpeed
            + Constants.tiresData[tireindex].CarPartTopSpeed;

        int carFullTorque = Constants.enginesData[engineindex].CarPartAcceleration
            + Constants.bodyData[bodyindex].CarPartAcceleration
            + Constants.tiresData[tireindex].CarPartAcceleration;

        float carSteerHelper = Constants.tiresData[tireindex].CarPartSteerHelper;
        float carTractionControl = Constants.tiresData[tireindex].CarPartTractionControl;
        int carDownForce = Constants.bodyData[bodyindex].CarPartDownForce;
        int carBreakTorque = Constants.enginesData[engineindex].CarPartBreakTorque
            + Constants.tiresData[tireindex].CarPartBreakTorque;

        decimal engine = decimal.Divide
    (EngineHealth, CarEngineHealth);
        decimal body = decimal.Divide
            (BodyHealth, CarBodyHealth);
        decimal eb = decimal.Divide(engine + body, 2);
        int topSpeed = (int)((eb * carTopSpeed) + carTopSpeed) / 2;
        int fullTorque = (int)((eb * carFullTorque) + carFullTorque) / 2;

        Color BodyColor = UserData.playerBodyColor[UserData.currentSelected[this.body]][bodyColor];
        Color VinylColor = UserData.playerBodyColor[UserData.currentSelected[this.body]][vinylColor];
        Color GlassColor = UserData.playerBodyColor[UserData.currentSelected[this.body]][glassColor];
        Color TireColor = UserData.playerBodyColor[UserData.currentSelected[this.body]][tireIndex];
        Color InteriorColor = UserData.playerBodyColor[UserData.currentSelected[this.body]][interiorColor];
        Color SpoilerColor = UserData.playerBodyColor[UserData.currentSelected[this.body]][spoilerColor];
        List<float> ColorTypeBody = new List<float>() {UserData.playerBody[UserData.currentSelected[this.body]][bodyMetallic],
        UserData.playerBody[UserData.currentSelected[this.body]][bodySmoothness]};
        List<float> ColorTypeVinyl = new List<float>() {UserData.playerBody[UserData.currentSelected[this.body]][vinylMetallic],
        UserData.playerBody[UserData.currentSelected[this.body]][vinylSmoothness]};

        carPaint = new ColorTheme(BodyColor, VinylColor, GlassColor, TireColor, InteriorColor, SpoilerColor, ColorTypeBody, ColorTypeVinyl);

        playerCarData = new Car(engineindex, bodyindex, tireindex, nitrosindex, carPaint, SpoilerIndex, BodySkitIndex, VinylIndex, (int)EngineHealth,
            (int)BodyHealth, (int)TireHealth, (int)NitroHealth, CarEngineHealth, CarBodyHealth, CarTireHealth, CarNitroHealth, carMass, carTopSpeed, carFullTorque
            , carSteerHelper, carTractionControl, carDownForce, carBreakTorque, topSpeed, fullTorque);


    }
    private void CreateCar()
    {
        //if(currentcar==null){currentcar.GetComponent<CarObjects>().destroyObject();}

        currentcar = Instantiate(carData.carPrefabs[(int)UserData.playerBody[UserData.currentSelected[body]][bodyIndex]]);
        currentcar.transform.position = carPosition;
        currentcar.SetActive(true);
        objects = currentcar.GetComponent<CarObjects>();
        carMeshRender = objects.carBody.GetComponent<MeshRenderer>();
        carSpoilRender = objects.spoiler.GetComponent<MeshRenderer>();
        carMeshFilter = objects.carBody.GetComponent<MeshFilter>();
        spoilerMeshFilter = objects.spoiler.GetComponent<MeshFilter>();

        Color carBodyColor = UserData.playerBodyColor[UserData.currentSelected[body]][bodyColor];
        Color carVinylColor = UserData.playerBodyColor[UserData.currentSelected[body]][vinylColor];
        Color carGlassColor = UserData.playerBodyColor[UserData.currentSelected[body]][glassColor];

        carMeshRender.materials[carMeshRenderBody].color = carBodyColor;
        carMeshRender.materials[carMeshRenderVinyl].color = carVinylColor;
        carMeshRender.materials[carMeshRenderGlass].color = carGlassColor;
        carMeshRender.materials[carMeshRenderBody].SetFloat("_Metallic", UserData.playerBody[UserData.currentSelected[body]][bodyMetallic]);
        carMeshRender.materials[carMeshRenderBody].SetFloat("_Smoothness", UserData.playerBody[UserData.currentSelected[body]][bodySmoothness]);
        carMeshRender.materials[carMeshRenderVinyl].SetFloat("_Metallic", UserData.playerBody[UserData.currentSelected[body]][vinylMetallic]);
        carMeshRender.materials[carMeshRenderVinyl].SetFloat("_Smoothness", UserData.playerBody[UserData.currentSelected[body]][vinylSmoothness]);
        objects.interior.material.color = UserData.playerBodyColor[UserData.currentSelected[body]][interiorColor];
        carSpoilRender.material.color = UserData.playerBodyColor[UserData.currentSelected[body]][spoilerColor];
        for (int i = 0; i < 4; i++)
        {
            MeshRenderer wheelMeshRender = objects.wheel[i].GetComponent<MeshRenderer>();
            wheelMeshRender.material.color = UserData.playerTireColor[UserData.currentSelected[tire]];

            MeshFilter wheelMeshFilter = objects.wheel[i].GetComponent<MeshFilter>();
            wheelMeshFilter.mesh = carData.carTireMesh[UserData.playerTires[UserData.currentSelected[tire]][tireIndex]];
        }
        int carVinylIndex = (int)UserData.playerBody[UserData.currentSelected[body]][vinylIndex];
        int carBodyTexture = (5 * (int)UserData.playerBody[UserData.currentSelected[body]][bodyIndex]);

        if (carVinylIndex == -1)
        { 
            carVinylIndex = 55;
            carMeshRender.materials[carMeshRenderVinyl].mainTexture = carData.carVinylTexture[carVinylIndex ];
        }
        else carMeshRender.materials[carMeshRenderVinyl].mainTexture = carData.carVinylTexture[carVinylIndex + carBodyTexture];


        carMeshFilter.mesh = carData.carBFMesh[(int)UserData.playerBody[UserData.currentSelected[body]][bodyModIndex] + (3 * (int)UserData.playerBody[UserData.currentSelected[body]][bodyIndex])];
        if (UserData.playerBody[UserData.currentSelected[body]][spoilerIndex] == -1) carSpoilRender.enabled = false;
        else spoilerMeshFilter.mesh = carData.carSpoilerMesh[(int)UserData.playerBody[UserData.currentSelected[body]][spoilerIndex]];

        SetWheels(UserData.playerTireColor[UserData.currentSelected[tire]], carData.carTireMesh[UserData.playerTires[UserData.currentSelected[tire]][tireIndex]]);

    }
    
    public void PartColorChanger(CarPIcker.CarPart carPart, Color partColor , List<float> colorType) 
    {
        if (CarPIcker.CarPart.Body == carPart)
        {
            carMeshRender.materials[carMeshRenderBody].color = partColor;
            carMeshRender.materials[carMeshRenderBody].SetFloat("_Metallic", colorType[0]);
            carMeshRender.materials[carMeshRenderBody].SetFloat("_Smoothness", colorType[1]);
        }
        if (CarPIcker.CarPart.Vinyl == carPart)
        {
            carMeshRender.materials[carMeshRenderVinyl].color = partColor;
            carMeshRender.materials[carMeshRenderVinyl].SetFloat("_Metallic", colorType[0]);
            carMeshRender.materials[carMeshRenderVinyl].SetFloat("_Smoothness", colorType[1]);
        }
        if (CarPIcker.CarPart.Tire == carPart)
        {SetWheels(partColor, carData.carTireMesh[UserData.playerTires[UserData.currentSelected[tire]][tireIndex]]);}
        if (CarPIcker.CarPart.Glass == carPart)
        {
            partColor.a = 0.7f;
            carMeshRender.materials[carMeshRenderGlass].color = partColor;
        }

        if (CarPIcker.CarPart.Spoiler == carPart) carSpoilRender.material.color = partColor;
        if (CarPIcker.CarPart.Interior == carPart) objects.interior.material.color = partColor;

    }

    public void CarReset()
    {
        int carVinylIndex = (int)UserData.playerBody[UserData.currentSelected[body]][vinylIndex];
        int carBodyTexture = (5 * (int)UserData.playerBody[UserData.currentSelected[body]][bodyIndex]);

        if (carVinylIndex == -1)
        {
            carVinylIndex = 55;
            carMeshRender.materials[carMeshRenderVinyl].mainTexture = carData.carVinylTexture[carVinylIndex];
        }
        else carMeshRender.materials[carMeshRenderVinyl].mainTexture = carData.carVinylTexture[carVinylIndex + carBodyTexture];
        carMeshFilter.mesh = carData.carBFMesh[(int)UserData.playerBody[UserData.currentSelected[body]][bodyModIndex] + (3 * (int)UserData.playerBody[UserData.currentSelected[body]][bodyIndex])];
        if (UserData.playerBody[UserData.currentSelected[body]][spoilerIndex] == -1) carSpoilRender.enabled = false;
        else spoilerMeshFilter.mesh = carData.carSpoilerMesh[(int)UserData.playerBody[UserData.currentSelected[body]][spoilerIndex]];
        carMeshRender.materials[carMeshRenderBody].color = UserData.playerBodyColor[UserData.currentSelected[body]][bodyColor];
        carMeshRender.materials[carMeshRenderVinyl].color = UserData.playerBodyColor[UserData.currentSelected[body]][vinylColor];
        carMeshRender.materials[carMeshRenderGlass].color = UserData.playerBodyColor[UserData.currentSelected[body]][glassColor];
        carMeshRender.materials[carMeshRenderBody].SetFloat("_Metallic", UserData.playerBody[UserData.currentSelected[body]][bodyMetallic]);
        carMeshRender.materials[carMeshRenderBody].SetFloat("_Smoothness", UserData.playerBody[UserData.currentSelected[body]][bodySmoothness]);
        carMeshRender.materials[carMeshRenderVinyl].SetFloat("_Metallic", UserData.playerBody[UserData.currentSelected[body]][vinylMetallic]);
        carMeshRender.materials[carMeshRenderVinyl].SetFloat("_Smoothness", UserData.playerBody[UserData.currentSelected[body]][vinylSmoothness]);
        objects.interior.material.color = UserData.playerBodyColor[UserData.currentSelected[body]][interiorColor];
        carSpoilRender.material.color = UserData.playerBodyColor[UserData.currentSelected[body]][spoilerColor];
        for (int i = 0; i < 4; i++)
        {
            MeshRenderer wheelMeshRender = objects.wheel[i].GetComponent<MeshRenderer>();
            wheelMeshRender.material.color = UserData.playerTireColor[UserData.currentSelected[tire]];

            MeshFilter wheelMeshFilter = objects.wheel[i].GetComponent<MeshFilter>();
            wheelMeshFilter.mesh = carData.carTireMesh[UserData.playerTires[UserData.currentSelected[tire]][tireIndex]];
        }
    }

    public void PurchaseColor(int cost, CarPIcker.CarPart carPart, Color partColor, List<float> colorType)
    {
        UserData.playerCoins -= cost;
        if (CarPIcker.CarPart.Body == carPart)
        {
            UserData.playerBodyColor[UserData.currentSelected[body]][bodyColor] = partColor;
            UserData.playerBody[UserData.currentSelected[body]][bodyMetallic] = colorType[0];
            UserData.playerBody[UserData.currentSelected[body]][bodySmoothness] = colorType[1];
        }
        if (CarPIcker.CarPart.Vinyl == carPart)
        {
            UserData.playerBodyColor[UserData.currentSelected[body]][vinylColor] = partColor;
            UserData.playerBody[UserData.currentSelected[body]][vinylMetallic] = colorType[0];
            UserData.playerBody[UserData.currentSelected[body]][vinylSmoothness] = colorType[1];
        }
        if (CarPIcker.CarPart.Tire == carPart)
        { UserData.playerTireColor[UserData.currentSelected[tire]] = partColor; }
        if (CarPIcker.CarPart.Glass == carPart)
        {
            partColor.a = 0.7f;
            UserData.playerBodyColor[UserData.currentSelected[body]][glassColor] = partColor;
        }

        if (CarPIcker.CarPart.Spoiler == carPart) 
        {
            UserData.playerBodyColor[UserData.currentSelected[body]][spoilerColor] = partColor;
        } 
        if (CarPIcker.CarPart.Interior == carPart)
        {
            UserData.playerBodyColor[UserData.currentSelected[body]][interiorColor] = partColor;
        }

        /*        WriteUserData(playerEngine,playerTires,playerNitros,playerBody,playerBodyColor,playerTireColor
                    ,playerCoins,reputation,currentSelected,kingDefeated);*/
        UserData.WriteUserData();
        UpdateUI();
      
    }

    public void CarBodyChange(CarBodyChanges.CarChanges changes ,int index)
    {
        if(CarBodyChanges.CarChanges.BodySkit == changes)
        {
            carMeshFilter.mesh = carData.carBFMesh[index + (3 * (int)UserData.playerBody[UserData.currentSelected[body]][bodyIndex])];
        }
        if (CarBodyChanges.CarChanges.Spoiler == changes)
        {
            carSpoilRender.enabled = true;
            spoilerMeshFilter.mesh = carData.carSpoilerMesh[index];
        }
        if (CarBodyChanges.CarChanges.Vinyl == changes)
        {
            int carVinylIndex = index;
            int carBodyTexture = (5 * (int)UserData.playerBody[UserData.currentSelected[body]][bodyIndex]);

            if (carVinylIndex == -1)
            {
                carVinylIndex = 55;
                carMeshRender.materials[carMeshRenderVinyl].mainTexture = carData.carVinylTexture[carVinylIndex];
            }
            else carMeshRender.materials[carMeshRenderVinyl].mainTexture = carData.carVinylTexture[carVinylIndex + carBodyTexture];
        }
    }

    public void PurchaseCarBody(CarBodyChanges.CarChanges changes, int index,int price)
    {
        UserData.playerCoins -= price;
        if (CarBodyChanges.CarChanges.BodySkit == changes)
        {
            UserData.playerBody[UserData.currentSelected[body]][bodyModIndex] = index;
        }
        if (CarBodyChanges.CarChanges.Spoiler == changes)
        {
            UserData.playerBody[UserData.currentSelected[body]][spoilerIndex] = index;
        }
        if (CarBodyChanges.CarChanges.Vinyl == changes)
        {
            UserData.playerBody[UserData.currentSelected[body]][vinylIndex] = index;
        }

        /*
                WriteUserData(playerEngine, playerTires, playerNitros, playerBody, playerBodyColor, playerTireColor
                    , playerCoins, reputation, currentSelected, kingDefeated);*/
        UserData.WriteUserData();
                UpdateUI();

    }

    public int CarBodyIndex()
    {
        int index = (int)UserData.playerBody[UserData.currentSelected[body]][bodyIndex];
        return index;
    }
    
    public int GetPlayerReputation()
    {
        return UserData.reputation[reputationLevel];
    }

    public int GetPlayerCoins()
    {
        return UserData.playerCoins;
    }

    public void BuyCarItems(string type, int index, int price)
    {
        UserData.playerCoins -= price;
        List<int> carItem = new List<int>() { index, 100 };
        if (type == "BODY")
        {
            List<float> carBody = new List<float>() { index, 100, -1, -1, 0, 0, .5f, 0, .5f };
            List<Color32> carColor = new List<Color32>()
            { Color.grey, Color.white, new Color(0, 0, 0, .7f), Color.grey, Color.grey };
            UserData.playerBody.Add(carBody);
            UserData.playerBodyColor.Add(carColor);
        }

        if (type == "ENGINE")
        {
            UserData.playerEngine.Add(carItem);
        }
        if (type == "TIRE")
        {
            UserData.playerTireColor.Add(Color.white);
            UserData.playerTires.Add(carItem);
        }
        if (type == "NITRO")
        {
            UserData.playerNitros.Add(carItem);
        }

/*        WriteUserData(playerEngine, playerTires, playerNitros, playerBody, playerBodyColor, playerTireColor
            , playerCoins, reputation, currentSelected, kingDefeated);*/
        UpdateUI();
        UserData.WriteUserData();
    }

    public List<int> PartIndex()
    {
        int carEngine = UserData.playerEngine[UserData.currentSelected[engine]][engineIndex];
        int carBody = (int)UserData.playerBody[UserData.currentSelected[body]][bodyIndex];
        int carTire = UserData.playerTires[UserData.currentSelected[tire]][tireIndex];
        int carNitros = UserData.playerNitros[UserData.currentSelected[nitros]][nitrosIndex];
        List<int> data = new List<int>();
        data.Add(carEngine); data.Add(carBody);
        data.Add(carTire); data.Add(carNitros);
        return data;
    }
    public List<int> PartHealth()
    {
        List<int> data = new List<int>()
        {
            UserData.playerEngine[UserData.currentSelected[engine]][engineHealthIndex],
            (int)UserData.playerBody[UserData.currentSelected[body]][bodyHealthIndex],
            UserData.playerTires[UserData.currentSelected[tire]][tireHealthIndex],
            UserData.playerNitros[UserData.currentSelected[nitros]][nitrosHealthIndex]
        };
        return data;
    }

    public void RepairParts(int part,int price)
    {
        UserData.playerCoins -= price;
        if (part == 0) UserData.playerEngine[UserData.currentSelected[engine]][engineHealthIndex] = 100;
        if (part == 1) UserData.playerBody[UserData.currentSelected[body]][bodyHealthIndex] = 100;
        if (part == 2) UserData.playerTires[UserData.currentSelected[tire]][tireHealthIndex] = 100;
        if (part == 3) UserData.playerNitros[UserData.currentSelected[nitros]][nitrosHealthIndex] = 100;

/*        WriteUserData(playerEngine, playerTires, playerNitros, playerBody, playerBodyColor, playerTireColor
            , playerCoins, reputation, currentSelected, kingDefeated);*/
        UpdateUI();
        UserData.WriteUserData();

    }

    public List<List<int>> GetInventoryData()
    {
        List<List<int>> data = new List<List<int>>();

        List<int> engineList = new List<int>();
        List<int> bodyList = new List<int>();
        List<int> tireList = new List<int>();
        List<int> nitroList = new List<int>();

        for (int i = 0; i < UserData.playerBody.Count; i++)
        {
            bodyList.Add((int)UserData.playerBody[i][bodyIndex]);
        }
        for (int i = 0; i < UserData.playerEngine.Count; i++)
        {
            engineList.Add((int)UserData.playerEngine[i][engineIndex]);
        }
        for (int i = 0; i < UserData.playerTires.Count; i++)
        {
            tireList.Add((int)UserData.playerTires[i][tireIndex]);
        }
        for (int i = 0; i < UserData.playerNitros.Count; i++)
        {
            nitroList.Add((int)UserData.playerNitros[i][nitrosIndex]);
        }

        data.Add(engineList); data.Add(bodyList);
        data.Add(tireList); data.Add(nitroList);
        return data;
    }
    public List<int> GetEquippedItems()
    {
        List<int> data = new List<int>()
        {
            UserData.currentSelected[engine],
            UserData.currentSelected[body],
            UserData.currentSelected[tire],
            UserData.currentSelected[nitros]
        };
        return data;
    }
    /*    int engine = 0/0; int tire = 1;
     *    int nitros = 2; int body = 3;*/
    public void ChangeCurrentSelected(int index,int type)
    {
        int ogType;
        ogType = type;
        if (type == 1) ogType = 3;
        if (type == 2) ogType = 1;
        if (type == 3) ogType = 2;

        UserData.currentSelected[ogType] = index;
        if(ogType == 3)
        {
            Destroy(currentcar);
            CreateCar();
        }
        if (ogType == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                MeshRenderer wheelMeshRender = objects.wheel[i].GetComponent<MeshRenderer>();
                wheelMeshRender.material.color = UserData.playerTireColor[UserData.currentSelected[tire]];

                MeshFilter wheelMeshFilter = objects.wheel[i].GetComponent<MeshFilter>();
                wheelMeshFilter.mesh = carData.carTireMesh[UserData.playerTires[UserData.currentSelected[tire]][tireIndex]];
            }
        }

/*        WriteUserData(playerEngine, playerTires, playerNitros, playerBody, playerBodyColor, playerTireColor
            , playerCoins, reputation, currentSelected, kingDefeated);*/

        UserData.WriteThenRead();
        UpdateUI();
    }

    public void RefreshCar()
    {
        Destroy(currentcar);
        CreateCar();
    }
    public void WorldEntryFee(int price)
    {
        UserData.playerCoins -= price;
/*        WriteUserData(playerEngine, playerTires, playerNitros, playerBody, playerBodyColor, playerTireColor
            , playerCoins, reputation, currentSelected, kingDefeated);*/
        UpdateUI();
        UserData.WriteUserData();
    }

    public int GetItemCount(int i)
    {
        List<int> data = new List<int>()
        {
            UserData.playerEngine.Count,
            UserData.playerBody.Count,
            UserData.playerTires.Count,
            UserData.playerNitros.Count

        };
        return data[i];
    }

    public List<List<List<int>>> GetPlayerItems()
    {
        List<List<List<int>>> data = new List<List<List<int>>>()
        {
            UserData.playerEngine,
            UserData.playerTires,
            UserData.playerNitros
        };
        return data;
    }

    public List<List<float>> GetPlayerBody()
    {  
        return UserData.playerBody;
    }
    public void IncreasePlayerGems(int gems)
    {
        UserData.playerGems += gems;
        UserData.WriteThenRead();
        UpdateUI();

    }

    public void AdReward(int coins)
    {
        UserData.playerCoins += coins;
        UserData.WriteThenRead();
        UpdateUI();
    }
    public void SellCost(int cost)
    {
        UserData.playerCoins += cost;
    }

    public void RemoveItemFromInventory(string type, int index)
    {
        if ("ENGINE" == type) UserData.playerEngine.RemoveAt(index);
        if ("BODY" == type)
        { 
            UserData.playerBody.RemoveAt(index);
            UserData.playerBodyColor.RemoveAt(index);
        }
        if ("TIRE" == type)
        { 
            UserData.playerTires.RemoveAt(index);
            UserData.playerTireColor.RemoveAt(index);
        }
        if ("NITRO" == type) UserData.playerNitros.RemoveAt(index);

        if(UserData.currentSelected[engine] >= UserData.playerEngine.Count)
        {
            UserData.currentSelected[engine] = 0;
        }
        if (UserData.currentSelected[body] >= UserData.playerBody.Count)
        {
            UserData.currentSelected[body] = 0;
        }
        if (UserData.currentSelected[tire] >= UserData.playerTires.Count)
        {
            UserData.currentSelected[tire] = 0;
        }
        if (UserData.currentSelected[nitros] >= UserData.playerNitros.Count)
        {
            UserData.currentSelected[nitros] = 0;
        }
        UserData.WriteThenRead();
        UpdateUI();

    }
}
