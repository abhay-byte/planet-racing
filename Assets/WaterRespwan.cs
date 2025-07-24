using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRespwan : MonoBehaviour
{
    [SerializeField] private RaceData raceData;
    private void OnTriggerEnter(Collider other)
    {
        if (raceData.PlayerCarBody == other)
        {
            raceData.PlayerCar.GetComponent<CarObjects>().userControl.Refresh();
            raceData.UIDataPlanet.raceOnTracks.SetTrigger("on");
        }

    }
}
