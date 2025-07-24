using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienData : MonoBehaviour
{
    public Sprite[] alienSprites = new Sprite[11];

    public List<string> aliensName = new List<string>()
    {"Otals","Kek","Vincol","B1-01","Stircil","Enza","Umnos","Aman","FF-RR","Thenqiks","Dregeve","Vid",
    "Craegrods","Neks","C0.56","A0-11","Scaczaits","Guustrals","C0^88","Zirnil","Chophit","Onders","Dolqin",
    "Krusuth","Maeqnon","Stil","Sceds","Uunkits","Vrulguns","Seelvoll","Eif'aeds","Arna","Cunads","Ehluth",
    "Dhinieds","Shollea","B0-11","ZX-%%","CZXI11"};

    public List<string> alienAbout = new List<string>()
    { 
        "Hi, nice to meet you. Would you like to race with me."
    };

    public List<string> higherReputationRejection = new List<string>()
    {
        "I don't race an opponent lower than my reputation."
    };

}
