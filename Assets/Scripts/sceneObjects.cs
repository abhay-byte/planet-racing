using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sceneObjects : MonoBehaviour
{
    public Transform twoPlayer;
    public GameObject TrackNo1;
    public Transform aiPlayer;
    public Transform checkpointListT1;

    public GameObject WrongDirectionM1;
    public GameObject WrongDirectionM2;
    public GameObject WrongDirectionUniversal;
    public CanvasGroup HomeUI;
    public CanvasGroup RaceUI;
    public Slider PlayerSlider1;
    public Slider PlayerSlider2;
    public Slider PlayerSliderUniversal;

    public Text MlapsPlayer1;
    public Text MlapsPlayer2;

    public Rigidbody PlayerM1Rigidbody;
    public Rigidbody PlayerM2Rigidbody;

    public List<GameObject> WrongDirectionPlayer;

    public List<Vector3> MT1PlayerPosition = new List<Vector3>()
    {new Vector3(-198.3995f, 76.35697f, -273.1096f),new Vector3(-198.2996f, 76.35697f, -257.6096f)};
    public List<float> MT1PlayerRotation = new List<float>()
    {0,-90f,0};
    // public List<Rigidbody> playerRigidbody = new List<Rigidbody>()
    // {PlayerM1Rigidbody,PlayerM2Rigidbody};
    public Vector3 MT1Player1Position = new Vector3(-198.3995f, 76.35697f, -273.1096f);
    public Vector3 MT1Player2Position = new Vector3(-198.2996f, 76.35697f, -257.6096f);
    
    void Start()
    {

        WrongDirectionPlayer = new List<GameObject>()
    {WrongDirectionM1,WrongDirectionM2};
    }
}
