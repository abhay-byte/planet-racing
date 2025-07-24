using UnityEngine;
using Assets.Scripts.Models;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Opponent
{
    // Opponent Data
    private string opponentName ;
    private string about;
    private int laps;
    private RaceData.RaceType raceType ;
    private int offer ;
    private UnityEngine.Sprite image;
    private int reputation;
    private int offerChange;
    private bool carOffer;
    private int timeRequired;

    // Car Data
    private Car car;

    public Opponent(string opponentName, string about, int laps, RaceData.RaceType raceType, int offer, Sprite image, int reputation, Car car, int offerChange, bool carOffer, int timeRequired)
    {
        this.opponentName = opponentName;
        this.about = about;
        this.laps = laps;
        this.raceType = raceType;
        this.offer = offer;
        this.image = image;
        this.reputation = reputation;
        this.car = car;
        this.offerChange = offerChange;
        this.carOffer = carOffer;
        this.timeRequired = timeRequired;
    }

    private static List<int> increaseChangeMoney = new List<int>()
    {
        2500,7500,10000,15000,20000,30000,50000,75000,100000,150000,250000,500000
    };
    private static List<bool> carOfferList = new List<bool>()
    {
        true,true,false,false,false,false,false,false,false,false
    };

    private static List<int> lapsProb = new List<int>()
    {
        1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,1,1,1,1,1,1,1,1,2
    };
    public static Opponent FromLevel(int Level, AlienData alienData)
    {
        int worldLevel = SceneManager.GetActiveScene().buildIndex;
        string opponentName = PRUtils.GetSingle(alienData.aliensName);
        string about = PRUtils.GetSingle(alienData.alienAbout);
        int laps = PRUtils.GetSingle(lapsProb);

        RaceData.RaceType raceType = PRUtils.GetSingle(new List<RaceData.RaceType>() { RaceData.RaceType.Time, RaceData.RaceType.AgainstThree, RaceData.RaceType.Reverse, RaceData.RaceType.Sprint });

        int offer = PRUtils.randomNumbers.Next(CurrencyData.worldOffers[Level-1],
            CurrencyData.worldOffers[Level]*(1+(worldLevel/10)));
        UnityEngine.Sprite image = PRUtils.GetSingle(alienData.alienSprites) ;
        int reputation = Level;
        Car car = Car.FromLevel(Level);
        int offerChange = increaseChangeMoney[Level];
        bool caroffer = PRUtils.GetSingle(carOfferList);
        int timeReq = (Constants.timeRequired[worldLevel-1] - Random.Range(-40,20)) * laps;

        return new Opponent(opponentName, about, laps, raceType, offer, image, reputation, car, offerChange,
            caroffer, timeReq);
    }

    public string OpponentName { get => opponentName; set => opponentName = value; }
    public string About { get => about; set => about = value; }
    public int Laps { get => laps; set => laps = value; }
    public RaceData.RaceType RaceType { get => raceType; set => raceType = value; }
    public int Offer { get => offer; set => offer = value; }
    public Sprite Image { get => image; set => image = value; }
    public int Reputation { get => reputation; set => reputation = value; }
    public Car Car { get => car; set => car = value; }
    public int OfferChange { get => offerChange; set => offerChange = value; }
    public bool CarOffer { get => carOffer; set => carOffer = value; }
    public int TimeRequired { get => timeRequired; set => timeRequired = value; }
}
