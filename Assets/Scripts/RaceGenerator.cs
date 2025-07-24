using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using alias = System;

public class RaceGenerator : MonoBehaviour
{
    [SerializeField] private Button accept;
    [SerializeField] private Button decline;
    [SerializeField] private Button continueB;
    [SerializeField] private CarData carData;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private CurrencyData currencyData;
    [SerializeField] private RaceData raceData;
    [SerializeField] private AlienData alienData;
    [SerializeField] private UIDataPlanet uIDataPlanet;
    [SerializeField] private GameObject carGameObjectParent;
    public readonly static Vector3 opponentPosition = new Vector3(-23.97f, 4.07f, -10.82f);
    public readonly static Vector3 opponentReachPosition = new Vector3(-20.99f, 4.07f, 16.41f);
    public readonly static Vector3 destroyPosition = new Vector3(-16.99f, 1.28f, 52.53f);
    public readonly static List<Vector3> PositionListStraight = new List<Vector3>()
    {
        new Vector3(-32.23f,4.07f,2.57f),
        new Vector3(-22.66f,4.07f,1.51f),
        new Vector3(-33.54f,4.07f,-9.76f),
        new Vector3(-23.97f,4.07f,-10.82f)
    };
    public readonly static List<Vector3> PositionListReverse = new List<Vector3>()
    {
        new Vector3(-31.12f,4.07f,11.34f),
        new Vector3(-21.52f,4.07f,10.24f),
        new Vector3(-29.82f,4.07f,23.64f),
        new Vector3(-20.22f,4.07f,22.54f)
    };

    [SerializeField] private int worldLevel;
    [SerializeField] private Vector3 forceOnCar;
    private int currentCar = 0;
    private GameObject playerCar;
    private GameObject opponentCar;

    private bool offerAccepted = true;

    private bool carStopped;

    private bool prevCarAvailable;
    private int originalOffer;
    private string offerType;
    private List<bool> listBool;
    private int noOfCarLoops;


    //TOHOME
    [SerializeField] private GameObject loadingScene;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text percent;
    private void UIRaceTypeUpdate(RaceData.RaceType type)
    {
        uIDataPlanet.DuelRaceImage.color = Color.grey;
        uIDataPlanet.ReverseRaceImage.color = Color.grey;
        uIDataPlanet.FourRaceImage1.color = Color.grey;
        uIDataPlanet.RaceImage1.color = Color.grey;
        if (type == RaceData.RaceType.Sprint)
        {
            uIDataPlanet.DuelRaceImage.color = Color.white;
        }
        if (type == RaceData.RaceType.Reverse)
        {
            uIDataPlanet.ReverseRaceImage.color = Color.white;
        }
        if (type == RaceData.RaceType.Time)
        {
            uIDataPlanet.FourRaceImage1.color = Color.white;
        }
        if (type == RaceData.RaceType.AgainstThree)
        {
            uIDataPlanet.RaceImage1.color = Color.white;
        }

    }
    private void Start()
    {

        currentCar = 0;
        noOfCarLoops = 0;
        prevCarAvailable = false;
        worldLevel = SceneManager.GetActiveScene().buildIndex;
        uIDataPlanet.ChallengeButton.onClick.AddListener(DeleteCurrentCar);

        StartCoroutine(Loading());
        StartCoroutine(BeginLevel());
    }
    IEnumerator Loading()
    {

        yield return new WaitForSeconds(0.1f);
        int j = SceneManager.GetActiveScene().buildIndex - 2;
        int condition = playerData.CheckKingDefeated(j);
        if (condition == 1)
        {
            uIDataPlanet.KingButton.gameObject.SetActive(false);
        }

        for (int i = 0; i < 10; i++)
        {
            GenerateOpponents();
        }
    }
    public IEnumerator BeginLevel()
    {
        yield return new WaitForSeconds(0.5f);
        offerAccepted = true;
        offerType = "COINS";
        listBool = new List<bool>()
        {
            true,true,true,true,true,true,true,true,true,true
        };
        GenerateCarWithUI();
        uIDataPlanet.OfferDeclineButton.onClick.RemoveAllListeners();
        uIDataPlanet.OfferAcceptButton.onClick.RemoveAllListeners();
        uIDataPlanet.OfferDeclineButton.onClick.AddListener(NextCar);
        uIDataPlanet.OfferAcceptButton.onClick.AddListener(SetRace);

        uIDataPlanet.OfferIncreaseButton.onClick.RemoveAllListeners();
        uIDataPlanet.OfferDecreaseButton.onClick.RemoveAllListeners();
        uIDataPlanet.OfferIncreaseButton.onClick.AddListener(IncreaseOffer);
        uIDataPlanet.OfferDecreaseButton.onClick.AddListener(DecreaseOffer);

        uIDataPlanet.OfferTypeChange.onClick.RemoveAllListeners();
        uIDataPlanet.OfferTypeChange.onClick.AddListener(TypeChangeCarOffer);
    }
    int step = 0;

