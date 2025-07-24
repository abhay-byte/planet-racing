using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Models;
using Assets.Scripts.Utils;

public class ShopItems : MonoBehaviour
{
    public GameObject items;

    //public GameObject[] carCamera = new GameObject[11];
    public GameObject scrollViewBodyParent;
    public GameObject scrollViewEngineParent;
    public GameObject scrollViewTireParent;
    public GameObject scrollViewNitroParent;
    public GameObject Message;

    

    public TMP_Text infoItemName;
    public Button buy;
    public Button close;

    public Player playerFunctions;
    public CarData carData;
    public CurrencyData currencyData;

    private List<int> engineQuantity = new List<int>();
    private List<int> bodyQuantity = new List<int>();
    private List<int> tireQuantity = new List<int>();
    private List<int> nitroQuantity = new List<int>();


    private int playerReputation;
    private int playerCoins;
    int quantity;
    MessageObjects messageObjects;

    IEnumerator Data()
    {
        yield return new WaitForSeconds(1f);

        playerReputation = playerFunctions.GetPlayerReputation();
        playerCoins = playerFunctions.GetPlayerCoins();
    }

    void Start()
    {
        close.onClick.AddListener(Close);
        GenerateStoreBody();
        GenerateStoreEngine();
        GenerateStoreTire();
        GenerateStoreNitro();
        messageObjects = Message.GetComponent<MessageObjects>();
        StartCoroutine(Data());
    }
    public void GenerateStoreBody()
    {


        for (int i = 0; i < 11; i++)
        {
            int current = i;
            GameObject itemstobuy = Instantiate(items);
            //itemstobuy.transform.parent = scrollViewBodyParent.transform;
            itemstobuy.transform.SetParent(scrollViewBodyParent.transform, false);
            itemstobuy.SetActive(true);
            ItemObjects itemObjects = itemstobuy.GetComponent<ItemObjects>();
            int price;

            price = currencyData.bodyPrice[i];
            itemObjects.itemName.text = Constants.body[i];
            itemObjects.itemPrice.text = PRUtils.CurrencyFormater( price.ToString());
            quantity = new int();
            if (playerReputation <= i) quantity = 1;
            else quantity = Random.Range(0, 5);
            itemObjects.itemButton.onClick.AddListener(delegate { UpdateInfoUI("BODY", current, price, itemObjects.itemQuantity); });
            bodyQuantity.Add(quantity);
            itemObjects.itemQuantity.text = quantity.ToString();
        }    

    }
    public void GenerateStoreEngine()
    {

        for (int i = 0; i < 13; i++)
        {
            GameObject itemstobuy = Instantiate(items);
            itemstobuy.transform.SetParent(scrollViewEngineParent.transform, false);
            itemstobuy.SetActive(true);
            ItemObjects itemObjects = itemstobuy.GetComponent<ItemObjects>();
            int price;
            int current = i;

            price = currencyData.enginePrice[i];
            itemObjects.itemName.text = Constants.engines[i];
            itemObjects.itemPrice.text = PRUtils.CurrencyFormater(price.ToString());
            quantity = new int();
            if (playerReputation <= i) quantity = 1;
            else quantity = Random.Range(0, 5);
            itemObjects.itemButton.onClick.AddListener(delegate { UpdateInfoUI("ENGINE", current, price, itemObjects.itemQuantity); });
            engineQuantity.Add(quantity);
            itemObjects.itemQuantity.text = quantity.ToString();
        }
    }
    public void GenerateStoreTire()
    {

        for (int i = 0; i < 8; i++)
        {
            int current = i;
            GameObject itemstobuy = Instantiate(items);
            itemstobuy.transform.SetParent(scrollViewTireParent.transform, false);
            itemstobuy.SetActive(true);
            ItemObjects itemObjects = itemstobuy.GetComponent<ItemObjects>();
            int price;


            price = currencyData.tirePrice[i];
                itemObjects.itemName.text = Constants.tires[i];
                itemObjects.itemPrice.text = PRUtils.CurrencyFormater(price.ToString());
                quantity = new int();
                if (playerReputation <= i) quantity = 1;
                else quantity = Random.Range(0, 5);
                itemObjects.itemButton.onClick.AddListener(delegate { UpdateInfoUI("TIRE", current, price, itemObjects.itemQuantity); });
                tireQuantity.Add(quantity);
                itemObjects.itemQuantity.text = quantity.ToString();

        }
    }
    public void GenerateStoreNitro()
    {

        for (int i = 0; i < 5; i++)
        {
            GameObject itemstobuy = Instantiate(items);
            itemstobuy.transform.SetParent(scrollViewNitroParent.transform, false);
            itemstobuy.SetActive(true);
            ItemObjects itemObjects = itemstobuy.GetComponent<ItemObjects>();
            int price;
            int current = i;

            price = currencyData.nitroPrice[i];
            itemObjects.itemName.text = Constants.nitro[i];
            itemObjects.itemPrice.text = PRUtils.CurrencyFormater(price.ToString());
            quantity = new int();
            if (playerReputation <= i) quantity = 1;
            else quantity = Random.Range(0, 5);
            itemObjects.itemButton.onClick.AddListener(delegate { UpdateInfoUI("NITRO", current, price, itemObjects.itemQuantity); });
            nitroQuantity.Add(quantity);
            itemObjects.itemQuantity.text = quantity.ToString();
        }
    }
    void UpdateInfoUI(string type, int index, int price, TMPro.TMP_Text quantity)
    {
        buy.onClick.RemoveAllListeners();
        if (type == "BODY")
        {
            for (int i = 0; i < 11; i++)
            {
                //carCamera[i].SetActive(false);
            }
            //carCamera[index].SetActive(true);
            if (bodyQuantity[index] > 0)
            {

                //carCamera[index].SetActive(true);
                buy.onClick.AddListener(delegate { SendData(type, index, price, quantity); });
            }
            else
            {
                buy.onClick.AddListener(lackOfStock);
            }
        }
        else if (type == "ENGINE" && engineQuantity[index]>0) buy.onClick.AddListener(delegate { SendData(type, index, price, quantity); });

        else if (type == "TIRE" && tireQuantity[index] > 0) buy.onClick.AddListener(delegate { SendData(type, index, price, quantity); });

        else if (type == "NITRO" && nitroQuantity[index] > 0) buy.onClick.AddListener(delegate { SendData(type, index, price, quantity); });
        else buy.onClick.AddListener(lackOfStock);  
    }
    void lackOfStock()
    {
        messageObjects.message.text = "Out of stock.";
        Message.SetActive(true);
    }

