using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GarageInventory : MonoBehaviour
{
    public CarData carData;
    public CurrencyData currencyData;
    public Player playerFunctions;
    public GameObject message;


    public GameObject garage;
    public GameObject inventory;

    public Sprite[] partIcons = new Sprite[4];

    public TMP_Text[] partNames = new TMP_Text[4];
    public TMP_Text[] partRepairCost = new TMP_Text[4];
    public Slider[] partHealthSliders = new Slider[4];
    public Button[] changeButton = new Button[4];
    public Button[] repairButton = new Button[4];

    private List<int> partIndex = new List<int>();
    private List<int> partHealth = new List<int>();
    private int playerCoins;

    private List<string> partType = new List<string>()
    {"ENGINE","BODY","TIRE","NITRO"};
    private List<int> repairCost = new List<int>();

    private int engine = 0;
    private int body = 1;
    private int tire = 2;
    private int nitro = 3;
    private List<List<int>> inventoryData = new List<List<int>>();
    private MessageObjects messageObjects;
    private GameObject itemscrollview;
    private List<int> equippedItems;
    IEnumerator Load()
    {
        yield return new WaitForSeconds(.01f);
        SetGarage();
    }
    void Start()
    {
        StartCoroutine(Load());
        messageObjects = message.GetComponent<MessageObjects>();
    }

    public void SetGarage()
    {
        //index of current selected 1engine 3tire 2body 4nitro
        partIndex = playerFunctions.PartIndex();
        partHealth = playerFunctions.PartHealth();
        playerCoins = playerFunctions.GetPlayerCoins();
        repairCost = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            int current = i;
            // alldata= engine+body+tire+nitro
            partNames[i].text = carData.allData[i][partIndex[i]];
            partHealthSliders[i].value = (float)((float)partHealth[i]/100f);

            changeButton[i].onClick.RemoveAllListeners();
            repairButton[i].onClick.RemoveAllListeners();

            changeButton[i].onClick.AddListener(delegate { InventoryGenerate(current, partIndex[current]); });
            repairButton[i].onClick.AddListener(delegate { repair(current); });
            int cost = currencyData.RepairCost(i, partIndex[i],partHealth[i]);
            repairCost.Add(cost);
            partRepairCost[i].text = PRUtils.CurrencyFormater( cost.ToString());
        }
    }

    void repair(int type)
    {
        playerCoins = playerFunctions.GetPlayerCoins();
        if (repairCost[type] == 0)
        {
            message.SetActive(true);
            messageObjects.message.text = "Fully repaired already.";
        }
        else if (playerCoins > repairCost[type])
        {
            playerFunctions.RepairParts(type, repairCost[type]);
            partIndex = playerFunctions.PartIndex();
            partHealth = playerFunctions.PartHealth();
            playerCoins = playerFunctions.GetPlayerCoins();
            SetGarage();
            /*            partHealth[type] = 100;
                        partHealthSliders[type].value = 1;
                        partRepairCost[type].text = "0";
                        StartCoroutine(Load());*/
        }
        else
        {
            message.SetActive(true);
            messageObjects.message.text = "Not enough coins.";
        }


    }
    TMP_Text prevEquipText;
    Button prevEquip;

    void InventoryGenerate(int type,int index)
    {
        equippedItems = playerFunctions.GetEquippedItems();
        inventoryData = playerFunctions.GetInventoryData();
        garage.SetActive(false);

        itemscrollview = Instantiate(inventory);
        itemscrollview.transform.SetParent(gameObject.transform,false);
        itemscrollview.SetActive(true);
        itemscrollview.transform.position = inventory.transform.position;
        InventoryContentItem content = itemscrollview.GetComponent<InventoryContentItem>();
        content.close.onClick.AddListener(Close);
        for (int i = 0; i < inventoryData[type].Count; i++)
        {
            GameObject item = Instantiate(content.inventoryItem);
            item.transform.SetParent(content.contentParent.transform, false);
            item.SetActive(true);
            InventoryObjects inventoryObjects = item.GetComponent<InventoryObjects>();
            inventoryObjects.icon.sprite = partIcons[type];
            inventoryObjects.itemName.text = carData.allData[type][inventoryData[type][i]];
            int current = i;

            TMP_Text currText;
            Button currButton;
            if (equippedItems[type] == i)
            {
                prevEquipText = inventoryObjects.equipText;
                prevEquip = inventoryObjects.equip;
                prevEquipText.text = "Equipped";
                prevEquip.enabled = false;
                inventoryObjects.equip.onClick.AddListener(delegate{ equipItem(
                    inventoryObjects.equipText, inventoryObjects.equip, current, type); });

            }
            else
            {
                currText = inventoryObjects.equipText;
                currButton = inventoryObjects.equip;
                inventoryObjects.equip.onClick.AddListener(delegate{ equipItem(currText,
                    currButton, current, type); });
            }

        }


    }

    void equipItem(TMP_Text text, Button button, int index,int type)
    {
        prevEquipText.text = "Equip";
        prevEquip.enabled = true;
        prevEquip = button;
        prevEquipText = text;
        text.text = "Equipped";
        button.enabled = false;
        playerFunctions.ChangeCurrentSelected(index, type);
    }

    void Close()
    {
        garage.SetActive(true);
        Destroy(itemscrollview);

        for (int i = 0; i<4;i++)
        {
            changeButton[i].onClick.RemoveAllListeners();
            repairButton[i].onClick.RemoveAllListeners();
        }
        SetGarage();
    }
}
