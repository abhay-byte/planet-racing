using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyData : MonoBehaviour
{
    /*
        #Planet Entry Fee
        #Engine Price
        #Body Price
        #Material Price
        #Wheel Price
        #Nitros Price
        #Spoiler Price
        #BodySkit Price
        #Solid Price
        #Matte Price
        #Metallic Price
        #Pearlacent Price

        #World Level Offers

     */
    public static readonly List<int> worldOffers = new List<int>()
    {
        0,25000,50000,100000,250000,500000,1000000
        ,1500000,2500000,3500000,4000000,6000000,7500000
    };

     
    public readonly List<int> enginePrice = new List<int>()
    {
        25000,60000,125000,300000,565000,956000,2250000,
        4780000,5990000,7350000,8500000,12550000,18500000
    };

    public readonly List<int> bodyPrice = new List<int>()
    {
        37500,82000,165000,410000,620000,1280000,2100000,
        5750000,7350000,9820000,15200000
    };

    public readonly List<int> tirePrice = new List<int>()
    {
        4500,80000,120000,155000,350000,428000,750000,
        1000000
    };
    public readonly List<int> nitroPrice = new List<int>()
    {
        5000,10000,25000,30000,60000
    };
    public readonly List<int> spoilerPrice = new List<int>()
    {
        2500,3000,3200,3500,3800,4000,4200,4500,5000,6000
    };
    
    public int BodySkitPrice(int bodyIndex, int rate)
    {
        int bodySkitPrice = (bodyPrice[bodyIndex] * rate) / 100;
        return bodySkitPrice;
    }

    public readonly List<int> colorTypePrice = new List<int>()
    { 
        5000,15000,50000,85000
    };
    List<List<int>> allData;
    private void Start()
    {
        allData = new List<List<int>>()
        { enginePrice,bodyPrice,tirePrice,nitroPrice};
    }
    public int RepairCost(int type, int index,int health)
    {
        int factor = ((100 - health)*60)/100;
        int cost = (allData[type][index] * factor) /100;
        return cost;
    }

    

/*    public int solid = 5000;
    public int pearlescent = 15000;
    public int matte = 50000;
    public int metallic = 85000;*/
}