    void IncreaseOffer()
    {

        if (step < 10)
        {

            step++;
            listBool.Add(false);
            opponents[currentCar].Offer += opponents[currentCar].OfferChange;
            uIDataPlanet.AlienOfferText.text = PRUtils.CurrencyFormater(opponents[currentCar].Offer.ToString());
            uIDataPlanet.OfferChangeButton.SetActive(true);
            uIDataPlanet.OfferChangeButton.GetComponent<Button>().onClick.RemoveAllListeners();
            uIDataPlanet.OfferChangeButton.GetComponent<Button>().onClick.AddListener(SetRace);
            if (opponents[currentCar].Offer == originalOffer)
            {
                uIDataPlanet.OfferChangeButton.SetActive(false);
            }
        }

    }
    void DecreaseOffer()
    {

        if (step > -10 && opponents[currentCar].Offer > opponents[currentCar].OfferChange)
        {

            step--;
            listBool.Add(true);
            opponents[currentCar].Offer -= opponents[currentCar].OfferChange;
            uIDataPlanet.AlienOfferText.text = PRUtils.CurrencyFormater(opponents[currentCar].Offer.ToString());
            uIDataPlanet.OfferChangeButton.SetActive(true);
            uIDataPlanet.OfferChangeButton.GetComponent<Button>().onClick.RemoveAllListeners();
            uIDataPlanet.OfferChangeButton.GetComponent<Button>().onClick.AddListener(SetRace);
            if (opponents[currentCar].Offer == originalOffer)
            {
                uIDataPlanet.OfferChangeButton.SetActive(false);
            }
        }
    }

    void TypeChangeCarOffer()
    {
        if (offerType == "COINS")
        {
            offerType = "CAR";
            uIDataPlanet.CurrencyTypeImage.sprite = uIDataPlanet.Cars;
            uIDataPlanet.AlienOfferText.text = "Car";
            uIDataPlanet.OfferChangeButton.SetActive(true);
            uIDataPlanet.OfferIncreaseButton.enabled = false;
            uIDataPlanet.OfferDecreaseButton.enabled = false;
            uIDataPlanet.OfferChangeButton.GetComponent<Button>().onClick.RemoveAllListeners();
            uIDataPlanet.OfferChangeButton.GetComponent<Button>().onClick.AddListener(SetRace);

            uIDataPlanet.OfferTypeChange.onClick.RemoveAllListeners();
            uIDataPlanet.OfferTypeChange.onClick.AddListener(TypeChangeCoinsOffer);
        }


    }
    void TypeChangeCoinsOffer()
    {
        if (offerType == "CAR")
        {
            offerType = "COINS";
            uIDataPlanet.CurrencyTypeImage.sprite = uIDataPlanet.Coins;
            uIDataPlanet.OfferIncreaseButton.enabled = true;
            uIDataPlanet.OfferDecreaseButton.enabled = true;
            step = 0;
            opponents[currentCar].Offer = originalOffer;
            uIDataPlanet.AlienOfferText.text = PRUtils.CurrencyFormater(opponents[currentCar].Offer.ToString());
            uIDataPlanet.OfferChangeButton.SetActive(false);

            uIDataPlanet.OfferTypeChange.onClick.RemoveAllListeners();
            uIDataPlanet.OfferTypeChange.onClick.AddListener(TypeChangeCarOffer);
        }
    }

    private readonly static Dictionary<int, List<int>> probabilties = new Dictionary<int, List<int>>() {
        {2, new List<int>(){3, 3, 2,0,1,0,1,0} },
        {3, new List<int>(){2,3, 3,0,0,1,0,1} },
        {4, new List<int>(){0, 2, 3, 3,1,0,1} },
        {5, new List<int>(){0, 1, 2, 3,2,1,0,1} },
        {6, new List<int>(){0, 2, 1, 2,2,2,0,0,1} },
        {7, new List<int>(){0, 0, 2, 1,2,2,2,0,1} },
        {8, new List<int>(){0, 0, 0, 1,2,3,2,1,0,1} },
        {9, new List<int>(){0, 0, 0, 0,1,2,2,2,1,1,1} },
        {10, new List<int>(){0, 0, 0, 0,0,0,1,2,3,4} },
        {11, new List<int>(){0, 0, 0, 0,0,1,2,1,3,3,1} },
        {12, new List<int>(){0, 0, 0, 0,0,0,1,2,2,3,2} },
        {13, new List<int>(){0, 0, 0, 0,0,0,0,1,2,3,4} },

    };

