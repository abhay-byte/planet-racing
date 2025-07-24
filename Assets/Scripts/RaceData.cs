using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts.Utils;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;


public class RaceData : MonoBehaviour
{


    public enum RaceType { Reverse, Time, Sprint, AgainstThree };
    RaceType currentRaceType;
    public RaceGenerator raceGenerator;
    public GraphicSettings settings;
    public PlanetEnvironmentSystem planetEnvironment;

    public TMP_Text curTimeText;
    public TMP_Text bestTimeText;

    public GameObject gyroControl;
    public GameObject arrowControl;

    [SerializeField] GameObject checkpointsStraight;
    [SerializeField] GameObject checkpointReversed;

    [SerializeField]  private int lenght;

    [SerializeField]  private Transform pathOne;
    [SerializeField]  private Transform pathOneStepOne;

    [SerializeField]  private Transform pathTwo;
    [SerializeField]  private Transform pathTwoStepOne;

    [SerializeField]  private Transform pathThree;
    [SerializeField]  private Transform pathThreeStepOne;

    [SerializeField]  private Transform pathFour;
    [SerializeField]  private Transform pathFourStepOne;

    [SerializeField]  private Transform pathFive;
    [SerializeField]  private Transform pathFiveStepOne;

    [SerializeField] private Transform pathOneReverse;
    [SerializeField] private Transform pathOneStepOneReverse;

    [SerializeField] private UIDataPlanet uIDataPlanet;
    [SerializeField] private PlayerData playerData;

    [SerializeField] private int playerCurrentLap = 0;
    [SerializeField] private int playerPreviousCheckpoint = -1;
    [SerializeField] private int playerCurrentCheckpoint = 0;

    [SerializeField] private int noOfLaps = 0;
    [SerializeField] private bool playerWin;
    [SerializeField] private string offerType;
    public int currentOffer;

    private bool raceStart = false;
    [SerializeField] private bool gotTheWinner;

    private int noOfOpponents = 1;

    private bool raceReverse = false;
    private bool againstThree = false;
    private bool time = false;
    private float totaltime;

    private GameObject playerCar;
    private GameObject opponentCar;
    private GameObject opponentCar1;
    private GameObject opponentCar2;

    private Opponent opponent;
    private Opponent opponent1;
    private Opponent opponent2;
    private bool kingChallenge;
    private MeshCollider playerCarBody;

    private Slider opponent1Completion;
    private Slider opponent2Completion;

    public GameObject PlayerCar { get => playerCar; set => playerCar = value; }
    public MeshCollider PlayerCarBody { get => playerCarBody; set => playerCarBody = value; }
    public int PlayerCurrentLap { get => playerCurrentLap; set => playerCurrentLap = value; }
    public int PlayerCheckpoint { get => playerCurrentCheckpoint; set => playerCurrentCheckpoint = value; }
    public int PlayerPreviousCheckpoint { get => playerPreviousCheckpoint; set => playerPreviousCheckpoint = value; }
    public int NoOfLaps { get => noOfLaps; set => noOfLaps = value; }
    public int Lenght { get => lenght; set => lenght = value; }
    public bool RaceStart { get => raceStart; set => raceStart = value; }
    public bool GotTheWinner { get => gotTheWinner; set => gotTheWinner = value; }
    public bool PlayerWin { get => playerWin; set => playerWin = value; }

    public GameObject OpponentCar { get => opponentCar; set => opponentCar = value; }
    public Transform PathOneStepOne { get => pathOneStepOne; set => pathOneStepOne = value; }
    public Slider PlayerSlider { get => playerSlider; set => playerSlider = value; }
    public Slider OpponentSlider { get => opponentSlider; set => opponentSlider = value; }
    public TMP_Text Laps1 { get => Laps; set => Laps = value; }
    public Transform PathOne { get => pathOne; set => pathOne = value; }
    public int PlayerCurrentCheckpoint { get => playerCurrentCheckpoint; set => playerCurrentCheckpoint = value; }
    public bool KingChallenge { get => kingChallenge; set => kingChallenge = value; }
    public RaceType CurrentRaceType { get => currentRaceType; set => currentRaceType = value; }
    public bool RaceReverse { get => raceReverse; set => raceReverse = value; }
    public UIDataPlanet UIDataPlanet { get => uIDataPlanet; set => uIDataPlanet = value; }
    public PlayerData PlayerData { get => playerData; set => playerData = value; }
    public Transform PathOneReverse { get => pathOneReverse; set => pathOneReverse = value; }
    public Transform PathOneStepOneReverse { get => pathOneStepOneReverse; set => pathOneStepOneReverse = value; }
    public GameObject CheckpointsStraight { get => checkpointsStraight; set => checkpointsStraight = value; }
    public GameObject CheckpointReversed { get => checkpointReversed; set => checkpointReversed = value; }

