using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDataPlanet : MonoBehaviour
{
    [SerializeField] private Animator opponentPanel;
    [SerializeField] private GameObject opponentInfoPanel;
    [SerializeField] private GameObject raceUI;
    [SerializeField] private GameObject race;
    [SerializeField] private GameObject carControlUI;
    [SerializeField] private Slider playerReputationSlider;
    [SerializeField] private TMP_Text playerCoinsText;
    [SerializeField] public Animator raceOnTracks;
    [SerializeField] private TMP_Text playerReputationText;
    [SerializeField] private TMP_Text alienNameText;
    [SerializeField] private TMP_Text alienReputationText;
    [SerializeField] private TMP_Text alienAboutText;
    [SerializeField] private TMP_Text alienOfferText;
    [SerializeField] private TMP_Text noOfLapsText;
    [SerializeField] private Button offerAcceptButton;
    [SerializeField] private Button offerDeclineButton;
    [SerializeField] private GameObject offerChangeButton;
    [SerializeField] private Button offerIncreaseButton;
    [SerializeField] private Button offerDecreaseButton;
    [SerializeField] private Button offerTypeChange;


    [SerializeField] private Image alienImage;
    [SerializeField] private Image duelRaceImage;
    [SerializeField] private Image reverseRaceImage;
    [SerializeField] private Image FourRaceImage;
    [SerializeField] private Image RaceImage;
    [SerializeField] private Image currencyTypeImage;

    [SerializeField] private Sprite coins;
    [SerializeField] private Sprite cars;
    [SerializeField] private Sprite cross;
    [SerializeField] private Sprite car;

    public Animator OpponentPanel { get => opponentPanel; set => opponentPanel = value; }
    public GameObject RaceUI { get => raceUI; set => raceUI = value; }
    public GameObject CarControlUI { get => carControlUI; set => carControlUI = value; }
    public Slider PlayerReputationSlider { get => playerReputationSlider; set => playerReputationSlider = value; }
    public TMP_Text PlayerCoinsText { get => playerCoinsText; set => playerCoinsText = value; }
    public TMP_Text PlayerReputationText { get => playerReputationText; set => playerReputationText = value; }
    public TMP_Text AlienNameText { get => alienNameText; set => alienNameText = value; }
    public TMP_Text AlienReputationText { get => alienReputationText; set => alienReputationText = value; }
    public TMP_Text AlienAboutText { get => alienAboutText; set => alienAboutText = value; }
    public TMP_Text AlienOfferText { get => alienOfferText; set => alienOfferText = value; }
    public TMP_Text NoOfLapsText { get => noOfLapsText; set => noOfLapsText = value; }
    public Button OfferAcceptButton { get => offerAcceptButton; set => offerAcceptButton = value; }
    public Button OfferDeclineButton { get => offerDeclineButton; set => offerDeclineButton = value; }
    public GameObject OfferChangeButton { get => offerChangeButton; set => offerChangeButton = value; }
    public Button OfferIncreaseButton { get => offerIncreaseButton; set => offerIncreaseButton = value; }
    public Button OfferDecreaseButton { get => offerDecreaseButton; set => offerDecreaseButton = value; }
    public Button OfferTypeChange { get => offerTypeChange; set => offerTypeChange = value; }
    public Image AlienImage { get => alienImage; set => alienImage = value; }
    public Image DuelRaceImage { get => duelRaceImage; set => duelRaceImage = value; }
    public Image ReverseRaceImage { get => reverseRaceImage; set => reverseRaceImage = value; }
    public Image FourRaceImage1 { get => FourRaceImage; set => FourRaceImage = value; }
    public Image RaceImage1 { get => RaceImage; set => RaceImage = value; }
    public Image CurrencyTypeImage { get => currencyTypeImage; set => currencyTypeImage = value; }
    public TMP_Text NoOfLaps { get => noOfLaps; set => noOfLaps = value; }
    public Slider NitrosSlider { get => nitrosSlider; set => nitrosSlider = value; }
    public GameObject Totalled1 { get => Totalled; set => Totalled = value; }
    public TMP_Text Reason { get => reason; set => reason = value; }
    public Button ReturnHome { get => returnHome; set => returnHome = value; }
    public GameObject RaceCompletion { get => raceCompletion; set => raceCompletion = value; }
    public TMP_Text RaceResult { get => raceResult; set => raceResult = value; }
    public Slider EngineHealthSlider { get => engineHealthSlider; set => engineHealthSlider = value; }
    public Slider BodyHealthSlider { get => bodyHealthSlider; set => bodyHealthSlider = value; }
    public Slider TireHealthSlider { get => tireHealthSlider; set => tireHealthSlider = value; }
    public Slider NitrosHealthSlider { get => nitrosHealthSlider; set => nitrosHealthSlider = value; }
    public Button RaceComtinue { get => raceComtinue; set => raceComtinue = value; }
    public GameObject Message { get => message; set => message = value; }
    public TMP_Text MessageText { get => messageText; set => messageText = value; }
    public GameObject Controller { get => controller; set => controller = value; }
    public GameObject StartRace { get => startRace; set => startRace = value; }
    public Slider PlayerCompletionSlider { get => playerCompletionSlider; set => playerCompletionSlider = value; }
    public Slider OpponentCompletionSlider { get => opponentCompletionSlider; set => opponentCompletionSlider = value; }
    public GameObject DeclineOffer { get => declineOffer; set => declineOffer = value; }
    public TMP_Text DeclineReason { get => declineReason; set => declineReason = value; }
    public Button DeclineOfferContinueButton { get => declineOfferContinueButton; set => declineOfferContinueButton = value; }
    public GameObject OpponentInfoPanel { get => opponentInfoPanel; set => opponentInfoPanel = value; }
    public Sprite Coins { get => coins; set => coins = value; }
    public Sprite Cars { get => cars; set => cars = value; }
    public GameObject GoHome { get => goHome; set => goHome = value; }
    public Button GoHomeButton { get => goHomeButton; set => goHomeButton = value; }
    public Image SliderOpponentImage { get => sliderOpponentImage; set => sliderOpponentImage = value; }
    public Image SliderPlayerImage { get => sliderPlayerImage; set => sliderPlayerImage = value; }
    public Sprite Cross { get => cross; set => cross = value; }
    public Sprite Car { get => car; set => car = value; }
    public GameObject KingPanel { get => kingPanel; set => kingPanel = value; }
    public Button KingButton { get => kingButton; set => kingButton = value; }
    public TMP_Text KingTitle { get => kingTitle; set => kingTitle = value; }
    public TMP_Text KingDescription { get => kingDescription; set => kingDescription = value; }
    public Button ChallengeButton { get => challengeButton; set => challengeButton = value; }
    public Button KingPanelCloseButton { get => kingPanelCloseButton; set => kingPanelCloseButton = value; }
    public Slider RaceEngineHealth { get => raceEngineHealth; set => raceEngineHealth = value; }
    public Slider RaceBodyHealth { get => raceBodyHealth; set => raceBodyHealth = value; }
    public Slider RaceTireHealth { get => raceTireHealth; set => raceTireHealth = value; }
    public GameObject KingFinishPanel { get => kingFinishPanel; set => kingFinishPanel = value; }
    public TMP_Text KingFinishText { get => kingFinishText; set => kingFinishText = value; }
    public Button KingGoHomeButton { get => kingGoHomeButton; set => kingGoHomeButton = value; }
    public GameObject Race { get => race; set => race = value; }
    public GameObject TimePanel { get => timePanel; set => timePanel = value; }
    public TMP_Text TimeLeftText { get => timeLeftText; set => timeLeftText = value; }
    public TMP_Text RaceCompletionPercentage { get => raceCompletionPercentage; set => raceCompletionPercentage = value; }
    public TMP_Text KingRepReq { get => kingRepReq; set => kingRepReq = value; }
    public TMP_Text KingLaps { get => kingLaps; set => kingLaps = value; }
    public TMP_Text KingOffer { get => kingOffer; set => kingOffer = value; }
    public Image KingPicture { get => kingPicture; set => kingPicture = value; }
    public TMP_Text RaceResultText { get => raceResultText; set => raceResultText = value; }
    public Image RaceResultIcon { get => raceResultIcon; set => raceResultIcon = value; }
    public Sprite Victory { get => victory; set => victory = value; }
    public Sprite Defeat { get => defeat; set => defeat = value; }

    [SerializeField] private TMP_Text noOfLaps;
    [SerializeField] private Slider playerCompletionSlider;
    [SerializeField] private Slider opponentCompletionSlider;
    [SerializeField] private Slider nitrosSlider;

    [SerializeField] private GameObject Totalled;
    [SerializeField] private GameObject controller;
    [SerializeField] private GameObject startRace;
    [SerializeField] private TMP_Text reason;
    [SerializeField] private Button returnHome;

    [SerializeField] private GameObject raceCompletion;
    [SerializeField] private TMP_Text raceResult;
    [SerializeField] private Slider engineHealthSlider;
    [SerializeField] private Slider bodyHealthSlider;
    [SerializeField] private Slider tireHealthSlider;
    [SerializeField] private Slider nitrosHealthSlider;
    [SerializeField] private Button raceComtinue;

    [SerializeField] private GameObject message;
    [SerializeField] private TMP_Text messageText;


    [SerializeField] private GameObject declineOffer;
    [SerializeField] private TMP_Text declineReason;
    [SerializeField] private Button declineOfferContinueButton;

    [SerializeField] private GameObject goHome;
    [SerializeField] private Button goHomeButton;

    [SerializeField] private Image sliderOpponentImage;
    [SerializeField] private Image sliderPlayerImage;

    [SerializeField] private GameObject kingPanel;
    [SerializeField] private Button kingButton;
    [SerializeField] private TMP_Text kingTitle;
    [SerializeField] private TMP_Text kingDescription;
    [SerializeField] private TMP_Text kingRepReq;
    [SerializeField] private TMP_Text kingLaps;
    [SerializeField] private TMP_Text kingOffer;
    [SerializeField] private Image kingPicture;
    [SerializeField] private Button challengeButton;
    [SerializeField] private Button kingPanelCloseButton;

    [SerializeField] private Slider raceEngineHealth;
    [SerializeField] private Slider raceBodyHealth;
    [SerializeField] private Slider raceTireHealth;

    [SerializeField] private GameObject kingFinishPanel;
    [SerializeField] private TMP_Text kingFinishText;
    [SerializeField] private Button kingGoHomeButton;

    [SerializeField] private GameObject timePanel;
    [SerializeField] private TMP_Text timeLeftText;
    [SerializeField] private TMP_Text raceCompletionPercentage;

    [SerializeField] private TMP_Text raceResultText;
    [SerializeField] private Image raceResultIcon;

    [SerializeField] private Sprite victory;
    [SerializeField] private Sprite defeat;

}
