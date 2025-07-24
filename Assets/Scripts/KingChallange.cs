using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using Assets.Scripts.Models;
using Assets.Scripts.Utils;
using UnityEngine.UI;

public class KingChallange : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] UIDataPlanet uIDataPlanet;
    [SerializeField] RaceData raceData;
    [SerializeField] CarData carData;
    [SerializeField] PlayerData PlayerData;

    [SerializeField] private string kingTitle;
    [SerializeField] private string kingDescription;


    private Car kingCarData;
    private Car henchmanCarData;
    [SerializeField] int kingOffer;
    [SerializeField] int offerReputationRequired;
    [SerializeField] private CinemachineVirtualCamera kingCam;
    [SerializeField] private Vector3 king;
    [SerializeField] private Vector3 playerRaceLocation;
    [SerializeField] private Vector3 kingRaceLocation;
    [SerializeField] private Vector3[] henchman;
    [SerializeField] private Vector3[] henchmanRace;
    [SerializeField] [Range(1, 4)] int noOfHenchman;

    [SerializeField] private int engineindex;
    [SerializeField] private int bodyindex;
    [SerializeField] private int tireindex;
    [SerializeField] private int nitroindex;
    [SerializeField] private int spoilerIndex;
    [SerializeField] private int bodySkitIndex;
    [SerializeField] private Texture vinylTexture;

    [SerializeField] private Color bodyColor;
    [SerializeField] private Color vinylColor;
    [SerializeField] private Color glassColor;
    [SerializeField] private Color tireColor;
    [SerializeField] private Color interiorColor;
    [SerializeField] private Color spoilerColor;
    [SerializeField] private List<float> colorTypeBody;
    [SerializeField] private List<float> colorTypeVinyl;
    private MeshCollider playerCarBody;

    private GameObject kingCarGameobject;
    private List<GameObject> henchmenCarGameobject = new List<GameObject>();
    private GameObject playerCarGameobject;
    /*  [SerializeField] private int playerCurrentLap = 0;
        [SerializeField] private int playerPreviousCheckpoint = -1;
        [SerializeField] private int playerCurrentCheckpoint = 0;*/

    [SerializeField] private int noOfLaps;
    [SerializeField] private bool playerWin;

    private bool raceStart = false;
    [SerializeField] private bool gotTheWinner;
    int i;

    public MeshCollider PlayerCarBody { get => playerCarBody; set => playerCarBody = value; }

    void Start()
    {

         i = SceneManager.GetActiveScene().buildIndex - 2;


        if (i == 0)
        {
            engineindex = 3; bodyindex = 1; tireindex = 2; nitroindex = 2;
            bodyColor = Color.magenta; vinylColor = Color.green; glassColor = Color.green; glassColor.a = .5f;
            tireColor = Color.magenta; interiorColor = Color.cyan; spoilerColor = Color.green;
            colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
        }

        else if (i == 1)
        {
            engineindex = 4; bodyindex = 2; tireindex = 2; nitroindex = 2;
            bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
            tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
            colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
        }

        else if (i == 2)
        {
            engineindex = 5; bodyindex = 3; tireindex = 2; nitroindex = 3;
            bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
            tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
            colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
        }
        else if (i == 3)
        {
            engineindex = 6; bodyindex = 3; tireindex = 2; nitroindex = 4;
            bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
            tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
            colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
        }

        else if (i == 4)
        {
            engineindex = 7; bodyindex = 4; tireindex = 3; nitroindex = 4;
            bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
            tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
            colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
        }

        else if (i == 5)
        {
            engineindex = 8; bodyindex = 5; tireindex = 7; nitroindex = 4;
            bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
            tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
            colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
        }

        else if (i == 6)
        {
            engineindex = 9; bodyindex = 6; tireindex = 7; nitroindex = 4;
            bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
            tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
            colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
        }

        else if (i == 7)
        {
            engineindex = 10; bodyindex = 7; tireindex = 7; nitroindex = 4;
            bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
            tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
            colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
        }

        else if (i == 8)
        {
            engineindex = 11; bodyindex = 8; tireindex = 6; nitroindex = 4;
            bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
            tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
            colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
        }

        else if (i == 9)
        {
            engineindex = 12; bodyindex = 9; tireindex = 7; nitroindex = 4;
            bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
            tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
            colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
        }

        else if (i == 10)
        {
            engineindex = 13; bodyindex = 10; tireindex = 4; nitroindex = 4;
            bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
            tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
            colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
        }

        else if (i == 11)
        {
            engineindex = 13; bodyindex = 10; tireindex = 7; nitroindex = 4;
            bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
            tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
            colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
        }

        else if (i == 12)
        {
            engineindex = 13; bodyindex = 10; tireindex = 7; nitroindex = 4;
            bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
            tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
            colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
        }
        bodyColor = Color.magenta; vinylColor = Color.green; glassColor = Color.green; glassColor.a = .5f;
        tireColor = Color.magenta; interiorColor = Color.cyan; spoilerColor = Color.green;
        colorTypeBody[0] = 0.5f; colorTypeBody[1] = 0.8f;
        kingCam.m_Priority = 1;
        StartCoroutine(SetGame());

        noOfHenchman = Constants.planetDescription[i].KingData.NoOfHenchmen;
        uIDataPlanet.KingButton.onClick.AddListener(KingPageEnter);
        uIDataPlanet.KingPanelCloseButton.onClick.AddListener(KingPageExit);
        uIDataPlanet.ChallengeButton.onClick.AddListener(SetKingChallenge);
        uIDataPlanet.KingDescription.text = Constants.planetDescription[i].KingData.KingDescription;
        uIDataPlanet.KingTitle.text = Constants.planetDescription[i].KingData.KingTitle;
        uIDataPlanet.KingLaps.text = Constants.planetDescription[i].KingData.Laps+"";
        uIDataPlanet.KingRepReq.text = "Reputation required\n"+Constants.planetDescription[i].KingData.ReputationRequired;
        uIDataPlanet.KingOffer.text = "Offer\n" + Constants.planetDescription[i].KingData.Offer;
        kingOffer = Constants.planetDescription[i].KingData.Offer;
        offerReputationRequired = Constants.planetDescription[i].KingData.ReputationRequired;
        noOfLaps = Constants.planetDescription[i].KingData.Laps;
        LowerVal = noOfLaps * raceData.Lenght;

    }
    decimal LowerVal;
    private void Update()
    {
        if (raceStart)
        {
            decimal playerUpperVal = (raceData.PlayerCurrentLap * raceData.Lenght) + raceData.PlayerCurrentCheckpoint;
            decimal opponentUpperVal = (kingCarGameobject.GetComponent<CarObjects>()
                .carAIControl.CurrentLap * raceData.Lenght) +
            kingCarGameobject.GetComponent<CarObjects>().carAIControl.CurrentTarget;
            if (!gotTheWinner)
            {
                if (opponentUpperVal == LowerVal)
                {
                    gotTheWinner = true;
                    raceData.GotTheWinner = true;
                    raceData.PlayerWin = false;
                    kingCarGameobject.GetComponent<CarObjects>().AiCarSelfRighting.StartRace1 = false;
                    playerWin = false;
                }
            }
            decimal valuePlayer;
            decimal valueOpponent;
            try
            {
                valuePlayer = decimal.Divide(
                playerUpperVal,
                LowerVal);
                valueOpponent = decimal.
                Divide(opponentUpperVal, LowerVal);
            }
            catch
            {
                valuePlayer = 0;
                valueOpponent = 0;
            }



            raceData.PlayerSlider.value = (float)valuePlayer;
            raceData.OpponentSlider.value = (float)valueOpponent;
            raceData.Laps1.text = "Laps " + (raceData.PlayerCurrentLap + 1) + "/" + noOfLaps;
        }

    }
    IEnumerator SetGame()
    {
        yield return new WaitForSeconds(0.5f);
        kingCarData = GenerateCarData(bodyindex,engineindex,tireindex,nitroindex,spoilerIndex,bodySkitIndex,1);
        henchmanCarData = GenerateCarData(bodyindex-1, engineindex, tireindex, nitroindex, spoilerIndex, bodySkitIndex, 1);
        kingCarGameobject =  GenerateCar(kingCarData);
        //ChangeCarTexture(kingCarGameobject);
        kingCarGameobject.transform.SetParent(gameObject.transform);
        kingCarGameobject.transform.localPosition = king;
        kingCarGameobject.GetComponent<CarObjects>().userControl.enabled = false;
        kingCarGameobject.SetActive(true);
        OpponentCarSet(kingCarGameobject);

        GameObject henchmenGameobject = GenerateCar(henchmanCarData);
        henchmenCarGameobject.Add(henchmenGameobject);
        henchmenCarGameobject[0].transform.SetParent( gameObject.transform);
        henchmenCarGameobject[0].transform.localPosition = henchman[0];
        henchmenCarGameobject[0].GetComponent<CarObjects>().userControl.enabled = false;
        henchmenCarGameobject[0].SetActive(true);
        OpponentCarSet(henchmenCarGameobject[0]);

        for (int i = 1; i < noOfHenchman; i++)
        {
            henchmenCarGameobject.Add(Instantiate(henchmenGameobject));
            henchmenCarGameobject[i].transform.SetParent(gameObject.transform);
            henchmenCarGameobject[i].transform.localPosition = henchman[i];
            henchmenCarGameobject[i].GetComponent<CarObjects>().userControl.enabled = false;
            henchmenCarGameobject[i].SetActive(true);
            OpponentCarSet(henchmenCarGameobject[i]);
        }
        

    }

    public void KingPageEnter()
    {
        kingCam.m_Priority = 100;
    }
    public void KingPageExit()
    {
        kingCam.m_Priority = 1;
    }

    private Car GenerateCarData(int bodyindex,int engineindex,int tireindex,int nitroindex,
        int spoilerindex,int bodyskitindex, int vinylindex)
    {

        int CarBodyHealth = Constants.bodyData[bodyindex].CarPartHealth;
        int CarEngineHealth = Constants.enginesData[engineindex].CarPartHealth;
        int CarTireHealth = Constants.tiresData[tireindex].CarPartHealth;
        int CarNitroHealth = Constants.nitrosData[nitroindex].CarPartHealth;

        decimal BodyHealth = CarBodyHealth;
        decimal EngineHealth = CarEngineHealth;
        decimal TireHealth = CarTireHealth;
        decimal NitroHealth = CarNitroHealth;

        int carMass = Constants.enginesData[engineindex].CarPartMass
            + Constants.bodyData[bodyindex].CarPartMass
            + Constants.tiresData[tireindex].CarPartMass
            + Constants.nitrosData[nitroindex].CarPartMass;

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


        ColorTheme carPaint = new ColorTheme(bodyColor, vinylColor, glassColor, tireColor, interiorColor, spoilerColor, colorTypeBody, colorTypeVinyl);

        Car carData = new Car(engineindex, bodyindex, tireindex, nitroindex, carPaint, spoilerindex, bodyskitindex, vinylindex, (int)EngineHealth,
            (int)BodyHealth, (int)TireHealth, (int)NitroHealth, CarEngineHealth, CarBodyHealth, CarTireHealth, CarNitroHealth, carMass, carTopSpeed, carFullTorque
            , carSteerHelper, carTractionControl, carDownForce, carBreakTorque, topSpeed, fullTorque);
        return carData;
    }

    private GameObject GenerateCar(Car car)
    {
        GameObject currentcar = Instantiate(carData.carPrefabs[car.BodyIndex]);
        currentcar.SetActive(false);

        CarObjects objects = currentcar.GetComponent<CarObjects>();
        MeshRenderer carMeshRender = objects.carBody.GetComponent<MeshRenderer>();
        MeshRenderer carSpoilRender = objects.spoiler.GetComponent<MeshRenderer>();
        MeshFilter carMeshFilter = objects.carBody.GetComponent<MeshFilter>();
        MeshFilter spoilerMeshFilter = objects.spoiler.GetComponent<MeshFilter>();

        carMeshRender.materials[0].color = car.ColorTheme.BodyColor;
        carMeshRender.materials[1].color = car.ColorTheme.VinylColor;
        carMeshRender.materials[2].color = car.ColorTheme.GlassColor;
        carMeshRender.materials[0].SetFloat("_Metallic", car.ColorTheme.ColorTypeBody[0]);
        carMeshRender.materials[0].SetFloat("_Smoothness", car.ColorTheme.ColorTypeBody[1]);
        carMeshRender.materials[1].SetFloat("_Metallic", car.ColorTheme.ColorTypeVinyl[0]);
        carMeshRender.materials[1].SetFloat("_Smoothness", car.ColorTheme.ColorTypeVinyl[1]);

        objects.interior.material.color = car.ColorTheme.InteriorColor;
        carSpoilRender.material.color = car.ColorTheme.SpoilerColor;
        for (int i = 0; i < 4; i++)
        {
            MeshRenderer wheelMeshRender = objects.wheel[i].GetComponent<MeshRenderer>();
            wheelMeshRender.material.color = car.ColorTheme.TireColor;

            MeshFilter wheelMeshFilter = objects.wheel[i].GetComponent<MeshFilter>();
            wheelMeshFilter.mesh = carData.carTireMesh[car.TireIndex];
        }
        int carVinylIndex = car.VinylIndex;
        int carBodyTexture = (5 * car.BodyIndex);

        if (carVinylIndex == -1)
        {
            carVinylIndex = 55;
            carMeshRender.materials[1].mainTexture = carData.carVinylTexture[carVinylIndex];
        }
        else carMeshRender.materials[1].mainTexture = carData.carVinylTexture[carVinylIndex + carBodyTexture];


        carMeshFilter.mesh = carData.carBFMesh[car.BodySkitIndex + (3 * car.BodyIndex)];
        if (car.SpoilerIndex == -1) carSpoilRender.enabled = false;
        else spoilerMeshFilter.mesh = carData.carSpoilerMesh[car.SpoilerIndex];
        return currentcar;
    }
    private void ChangeCarTexture(GameObject car)
    {
        car.GetComponent<CarObjects>().carBody.
            GetComponent<MeshRenderer>().materials[1].mainTexture = vinylTexture;
    }
    private void playerCarSet()
    {
        uIDataPlanet.KingPanel.SetActive(false);
        raceData.NoOfLaps = noOfLaps;
        raceData.RaceStart = true;

        raceData.PlayerCarBody = playerCarBody;

        uIDataPlanet.CarControlUI.SetActive(true);

        uIDataPlanet.Controller.SetActive(false);
        uIDataPlanet.StartRace.SetActive(true);
        playerCarGameobject = PlayerData.CreateCar();
        playerCarGameobject.transform.SetParent(gameObject.transform);
        playerCarGameobject.transform.localPosition = playerRaceLocation;
        playerCarGameobject.GetComponent<CarObjects>().userControl.enabled = true;
        playerCarGameobject.SetActive(true);
        playerCarGameobject.GetComponent<UnityStandardAssets.Vehicles.Car.CarAudio>().player = true;
        playerCarGameobject.GetComponent<Rigidbody>().mass = (float)(PlayerData.PlayerCarData.CarMass * 1.5);

        raceData.PlayerCarBody = playerCarGameobject.GetComponent<CarObjects>()
                .carBody.GetComponent<MeshCollider>();
        playerCarGameobject.GetComponent<CarObjects>().carCamera.m_Priority = 101;
        playerCarGameobject.GetComponent<CarObjects>().indicatorGameObject.SetActive(true);
        playerCarGameobject.GetComponent<CarObjects>().indicatorMeshRenderer.material.color = Color.green;
        playerCarGameobject.GetComponent<CarObjects>().carAIControl.enabled = false;
        playerCarGameobject.GetComponent<CarObjects>().userControl.enabled = true;
        playerCarGameobject.GetComponent<CarObjects>().userControl.NitroHealth = PlayerData.PlayerCarData.CarNitroHealth;
        playerCarGameobject.GetComponent<CarObjects>().userControl.CurNitroHealth = PlayerData.PlayerCarData.NitroHealth;
        playerCarGameobject.GetComponent<CarObjects>().carController.Topspeed = PlayerData.PlayerCarData.CarTopSpeed;
        playerCarGameobject.GetComponent<CarObjects>().carController.BrakeTorque = PlayerData.PlayerCarData.CarBreakTorque;
        playerCarGameobject.GetComponent<CarObjects>().carController.Downforce = PlayerData.PlayerCarData.CarDownForce;
        playerCarGameobject.GetComponent<CarObjects>().carController.SteerHelper1 = PlayerData.PlayerCarData.CarSteerHelper;
        playerCarGameobject.GetComponent<CarObjects>().carController.TractionControl1 = PlayerData.PlayerCarData.CarTractionControl;
        playerCarGameobject.GetComponent<CarObjects>().carController.FullTorqueOverAllWheels = PlayerData.PlayerCarData.CarFullTorque;
        if (raceData.planetEnvironment.isLightNeeded) playerCarGameobject.GetComponent<CarObjects>().headLights.enabled = true;
        else playerCarGameobject.GetComponent<CarObjects>().headLights.enabled = false;
    }

    private void playerCarStart()
    {
        playerCarGameobject.GetComponent<CarObjects>().AiCarSelfRighting.enabled = false;
        raceData.PlayerCar = playerCarGameobject;
        playerCarGameobject.GetComponent<CarObjects>().DamageSystem.SetData(PlayerData.PlayerCarData, true);
        playerCarGameobject.GetComponent<CarObjects>().userControl.SetNitroHealth(PlayerData.PlayerCarData.CarNitroHealth);
        uIDataPlanet.CarControlUI.SetActive(true);
        uIDataPlanet.StartRace.SetActive(false);
        uIDataPlanet.Controller.SetActive(true);
    }
    private void OpponentCarSet(GameObject Car)
    {
        Car.GetComponent<Rigidbody>().mass = (float)(kingCarData.CarMass * 1.5);
        Car.GetComponent<CarObjects>().indicatorGameObject.SetActive(true);
        Car.GetComponent<CarObjects>().indicatorMeshRenderer.material.color = Color.red;
        Car.GetComponent<CarObjects>().carCamera.m_Priority = 1;
        Car.GetComponent<CarObjects>().UserGUIControl.enabled = false;
        Car.GetComponent<CarObjects>().carController.Topspeed = kingCarData.CarTopSpeed;
        Car.GetComponent<CarObjects>().AiCarSelfRighting.enabled = false;
        Car.GetComponent<CarObjects>().carController.BrakeTorque = kingCarData.CarBreakTorque;
        Car.GetComponent<CarObjects>().carController.Downforce = kingCarData.CarDownForce;
        Car.GetComponent<CarObjects>().carController.SteerHelper1 = kingCarData.CarSteerHelper;
        Car.GetComponent<CarObjects>().carController.TractionControl1 = kingCarData.CarTractionControl;
        Car.GetComponent<CarObjects>().carController.FullTorqueOverAllWheels = kingCarData.CarFullTorque;
    }
    private void OpponentCarStart(GameObject Car, bool king)
    {
        raceStart = true;
        //Car.GetComponent<Rigidbody>().velocity = raceGenerator.ForceOnCar;
        Car.GetComponent<CarObjects>().carAIControl.enabled = true;
        if(king)
        {
            Car.GetComponent<CarObjects>().carAIControl.Target = raceData.PathOne;
            Car.GetComponent<CarObjects>().carAIControl.TargetList = raceData.PathOneStepOne;
            Car.GetComponent<CarObjects>().carAIControl.StopWhenTargetReached = true;
            Car.GetComponent<CarObjects>().AiCarSelfRighting.enabled = true;
        }
        else
        {
            /*          Car.GetComponent<CarObjects>().carAIControl.Target = playerCarGameobject.transform;
                        Car.GetComponent<CarObjects>().carAIControl.TargetList = raceData.PathOneStepOne;
                        Car.GetComponent<CarObjects>().carAIControl.StopWhenTargetReached = false;
                        Car.GetComponent<CarObjects>().AiCarSelfRighting.enabled = false;*/
            Car.GetComponent<CarObjects>().carAIControl.Target = raceData.PathOne;
            Car.GetComponent<CarObjects>().carAIControl.TargetList = raceData.PathOneStepOne;
            Car.GetComponent<CarObjects>().carAIControl.StopWhenTargetReached = true;
            Car.GetComponent<CarObjects>().AiCarSelfRighting.enabled = true;
        }

        Car.GetComponent<CarObjects>().carAIControl.Laps = noOfLaps;
        Car.GetComponent<CarObjects>().carAIControl.TargetListLenght = raceData.Lenght;

        Car.GetComponent<CarObjects>().carAIControl.Driving = true;

        Car.GetComponent<CarObjects>().carAIControl.ReachTargetThreshold = 30f;
        Car.GetComponent<CarObjects>().userControl.enabled = false;
        Car.GetComponent<CarObjects>().AiCarSelfRighting.StartRace1 = true;

        Car.GetComponent<CarObjects>().DamageSystem.SetData(kingCarData, false);

    }

    List<GameObject> OpponentList = new List<GameObject>();
    private void SetKingChallenge()
    {
        raceData.RaceStart = false;
        if (PlayerData.GetPlayerCoin() >= kingOffer &&
            PlayerData.GetPlayerReputation() >= offerReputationRequired)
        {
            kingCarGameobject.transform.localPosition = kingRaceLocation;
            for (int i = 0; i<noOfHenchman; i++)
            {
                henchmenCarGameobject[i].transform.localPosition = henchmanRace[i];
                OpponentList.Add(henchmenCarGameobject[i]);
            }


            playerCarSet();
            PlayerData.DecreaseCoins(kingOffer);

            for (int i = 0; i<OpponentList.Count;i++)
            {
                OpponentCarSet(OpponentList[i]);
            }
            OpponentCarSet(kingCarGameobject);

            uIDataPlanet.StartRace.SetActive(true);
            uIDataPlanet.StartRace.GetComponent<Button>().onClick.RemoveAllListeners();
            uIDataPlanet.StartRace.GetComponent<Button>().onClick.AddListener(StartChallange);
        }
        else
        {
            uIDataPlanet.Message.SetActive(true);
            uIDataPlanet.MessageText.text = "You don't have enough coins or enough reputation";
        }
    }

    private void StartChallange()
    {
        raceData.RaceStart = true;
        raceData.KingChallenge = true;
        raceData.CurrentRaceType = RaceData.RaceType.Sprint;
        raceData.CheckpointsStraight.SetActive(true);
        raceData.CheckpointReversed.SetActive(false);


        uIDataPlanet.TimePanel.SetActive(false);
        OpponentCarStart(kingCarGameobject,true);
        raceData.OpponentCar = kingCarGameobject;
        raceData.PlayerCar = playerCarGameobject;
        LowerVal = noOfLaps * raceData.Lenght;
        playerCarBody = playerCarGameobject.GetComponent<CarObjects>().carBody.GetComponent<MeshCollider>();

        for (int i = 0; i < OpponentList.Count; i++)
        {
            OpponentCarStart(OpponentList[i],false);
        }
        playerCarStart();

    }

 /*   public void EndChallenge()
    {

        if (noOfLaps - 1 == playerCurrentLap && raceStart == true)
        {
            raceStart = false;
            uIDataPlanet.Controller.SetActive(false);
            uIDataPlanet.RaceCompletion.SetActive(true);
            uIDataPlanet.RaceComtinue.onClick.RemoveAllListeners();
            uIDataPlanet.RaceComtinue.onClick.AddListener(Reset);

            int currentEngineHealth = (int)playerCarGameobject.GetComponent<CarObjects>().DamageSystem.EngineHP1;
            int currentBodyHealth = (int)playerCarGameobject.GetComponent<CarObjects>().DamageSystem.BodyHP1;
            int currentNitroHealth = (int)playerCarGameobject.GetComponent<CarObjects>().userControl.NitroHealth;
            int currentTireHealth = (int)playerCarGameobject.GetComponent<CarObjects>().DamageSystem.TireHP1;
            if (currentBodyHealth - 300 > 0 && currentEngineHealth - 300 > 0 && currentTireHealth - 300 > 0)
            {
                currentBodyHealth -= 100;
                currentEngineHealth -= 100;
                currentTireHealth -= 100;
            }

            PlayerData.PlayerCarData.EngineHealth = currentEngineHealth;
            PlayerData.PlayerCarData.BodyHealth = currentBodyHealth;
            PlayerData.PlayerCarData.TireHealth = currentTireHealth;
            PlayerData.PlayerCarData.NitroHealth = currentNitroHealth;

            PlayerData.UpdateCarHealth(PlayerData.PlayerCarData);
            uIDataPlanet.EngineHealthSlider.value = (float)decimal.Divide(PlayerData.PlayerCarData.EngineHealth, PlayerData.PlayerCarData.CarEngineHealth);
            uIDataPlanet.BodyHealthSlider.value = (float)decimal.Divide(PlayerData.PlayerCarData.BodyHealth, PlayerData.PlayerCarData.CarBodyHealth);
            uIDataPlanet.TireHealthSlider.value = (float)decimal.Divide(PlayerData.PlayerCarData.TireHealth, PlayerData.PlayerCarData.CarTireHealth);
            uIDataPlanet.NitrosHealthSlider.value = (float)decimal.Divide(PlayerData.PlayerCarData.NitroHealth, PlayerData.PlayerCarData.CarNitroHealth);
        }
    }*/
}