    private readonly List<Opponent> opponents = new List<Opponent>();

    List<int> WorldLevelDataCreator(int Level)
    {
        var currProb = probabilties[Level];
        for (int i = 0; i < currProb.Count; i++)
        {
            int temp = currProb[i];
            for (int j = 0; j < temp; j++)
            {
                opponents.Add(Opponent.FromLevel(i + 1, alienData));
            };
        };

        //opponents.ForEach(e => Debug.Log(e));

        return currProb;
    }

    void GenerateOpponents()
    {
        List<int> opponentData = WorldLevelDataCreator(worldLevel);

    }
    GameObject car;
    GameObject PreviousCar;
    CarObjects carObjects;

    public Vector3 ForceOnCar { get => forceOnCar; set => forceOnCar = value; }
    public int OriginalOffer { get => originalOffer; set => originalOffer = value; }
    public GameObject CarGameObjectParent { get => carGameObjectParent; set => carGameObjectParent = value; }
    public CarData CarData { get => carData; set => carData = value; }
    public AlienData AlienData { get => alienData; set => alienData = value; }

    public List<Opponent> Opponents => opponents;

    void GenerateCarWithUI()
    {
        offerAccepted = true;
        offerType = "COINS";
        uIDataPlanet.CurrencyTypeImage.sprite = uIDataPlanet.Coins;
        originalOffer = opponents[currentCar].Offer - ((noOfCarLoops * 25) * (opponents[currentCar].Offer / 100));
        opponents[currentCar].Offer = originalOffer;
        uIDataPlanet.OfferChangeButton.SetActive(false);
        UIRaceTypeUpdate(opponents[currentCar].RaceType);
        listBool = new List<bool>()
        {
            true,true,true,true,true,true
        };
        step = 0;

        car = Instantiate(carData.carPrefabs[opponents[currentCar].Car.BodyIndex]);
        car.transform.SetParent(carGameObjectParent.transform);
        car.transform.localPosition = opponentPosition;
        car.SetActive(true);
        carObjects = car.GetComponent<CarObjects>();
        carObjects.AiCarSelfRighting.enabled = false;
        Rigidbody carRigidbody = car.GetComponent<Rigidbody>();
        car.GetComponent<CarObjects>().carCamera.m_Priority = 1;

        MeshRenderer carMeshRender = carObjects.carBody.GetComponent<MeshRenderer>();
        MeshRenderer carSpoilRender = carObjects.spoiler.GetComponent<MeshRenderer>();
        MeshFilter carMeshFilter = carObjects.carBody.GetComponent<MeshFilter>();
        MeshFilter spoilerMeshFilter = carObjects.spoiler.GetComponent<MeshFilter>();

        carMeshRender.materials[0].color = opponents[currentCar].Car.ColorTheme.BodyColor;
        carMeshRender.materials[1].color = opponents[currentCar].Car.ColorTheme.VinylColor;
        carMeshRender.materials[2].color = opponents[currentCar].Car.ColorTheme.GlassColor;
        carObjects.interior.material.color = opponents[currentCar].Car.ColorTheme.InteriorColor;
        carSpoilRender.material.color = opponents[currentCar].Car.ColorTheme.SpoilerColor;
        for (int i = 0; i < 4; i++)
        {
            MeshRenderer wheelMeshRender = carObjects.wheel[i].GetComponent<MeshRenderer>();
            wheelMeshRender.material.color = opponents[currentCar].Car.ColorTheme.TireColor;

            MeshFilter wheelMeshFilter = carObjects.wheel[i].GetComponent<MeshFilter>();
            wheelMeshFilter.mesh = carData.carTireMesh[opponents[currentCar].Car.TireIndex];
        }
        carMeshRender.materials[1].mainTexture = carData.carVinylTexture[opponents[currentCar].Car.VinylIndex + (5 * opponents[currentCar].Car.BodyIndex)];
        carMeshFilter.mesh = carData.carBFMesh[opponents[currentCar].Car.BodySkitIndex + (3 * opponents[currentCar].Car.BodyIndex)];
        spoilerMeshFilter.mesh = carData.carSpoilerMesh[opponents[currentCar].Car.SpoilerIndex];

        carRigidbody.velocity = forceOnCar;

        uIDataPlanet.AlienNameText.text = opponents[currentCar].OpponentName;
        uIDataPlanet.AlienAboutText.text = opponents[currentCar].About;
        uIDataPlanet.AlienOfferText.text = PRUtils.CurrencyFormater(opponents[currentCar].Offer.ToString());
        uIDataPlanet.NoOfLapsText.text = opponents[currentCar].Laps.ToString();
        uIDataPlanet.AlienImage.sprite = opponents[currentCar].Image;
        carMeshRender.materials[0].SetFloat("_Metallic", opponents[currentCar].Car.ColorTheme.ColorTypeBody[0]);
        carMeshRender.materials[0].SetFloat("_Smoothness", opponents[currentCar].Car.ColorTheme.ColorTypeBody[1]);
        carMeshRender.materials[1].SetFloat("_Metallic", opponents[currentCar].Car.ColorTheme.ColorTypeVinyl[0]);
        carMeshRender.materials[1].SetFloat("_Smoothness", opponents[currentCar].Car.ColorTheme.ColorTypeVinyl[1]);

        uIDataPlanet.AlienReputationText.text = "Reputation : " + opponents[currentCar].Reputation.ToString();

        if ((opponents[currentCar].Reputation - playerData.GetPlayerReputation()) > 2 && UnityEngine.Random.RandomRange(0,2)==0)
        {
            uIDataPlanet.OpponentInfoPanel.SetActive(false);
            uIDataPlanet.DeclineOffer.SetActive(true);
            uIDataPlanet.DeclineReason.text = PRUtils.GetSingle(reject) + "(Rejected due to low reputation)";
            uIDataPlanet.DeclineOfferContinueButton.onClick.RemoveAllListeners();
            uIDataPlanet.DeclineOfferContinueButton.onClick.AddListener(NextCar);

        }

        carStopped = false;
    }