    private TMPro.TMP_Text Laps;
    private Slider playerSlider;
    private Slider opponentSlider;
    private TMP_Text timeLeftText;
    private TMP_Text percentageComplete;
    private List<GameObject> CarGameObjects = new List<GameObject>();
    public RewardedADS rewardedADS;
    private List<GameObject> OpponentGameObjects = new List<GameObject>();
    private void Start()
    {

        gotTheWinner = false;
        kingChallenge = false;
        

        Laps = UIDataPlanet.NoOfLaps;
        timeLeftText = UIDataPlanet.TimeLeftText;
        percentageComplete = UIDataPlanet.RaceCompletionPercentage;
        playerSlider = UIDataPlanet.PlayerCompletionSlider;
        opponentSlider = UIDataPlanet.OpponentCompletionSlider;
        if (settings.controllerIndex == 0)
        {
            gyroControl.SetActive(true);
            arrowControl.SetActive(false);
        }
        else
        {
            gyroControl.SetActive(false);
            arrowControl.SetActive(true);
        }

    }
    private void Time()
    {
        time = true;
        totaltime = opponent.TimeRequired;
        timeLeftText.text = "Time Left: " + totaltime;
        percentageComplete.text = "Race Completion : " + 0 + "%";

        opponentCar.SetActive(false);
        UIDataPlanet.TimePanel.SetActive(true);
        playerSlider.gameObject.SetActive(false);
        opponentSlider.gameObject.SetActive(false);
        CarGameObjects.Add(playerCar);
    }
    private void DuelRace()
    {
        CarGameObjects.Add(opponentCar);
        OpponentGameObjects.Add(opponentCar);
        SetOpponentCar(opponentCar,opponent);
        CarGameObjects.Add(playerCar);
    }
    private readonly List<Opponent> newOpponent = new List<Opponent>();
    private void AgainstThreeRace()
    {
        againstThree = true;
        int i = SceneManager.GetActiveScene().buildIndex;
        CarGameObjects.Add(opponentCar);

        opponent1 = raceGenerator.Opponents[Random.Range(0,25) + i];
        opponent2 = raceGenerator.Opponents[Random.Range(0, 25) + i+1];

        newOpponent.Add(opponent1);
        newOpponent.Add(opponent2);

        opponentCar1 = CreateCar(newOpponent[0]);
        opponentCar2 = CreateCar(newOpponent[1]);

        OpponentGameObjects.Add(opponentCar1);
        OpponentGameObjects.Add(opponentCar2);
        CarGameObjects.Add(opponentCar1);
        CarGameObjects.Add(opponentCar2);

        SetOpponentCar(opponentCar1, opponent1);
        SetOpponentCar(opponentCar2, opponent2);
        SetOpponentCar(opponentCar, opponent);


        opponent1Completion = Instantiate(opponentSlider);
        opponent2Completion = Instantiate(opponentSlider);

        opponent1Completion.gameObject.transform.localScale = new Vector3(1, 1, 1);
        opponent1Completion.gameObject.transform.localScale = new Vector3(1, 1, 1);

        opponent1Completion.transform.position = opponentSlider.transform.position;
        opponent2Completion.transform.position = opponentSlider.transform.position;

        opponent1Completion.image.color = Color.red;
        opponent2Completion.image.color = Color.green;

        opponent1Completion.transform.SetParent(UIDataPlanet.Race.transform);
        opponent2Completion.transform.SetParent(UIDataPlanet.Race.transform);

        CarGameObjects.Add(playerCar);
    }
    private GameObject CreateCar(Opponent data)
    {

        GameObject car = Instantiate(raceGenerator.CarData.carPrefabs[data.Car.BodyIndex]);
        car.SetActive(false);
        car.transform.SetParent(raceGenerator.CarGameObjectParent.transform);

        CarObjects carObjects = car.GetComponent<CarObjects>();
        carObjects.AiCarSelfRighting.enabled = false;
        carObjects.userControl.enabled = false;
        carObjects.carAIControl.enabled = false;
        Rigidbody carRigidbody = car.GetComponent<Rigidbody>();
        car.GetComponent<CarObjects>().carCamera.m_Priority = 1;

        MeshRenderer carMeshRender = carObjects.carBody.GetComponent<MeshRenderer>();
        MeshRenderer carSpoilRender = carObjects.spoiler.GetComponent<MeshRenderer>();
        MeshFilter carMeshFilter = carObjects.carBody.GetComponent<MeshFilter>();
        MeshFilter spoilerMeshFilter = carObjects.spoiler.GetComponent<MeshFilter>();

        carMeshRender.materials[0].color = data.Car.ColorTheme.BodyColor;
        carMeshRender.materials[1].color = data.Car.ColorTheme.VinylColor;
        carMeshRender.materials[2].color = data.Car.ColorTheme.GlassColor;
        carObjects.interior.material.color = data.Car.ColorTheme.InteriorColor;
        carSpoilRender.material.color = data.Car.ColorTheme.SpoilerColor;
        for (int i = 0; i < 4; i++)
        {
            MeshRenderer wheelMeshRender = carObjects.wheel[i].GetComponent<MeshRenderer>();
            wheelMeshRender.material.color = data.Car.ColorTheme.TireColor;

            MeshFilter wheelMeshFilter = carObjects.wheel[i].GetComponent<MeshFilter>();
            wheelMeshFilter.mesh = raceGenerator.CarData.carTireMesh[data.Car.TireIndex];
        }
        carMeshRender.materials[1].mainTexture = raceGenerator.CarData.carVinylTexture[data.Car.VinylIndex + (5 * data.Car.BodyIndex)];
        carMeshFilter.mesh = raceGenerator.CarData.carBFMesh[data.Car.BodySkitIndex + (3 * data.Car.BodyIndex)];
        spoilerMeshFilter.mesh = raceGenerator.CarData.carSpoilerMesh[data.Car.SpoilerIndex];

        carMeshRender.materials[0].SetFloat("_Metallic", data.Car.ColorTheme.ColorTypeBody[0]);
        carMeshRender.materials[0].SetFloat("_Smoothness", data.Car.ColorTheme.ColorTypeBody[1]);
        carMeshRender.materials[1].SetFloat("_Metallic", data.Car.ColorTheme.ColorTypeVinyl[0]);
        carMeshRender.materials[1].SetFloat("_Smoothness", data.Car.ColorTheme.ColorTypeVinyl[1]);
        carRigidbody.mass = data.Car.CarMass;
        return car;
    }
    private void SetPlayerCar()
    {     
        playerCarBody = PlayerCar.GetComponent<CarObjects>().carBody.GetComponent<MeshCollider>();

        playerCar.GetComponent<Rigidbody>().mass = (float)(playerData.PlayerCarData.CarMass*1.5);
        playerCar.GetComponent<UnityStandardAssets.Vehicles.Car.CarAudio>().player = true;
        playerCar.GetComponent<CarObjects>().carCamera.m_Priority = 11;
        playerCar.GetComponent<CarObjects>().carAIControl.enabled = false;
        playerCar.GetComponent<CarObjects>().userControl.enabled = true;
        playerCar.GetComponent<CarObjects>().indicatorGameObject.SetActive(true);
        playerCar.GetComponent<CarObjects>().indicatorMeshRenderer.material.color = Color.green;
        playerCar.GetComponent<CarObjects>().userControl.NitroHealth = playerData.PlayerCarData.CarNitroHealth;
        playerCar.GetComponent<CarObjects>().userControl.CurNitroHealth = playerData.PlayerCarData.NitroHealth;
        playerCar.GetComponent<CarObjects>().carController.Topspeed = playerData.PlayerCarData.CarTopSpeed;
        playerCar.GetComponent<CarObjects>().carController.BrakeTorque = playerData.PlayerCarData.CarBreakTorque;
        playerCar.GetComponent<CarObjects>().carController.Downforce = playerData.PlayerCarData.CarDownForce;
        playerCar.GetComponent<CarObjects>().carController.SteerHelper1 = playerData.PlayerCarData.CarSteerHelper;
        playerCar.GetComponent<CarObjects>().carController.TractionControl1 = playerData.PlayerCarData.CarTractionControl;
        playerCar.GetComponent<CarObjects>().carController.FullTorqueOverAllWheels = playerData.PlayerCarData.CarFullTorque;

        if (planetEnvironment.isLightNeeded) playerCar.GetComponent<CarObjects>().headLights.enabled = true;
        else playerCar.GetComponent<CarObjects>().headLights.enabled = false;

    }
    private void SetOpponentCar(GameObject car, Opponent opponent)
    {
        car.GetComponent<Rigidbody>().velocity = new Vector3 (0,0,0);
        car.GetComponent<Rigidbody>().mass = (float)(opponent.Car.CarMass*1.5);
        car.GetComponent<CarObjects>().carCamera.m_Priority = 1;
        car.GetComponent<CarObjects>().indicatorGameObject.SetActive(true);
        car.GetComponent<CarObjects>().indicatorMeshRenderer.material.color = Color.red;
        car.GetComponent<CarObjects>().UserGUIControl.enabled = false;
        car.GetComponent<CarObjects>().carController.Topspeed = opponent.Car.CarTopSpeed;
        car.GetComponent<CarObjects>().AiCarSelfRighting.enabled = false;
        car.GetComponent<CarObjects>().carController.BrakeTorque = opponent.Car.CarBreakTorque;
        car.GetComponent<CarObjects>().carController.Downforce = opponent.Car.CarDownForce;
        car.GetComponent<CarObjects>().carController.SteerHelper1 = opponent.Car.CarSteerHelper;
        car.GetComponent<CarObjects>().carController.TractionControl1 = opponent.Car.CarTractionControl;
        car.GetComponent<CarObjects>().carController.FullTorqueOverAllWheels = opponent.Car.CarFullTorque;
    }

