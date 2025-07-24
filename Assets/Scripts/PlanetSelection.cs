using UnityEngine;
using UnityEngine.Video;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using Assets.Scripts.Models;
using Assets.Scripts.Utils;

public class PlanetSelection : MonoBehaviour
{
	[SerializeField] private CurrencyData currencyData;
	[SerializeField] private Player player;
	[SerializeField] private GameObject lockGO;
	// called zero
	[SerializeField] private GameObject loadingScene;
	[SerializeField] private Slider slider;
	[SerializeField] private TMP_Text percent;
	[SerializeField] private Player playerFunctions;
	[SerializeField] private GameObject messege;
	[SerializeField] private int playerCoins;
    private int playerReputation;
    private MessageObjects messageObjects;


	//public GameObject destroy;

	public Sprite[] planet = new Sprite[10];


	[SerializeField] private Button enter;
	[SerializeField] private TMP_Text priceText;
	[SerializeField] private Button left;
	[SerializeField] private Button right;
	[SerializeField] private Image BG;
	[SerializeField] private TMP_Text planetLevel;
	[SerializeField] private TMP_Text planetName;
	[SerializeField] private TMP_Text planetGravity;
	[SerializeField] private TMP_Text planetWeather;
	[SerializeField] private TMP_Text planetTemperature;
	[SerializeField] private TMP_Text planetDescription;
	[SerializeField] private TMP_Text planetLenght;

	[SerializeField] private int currentWorld = 1;
	[SerializeField] private int worldEntryFees = 1;
	Animator animator;

	private void Start()
    {
		playerReputation = playerFunctions.GetPlayerReputation();
		right.onClick.AddListener(NextLevel);
		left.onClick.AddListener(PreviousLevel);
		enter.onClick.AddListener(LoadScene);
		animator = gameObject.GetComponent<Animator>();

		BG.sprite = planet[curLevel];
		planetLevel.text = "Level." + (curLevel + 1);
		planetName.text = Constants.planetDescription[0].PlanetName;
		planetDescription.text = Constants.planetDescription[0].PlanetDescription;
		planetTemperature.text = Constants.planetDescription[0].PlanetTemperature;
		planetWeather.text = Constants.planetDescription[0].PlanetWeatherDescription;
		planetGravity.text = Constants.planetDescription[0].PlanetGravityDescription;
		planetLenght.text = "Length. " + Constants.planetDescription[0].PlanetLenght+" KM";
		priceText.text = "Entry Fees. " + PRUtils.CurrencyFormater(Constants.planetDescription[0].PlanetEntryCost.ToString());
		worldEntryFees = Constants.planetDescription[0].PlanetEntryCost;


	}


	int curLevel = 0;
	public void NextLevel()
    {
		if(curLevel<10)
        {
			curLevel++;
			//animator.SetTrigger("Exit");
			StartCoroutine(DataChange());
		}



	}
	public void PreviousLevel()
	{
		if (curLevel > 0)
		{
			curLevel--;
			//animator.SetTrigger("Exit");
			StartCoroutine(DataChange());
		}

	}

	IEnumerator DataChange()
    {
		if (Constants.planetDescription[curLevel].PlanetEntryReputation > playerReputation)
		{
			lockGO.SetActive(true);
		}
		else lockGO.SetActive(false);
		yield return new WaitForSeconds(.05f);
		BG.sprite = planet[curLevel];
		planetLevel.text = "Level." + (curLevel + 1);
		planetName.text = Constants.planetDescription[curLevel].PlanetName;
		planetDescription.text = Constants.planetDescription[curLevel].PlanetDescription;
		planetTemperature.text = Constants.planetDescription[curLevel].PlanetTemperature;
		planetWeather.text = Constants.planetDescription[curLevel].PlanetWeatherDescription;
		planetGravity.text = Constants.planetDescription[curLevel].PlanetGravityDescription;
		planetLenght.text = "Length. " + Constants.planetDescription[curLevel].PlanetLenght + " KM";
		priceText.text = "Entry Fee. "+Constants.planetDescription[curLevel].PlanetEntryCost ;
		worldEntryFees = Constants.planetDescription[curLevel].PlanetEntryCost;
		animator.SetTrigger("Enter");
	}

	public void loadWorld()
    {
		StartCoroutine(LoadAsynchronously(curLevel+2));


	}
	public void LoadScene()
	{
		playerCoins = playerFunctions.GetPlayerCoins();
		playerReputation = playerFunctions.GetPlayerReputation();
        if (playerCoins > worldEntryFees && Constants.planetDescription[curLevel].PlanetEntryReputation<=playerReputation)
        {
			playerFunctions.WorldEntryFee(worldEntryFees);
			StartCoroutine(LoadAsynchronously(curLevel + 2));
		}
        else
        {
			messege.SetActive(true);
			messageObjects.message.text = "Not enough coins.";
        }

	}

	IEnumerator LoadAsynchronously(int sceneIndex)
	{
		loadingScene.SetActive(true);
		yield return new WaitForSeconds(1);
		//SceneManager.LoadScene(sceneIndex);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);



        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            int val = Convert.ToInt32(progress * 100);
            percent.text = val + "%";

            yield return null;
        }
    }
}
