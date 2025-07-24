using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarPIcker : MonoBehaviour
{
    public enum CarPart { Body, Vinyl, Tire, Glass, Spoiler, Interior }

    private List<float> solidColorType = new List<float>() { 0, .8f };
    private List<float> pearlescentColorType = new List<float>() { .5f, 1 };
    private List<float> matteColorType = new List<float>() { .5f, .2f };
    private List<float> metallicColorType = new List<float>() { 1, .85f };
    List<float> partColorType;
    int metallic = 0; int smoothness = 1;

    public GameObject prefabFCP;
    public GameObject visualParent;
    public CurrencyData currencyData;

    [SerializeField]
    CarPart carPart;
    public GameObject colorPicker;

    public Player playerFunctions;

    FlexibleColorPicker flexibleColorPicker;
    ColorPickerObjects colorPickerObjects;

    public CarPIcker object1;
    public CarPIcker object2;
    public CarPIcker object3;
    public CarPIcker object4;

    void Start()
    {
        
    }
    public void ColorChangeData()
    {
        if (colorPicker != null || object1.colorPicker != null || object2.colorPicker != null || object3.colorPicker != null || object4.colorPicker != null)
        {
            Destroy(colorPicker);
            Destroy(object1.colorPicker);
            Destroy(object2.colorPicker);
            Destroy(object3.colorPicker);
            Destroy(object4.colorPicker);
        }
        colorPicker = Instantiate(prefabFCP);
        colorPicker.transform.SetParent(visualParent.transform, false);
        colorPicker.transform.localScale = new Vector3(2.8f, 2.8f, 2.8f);
        colorPicker.transform.localPosition = new Vector3(1656f, 275f, 0);
        colorPicker.SetActive(true);

        colorPickerObjects = colorPicker.GetComponent<ColorPickerObjects>();
        colorPickerObjects.selectButton.onClick.AddListener(Buy);
        if (carPart == CarPart.Body || carPart == CarPart.Vinyl)
        {
            colorPickerObjects.colorType.enabled = true;
            colorPickerObjects.colorType.onValueChanged.AddListener(ColorTypeChange);

        }
        else
        {
            colorPickerObjects.colorType.interactable = false;
        }

        partColorType = solidColorType;
        ServiceCost();
        flexibleColorPicker = colorPicker.GetComponent<FlexibleColorPicker>();
        flexibleColorPicker.onColorChange.AddListener(SendData);
        colorPickerObjects.closeButton.onClick.AddListener(ColorReset);
    }
    int colorTypeIndex = 0;
    int cost;
    int discount = 0;

    private void ServiceCost()
    {
        if (carPart == CarPart.Glass) { discount = 3000; }
        if (carPart == CarPart.Interior) { discount = 2500; }
        if (carPart == CarPart.Spoiler) { discount = 4500; }
        if (carPart == CarPart.Tire) { discount = 1000; }
        cost = currencyData.colorTypePrice[colorTypeIndex] - discount;
        colorPickerObjects.priceText.text = cost.ToString();
    }
    void ColorTypeChange(int Val)
    {
        colorTypeIndex = Val;
        if (Val == 0) { partColorType = solidColorType; }
        if (Val == 1) { partColorType = pearlescentColorType; }
        if (Val == 2) { partColorType = matteColorType; }
        if (Val == 3) { partColorType = metallicColorType; }
        ServiceCost();


    }
    Color colorSelected;
    void ColorReset()
    {
        playerFunctions.CarReset();
        Destroy(colorPicker);
    }
    void Buy()
    {
        playerFunctions.PurchaseColor(cost, carPart, colorSelected, partColorType);
        Destroy(colorPicker);
    }
    void SendData(Color color)
    {
        colorSelected = color;
        playerFunctions.PartColorChanger(carPart, color, partColorType);
    }



}
