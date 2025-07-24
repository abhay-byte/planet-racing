using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarData : MonoBehaviour
{
    public readonly Dictionary<int, string> engines = new Dictionary<int, string>()
    {
        {0,"40 AP Basic"},
        {1,"60 AP Boost"},
        {2,"85 AP Charge"},
        {3,"135 AP Hybrid"},
        {4,"240 AP Twin Turbo"},
        {5,"350 AP Twin Charged"},
        {6,"500 AP Turbo X6"},
        {7,"750 AP Twin Turbo X6"},
        {8,"800 AP Turbo X8"},
        {9,"1050 AP Twin Turbo X8"},
        {10,"1250 AP Turbo X16"},
        {11,"1375 AP Twin Turbo X16"},
        {12,"1595 AP Qaud Turbo Charged X16"},
        {13,"1885 AP Quad Turbo Charged W16"},
    };

    public readonly Dictionary<int, List<int>> enginesRandom = new Dictionary<int, List<int>>()
    {
        {1, new List<int>(){0, 1} },
        {2, new List<int>(){1, 2} },
        {3, new List<int>(){2, 3} },
        {4, new List<int>(){3, 4} },
        {5, new List<int>(){4, 5} },
        {6, new List<int>(){5, 6} },
        {7, new List<int>(){6, 7} },
        {8, new List<int>(){7, 9} },
        {9, new List<int>(){9, 11} },
        {10, new List<int>(){11, 12} },
        {11, new List<int>(){11, 13} },
    };

    public readonly Dictionary<int, string> tires = new Dictionary<int, string>()
    {
        {0,"Low Grip"},
        {1,"Medium Grip"},
        {2,"High Grip"},
        {3,"Flow Grip"},
        {4,"Fast Grip"},
        {5,"Super Grip"},
        {6,"Max Grip"},
        {7,"Ultra Grip"},
    };

    public static readonly Dictionary<int, string> body = new Dictionary<int, string>()
    {
        {0,"Micro"},
        {1,"Basic"},
        {2,"Compact"},
        {3,"Blaze"},
        {4,"Coupe"},
        {5,"Roadster"},
        {6,"Spyder"},
        {7,"Sports"},
        {8,"Tuner"},
        {9,"Lightning"},
        {10,"Racing"},
    };

    public readonly Dictionary<int, string> nitro = new Dictionary<int, string>()
    {
        {0,"1 Litre"},
        {1,"3 Litre"},
        {2,"5 Litre"},
        {3,"10 Litre"},
        {4,"18 Litre"},

    };

    public readonly Dictionary<int, string> carBodyMaterial = new Dictionary<int, string>()
    {
        {0,"Steel"},
        {1,"Aluminium"},
        {2,"Titanium"},
        {3,"Fibre Glass"},
        {4,"Carbon Fiber"},

    };
    public readonly List<float> solidColorType = new List<float>() { 0, .8f };
    public readonly List<float> pearlescentColorType = new List<float>() { .5f, 1 };
    public readonly List<float> matteColorType = new List<float>() { .5f, .2f };
    public readonly List<float> metallicColorType = new List<float>() { 1, .85f };

    public List<List<float>> colorType = new List<List<float>>();

    public List<Dictionary<int, string>> allData = new List<Dictionary<int, string>>();
    public GameObject[] carPrefabs = new GameObject[11];

    public Mesh[] carBFMesh = new Mesh[33];

    public Texture[] carVinylTexture = new Texture[57];

    public Mesh[] carTireMesh = new Mesh[8];

    public Mesh[] carSpoilerMesh = new Mesh[10];

    private void Start()
    {
        allData.Add(engines);
        allData.Add(body);
        allData.Add(tires);
        allData.Add(nitro);
        colorType.Add(solidColorType);
        colorType.Add(pearlescentColorType);
        colorType.Add(matteColorType);
        colorType.Add(metallicColorType);
    }
}