    private void StartOpponentRace(GameObject car, Opponent opponent)
    {
        car.GetComponent<CarObjects>().carAIControl.BrakeCondition1 = UnityStandardAssets.Vehicles.Car.CarAIControl.BrakeCondition.NeverBrake;
        car.GetComponent<CarObjects>().carAIControl.CautiousMaxDistance = 100;
        car.GetComponent<CarObjects>().carAIControl.CautiousSpeedFactor = 0.15f;
        car.GetComponent<CarObjects>().carAIControl.CautiousMaxAngle = 45;

        car.GetComponent<CarObjects>().carAIControl.Laps = this.opponent.Laps;
        car.GetComponent<CarObjects>().carAIControl.TargetListLenght = lenght;
        car.GetComponent<CarObjects>().carAIControl.Driving = true;
        car.GetComponent<CarObjects>().carAIControl.StopWhenTargetReached = true;
        car.GetComponent<CarObjects>().carAIControl.ReachTargetThreshold = 10f;
        car.GetComponent<CarObjects>().userControl.enabled = false;
        car.GetComponent<CarObjects>().UserGUIControl.enabled = false;
        car.GetComponent<CarObjects>().AiCarSelfRighting.enabled = true;
        car.GetComponent<CarObjects>().AiCarSelfRighting.StartRace1 = true;

        car.GetComponent<CarObjects>().DamageSystem.SetData(opponent.Car, false);
        car.GetComponent<CarObjects>().carAIControl.enabled = true;
        if(currentRaceType != RaceType.Reverse)
        {
            car.GetComponent<CarObjects>().carAIControl.Target = pathOne;
            car.GetComponent<CarObjects>().carAIControl.TargetList = pathOneStepOne;
        }

    }
    private void AllCarPositionSet()
    {
        if(currentRaceType == RaceType.Reverse)
        {
            raceReverse = true;
            checkpointsStraight.SetActive(false);
            checkpointReversed.SetActive(true);

            for (int i = 0; i < CarGameObjects.Count; i++)
            {
                CarGameObjects[i].SetActive(true);
                CarGameObjects[i].transform.SetParent(raceGenerator.CarGameObjectParent.transform);
                CarGameObjects[i].transform.localPosition = RaceGenerator.PositionListReverse[i];
                CarGameObjects[i].transform.localRotation =
                    new Quaternion(CarGameObjects[i].transform.localRotation.x,
                    CarGameObjects[i].transform.localRotation.y+187f,
                    CarGameObjects[i].transform.localRotation.z,
                    CarGameObjects[i].transform.localRotation.w
                    );
            }
        }
        else
        {
            raceReverse = false;
            checkpointsStraight.SetActive(true);
            checkpointReversed.SetActive(false);
            for (int i = 0; i < CarGameObjects.Count; i++)
            {
                CarGameObjects[i].SetActive(true);
                CarGameObjects[i].transform.SetParent(raceGenerator.CarGameObjectParent.transform);
                CarGameObjects[i].transform.localPosition = RaceGenerator.PositionListStraight[i];
            }
        }

    }
    private float startTime;
    public void SetRace(GameObject playerCar, GameObject opponentCar,Opponent opponent,string offerType)
    {
        UIDataPlanet.TimePanel.SetActive(false);
        playerSlider.gameObject.SetActive(true);
        opponentSlider.gameObject.SetActive(true);

        //Reset();
        currentRaceType = opponent.RaceType;
        this.opponentCar = opponentCar;
        this.playerCar = playerCar;
        this.opponent = opponent;
        raceStart = false;
        currentOffer = opponent.Offer;
        this.offerType = offerType;

        UIDataPlanet.Controller.SetActive(false);
        UIDataPlanet.StartRace.SetActive(true);
        UIDataPlanet.StartRace.GetComponent<Button>().onClick.RemoveAllListeners();
        UIDataPlanet.StartRace.GetComponent<Button>().onClick.AddListener(StartRace);

        if(currentRaceType == RaceType.Sprint || currentRaceType == RaceType.Reverse)
        {
            DuelRace();
        }
        else if(opponent.RaceType == RaceType.Time)
        {
            Time();
        }
        else if (opponent.RaceType == RaceType.AgainstThree)
        {
            AgainstThreeRace();
        }

        SetPlayerCar();
        SetOpponentCar(opponentCar, opponent);
        AllCarPositionSet();

        //playerCar.transform.position = opponentStartPosition;

        UIDataPlanet.CarControlUI.SetActive(true);
        UIDataPlanet.RaceUI.SetActive(false);


        noOfLaps = opponent.Laps;
        LowerVal = noOfLaps * lenght;

    }
    decimal LowerVal;
    void StartRace()
    {
        raceStart = true;
        startTime = UnityEngine.Time.fixedTime;
        //opponentCar.GetComponent<Rigidbody>().velocity = raceGenerator.ForceOnCar;
        opponentCar.GetComponent<CarObjects>().carAIControl.enabled = true;
        opponentCar.GetComponent<CarObjects>().AiCarSelfRighting.enabled = true;
        if(raceReverse)
        {
            opponentCar.GetComponent<CarObjects>().carAIControl.Target = pathOneReverse;
            opponentCar.GetComponent<CarObjects>().carAIControl.TargetList = pathOneStepOneReverse;
        }
        else
        {
            opponentCar.GetComponent<CarObjects>().carAIControl.Target = pathOne;
            opponentCar.GetComponent<CarObjects>().carAIControl.TargetList = pathOneStepOne;
        }

        if(currentRaceType == RaceType.AgainstThree)
        {
            StartOpponentRace(opponentCar1,opponent1);
            StartOpponentRace(opponentCar2, opponent2);
        }
        StartOpponentRace(opponentCar, opponent);
/*        opponentCar.GetComponent<CarObjects>().carAIControl.BrakeCondition1 = UnityStandardAssets.Vehicles.Car.CarAIControl.BrakeCondition.TargetDistance;
        opponentCar.GetComponent<CarObjects>().carAIControl.CautiousMaxDistance = 15;
        opponentCar.GetComponent<CarObjects>().carAIControl.CautiousSpeedFactor = 0.5f;

        opponentCar.GetComponent<CarObjects>().carAIControl.Laps = opponent.Laps;
        opponentCar.GetComponent<CarObjects>().carAIControl.TargetListLenght = lenght;
        opponentCar.GetComponent<CarObjects>().carAIControl.Driving = true;
        opponentCar.GetComponent<CarObjects>().carAIControl.StopWhenTargetReached = true;
        opponentCar.GetComponent<CarObjects>().carAIControl.ReachTargetThreshold = 10f;
        opponentCar.GetComponent<CarObjects>().userControl.enabled = false;
        opponentCar.GetComponent<CarObjects>().UserGUIControl.enabled = false;
        opponentCar.GetComponent<CarObjects>().AiCarSelfRighting.StartRace1 = true;

        opponentCar.GetComponent<CarObjects>().DamageSystem.SetData(opponent.Car,false);*/
        playerCar.GetComponent<CarObjects>().AiCarSelfRighting.enabled = false;
        playerCar.GetComponent<CarObjects>().DamageSystem.SetData(playerData.PlayerCarData,true);
        playerCar.GetComponent<CarObjects>().userControl.SetNitroHealth(playerData.PlayerCarData.CarNitroHealth);
        UIDataPlanet.Controller.SetActive(true);
        UIDataPlanet.StartRace.SetActive(false);
        
    }

