using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUpdator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject clone;
    private GPG_A_A_LB GPG_A_A_LB_RS;
    private UIDataMain DataUI;
    private userData userData;
    public Sprite goldCrown;
    public Sprite silverCrown;
    public Sprite bronzeCrown;

    public Transform HOFParent;
    public Transform ReputationParent;
    public Transform TrackRecordParent;

    void Start()
    {
        GPG_A_A_LB_RS = GameObject.Find("/UserData").GetComponent<GPG_A_A_LB>();
        userData = GameObject.Find("/UserData").GetComponent<userData>();
        DataUI = GetComponent<UIDataMain>();


        CreateLeaderboard(GPG_A_A_LB_RS.hallOfFameLeaderboardData, HOFParent);
        CreateLeaderboard(GPG_A_A_LB_RS.reputationLeaderboardData, ReputationParent);
        CreateLeaderboard(GPG_A_A_LB_RS.meadowLandRecordData, TrackRecordParent);
        SetPlayerRankingVisible(GPG_A_A_LB_RS.playerRepCenteredData);

    }

    IEnumerator CreateLeaderBoards()
    {
        yield return new WaitForSeconds(5f);

    }
    private void SetPlayerRankingVisible(List<GPG_A_A_LB.LeaderboardUserData> list)
    {
        try
        {
            foreach (GPG_A_A_LB.LeaderboardUserData data in list)
            {

                if (data.UserID == Social.localUser.id)
                {
                    DataUI.playerRankText.text = data.UserScore.rank.ToString();
                }

            }
        }
        catch (Exception error)
        {
            Debug.Log("Error in Setting User Ranking: " + error);
        }
    }
    private void CreateLeaderboard(List<GPG_A_A_LB.LeaderboardUserData> list, Transform parent)
    {
        foreach(GPG_A_A_LB.LeaderboardUserData data in list)
        {

            GameObject leaderboardObject = Instantiate(clone);
            leaderboardObject.SetActive(true);
            leaderboardObject.transform.SetParent(parent, false);
            LeaderboardObjects leaderboardObjects = leaderboardObject.GetComponent<LeaderboardObjects>();


            switch (data.UserScore.rank)
            {

                case 1:
                    leaderboardObjects.playerIconRanking.sprite = goldCrown;
                    break;

                case 2:
                    leaderboardObjects.playerIconRanking.sprite = silverCrown;
                    break;

                case 3:
                    leaderboardObjects.playerIconRanking.sprite = bronzeCrown;
                    break;

                default:
                    leaderboardObjects.playerIconRanking.enabled = false;
                    break;

            }
            
            //leaderboardObjects.playerAvatorImage.sprite = ConvertToSprite(data.UserProfile.image);
            leaderboardObjects.playerName.text = data.UserProfile.userName;
            leaderboardObjects.playerRank.text = data.UserScore.rank+".";
            leaderboardObjects.playerScore.text = data.UserScore.formattedValue;

            try
            {
                if (data.UserID == Social.localUser.id)
                {
                    leaderboardObject.GetComponent<Image>().enabled = true;
                    leaderboardObjects.playerName.text = userData.playerName;
                }
            }
            catch (Exception error)
            {
                Debug.Log("Error in Checking Current Userid: " + error);
            }
        }
    }
    public Sprite ConvertToSprite(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }
}
