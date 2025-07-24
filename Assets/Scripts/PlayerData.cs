using CI.QuickSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;
using Assets.Scripts.Utils;

public class PlayerData : MonoBehaviour
{
    public userData UserData;
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

    [SerializeField] private string uniqueId;
    [SerializeField] private Vector3 playerPosition;

    private GameObject currentcar;

    private CarObjects objects;
    private MeshRenderer carMeshRender;
    private MeshRenderer carSpoilRender;
    private MeshFilter carMeshFilter;
    private MeshFilter spoilerMeshFilter;

    private readonly int carMeshRenderBody = 0;
    private readonly int carMeshRenderVinyl = 1;
    private readonly int carMeshRenderGlass = 2;

    private CarData carData;
    private UIDataPlanet uIData;

    private Car playerCarData ;
    private ColorTheme carPaint;



    public  Car PlayerCarData { get => playerCarData; set => playerCarData = value; }
    public ColorTheme CarPaint { get => carPaint; set => carPaint = value; }


    void Start()
    {
        UserData = GameObject.Find("/UserData").GetComponent<userData>();
        carData = GetComponent<CarData>();
        uIData = GetComponent<UIDataPlanet>();


        StartCoroutine(Loading());

    }

    public void UpdateUI()
    {
        if (ReputationLevelScaling(UserData.reputation[reputationLevel]) < UserData.reputation[reputationExperience])
        {
            UserData.reputation[reputationLevel]++;
            UserData.storyIndex++;
            UserData.storyTold = false;
        }
        uIData.PlayerCoinsText.text = PRUtils.CurrencyFormater(UserData.playerCoins.ToString());
        uIData.PlayerReputationText.text = UserData.reputation[reputationLevel].ToString();
        float scaleValue;
        float value;
        if (UserData.reputation[reputationLevel]==1)
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
        uIData.PlayerReputationSlider.value = value;
    }

    int ReputationLevelScaling(int lvl)
    {
        int Lvl = 10 + 10 * (lvl - 1) + 25 * (lvl - 1);

        return Lvl;
    }
    IEnumerator Loading()
    {

        ReadUserData();
        UpdateUI();

        yield return new WaitForSeconds(0.5f);
    }



    private void ReadUserData()
    {

        int bodyindex = (int)UserData.playerBody[UserData.currentSelected[this.body]][bodyIndex];
        int engineindex = UserData.playerEngine[UserData.currentSelected[this.engine]][engineIndex];
        int tireindex = UserData.playerTires[UserData.currentSelected[tire]][tireIndex];
        int nitrosindex = UserData.playerNitros[UserData.currentSelected[nitros]][nitrosIndex];

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

        carPaint = new ColorTheme(BodyColor,VinylColor,GlassColor,TireColor,InteriorColor,SpoilerColor,ColorTypeBody,ColorTypeVinyl);

        playerCarData = new Car(engineindex, bodyindex, tireindex, nitrosindex, carPaint, SpoilerIndex, BodySkitIndex, VinylIndex, (int)EngineHealth,
            (int)BodyHealth, (int)TireHealth, (int)NitroHealth, CarEngineHealth, CarBodyHealth, CarTireHealth, CarNitroHealth, carMass, carTopSpeed, carFullTorque
            , carSteerHelper, carTractionControl, carDownForce, carBreakTorque, topSpeed, fullTorque);


    }


    public GameObject CreateCar()
    {
        //if(currentcar==null){currentcar.GetComponent<CarObjects>().destroyObject();}

        currentcar = Instantiate(carData.carPrefabs[(int)UserData.playerBody[UserData.currentSelected[body]][bodyIndex]]);
        currentcar.transform.position = playerPosition;
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
            carMeshRender.materials[carMeshRenderVinyl].mainTexture = carData.carVinylTexture[carVinylIndex];
        }
        else carMeshRender.materials[carMeshRenderVinyl].mainTexture = carData.carVinylTexture[carVinylIndex + carBodyTexture];