    decimal valuePlayer;
    decimal valueOpponent;
    decimal valueOpponent1;
    decimal valueOpponent2;
    decimal opponentUpperVal1;
    decimal opponentUpperVal2;

    private void FixedUpdate()
    {
        if(time)
        {
            if (totaltime > 0)
            {
                decimal playerUpperVal = (playerCurrentLap * lenght) + playerCurrentCheckpoint;
                try
                {
                    valuePlayer = decimal.Divide(
                    playerUpperVal,
                    LowerVal);
                }
                catch
                {
                    valuePlayer = 0;
                }
                Laps.text = "Laps " + (playerCurrentLap + 1) + "/" + noOfLaps;

                timeLeftText.text = "Time Left: " +(int)totaltime;

                totaltime -= UnityEngine.Time.deltaTime;
                percentageComplete.text = "Race Completion : "+ (int)(valuePlayer*100) +"%";
            }
            else
            {
                if (!gotTheWinner)
                {
                    gotTheWinner = true;
                    playerWin = false;                    
                }
                //playerCar.GetComponent<CarObjects>().carCamera. ;
            }
        }
    }
    private void Update()
    {
       if(!time)
       {
            if (raceStart && !kingChallenge)
            {
                decimal playerUpperVal = (playerCurrentLap * lenght) + playerCurrentCheckpoint;
                if (againstThree)
                {
                    opponentUpperVal1 = (opponentCar1.GetComponent<CarObjects>().carAIControl.CurrentLap * lenght) +
                    opponentCar1.GetComponent<CarObjects>().carAIControl.CurrentTarget;
                    opponentUpperVal2 = (opponentCar2.GetComponent<CarObjects>().carAIControl.CurrentLap * lenght) +
                    opponentCar2.GetComponent<CarObjects>().carAIControl.CurrentTarget;
                }
                decimal opponentUpperVal = (opponentCar.GetComponent<CarObjects>().carAIControl.CurrentLap * lenght) +
                opponentCar.GetComponent<CarObjects>().carAIControl.CurrentTarget;
                if (!gotTheWinner)
                {
                    if (opponentUpperVal == LowerVal)
                    {
                        gotTheWinner = true;
                        opponentCar.GetComponent<CarObjects>().AiCarSelfRighting.StartRace1 = false;
                        playerWin = false;
                    }
                    if (againstThree)
                    {
                        if (opponentUpperVal1 == LowerVal)
                        {
                            gotTheWinner = true;
                            opponentCar.GetComponent<CarObjects>().AiCarSelfRighting.StartRace1 = false;
                            playerWin = false;
                        }
                        if (opponentUpperVal2 == LowerVal)
                        {
                            gotTheWinner = true;
                            opponentCar.GetComponent<CarObjects>().AiCarSelfRighting.StartRace1 = false;
                            playerWin = false;
                        }
                    }
                }

                try
                {
                    valuePlayer = decimal.Divide(
                    playerUpperVal,
                    LowerVal);
                    valueOpponent = decimal.
                    Divide(opponentUpperVal, LowerVal);
                    if (againstThree)
                    {
                        valueOpponent1 = decimal.
                        Divide(opponentUpperVal1, LowerVal);
                        valueOpponent2 = decimal.
                        Divide(opponentUpperVal2, LowerVal);
                    }
                }
                catch
                {
                    valuePlayer = 0;
                    valueOpponent = 0;
                }

                playerSlider.value = (float)valuePlayer;
                opponentSlider.value = (float)valueOpponent;
                if (againstThree)
                {
                    opponent1Completion.value = (float)valueOpponent1;
                    opponent2Completion.value = (float)valueOpponent2;
                }
                Laps.text = "Laps " + (playerCurrentLap + 1) + "/" + noOfLaps;
            }
       }
  
    }
    private void KingFinishRace()
    {
        int i = SceneManager.GetActiveScene().buildIndex - 2;
        UIDataPlanet.KingFinishPanel.SetActive(true);
        playerCar.GetComponent<CarObjects>().carCamera.enabled = false;
        playerData.UpdateCarHealth(playerData.PlayerCarData);

        if (playerWin)
        {
            var defaultText = "You have defeated the king...\nYou are now the new king of this planet...\nYou have won ";
            playerData.IncreaseCoins(2*Constants.planetDescription[i].KingData.Offer);
            UIDataPlanet.KingFinishText.text = defaultText + PRUtils.CurrencyFormater(Constants.planetDescription[i].KingData.Offer.ToString()) + " !!!!";
            playerData.IncreaseReputation(Constants.planetDescription[i].KingData.ReputationRequired*10);
            playerData.KingDefeated(i);
            if (i == 10) { raceGenerator.ending(); }
        }
        else
        {
            UIDataPlanet.KingFinishPanel.SetActive(true);
            UIDataPlanet.KingFinishText.text = "You have lost the challenge. Better luck next time.";

        }

    }
    private void NewCarAchievement()
    {
        // Got A New Car
        Social.ReportProgress("CgkIk4emifEBEAIQDw", 100.0f, (bool success) => {
            // handle success or failure
            Debug.Log("Got A New Car : Unlocked");
        });
        //Car Dealer
        PlayGamesPlatform.Instance.IncrementAchievement(
        "CgkIk4emifEBEAIQEA", 1, (bool success) => {
            Debug.Log("++ winner Car Dealer");
        });
    }
    private void RaceWinIncrement()
    {
        try
        {
            //Sweet Victory
            Social.ReportProgress("CgkIk4emifEBEAIQEQ", 100.0f, (bool success) => {
                // handle success or failure
                Debug.Log("Sweet Victory : Unlocked");
            });

            //Newbie racer
            PlayGamesPlatform.Instance.IncrementAchievement(
            "CgkIk4emifEBEAIQBA", 1, (bool success) => {
                Debug.Log("++ winner Newbie racer");
            });

            //Rookie racer
            PlayGamesPlatform.Instance.IncrementAchievement(
            "CgkIk4emifEBEAIQBQ", 1, (bool success) => {
                Debug.Log("++ winner Rookie racer");
            });

            //Skilled Racer racer
            PlayGamesPlatform.Instance.IncrementAchievement(
            "CgkIk4emifEBEAIQBg", 1, (bool success) => {
                Debug.Log("++ winner Skilled Racer racer");
            });

            //Seasoned  Racer racer
            PlayGamesPlatform.Instance.IncrementAchievement(
            "CgkIk4emifEBEAIQBw", 1, (bool success) => {
                Debug.Log("++ winner Seasoned  Racer racer");
            });

            //Experienced Racer racer
            PlayGamesPlatform.Instance.IncrementAchievement(
            "CgkIk4emifEBEAIQCA", 1, (bool success) => {
                Debug.Log("++ winner Experienced  Racer racer");
            });

            //Expert   Racer racer
            PlayGamesPlatform.Instance.IncrementAchievement(
            "CgkIk4emifEBEAIQCQ", 1, (bool success) => {
                Debug.Log("++ winner Expert Racer racer");
            });

            //Fantasy  Racer racer
            PlayGamesPlatform.Instance.IncrementAchievement(
            "CgkIk4emifEBEAIQCg", 1, (bool success) => {
                Debug.Log("++ winner Fantasy Racer racer");
            });
        }
        catch(System.Exception error)
        {
            Debug.Log("Error Encountered in Win increament Achievement: "+error);
        }

    }
    private float finishTime;
    public void FinishRace()
    {
        if (noOfLaps-1 == playerCurrentLap && raceStart==true)
        {
            finishTime = UnityEngine.Time.fixedTime;
            float TimeTaken = finishTime - startTime;
            curTimeText.text = "Current Timimg: " + TimeTaken + "s";
            float bestTime = playerData.UserData.trackTimeCompleted[SceneManager.GetActiveScene().buildIndex - 2];
            bestTimeText.text = "Best Timimg: " + bestTime + "s";
            Debug.Log("Time Taken: " + TimeTaken);
            try
            {
                
                if (bestTime > TimeTaken || bestTime == 0 )
                {
                    playerData.UserData.trackTimeCompleted[SceneManager.GetActiveScene().buildIndex - 2] = TimeTaken;
                }
            }
            catch(System.Exception error)
            {
                Debug.Log("Error At reading time completion: " + error+" | Applying fix.");
                playerData.UserData.trackTimeCompleted.Add(TimeTaken);
            }
            
            if (kingChallenge)
            {
                raceStart = false;
                UIDataPlanet.Controller.SetActive(false);
                UIDataPlanet.RaceCompletion.SetActive(false);
                KingFinishRace();
            }
            else 
            {
                raceStart = false;
                UIDataPlanet.Controller.SetActive(false);
                UIDataPlanet.RaceCompletion.SetActive(true);
                UIDataPlanet.RaceComtinue.onClick.RemoveAllListeners();
                UIDataPlanet.RaceComtinue.onClick.AddListener(Reset);

                int currentEngineHealth = (int)playerCar.GetComponent<CarObjects>().DamageSystem.EngineHP1;
                int currentBodyHealth = (int)playerCar.GetComponent<CarObjects>().DamageSystem.BodyHP1;
                int currentNitroHealth = (int)playerCar.GetComponent<CarObjects>().userControl.CurNitroHealth;
                int currentTireHealth = (int)playerCar.GetComponent<CarObjects>().DamageSystem.TireHP1;
                if (currentBodyHealth - 300 > 0 && currentEngineHealth - 300 > 0 && currentTireHealth - 300 > 0)
                {
                    currentBodyHealth -= 100;
                    currentEngineHealth -= 100;
                    currentTireHealth -= 100;
                }

                playerData.PlayerCarData.EngineHealth = currentEngineHealth;
                playerData.PlayerCarData.BodyHealth = currentBodyHealth;
                playerData.PlayerCarData.TireHealth = currentTireHealth;
                playerData.PlayerCarData.NitroHealth = currentNitroHealth;

                playerData.UpdateCarHealth(playerData.PlayerCarData);
                UIDataPlanet.EngineHealthSlider.value = (float)decimal.Divide(playerData.PlayerCarData.EngineHealth, playerData.PlayerCarData.CarEngineHealth);
                UIDataPlanet.BodyHealthSlider.value = (float)decimal.Divide(playerData.PlayerCarData.BodyHealth, playerData.PlayerCarData.CarBodyHealth);
                UIDataPlanet.TireHealthSlider.value = (float)decimal.Divide(playerData.PlayerCarData.TireHealth, playerData.PlayerCarData.CarTireHealth);
                UIDataPlanet.NitrosHealthSlider.value = (float)decimal.Divide(playerData.PlayerCarData.NitroHealth, playerData.PlayerCarData.CarNitroHealth);

                if (playerWin)
                {
                    RaceWinIncrement();
                    uIDataPlanet.RaceResultText.text = "Victory";
                    uIDataPlanet.RaceResultIcon.sprite = uIDataPlanet.Victory;
                    playerData.UserData.playerStats[0] += 1;

                    if (offerType == "COINS")
                    {

                        playerData.UserData.playerStats[5] += opponent.Offer;
                        playerData.IncreaseCoins(opponent.Offer);
                        playerData.IncreaseReputation(opponent.Reputation);
                        UIDataPlanet.RaceResult.text = "You have Won " + PRUtils.CurrencyFormater(opponent.Offer.ToString()) + " !!!!!!";
                        UIDataPlanet.RaceComtinue.onClick.RemoveAllListeners();
                        UIDataPlanet.RaceComtinue.onClick.AddListener(Reset);
                    }
                    else if (offerType == "CAR")
                    {
                        NewCarAchievement();
                        playerData.UserData.playerStats[4] += 1;
                        playerData.OpponentNewCar(opponent);
                        playerData.IncreaseReputation(opponent.Reputation);
                        UIDataPlanet.RaceResult.text = "You have Won a car !!!!!!";
                        UIDataPlanet.RaceComtinue.onClick.RemoveAllListeners();
                        UIDataPlanet.RaceComtinue.onClick.AddListener(Reset);
                    }

                }
                else
                {
                    uIDataPlanet.RaceResultText.text = "Defeat";
                    uIDataPlanet.RaceResultIcon.sprite = uIDataPlanet.Defeat;
                    playerData.UserData.playerStats[1] += 1;
                    UIDataPlanet.RaceComtinue.onClick.RemoveAllListeners();
                    UIDataPlanet.RaceComtinue.onClick.AddListener(Reset);
                    UIDataPlanet.RaceResult.text = "You have Lost " + PRUtils.CurrencyFormater(opponent.Offer.ToString()) + "!!!!!!";
                    if (offerType == "CAR")
                    {
                        //Car Lost!!!!
                        Social.ReportProgress("CgkIk4emifEBEAIQIg", 100.0f, (bool success) => {
                            // handle success or failure
                            Debug.Log("Car Lost!!!! : Unlocked");
                        });
                        playerData.RemoveCurrentCar();
                        UIDataPlanet.RaceResult.text = "You have Lost your car!!!!!!";

                        UIDataPlanet.RaceComtinue.onClick.RemoveAllListeners();
                        UIDataPlanet.RaceComtinue.onClick.AddListener(raceGenerator.SendToWorkshop);
                    }

                }


                playerCar.GetComponent<CarObjects>().userControl.enabled = false;
                playerCar.GetComponent<CarObjects>().carAIControl.enabled = true;
                playerCar.GetComponent<CarObjects>().carAIControl.Target = pathOne;
                playerCar.GetComponent<CarObjects>().carAIControl.TargetList = pathOneStepOne;
                playerCar.GetComponent<CarObjects>().carAIControl.Laps = opponent.Laps;
                playerCar.GetComponent<CarObjects>().carAIControl.TargetListLenght = lenght;
                playerCar.GetComponent<CarObjects>().carAIControl.Driving = true;
                playerCar.GetComponent<CarObjects>().carAIControl.StopWhenTargetReached = true;
                playerCar.GetComponent<CarObjects>().carAIControl.ReachTargetThreshold = 30f;
                playerCar.GetComponent<CarObjects>().userControl.enabled = false;

            }

        }
    }

