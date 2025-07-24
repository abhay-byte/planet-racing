using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellSystem : MonoBehaviour
{
    private const float rate = 0.75f;

    // Start is called before the first frame update
    [SerializeField] private Player playerData;
    public GameObject scrollViewBodyParent;
    public GameObject scrollViewEngineParent;
    public GameObject scrollViewTireParent;
    public GameObject scrollViewNitroParent;
    public GameObject items;

    public Button close;
    public Button sell;

    private List<GameObject> ItemsAll = new List<GameObject>();


     [SerializeField] private List<List<int>> playerEngine;
    readonly int engineIndex = 0;
    readonly int engineHealthIndex = 1;
     [SerializeField] private List<List<int>> playerTires;
    readonly int tireIndex = 0;
    readonly int tireHealthIndex = 1;
       [SerializeField] private List<List<int>> playerNitros;
    readonly int nitrosIndex = 0;
    readonly int nitrosHealthIndex = 1;

       [SerializeField] private List<List<float>> playerBody;
    readonly int bodyIndex = 0;
    readonly int bodyHealthIndex = 1;
    readonly int spoilerIndex = 2;
    readonly int vinylIndex = 3;
    readonly int bodyModIndex = 4;
    readonly int bodyMetallic = 5;
    readonly int bodySmoothness = 6;
    readonly int vinylMetallic = 7;
    readonly int vinylSmoothness = 8;

    private List<int> engineQuantity = new List<int>();
    private List<int> bodyQuantity = new List<int>();
    private List<int> tireQuantity = new List<int>();
    private List<int> nitroQuantity = new List<int>();

    public Button changeToSell;
    public Button changeToBuy;

    [SerializeField] private CurrencyData currencyData;

    void Start()
    {
        changeToSell.onClick.AddListener(Loading);
    }

    private void Loading()
    {

        List<List<List<int>>> data = playerData.GetPlayerItems();
        playerBody = playerData.GetPlayerBody();
        playerEngine = data[0];
        playerTires = data[1];
        playerNitros = data[2];

        CreateEngineSellItems();
        CreateBodySellItems();
        CreateTireSellItems();
        CreateNitrosSellItems();

        changeToSell.onClick.RemoveAllListeners();
        changeToBuy.onClick.AddListener(DeleteAll);

    }

    private void DeleteAll()
    {
        for (int i = 0; i< ItemsAll.Count;i++)
        {
            Destroy(ItemsAll[i]);
        }
        ItemsAll = new List<GameObject>();
        changeToSell.onClick.AddListener(Loading);
        changeToBuy.onClick.RemoveAllListeners();
    }

    private void CreateEngineSellItems()
    {
        for (int i = 0; i < playerData.GetItemCount(0); i++)
        {
            int itemIndex = playerEngine[i][0];
            int itemHealth = playerEngine[i][1];
            int current = i;
            string itemType = "ENGINE";
            GameObject itemstobuy = Instantiate(items);
            ItemsAll.Add(itemstobuy);
            itemstobuy.transform.SetParent(scrollViewEngineParent.transform, false);
            itemstobuy.SetActive(true);

            ItemObjects itemObjects = itemstobuy.GetComponent<ItemObjects>();
            int price = (int)((currencyData.enginePrice[itemIndex] * rate)
                -(currencyData.RepairCost(0, itemIndex,
                itemHealth) * rate));
            if (price<0) { price = 1000; }
            itemObjects.itemName.text = Constants.engines[itemIndex];
            itemObjects.itemPrice.text = price.ToString();
            itemObjects.itemQuantity.text = "";
            itemObjects.itemButton.onClick.AddListener(delegate { SelectItem(itemstobuy,price, current, itemType); });

        }

    }

    private void CreateBodySellItems()
    {
        for (int i = 0; i < playerData.GetItemCount(1); i++)
        {
            int itemIndex = (int)playerBody[i][0];
            int itemHealth = (int)playerBody[i][1];
            int current = i;
            string itemType = "BODY";
            GameObject itemstobuy = Instantiate(items);
            ItemsAll.Add(itemstobuy);
            itemstobuy.transform.SetParent(scrollViewBodyParent.transform, false);
            itemstobuy.SetActive(true);

            ItemObjects itemObjects = itemstobuy.GetComponent<ItemObjects>();
            int price = (int)((currencyData.bodyPrice[itemIndex] * rate)
                - (currencyData.RepairCost(0, itemIndex,
                itemHealth) * rate));
            if (price < 0) { price = 1000; }
            itemObjects.itemName.text = Constants.body[itemIndex];
            itemObjects.itemPrice.text = price.ToString();
            itemObjects.itemQuantity.text = "";
            itemObjects.itemButton.onClick.AddListener(delegate { SelectItem(itemstobuy, price, current, itemType); });

        }

    }

    private void CreateTireSellItems()
    {
        for (int i = 0; i < playerData.GetItemCount(2); i++)
        {
            int itemIndex = playerTires[i][0];
            int itemHealth = playerTires[i][1];
            int current = i;
            string itemType = "TIRE";
            GameObject itemstobuy = Instantiate(items);
            ItemsAll.Add(itemstobuy);
            itemstobuy.transform.SetParent(scrollViewTireParent.transform, false);
            itemstobuy.SetActive(true);

            ItemObjects itemObjects = itemstobuy.GetComponent<ItemObjects>();
            int price = (int)((currencyData.tirePrice[itemIndex] * rate)
                - (currencyData.RepairCost(0, itemIndex,
                itemHealth) * rate));
            if (price < 0) { price = 1000; }
            itemObjects.itemName.text = Constants.tires[itemIndex];
            itemObjects.itemPrice.text = price.ToString();
            itemObjects.itemQuantity.text = "";
            itemObjects.itemButton.onClick.AddListener(delegate { SelectItem(itemstobuy, price, current, itemType); });

        }

    }

    private void CreateNitrosSellItems()
    {
        for (int i = 0; i < playerData.GetItemCount(3); i++)
        {
            int itemIndex = playerNitros[i][0];
            int itemHealth = playerNitros[i][1];
            int current = i;
            string itemType = "NITRO";
            GameObject itemstobuy = Instantiate(items);
            ItemsAll.Add(itemstobuy);
            itemstobuy.transform.SetParent(scrollViewNitroParent.transform, false);
            itemstobuy.SetActive(true);

            ItemObjects itemObjects = itemstobuy.GetComponent<ItemObjects>();
            int price = (int)((currencyData.nitroPrice[itemIndex] * rate)
                - (currencyData.RepairCost(0, itemIndex,
                itemHealth) * rate));
            if (price < 0) { price = 1000; }
            itemObjects.itemName.text = Constants.nitro[itemIndex];
            itemObjects.itemPrice.text = price.ToString();
            itemObjects.itemQuantity.text = "";
            itemObjects.itemButton.onClick.AddListener(delegate { SelectItem(itemstobuy, price, current, itemType); });

        }

    }

    private void SelectItem(GameObject item, int coins, int index , string type)
    {
        sell.gameObject.SetActive(true);
        sell.onClick.RemoveAllListeners();

        if ("ENGINE" == type && playerEngine.Count > 1) sell.onClick.AddListener(delegate { SellItem(item, coins, index, type); });
        if ("BODY" == type && playerBody.Count > 1) sell.onClick.AddListener(delegate { SellItem(item, coins, index, type); });
        if ("TIRE" == type && playerTires.Count > 1) sell.onClick.AddListener(delegate { SellItem(item, coins, index, type); });
        if ("NITRO" == type && playerNitros.Count > 1) sell.onClick.AddListener(delegate { SellItem(item, coins, index, type); });

    }
    public GarageInventory garageInventory;
    public Player player;
    private void SellItem(GameObject item, int coins, int index, string type)
    {
        Destroy(item);
        sell.gameObject.SetActive(false);
        playerData.SellCost(coins);
        playerData.RemoveItemFromInventory(type,index);




        DeleteAll();
        Loading();

        garageInventory.SetGarage();
        if (type == "BODY" || type == "TIRE") player.RefreshCar();


    }

}