    readonly List<string> reject = new List<string>()
        {
            "I understand why you all may be hesitant to race against me, but I assure you that I am a fair and skilled racer.",
            "It's a shame that my reputation precedes me and you all are unwilling to race. I hope to prove myself and earn your respect in the future.",
            "I can understand your concerns, but I assure you that I will race with integrity and sportsmanship.",
            "I may have a tarnished reputation, but I promise to show you all that I am a capable and worthy racer.",
            "I respect your decision not to race against me, but I hope that someday you will see the racer I truly am and give me the chance to prove myself.",
            "Well isn't this just disappointing. I guess all of you are too afraid to race against a real competitor.",
            "Hmph, I see that your lack of confidence is preventing you from racing against me. What a shame.",
            "It seems like none of you have the guts to race against me. I guess I'll have to find more worthy opponents.",
            "So much for the supposed elite racers of this galaxy. I guess I expected too much from all of you.",
            "I'm not surprised by your decision not to race against me. It takes a real champion to race with the best, and clearly none of you fit the bill."
        };



    private void Update()
    {

        if (Vector3.Distance(car.transform.localPosition, opponentReachPosition) < 1.5f && !carStopped)
        {

            carStopped = true;
            accept.enabled = true;
            decline.enabled = true;
            continueB.enabled = true;

            car.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            car.GetComponent<Rigidbody>().isKinematic = true;
           uIDataPlanet.OpponentPanel.SetTrigger("racePanelOn");

        }

        if(prevCarAvailable)
        {
            accept.enabled = false;
            decline.enabled = false;
            continueB.enabled = false;

            StartCoroutine(DestroyCar());
            /*            if (Vector3.Distance(PreviousCar.transform.localPosition, destroyPosition) < 10f)
                        {
                            StartCoroutine(DestroyCar());

                        }*/
        }

    }

    IEnumerator DestroyCar()
    {
        prevCarAvailable = false;
        yield return new WaitForSeconds(2.5f);

        Destroy(PreviousCar);
    }

    void NextCar()
    {
        if (noOfCarLoops == 3)
        {
            SendToWorkshop();
        }
        else
        {
            uIDataPlanet.OpponentInfoPanel.SetActive(true);
            uIDataPlanet.DeclineOffer.SetActive(false);
            PreviousCar = car;
            prevCarAvailable = true;

            currentCar++;
            if (currentCar > 9)
            {
                currentCar = 0;
                noOfCarLoops++;
            }

            /*        currentCar = (++currentCar) % 10;*/
            GenerateCarWithUI();

            uIDataPlanet.OpponentPanel.SetTrigger("racePanelOff");
            PreviousCar.GetComponent<Rigidbody>().isKinematic = false;
            PreviousCar.GetComponent<Rigidbody>().velocity = forceOnCar;

        }



    }

