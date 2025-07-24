using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Utils;
using DigitalRuby.RainMaker;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class PlanetEnvironmentSystem : MonoBehaviour
{
    // Start is called before the first frame update
    float sunlightIntensity;
    float environmentalFog;
    float skyboxHDRExposure;
    float sunstrength;
    float rainSunlightIntensity = 0.75f;
    public Volume DenseFogPP;
    [SerializeField] private GameObject waterPuddle;
    //[SerializeField] private GameObject hailGameobject;
    //[SerializeField] private GameObject ashGameobject;
    //[SerializeField] private GameObject lavaPuddle;
    [SerializeField] private GameObject poisonPuddle;
    //[SerializeField] private GameObject swamps;
    //[SerializeField] private GameObject tornadoGroup;
    //[SerializeField] private GameObject waterWaves;
    //[SerializeField] private GameObject meteorGameobject;

    public Sprite morning;
    public Sprite noon;
    public Sprite evening;
    public Sprite night;
    public Sprite raining;
    public Sprite foggy;
    public Sprite meteor;
    public Sprite wind;
    public Sprite acidRain;
    public Sprite ashFalling;
    public Sprite hail;
    public Sprite tornado;


    public GameObject weatherEffectImage;
    public GameObject parentWeatherEffect;

    public enum Weathereffects {lightrain,heavyrain,fog,ashfall,rockshower,hailfall,poisonrain};
    public GameObject RainEffect;
    public List<float> fogDesity = new List<float>() 
    {
           0.0004f,0.001f,0.001f,
    };


    public List<Quaternion> sunPositionMorning = new List<Quaternion>()
    {
        new Quaternion(0.2f, 0, 0, 1f),
        new Quaternion(0.1f, -0.8f, 0.1f, 0.5f),
        new Quaternion(1.0f, 0.0f, 0.0f, 0.2f),
    };
    public List<Quaternion> sunPositionEvening = new List<Quaternion>()
    {
        new Quaternion(1f, 0, 0.2f, 0),
        new Quaternion(0.5f, 0.0f, 0.8f, 0.0f),
        new Quaternion(0.0f, 0.0f, 0.0f, 1.0f)
    };


    public bool isLightNeeded;

    public enum WorldTriggerEffect {wave,tornado,swaamp,watersurface,poisonsurface,hotsurface,windeffect,carattack};
    int currentSceen;
    int timeSettings;
    int planetInt;
    List<int> numbers = new List<int>()
    {
        0,1,2,3
    };
    void Start()
    {
        currentSceen = SceneManager.GetActiveScene().buildIndex;
        
        timeSettings = PRUtils.GetSingle(numbers);

        planetInt = currentSceen-2;
        //TimeSetting(timeSettings);
        ApplySettings();
    }

    void TimeSetting(int i)
    {
        //i = 0;
        //Morning
        GameObject EffectImage = Instantiate(weatherEffectImage);
        EffectImage.transform.SetParent(parentWeatherEffect.transform);
        EffectImage.SetActive(true);

        if (i==0)
        {
            EffectImage.GetComponent<Image>().sprite = morning;
            EnvironmentSettings(1.1f, fogDesity[planetInt],1f,3.5f,
                RenderSettings.fogColor,sunPositionMorning[planetInt]);

        }
        
        //Noon
        if (i == 1)
        {
            EffectImage.GetComponent<Image>().sprite = noon;
            EnvironmentSettings(1.25f, 0, 1.3f, 7.5f,
                RenderSettings.fogColor, new Quaternion(0.8f, 0, 0, 0.6f));
        }

        //Evening
        if (i == 2)
        {
            EffectImage.GetComponent<Image>().sprite = evening;
            EnvironmentSettings(.75f, fogDesity[planetInt], 1.8F, 6.5F,
                Color.gray, sunPositionEvening[planetInt]);
            isLightNeeded = true;
        }

        //Night
        if (i == 3)
        {
            EffectImage.GetComponent<Image>().sprite = night;
            EnvironmentSettings(0, fogDesity[planetInt], .35f, 0f,
                Color.gray, new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
            isLightNeeded = true;
        }

    }

    void EnvironmentSettings(float sunLightIntesity, float environmentalFog, float skyboxExposure,
        float sunStrenght,Color fogColor, Quaternion sunRotation)
    {
        sunlightIntensity = sunLightIntesity;
        this.environmentalFog = environmentalFog;
        skyboxHDRExposure = skyboxExposure;
        sunstrength = sunStrenght;
        RenderSettings.fogColor = fogColor;
        RenderSettings.sun.gameObject.transform.rotation = sunRotation;
    }

    // Update is called once per frame
    void CreateImageForEffect(Sprite sprite)
    {
        GameObject EffectImage = Instantiate(weatherEffectImage);
        EffectImage.transform.SetParent(parentWeatherEffect.transform);
        EffectImage.SetActive(true);
        EffectImage.GetComponent<Image>().sprite = sprite;
    }
    void ApplySettings()
    {
        //RenderSettings.fogDensity = environmentalFog;
        //RenderSettings.sun.intensity = sunlightIntensity;
        //RenderSettings.skybox.SetFloat("_HdrExposure", skyboxHDRExposure);
        //RenderSettings.skybox.SetFloat("_SunStrength", sunstrength);
        //Physics.gravity = new Vector3(0, -(float)(Constants.planetDescription[planetInt].PlanetGravityFactor*9.81),0);
        
        if(Constants.planetDescription[planetInt].IsRaining && PRUtils.GetSingle(numbers)==2)
        {
            if (Constants.planetDescription[planetInt].IsRainPoisonous)
            { 
                CreateImageForEffect(acidRain);
                poisonPuddle.SetActive(true);
            }
            else CreateImageForEffect(raining); waterPuddle.SetActive(true);
            RainEffect.SetActive(true);


            RainEffect.GetComponent<RainScript>().RainIntensity =
                Constants.planetDescription[planetInt].RainIntensity;
            RainEffect.GetComponent<RainScript>().RainMistThreshold =
                Constants.planetDescription[planetInt].IsRainFog;
            if(timeSettings<2)
            {
                sunlightIntensity = rainSunlightIntensity;
            }
        }

        if(Constants.planetDescription[planetInt].IsDenseFogPresent && PRUtils.GetSingle(numbers) == 2 
            && timeSettings == 0)
        {
            CreateImageForEffect(foggy);
            RenderSettings.fogDensity = 0.0035f;
            DenseFogPP.enabled = true;
            RenderSettings.skybox.SetFloat("_HdrExposure", skyboxHDRExposure-.5f);
        }

        if(Constants.planetDescription[planetInt].IsAshfalling)
        {
            CreateImageForEffect(ashFalling);

        }

        if (Constants.planetDescription[planetInt].IsMeteorShowerPresent)
        {
            CreateImageForEffect(meteor);
        }

        if (Constants.planetDescription[planetInt].IsHailfallPresent)
        {
            CreateImageForEffect(hail);
        }

        if (Constants.planetDescription[planetInt].IsTornadoPresent)
        {
            CreateImageForEffect(tornado);
        }

        if (Constants.planetDescription[planetInt].IsWindEffectsPresent && PRUtils.GetSingle(numbers) == 2)
        {
            CreateImageForEffect(wind);
        }

    }
}
