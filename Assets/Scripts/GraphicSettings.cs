using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GraphicSettings : MonoBehaviour
{
    public AudioMixer mixerController;
    public GameObject fpsCounter;
    public RaceData raceData;
    public RenderPipelineAsset[] allResolution;
    public Slider refreshRate;
    public TMP_Text refreshRateValue;
    public Slider renderScale;
    public TMP_Text renderScaleValue;
    public Toggle[] graphicButton;
    private int heightScreen;
    private int widthScreen;
    private userData Data;



    public TMP_Dropdown languageDropdown;

    public Toggle[] UIController;


    public Toggle isFPSEnabled;

    public Toggle isAudioEnabled;
    public Slider masterVolumeSlider;
    public TMP_Text masterVolumeText;

    public Slider engineVolumeSlider;
    public TMP_Text engineVolumeText;

    public Slider musicVolumeSlider;
    public TMP_Text musicVolumeText;

    public Slider uiVolumeSlider;
    public TMP_Text uiVolumeText;

    public Button close;

    public int graphicLevelIndex;
    public int refreshRateIndex;
    public int renderResVal;
    public int controllerIndex;
    public int masterVolume;
    public int engineVolume;
    public int musicVolume;
    public int uiVolume;
    public bool fps;
    public bool music;
    public int languageIndex;



    private void Awake()
    {
        Data = GameObject.Find("/UserData").GetComponent<userData>();
    }

    private void SetVolumeAll(bool enable)
    {
        if (enable) 
        {
            if (musicVolume == 0) mixerController.SetFloat("MusicVolume", Mathf.Log10(0.0001f) * 20f);
            else mixerController.SetFloat("MusicVolume", Mathf.Log10(musicVolume / 100f) * 20f);

            if (masterVolume == 0) mixerController.SetFloat("MasterVolume", Mathf.Log10(0.0001f) * 20f);
            else mixerController.SetFloat("MasterVolume", Mathf.Log10(masterVolume / 100f) * 20f);

            if (engineVolume == 0) mixerController.SetFloat("EngineVolume", Mathf.Log10(0.0001f) * 20f);
            else mixerController.SetFloat("EngineVolume", Mathf.Log10(engineVolume / 100f) * 20f);

            if (uiVolume == 0) mixerController.SetFloat("UIVolume", Mathf.Log10(0.0001f) * 20f);
            else mixerController.SetFloat("UIVolume", Mathf.Log10(uiVolume / 100f) * 20f);


        }
        else
        {
            mixerController.SetFloat("MusicVolume", Mathf.Log10(0.0001f) * 20f);
            mixerController.SetFloat("MasterVolume", Mathf.Log10(0.0001f) * 20f);
            mixerController.SetFloat("EngineVolume", Mathf.Log10(0.0001f) * 20f);
            mixerController.SetFloat("UIVolume", Mathf.Log10(0.0001f) * 20f);
        }
    }
    public void SettingsUpdateUI()
    {
        graphicLevelIndex = Data.graphicSetting;

        refreshRateIndex = Data.frameRate;
        renderResVal = Data.renderRes;

        controllerIndex = Data.controlMode;

        masterVolume = Data.gameVolume;
        engineVolume = Data.gameVolumeEngine;
        musicVolume = Data.gameVolumeMusic;
        uiVolume = Data.gameVolumeUI;

        fps = Data.showFps;
        music = Data.musicEnabled;

        languageIndex= Data.languageIndex;

        SetVolumeAll(music);
        mixerController.SetFloat("MusicVolume",Mathf.Log10(musicVolume/100f)*20f);
        Application.targetFrameRate = refreshRateIndex;
        Screen.SetResolution((int)(Data.widthScreen * (renderResVal / 100f)), (int)(Data.heightScreen * (renderResVal / 100f)), true,0);
        renderScale.value = (renderResVal / 100f);
        refreshRate.value = refreshRateIndex;
        fpsCounter.SetActive(fps);

        languageDropdown.value = languageIndex;

        isAudioEnabled.isOn = music;
        isFPSEnabled.isOn = fps;

        masterVolumeSlider.value = masterVolume / 100f;
        engineVolumeSlider.value = engineVolume / 100f;
        musicVolumeSlider.value = musicVolume / 100f;
        uiVolumeSlider.value = uiVolume / 100f;

        masterVolumeText.text = masterVolume + "%";
        engineVolumeText.text = engineVolume + "%";
        musicVolumeText.text = musicVolume  + "%";
        uiVolumeText.text = uiVolume + "%";
        if(controllerIndex == 0)
        {
            UIController[2].isOn = true;
        }
        else { UIController[1].isOn = true; }

        int i = 0;
        while (i < 5)
        {
            if (i == graphicLevelIndex)
            {
                QualitySettings.renderPipeline = allResolution[i];
                graphicButton[i].isOn = true;
            }


            i++;
        }



    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void RRonChange() 
    {
        Application.targetFrameRate = (int)refreshRate.value;
        refreshRateIndex = (int)(refreshRate.value);
        refreshRateValue.text = (int)refreshRate.value + " fps";
    }

    public void CResolution()
    {
        //Screen.SetResolution((int)(Data.widthScreen* renderScale.value), (int)(Data.heightScreen*renderScale.value), true,0);
        renderResVal = (int)(renderScale.value * 100f);
        renderScaleValue.text = (int)(renderScale.value*100) + "%";
    }

    public void MasterVolumeChange()
    {
        masterVolumeText.text = (int)(masterVolumeSlider.value*100)+"%";
        masterVolume = (int)(masterVolumeSlider.value * 100f);
        SetVolumeAll(music);
    }
    public void EngineVolumeChange()
    {
        engineVolumeText.text = (int)(engineVolumeSlider.value * 100) + "%";
        engineVolume = (int)(engineVolumeSlider.value * 100f);
        SetVolumeAll(music);
    }
    public void UIVolumeChange()
    {
        uiVolumeText.text = (int)(uiVolumeSlider.value * 100) + "%";
        uiVolume = (int)(uiVolumeSlider.value * 100f);
        SetVolumeAll(music);
    }
    public void MusicVolumeChange()
    {
        musicVolumeText.text = (int)(musicVolumeSlider.value * 100) + "%";
        musicVolume = (int)(musicVolumeSlider.value * 100f);
        SetVolumeAll(music);
    }
    private void Start()
    {
        close.onClick.AddListener(SaveChanges);
        SettingsUpdateUI();
/*        graphicButton[4].onClick.AddListener(SetVeryLowGraphicSettings);
        graphicButton[3].onClick.AddListener(SetLowGraphicSettings);
        graphicButton[2].onClick.AddListener(SetMediumGraphicSettings);
        graphicButton[1].onClick.AddListener(SetHighGraphicSettings);
        graphicButton[0].onClick.AddListener(SetUltraGraphicSettings);*/
    }

    public void FPS(bool val)
    {
        fps = val;
        fpsCounter.SetActive(val);
    }

    public void MUSIC(bool val)
    {
        music = val;
        SetVolumeAll(music);
    }

    private void SaveChanges()
    {
        Data.graphicSetting =  graphicLevelIndex;
        Data.frameRate =  refreshRateIndex;
        Data.renderRes =  renderResVal;
        Data.controlMode = controllerIndex;
        Data.gameVolume =  masterVolume;
        Data.gameVolumeEngine = engineVolume;
        Data.gameVolumeMusic = musicVolume;
        Data.gameVolumeUI = uiVolume;
        Data.showFps = fps;
        Data.musicEnabled = music;
        Data.languageIndex = languageIndex;
        Screen.SetResolution((int)(Data.widthScreen* renderScale.value), (int)(Data.heightScreen*renderScale.value), true,0);
        Data.WriteThenRead();
    }


    private void ResolutionChange(int val)
    {
        GraphicsSettings.defaultRenderPipeline = allResolution[val];
        QualitySettings.renderPipeline = allResolution[val];
    }
    public void JoyStick()
    {
        
        controllerIndex = 0;
    }
    public void Arrow()
    {
        controllerIndex = 1;
        if (raceData != null) { 
            raceData.gyroControl.SetActive(false);
            raceData.arrowControl.SetActive(true);
            raceData.PlayerCar.GetComponent<CarObjects>().userControl.ChangeController(controllerIndex);
        }

    }
    public void Gyro()
    {
        controllerIndex = 0;
        if (raceData != null)
        {
            raceData.gyroControl.SetActive(true);
            raceData.arrowControl.SetActive(false);
            raceData.PlayerCar.GetComponent<CarObjects>().userControl.ChangeController(controllerIndex);
        }
    }
    public void SetVeryLowGraphicSettings()
    {
        GraphicsSettings.defaultRenderPipeline = allResolution[5];
        QualitySettings.renderPipeline = allResolution[5];
        graphicLevelIndex = 4;
    }
    public void SetLowGraphicSettings()
    {
        GraphicsSettings.defaultRenderPipeline = allResolution[4];
        QualitySettings.renderPipeline = allResolution[4];
        graphicLevelIndex = 3;
    }
    public void SetMediumGraphicSettings()
    {
        GraphicsSettings.defaultRenderPipeline = allResolution[3];
        QualitySettings.renderPipeline = allResolution[3];
        graphicLevelIndex = 2;
    }
    public void SetHighGraphicSettings()
    {
        GraphicsSettings.defaultRenderPipeline = allResolution[2];
        QualitySettings.renderPipeline = allResolution[2];
        graphicLevelIndex = 1;
    }
    public void SetUltraGraphicSettings()
    {
        GraphicsSettings.defaultRenderPipeline = allResolution[0];
        QualitySettings.renderPipeline = allResolution[0];
        graphicLevelIndex = 0;
    }
}
