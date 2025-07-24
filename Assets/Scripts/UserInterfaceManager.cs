using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour
{
    public GameObject home;
    Animator homeController;

    public GameObject homeRacemenu;
    public GameObject homeRaceplanets;

    public GameObject homeShop;
    public GameObject homeShopBuy;
    public GameObject homeShopSell;

    public GameObject homePlayer;
    public GameObject homePlayerProfile;
    public GameObject homePlayerGarage;
    public GameObject homePlayerCustomisation;

    public GameObject gamebarInfo;
    public GameObject gamebarBack;
    public Button gamebarBackButton;

    public int currentState;
    public string currentPlace;

    

    private void Start()
    {
        currentState = 0;
        homeController = home.GetComponent<Animator>();
        homeController.SetTrigger("HomeEnter");

        gamebarBackButton.onClick.AddListener(BackButton);

    }

    public void ChangeUI(string place)
    {

        currentState++;
        currentPlace = place;
        if (currentState>0)
        {
            gamebarInfo.SetActive(false);
            gamebarBack.SetActive(true);
        }
        if (place == "racemenu")
        {
            home.SetActive(false);
            homeRacemenu.SetActive(true);
            homeController.SetTrigger("HomeExit");
        }
        else if (place == "racemenuPlanets")
        {
            homeRacemenu.SetActive(false);
            homeRaceplanets.SetActive(true);
        }
        else if (place == "Shop")
        {
            home.SetActive(false);
            homeShop.SetActive(true);
            homeController.SetTrigger("HomeExit");
        }
        else if (place == "ShopBuy")
        {
            homeShop.SetActive(false);
            homeShopBuy.SetActive(true);
        }
        else if (place == "ShopSell")
        {
            homeShop.SetActive(false);
            homeShopSell.SetActive(true);
        }
        else if (place == "homeProfile")
        {
            home.SetActive(false);
            homePlayer.SetActive(true);
            homePlayerGarage.SetActive(false);
            homePlayerProfile.SetActive(true);
            homeController.SetTrigger("HomeExit");
        }
        else if (place == "HomeGarage")
        {
            home.SetActive(false);
            homePlayer.SetActive(true);
            homePlayerGarage.SetActive(true);
            homePlayerProfile.SetActive(false);
            homeController.SetTrigger("HomeExit");
        }
        else if (place == "garage")
        {
            homePlayerGarage.SetActive(true);
            homePlayerProfile.SetActive(false);
            currentState--;
        }
        else if (place == "profile")
        {
            homePlayerGarage.SetActive(false);
            homePlayerProfile.SetActive(true);
            currentState--;
        }
        else if (place == "garageCustomisation")
        {
            homePlayerGarage.SetActive(false);
            homePlayerCustomisation.SetActive(true);
            homePlayer.SetActive(true);
        }
        else if (place == "playerRace")
        {
            homePlayerGarage.SetActive(false);
            homePlayerProfile.SetActive(false);
            homePlayerCustomisation.SetActive(false);
            homePlayer.SetActive(false);
            homeRacemenu.SetActive(true);

        }
        else if (place == "playerShop")
        {
            homePlayerGarage.SetActive(false);
            homePlayerProfile.SetActive(false);
            homePlayerCustomisation.SetActive(false);
            homePlayer.SetActive(false);
            homeShop.SetActive(true);
        }

    }
    
    public void BackButton()
    {
        currentState--;

        if (currentState == 0)
        {
            gamebarInfo.SetActive(true);
            gamebarBack.SetActive(false);
        }
        if (currentPlace == "racemenu")
        {
            home.SetActive(true);
            homeRacemenu.SetActive(false);
            homeController.SetTrigger("HomeEnter");

        }
        else if (currentPlace == "racemenuPlanets")
        {
            homeRacemenu.SetActive(true);
            homeRaceplanets.SetActive(false);
            currentPlace = "racemenu";
        }
        else if (currentPlace == "Shop")
        {
            home.SetActive(true);
            homeShop.SetActive(false);
            homeController.SetTrigger("HomeEnter");
        }
        else if (currentPlace == "ShopBuy")
        {
            homeShop.SetActive(true);
            homeShopBuy.SetActive(false);
            currentPlace = "Shop";
        }
        else if (currentPlace == "ShopSell")
        {
            homeShop.SetActive(true);
            homeShopSell.SetActive(false);
            currentPlace = "Shop";
        }
        else if (currentPlace == "homeProfile")
        {
            home.SetActive(true);
            homePlayer.SetActive(false);
            homePlayerGarage.SetActive(true);
            homePlayerProfile.SetActive(false);
            homeController.SetTrigger("HomeEnter");
        }
        else if (currentPlace == "HomeGarage")
        {
            home.SetActive(true);
            homePlayer.SetActive(false);
            homePlayerGarage.SetActive(false);
            homePlayerProfile.SetActive(true);
            homeController.SetTrigger("HomeEnter");
        }
        else if (currentPlace == "garage")
        {
            home.SetActive(true);
            homePlayer.SetActive(false);
            homePlayerGarage.SetActive(false);
            homePlayerProfile.SetActive(false);
            homeController.SetTrigger("HomeEnter");
        }
        else if (currentPlace == "profile")
        {
            home.SetActive(true);
            homePlayer.SetActive(false);
            homePlayerGarage.SetActive(false);
            homeController.SetTrigger("HomeEnter");
            homePlayerProfile.SetActive(false);
        }
        else if (currentPlace == "garageCustomisation")
        {
            homePlayer.SetActive(true);
            homePlayerGarage.SetActive(true);
            homePlayerCustomisation.SetActive(false);
            currentPlace = "HomeGarage";
        }
        else if (currentPlace == "playerRace")
        {
            homePlayerGarage.SetActive(true);
            homePlayerProfile.SetActive(false);
            homePlayerCustomisation.SetActive(false);
            homePlayer.SetActive(true);
            homeRacemenu.SetActive(false);
            currentPlace = "HomeGarage";

        }
        else if (currentPlace == "playerShop")
        {
            homePlayerGarage.SetActive(true);
            homePlayerProfile.SetActive(false);
            homePlayerCustomisation.SetActive(false);
            homePlayer.SetActive(true);
            homeShop.SetActive(false);
            currentPlace = "HomeGarage";
        }
        if(home.activeSelf)
        {
            gamebarInfo.SetActive(true);
            gamebarBack.SetActive(false);
            homeController.SetTrigger("HomeEnter");
        }
    }
}
