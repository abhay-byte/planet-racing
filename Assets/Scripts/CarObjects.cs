using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityStandardAssets.Vehicles.Car;
using UnityEngine.InputSystem;

public class CarObjects : MonoBehaviour
{
    public GameObject carBody;
    public MeshRenderer indicatorMeshRenderer;
    public GameObject indicatorGameObject;
    public Light headLights;
    public MeshRenderer interior;
    public GameObject spoiler;
    public CinemachineVirtualCamera carCamera ;
    public CarController carController;
    public CarAIControl carAIControl ;
    public CarUserControl userControl;
    public AiCarSelfRighting AiCarSelfRighting;
    public GameObject[] wheel = new GameObject[4];
    public GameObject[] wheelCollider = new GameObject[4];
    public GameObject wheelDetached;
    public GameObject explosion;
    [SerializeField] private CarDamageSystem damageSystem;
    [SerializeField] private PlayerInput userGUIControl;
    public Material rearLight;
    //[SerializeField] private  damageSystem;

    public CarDamageSystem DamageSystem { get => damageSystem; set => damageSystem = value; }
    public PlayerInput UserGUIControl { get => userGUIControl; set => userGUIControl = value; }

    public void destroyObject()
    {
        Destroy(this.gameObject);

    }


}
