using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementUpdator : MonoBehaviour
{
    private GPG_A_A_LB GPG_A_A_LB_sr;
    private userData userData;

    public GameObject scrollViewParent;
    public GameObject clone;
    public UIDataMain uIData;
    int achievementsCompleted;

    public TMPro.TMP_Text number;

    void Start()
    {
        GPG_A_A_LB_sr = GameObject.Find("/UserData").GetComponent<GPG_A_A_LB>();
        userData = GameObject.Find("/UserData").GetComponent<userData>();
        GPG_A_A_LB_sr.ProgressUpdator();
        AchievementBuilder();

    }

    private void AchievementBuilder()
    {
        achievementsCompleted = 0;
        for (int i = 0; i<GPG_A_A_LB_sr.achievementData.Length;i++)
        {
            int current = i;
            GameObject localGameobject = Instantiate(clone);
            localGameobject.SetActive(true);
            localGameobject.transform.SetParent(scrollViewParent.transform,false);

            AchievementObjects objects = localGameobject.GetComponent<AchievementObjects>();

            objects.Name.text = GPG_A_A_LB_sr.achievementData[i].AchievementName;
            objects.Description.text = GPG_A_A_LB_sr.achievementData[i].AchievementDescription;
            objects.Icon.sprite = GPG_A_A_LB_sr.achievementData[i].AchievementIcon;
            if(GPG_A_A_LB_sr.achievementData[i].IsIncremental)
            {
                objects.ProgressSlider.value = GPG_A_A_LB_sr.progress[i] / 100.0f;
            }
            else
            {
                objects.ProgressSlider.gameObject.SetActive(false);
            }

            if(GPG_A_A_LB_sr.progress[i]>=100)
            {
                objects.ProgressText.text = "Completed";
                achievementsCompleted++;
                objects.ProgressText.color = Color.blue;
                objects.ProgressSlider.gameObject.SetActive(false);
                if (!userData.playerAchievements[i])
                {
                    objects.RewardGameobject.SetActive(true);
                    objects.RewardText.text = GPG_A_A_LB_sr.achievementData[i].AchievementReward + " Carbon";
                    objects.RewardButton.onClick.AddListener(delegate { SetReward(current, objects.RewardGameobject); });
                }
            }
            else
            {
                if (GPG_A_A_LB_sr.achievementData[i].IsIncremental) { objects.ProgressText.text = (GPG_A_A_LB_sr.progress[i]) + "%"; objects.ProgressText.color = Color.blue; }
                else { objects.ProgressText.text = "Not Completed"; objects.ProgressText.color = Color.blue; }
               
            }

        }
        number.text = "Achievements\n"+achievementsCompleted+"/34";

    }

    private void SetReward(int index, GameObject reward)
    {
        userData.playerAchievements[index] = true;
        userData.playerGems += GPG_A_A_LB_sr.achievementData[index].AchievementReward;
        userData.WriteThenRead();

        uIData.playerGemsText.text = userData.playerGems.ToString();
        reward.SetActive(false);
    }

}
