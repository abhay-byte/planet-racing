using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CompleteScript : MonoBehaviour
{
    public sceneObjects Data;
    public float Laps;
    public int noOfCheckpoints;
    List<int> currentCheckpoint = new List<int>(){0,0};
    List<float> currentLap = new List<float>(){0,0};
    List<bool> raceBegan = new List<bool>(){false,false,false};

    public void track1()
    {
        raceStartM(Data.twoPlayer,Data.MT1PlayerPosition,Data.checkpointListT1,Data.MT1PlayerRotation);
        raceBegan[0] = true;
        Data.TrackNo1.SetActive(true);
    }

    public void raceStartM(Transform Player,List<Vector3> playerPostion, Transform checkpoints,
    List<float> playerRotation)
    {
        noOfCheckpoints = checkpoints.childCount;
        SetPlayerMInWorld(Player, playerPostion,playerRotation);
        RaceUIOn();

    } 
    void Update()
    {
        // track 0 for two player
        raceProgressionM(Data.twoPlayer,Data.checkpointListT1,Data.WrongDirectionPlayer,
        Data.PlayerSlider1,Laps,0,Data.MT1PlayerPosition,Data.MlapsPlayer1,Data.MT1PlayerRotation,0);

        raceProgressionM(Data.twoPlayer,Data.checkpointListT1,Data.WrongDirectionPlayer,
        Data.PlayerSlider2,Laps,1,Data.MT1PlayerPosition,Data.MlapsPlayer2,Data.MT1PlayerRotation,0);
    }

    public void raceProgressionM(Transform Player,Transform checkpoints,List<GameObject> WrongDirectionPlayer,
    Slider uiSlider,float laps,int playerNo ,List<Vector3> playerPostion,
    Text LapText,List<float> playerRotation,int trackno)
    {

        if(raceBegan[trackno])
        {
            LapText.text = "Lap "+(currentLap[playerNo]+1)+"/"+laps;
            for (int i = 0; i < noOfCheckpoints; i++) 
            {
                if(Vector3.Distance(Player.GetChild(playerNo).position, checkpoints.GetChild(i).position)<50f)
                {    
                    if(currentCheckpoint[playerNo]==i)
                    {break;} 

                    if((i-currentCheckpoint[playerNo]<0 && i!=0 )|| i-currentCheckpoint[playerNo]>10)
                    {WrongDirectionPlayer[playerNo].SetActive(true);
                    if(i-currentCheckpoint[playerNo]>noOfCheckpoints-2){

                        Player.GetChild(playerNo).localPosition = playerPostion[playerNo]; 
                        Player.GetChild(playerNo).localRotation = Quaternion.Euler(playerRotation[0],
                        playerRotation[1], playerRotation[2]);

                        WrongDirectionPlayer[playerNo].SetActive(false);}}
                    else{WrongDirectionPlayer[playerNo].SetActive(false);} 

                    if (i == noOfCheckpoints-1 && currentCheckpoint[playerNo] - i> -5)
                    {currentLap[playerNo]++;
                    LapText.text = "Lap "+(currentLap[playerNo]+1)+"/"+laps;
                    if(currentLap[playerNo]==laps){

                        RaceEnded(uiSlider,Player,playerPostion,
                    playerRotation,trackno);

                    }}
                    
                    currentCheckpoint[playerNo] = i;
                
                    uiSlider.value = (((float)currentCheckpoint[playerNo]+(currentLap[playerNo]*noOfCheckpoints))/((float)noOfCheckpoints*laps))*1f;
                    break;
                }            
            }
        }
    }

    public void RaceEnded(Slider uiSlider,Transform Player,List<Vector3> playerPostion
    ,List<float> playerRotation,int trackno)
    {
        uiSlider.value = 0f;
        raceBegan[trackno] = false;

        Data.PlayerM1Rigidbody.isKinematic = true;
        Data.PlayerM2Rigidbody.isKinematic = true;
        SetPlayerMInWorld(Player,playerPostion,playerRotation);
        currentCheckpoint = new List<int>(){0,0};
        currentLap = new List<float>(){0,0};
        HomeUIOn();

    }

    public void SetPlayerMInWorld(Transform Player,List<Vector3> playerPostion,List<float> playerRotation)
    {
        Data.PlayerM1Rigidbody.isKinematic = false;
        Data.PlayerM2Rigidbody.isKinematic = false;
        Player.GetChild(0).localPosition = playerPostion[0];
        Player.GetChild(1).localPosition = playerPostion[1];
        Player.GetChild(0).localRotation = Quaternion.Euler(playerRotation[0], playerRotation[1], playerRotation[2]);
        Player.GetChild(1).localRotation = Quaternion.Euler(playerRotation[0], playerRotation[1], playerRotation[2]);  
    }
    public void HomeUIOn()
    {
        Data.HomeUI.alpha = 1f; Data.HomeUI.interactable = true; Data.HomeUI.blocksRaycasts = true;
        Data.RaceUI.alpha = 0f; Data.RaceUI.interactable = false; Data.RaceUI.blocksRaycasts = false;
    }
    public void RaceUIOn()
    {
        Data.HomeUI.alpha = 0f; Data.HomeUI.interactable = false; Data.HomeUI.blocksRaycasts = false;
        Data.RaceUI.alpha = 1f; Data.RaceUI.interactable = true; Data.RaceUI.blocksRaycasts = true;        
    }

}
