using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;
using Assets.Scripts.Utils;
using UnityEngine.UI;
using GooglePlayGames;

public class CarDamageSystem : MonoBehaviour
{

    [SerializeField] RaceGenerator raceGenerator;
    [SerializeField] PlayerData playerData;
    [SerializeField] RaceData raceData;
    [SerializeField] UIDataPlanet uIDataPlanet;
    [SerializeField] CarColliders carColliders;
    

    [SerializeField] private float BodyHP;
    [SerializeField] private float TireHP;
    [SerializeField] private float EngineHP;
    private Slider BodyHealth;
    private Slider EngineHealth;
    private Slider TireHealth;
    private bool player;
    private Car car;


    public float BodyHP1 { get => BodyHP; set => BodyHP = value; }
    public float TireHP1 { get => TireHP; set => TireHP = value; }
    public float EngineHP1 { get => EngineHP; set => EngineHP = value; }

    private void Start()
    {
        uIDataPlanet.SliderOpponentImage.sprite = uIDataPlanet.Car;
        uIDataPlanet.SliderPlayerImage.sprite = uIDataPlanet.Car;
        BodyHealth = uIDataPlanet.RaceBodyHealth;
        EngineHealth = uIDataPlanet.RaceEngineHealth;
        TireHealth = uIDataPlanet.RaceTireHealth;

        BodyHealth.value = (float)decimal.Divide(playerData.PlayerCarData.EngineHealth
            , playerData.PlayerCarData.CarEngineHealth);
        EngineHealth.value = (float)decimal.Divide(playerData.PlayerCarData.BodyHealth
            , playerData.PlayerCarData.CarBodyHealth);
        TireHealth.value = (float)decimal.Divide(playerData.PlayerCarData.TireHealth,
            playerData.PlayerCarData.CarTireHealth);
    }

    public void SetData(Car data,bool player)
    {
        car = data;
        EngineHP = car.EngineHealth;

        BodyHP = car.BodyHealth;

        TireHP = car.TireHealth;
        this.player = player;
        

    }
   void FixedUpdate()
    {
    }
    private void UpdateAchievements()
    {
        playerData.UserData.playerStats[3] += 1;
        playerData.UpdateCarHealth(car);
        try
        {
            //Bad Luck
            Social.ReportProgress("CgkIk4emifEBEAIQDQ", 100.0f, (bool success) => {
                // handle success or failure
                Debug.Log("Bad Luck : Unlocked");
            });
            //Extra Bad Luck
            PlayGamesPlatform.Instance.IncrementAchievement(
            "CgkIk4emifEBEAIQDg", 1, (bool success) => {
                Debug.Log("++ winner Extra Bad Luck");
            });
        }
        catch (System.Exception error) { Debug.Log("Error Encountered at Achievements: " + error); }

    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];

        if (contact.thisCollider == carColliders.Body)
        {
            BodyHP -= collision.relativeVelocity.magnitude;
        }
        if (contact.thisCollider == carColliders.Engine)
        {
            EngineHP -= collision.relativeVelocity.magnitude;
        }
        float damage = (float)decimal.Divide(car.TopSpeed, 13);
        EngineHP -= damage;
        BodyHP -= damage;
        TireHP -= damage-1;
        if (player)
        {
            BodyHealth.value = (float)decimal.Divide((decimal)EngineHP
                , playerData.PlayerCarData.CarEngineHealth);
            EngineHealth.value = (float)decimal.Divide((decimal)BodyHP
                , playerData.PlayerCarData.CarBodyHealth);
            TireHealth.value = (float)decimal.Divide((decimal)TireHP, 
                playerData.PlayerCarData.CarTireHealth);
            if (EngineHP < 0)
            {
                UpdateAchievements();

                uIDataPlanet.Controller.SetActive(false);
                uIDataPlanet.Totalled1.SetActive(true);
                raceData.PlayerCar.GetComponent<CarObjects>().explosion.SetActive(true);
                uIDataPlanet.Reason.text = "Engine has broken...";
            }
            if (BodyHP < 0)
            {
                UpdateAchievements();
                uIDataPlanet.Controller.SetActive(false);
                uIDataPlanet.Totalled1.SetActive(true);
                raceData.PlayerCar.GetComponent<CarObjects>().explosion.SetActive(true);
                uIDataPlanet.Reason.text = "Body has broken...";
            }
            if (TireHP < 0)
            {
                UpdateAchievements();
                uIDataPlanet.Controller.SetActive(false);
                uIDataPlanet.Totalled1.SetActive(true);
                for (int i = 0; i < 4; i++)
                {
                    raceData.PlayerCar.GetComponent<CarObjects>().wheel[i].SetActive(false);
                    raceData.PlayerCar.GetComponent<CarObjects>().wheelCollider[i].SetActive(false);
                }
                raceData.PlayerCar.GetComponent<CarObjects>().wheelDetached.SetActive(true);
                uIDataPlanet.Reason.text = "Tire has broken...";
            }
        }
        else
        {

            if (EngineHP < 0)
            {
                uIDataPlanet.SliderOpponentImage.sprite = uIDataPlanet.Cross;
                raceData.OpponentCar.GetComponent<Rigidbody>().mass = 1;
                raceData.OpponentCar.GetComponent<CarObjects>().explosion.SetActive(true);
                raceData.OpponentCar.GetComponent<CarObjects>().carController.Topspeed = 0;
                raceData.OpponentCar.GetComponent<CarObjects>().AiCarSelfRighting.enabled = false;
            }
            if (BodyHP < 0)
            {
                uIDataPlanet.SliderOpponentImage.sprite = uIDataPlanet.Cross;
                raceData.OpponentCar.GetComponent<Rigidbody>().mass = 1;
                raceData.OpponentCar.GetComponent<CarObjects>().explosion.SetActive(true);
                raceData.OpponentCar.GetComponent<CarObjects>().carController.Topspeed = 0;
                raceData.OpponentCar.GetComponent<CarObjects>().AiCarSelfRighting.enabled = false;
            }
            if (TireHP < 0)
            {
                uIDataPlanet.SliderOpponentImage.sprite = uIDataPlanet.Cross;
                raceData.OpponentCar.GetComponent<Rigidbody>().mass = 1;
                for (int i = 0; i < 4; i++)
                {
                    raceData.OpponentCar.GetComponent<CarObjects>().wheel[i].SetActive(false);
                    raceData.OpponentCar.GetComponent<CarObjects>().wheelCollider[i].SetActive(false);
                }
                raceData.OpponentCar.GetComponent<CarObjects>().wheelDetached.SetActive(true);
                raceData.OpponentCar.GetComponent<CarObjects>().carController.Topspeed = 0;
                raceData.OpponentCar.GetComponent<CarObjects>().AiCarSelfRighting.enabled = false;
            }
            
        }



    }
}