    void SendData(string type, int index, int price, TMPro.TMP_Text quantity)
    {
        playerCoins = playerFunctions.GetPlayerCoins();
        if (playerCoins > price && type == "BODY")
        { 
            playerFunctions.BuyCarItems(type, index, price);
            bodyQuantity[index] -= 1;
            quantity.text = bodyQuantity[index].ToString();
        }
        else if (playerCoins > price && type == "ENGINE")
        {
            playerFunctions.BuyCarItems(type, index, price);
            engineQuantity[index] -= 1;
            quantity.text = engineQuantity[index].ToString();
        }

        else if (playerCoins > price && type == "NITRO")
        {
            playerFunctions.BuyCarItems(type, index, price);
            nitroQuantity[index] -= 1;
            quantity.text = nitroQuantity[index].ToString();
        }

        else if (playerCoins > price && type == "TIRE")
        {
            playerFunctions.BuyCarItems(type, index, price);
            tireQuantity[index] -= 1;
            quantity.text = tireQuantity[index].ToString();
        }
            
        else
        {
            Message.SetActive(true);
            messageObjects.message.text = "Not enough coins.";
        }

    }
    public void Close()
    {
        for (int i = 0; i < 11; i++)
        {
            //carCamera[i].SetActive(false);
        }
    }
}