    void Reset()
    {
        playerData.UpdateUI();
        playerCurrentLap = 0;
        playerCurrentCheckpoint = 0;
        playerPreviousCheckpoint = -1;
        CarGameObjects = new List<GameObject>();
        OpponentGameObjects = new List<GameObject>();
        noOfLaps = 0;
        gotTheWinner = false;
        opponent.Offer = raceGenerator.OriginalOffer;
        UIDataPlanet.RaceUI.SetActive(true);
        UIDataPlanet.CarControlUI.SetActive(false);
        UIDataPlanet.RaceCompletion.SetActive(false);
        Destroy(playerCar);
        Destroy(opponentCar);
        StartCoroutine(raceGenerator.BeginLevel());
        raceReverse = false;
        againstThree = false;
        time = false;
        UIDataPlanet.TimePanel.SetActive(false);
        playerSlider.gameObject.SetActive(true);
        opponentSlider.gameObject.SetActive(true);
        /*        if (againstThree)
                {
                    Destroy(opponentCar1);
                    Destroy(opponentCar2);
                    Destroy(opponent1Completion);
                    Destroy(opponent2Completion);
                }*/
        Destroy(opponent1Completion.gameObject);
        Destroy(opponent2Completion.gameObject);
        Destroy(opponentCar1);
        Destroy(opponentCar2);
        rewardedADS.LoadRewardedAdLow();

    }
}
