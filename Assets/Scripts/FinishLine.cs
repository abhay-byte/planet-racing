using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] RaceData raceData;

    private void OnTriggerEnter(Collider collider)
    {
        if (raceData.RaceStart)
        {
            if (raceData.PlayerCarBody == collider)
            {
                if (raceData.PlayerCheckpoint == raceData.Lenght
                    && raceData.NoOfLaps - 1 == raceData.PlayerCurrentLap)
                {
                    //raceData.PlayerCurrentLap++;
                    if (!raceData.GotTheWinner)
                    {
                        raceData.GotTheWinner = true;
                        raceData.PlayerWin = true;
                    }
                    raceData.FinishRace();
                }
            }
            else
            {

                
            }
        }

    }
}
