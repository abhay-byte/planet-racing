using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBodyChanges : MonoBehaviour
{
    CarBodyObjects changeObjects;
    public CarBodyChanges object1;
    public CarBodyChanges object2;
    public enum CarChanges { BodySkit, Vinyl, Spoiler}
    [SerializeField]
    CarChanges carChange;

    public GameObject carBodyChange;
    public GameObject bodyParent;

    public Player playerFunctions;

    public CarData carData;
    public CurrencyData currencyData;
    public Sprite spoiler;
    public Sprite bodySkit;
    public Sprite vinyl;
    readonly int bodySkitInt = 2;
    readonly int vinylInt = 5;

    public GameObject carBody;
    int spoilerInt = 0;
    public void UpdateCarChanges()
    {
        if(carBody!=null || object1.carBody != null || object2.carBody !=null)
        {
            Destroy(carBody);
            Destroy(object1.carBody);
            Destroy(object2.carBody);
        }




        carBody = Instantiate(carBodyChange);
        carBody.transform.SetParent(bodyParent.transform,false);
        carBody.transform.localScale = bodyParent.transform.localScale;
        carBody.transform.localScale = new Vector3(.7f,.7f,.7f);
        carBody.transform.localPosition = new Vector3(1195f, 113f, 0);
        carBody.SetActive(true);
        changeObjects = carBody.GetComponent<CarBodyObjects>();
        changeObjects.close.onClick.AddListener(BodyChangesClose);
        if (carChange == CarChanges.BodySkit)
        {
            for (int i = 0; i <= bodySkitInt; i++)
            {
                int current = i;
                GameObject choice = Instantiate(changeObjects.choice);
                choice.transform.localScale = changeObjects.choice.transform.localScale;
                BodyItemName choiceObjects = choice.GetComponent<BodyItemName>();
                choiceObjects.icon.sprite = bodySkit;
                choice.transform.SetParent(changeObjects.scrollViewParent.transform, false);
                choice.SetActive(true);


                choiceObjects.itemName.text = "Body Skit "+ i;
                choiceObjects.itemButton.onClick.AddListener(delegate { choiceListener(current); });

            }
        }
        if (carChange == CarChanges.Spoiler)
        {
            for (spoilerInt = 1; spoilerInt <= carData.carSpoilerMesh.Length-1; spoilerInt++)
            {
                int current = spoilerInt;
                GameObject choice = Instantiate(changeObjects.choice);

                BodyItemName choiceObjects = choice.GetComponent<BodyItemName>();
                choiceObjects.icon.sprite = spoiler;
                choice.transform.SetParent(changeObjects.scrollViewParent.transform, false);
                choice.SetActive(true);

                choiceObjects.itemName.text = "Spoiler " + spoilerInt;
                choiceObjects.itemButton.onClick.AddListener(delegate { choiceListener(current); });

            }

        }
        if (carChange == CarChanges.Vinyl)
        {
            for (int i = 0; i < vinylInt; i++)
            {
                int current = i;
                GameObject choice = Instantiate(changeObjects.choice);
                choice.transform.localScale = changeObjects.choice.transform.localScale; 
                BodyItemName choiceObjects = choice.GetComponent<BodyItemName>();
                choiceObjects.icon.sprite = vinyl;
                choice.transform.SetParent(changeObjects.scrollViewParent.transform, false);
                choice.SetActive(true);
                choiceObjects.itemButton.onClick.AddListener(delegate { choiceListener(current); });
                choiceObjects.itemName.text = "Vinyl " + i;
            }
        }
    }   

    void choiceListener(int indx) 
    { 
        playerFunctions.CarBodyChange(carChange, indx);
        changeObjects.buy.onClick.RemoveAllListeners();
        int price = new int();
        if (carChange == CarChanges.BodySkit)
        {
             price = currencyData.BodySkitPrice(playerFunctions.CarBodyIndex(), indx);
        }
        if (carChange == CarChanges.Spoiler)
        {
             price = currencyData.spoilerPrice[indx];
        }
        if (carChange == CarChanges.Vinyl)
        {
             price = 1200;
        }


        changeObjects.price.text = price.ToString();
        changeObjects.buy.onClick.AddListener(delegate { purchase(indx,price); });

    }
    void purchase(int index,int price)
    {
        playerFunctions.PurchaseCarBody(carChange,index, price);
        Destroy(carBody);
    }

    void BodyChangesClose()
    {
        Destroy(carBody);
        playerFunctions.CarReset();
    }


}
