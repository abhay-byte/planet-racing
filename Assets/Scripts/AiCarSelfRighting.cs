using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class AiCarSelfRighting : MonoBehaviour
{
    // Automatically put the car the right way up, if it has come to rest upside-down.
    [SerializeField] private float m_WaitTime = 3f;           // time to wait before self righting
    [SerializeField] private float m_VelocityThreshold = 1f;
    private CarAIControl aIControl;// the velocity below which the car is considered stationary for self-righting

    private float m_LastOkTime; // the last time that the car was in an OK state
    private Rigidbody m_Rigidbody;
    public bool StartRace;

    public bool StartRace1 { get => StartRace; set => StartRace = value; }

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        aIControl = GetComponent<CarAIControl>();
    }


    private void Update()
    {
        // is the car is the right way up
        if (m_Rigidbody.velocity.magnitude > m_VelocityThreshold)
        {
            m_LastOkTime = Time.time;
        }

        if (Time.time > m_LastOkTime + m_WaitTime)
        {
            RightCar();
        }
    }


    // put the car back the right way up:
    private void RightCar()
    {
        if(StartRace)
        {
            // set the correct orientation for the car, and lift it off the ground a little.
            aIControl.transform.position = aIControl.TargetList.GetChild(aIControl.CurrentTarget - 1).position;
            transform.position += Vector3.up;
            transform.rotation = Quaternion.LookRotation(transform.forward);
        }

    }
}