        carMeshFilter.mesh = carData.carBFMesh[(int)UserData.playerBody[UserData.currentSelected[body]][bodyModIndex] + (3 * (int)UserData.playerBody[UserData.currentSelected[body]][bodyIndex])];
        if (UserData.playerBody[UserData.currentSelected[body]][spoilerIndex] == -1) carSpoilRender.enabled = false;
        else spoilerMeshFilter.mesh = carData.carSpoilerMesh[(int)UserData.playerBody[UserData.currentSelected[body]][spoilerIndex]];
        return currentcar;
    }

    public void DecreaseCoins(int price)
    {
        UserData.playerCoins -= price;
        UserData.WriteUserData();
        UpdateUI();
    }
    public void IncreaseCoins(int price)
    {
        UserData.playerCoins += 2*price;
        UserData.WriteUserData();
                UpdateUI();
    }

    public void OpponentNewCar(Opponent opponentCar)
    {

        List<float> body = new List<float>() { 
            opponentCar.Car.BodyIndex,
            opponentCar.Car.BodyHealth/opponentCar.Car.CarBodyHealth,
            opponentCar.Car.SpoilerIndex,
            opponentCar.Car.VinylIndex,
            opponentCar.Car.BodySkitIndex,
            opponentCar.Car.ColorTheme.ColorTypeBody[0],
            opponentCar.Car.ColorTheme.ColorTypeBody[1],
            opponentCar.Car.ColorTheme.ColorTypeVinyl[0],
            opponentCar.Car.ColorTheme.ColorTypeVinyl[1]
        };
        List<int> engine = new List<int>() { opponentCar.Car.EngineIndex, opponentCar.Car.EngineHealth/opponentCar.Car.CarEngineHealth };
        List<int> tire = new List<int>() { opponentCar.Car.TireIndex, opponentCar.Car.TireHealth/ opponentCar.Car.CarTireHealth };
        List<int> nitro = new List<int>() { opponentCar.Car.NitroIndex, opponentCar.Car.NitroHealth/ opponentCar.Car.CarNitroHealth };

        List<Color32> bodyColor = new List<Color32>() {
            opponentCar.Car.ColorTheme.BodyColor,
            opponentCar.Car.ColorTheme.VinylColor,
            opponentCar.Car.ColorTheme.GlassColor,
            opponentCar.Car.ColorTheme.SpoilerColor,
            opponentCar.Car.ColorTheme.InteriorColor
        };

        UserData.playerBody.Add(body);
        UserData.playerBodyColor.Add(bodyColor);
        UserData.playerEngine.Add(engine);
        UserData.playerNitros.Add(nitro);
        UserData.playerTires.Add(tire);
        UserData.playerTireColor.Add(opponentCar.Car.ColorTheme.TireColor);
        UserData.WriteUserData();
        UpdateUI();
    }
    public void RemoveCurrentCar()
    {
        UserData.playerBody.RemoveAt(UserData.currentSelected[body]);
        UserData.playerBodyColor.RemoveAt(UserData.currentSelected[body]);
        UserData.playerEngine.RemoveAt(UserData.currentSelected[engine]);
        UserData.playerNitros.RemoveAt(UserData.currentSelected[nitros]);
        UserData.playerTires.RemoveAt(UserData.currentSelected[tire]);
        UserData.playerTireColor.RemoveAt(UserData.currentSelected[tire]);

        for (int i = 0; i < 4; i++) { UserData.currentSelected[i] = 0; }

        UserData.WriteUserData();
        UpdateUI();
    }

    public void UpdateCarHealth(Car player)
    {
        decimal currentEngineHealth = decimal.Divide(player.EngineHealth, player.CarEngineHealth);
        decimal currentBodyHealth = decimal.Divide(player.BodyHealth, player.CarBodyHealth);
        decimal currentTireHealth = decimal.Divide(player.TireHealth, player.CarTireHealth);
        decimal currentNitrosHealth = decimal.Divide(player.NitroHealth, player.CarNitroHealth);
        UserData.playerBody[UserData.currentSelected[body]][bodyHealthIndex] = (float)(currentBodyHealth * 100);
        UserData.playerEngine[UserData.currentSelected[engine]][engineHealthIndex] = (int)(currentEngineHealth * 100);
        UserData.playerTires[UserData.currentSelected[tire]][tireHealthIndex] = (int)(currentTireHealth * 100);
        UserData.playerNitros[UserData.currentSelected[nitros]][nitrosHealthIndex] = (int)(currentNitrosHealth * 100);

        UserData.WriteUserData();
                ReadUserData();
    }

    public int GetPlayerCoin()
    {
        return UserData.playerCoins;
    }

    public int GetPlayerReputation()
    {
        return UserData.reputation[0];
    }
    public void IncreaseReputation(int val)
    {
        UserData.reputation[1] += val;
        UserData.WriteUserData();
        ReadUserData();
    }
    public void KingDefeated(int i)
    {
        UserData.kingDefeated[i] = 1;
        UserData.WriteUserData();
    }
    public int CheckKingDefeated(int i)
    {
        return UserData.kingDefeated[i];
    }
}
