using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    [SerializeField] private RaceData raceData;
    [SerializeField] private int checkpointIndex;


    private void OnTriggerEnter(Collider collider)
    {
        if(raceData.RaceStart)
        {
            if (raceData.PlayerCarBody == collider)
            {
                if (raceData.PlayerCheckpoint - raceData.PlayerPreviousCheckpoint == 1 ||
                    raceData.PlayerCheckpoint - raceData.PlayerPreviousCheckpoint == -(raceData.Lenght-1))
                    {
                    if(checkpointIndex - raceData.PlayerCheckpoint == 1 ||
                    checkpointIndex - raceData.PlayerCheckpoint == -(raceData.Lenght - 1))
                    {
                        raceData.PlayerPreviousCheckpoint = raceData.PlayerCheckpoint;
                        raceData.PlayerCheckpoint = checkpointIndex;

                        if (raceData.PlayerCheckpoint == raceData.Lenght
                            && raceData.NoOfLaps - 1 == raceData.PlayerCurrentLap)
                        {
                            //raceData.PlayerCurrentLap++;
                            if(!raceData.GotTheWinner)
                            {
                                raceData.GotTheWinner = true;
                                raceData.PlayerWin = true;
                            }
                            raceData.FinishRace();
                        }
                        if (raceData.NoOfLaps - 1 > raceData.PlayerCurrentLap && raceData.PlayerCheckpoint == raceData.Lenght)
                        {
                            raceData.PlayerCurrentLap++;
                            raceData.PlayerPreviousCheckpoint = -1;
                            raceData.PlayerCheckpoint = 0;
                        }
                    }

                }
            }
        }



    }

}