    public void SendToWorkshop()
    {
        uIDataPlanet.GoHome.SetActive(true);
        uIDataPlanet.RaceUI.SetActive(false);
    }
    public void GOHOME()
    {
        StartCoroutine(LoadAsynchronously(1));
    }
    public void ending()
    {
        StartCoroutine(LoadAsynchronously(15));
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        loadingScene.SetActive(true);
        yield return new WaitForSeconds(1);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);



        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            int val = Convert.ToInt32(progress * 100);
            percent.text = val + "%";

            yield return null;
        }
    }

    void SetRace()
    {
        car.GetComponent<Rigidbody>().isKinematic = false;
        if (offerType == "COINS")
        {
            offerAccepted = PRUtils.GetSingle(listBool);
            if (offerAccepted )
            {
                if(opponents[currentCar].Offer < playerData.GetPlayerCoin())
                {
                    playerData.DecreaseCoins(opponents[currentCar].Offer);
                    playerCar = playerData.CreateCar();
                    opponentCar = car;
                    raceData.SetRace(playerCar, opponentCar, opponents[currentCar], offerType);
                    //opponents[currentCar].Offer = originalOffer;
                    currentCar++;
                }
                else
                {
                    uIDataPlanet.OpponentInfoPanel.SetActive(false);
                    uIDataPlanet.DeclineOffer.SetActive(true);
                    uIDataPlanet.DeclineReason.text = "Your offer has been decline. Insufficient Coins.";
                    opponents[currentCar].Offer = originalOffer;

                    uIDataPlanet.DeclineOfferContinueButton.onClick.RemoveAllListeners();
                    uIDataPlanet.DeclineOfferContinueButton.onClick.AddListener(NextCar);
                }

            }
            else
            {
                uIDataPlanet.OpponentInfoPanel.SetActive(false);
                uIDataPlanet.DeclineOffer.SetActive(true);
                uIDataPlanet.DeclineReason.text = "Your offer has been decline.";
                opponents[currentCar].Offer = originalOffer;

                uIDataPlanet.DeclineOfferContinueButton.onClick.RemoveAllListeners();
                uIDataPlanet.DeclineOfferContinueButton.onClick.AddListener(NextCar);
            }
        }
        if(offerType == "CAR")
        {
            if (opponents[currentCar].CarOffer && 
            playerData.UserData.playerEngine.Count > 1 && playerData.UserData.playerNitros.Count > 1
            && playerData.UserData.playerBody.Count > 1)
            {
                playerData.DecreaseCoins(opponents[currentCar].Offer);
                playerCar = playerData.CreateCar();
                opponentCar = car;

                raceData.SetRace(playerCar, opponentCar, opponents[currentCar],offerType
                    );
                currentCar++;
            }
            else
            {
                if(!opponents[currentCar].CarOffer)
                {
                    uIDataPlanet.DeclineReason.text = "Your offer has been decline.";
                }
                else
                {
                    uIDataPlanet.DeclineReason.text = "You don't have more than one car set combination required.";
                }
                uIDataPlanet.OpponentInfoPanel.SetActive(false);
                uIDataPlanet.DeclineOffer.SetActive(true);

                uIDataPlanet.CurrencyTypeImage.sprite = uIDataPlanet.Coins;
                uIDataPlanet.OfferIncreaseButton.enabled = true;
                uIDataPlanet.OfferDecreaseButton.enabled = true;

                uIDataPlanet.OfferChangeButton.SetActive(false);

                uIDataPlanet.OfferTypeChange.onClick.RemoveAllListeners();
                uIDataPlanet.OfferTypeChange.onClick.AddListener(TypeChangeCarOffer);
                uIDataPlanet.DeclineOfferContinueButton.onClick.RemoveAllListeners();
                uIDataPlanet.DeclineOfferContinueButton.onClick.AddListener(NextCar);
            }
        }


    }
    public void RemoveOpponentFromList()
    {
        opponents.RemoveAt(currentCar-1);
    }
    private void DeleteCurrentCar()
    {
        car.SetActive(false);
    }

    private void EnableCurrentCar()
    {
        car.SetActive(true);
    }

}