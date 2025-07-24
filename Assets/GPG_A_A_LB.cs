using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;

public class GPG_A_A_LB : MonoBehaviour
{
    // Start is called before the first frame update
    public Button showAchievementsUI;
    public List<Sprite> iconSprites = new List<Sprite>();

    int journeyBegins = 0;
    int sweetVictory = 1;
    int newbieRacer = 2;
    int rookieRacer = 3;
    int skilledRacer = 4;
    int seasonedRacer = 5;
    int ExperiencedRacer = 6;
    int expertRacer = 7;
    int fantasyRacer = 8;
    int badLuck = 9;
    int extraBadLuck = 10;
    int gotANewCar = 11;
    int carDealer = 12;
    int firstFriend = 13;
    int friendCircle = 14;
    int friendGroup = 15;
    int IAMExtrovert = 16;
    int charismaticBeing = 17;
    int StarCoins100000 = 18;
    int millionaireRacer = 19;
    int billionaireRacer = 20;
    int roadToGladiator = 21;
    int braveWarrior = 22;
    int RighteousKnight = 23;
    int flashyGladiator = 24;
    int levelEvasion = 25;
    int levelLearner = 26;
    int LevelMaster = 27;
    int interplanetoryCitizen = 28;
    int kingReputation = 29;
    int emperorReputation = 30;
    int theConqueror = 31;
    int endOfLine = 32;
    public AchievementData[] achievementData;


    void Start()
    {
        userdata = GetComponent<userData>();
        playGames = GetComponent<PlayGames>();
        showAchievementsUI.onClick.AddListener(Social.ShowAchievementsUI);
        StartCoroutine(LoadAchievementData());


    }

    private string HOFleaderBoardID = "CgkIk4emifEBEAIQIw";
    private string RleaderBoardID = "CgkIk4emifEBEAIQJA";
    private string MLTRleaderBoardID = "CgkIk4emifEBEAIQJg";
    public List<LeaderboardUserData> hallOfFameLeaderboardData;
    public List<LeaderboardUserData> reputationLeaderboardData;
    public List<LeaderboardUserData> meadowLandRecordData;
    public List<LeaderboardUserData> playerRepCenteredData;

    public class LeaderboardUserData
    {
        string userID;
        IScore userScore;
        IUserProfile userProfile;

        public LeaderboardUserData(string userID, IScore userScore, IUserProfile userProfile)
        {
            this.userID = userID;
            this.userScore = userScore;
            this.userProfile = userProfile;
        }

        public string UserID { get => userID; set => userID = value; }
        public IScore UserScore { get => userScore; set => userScore = value; }
        public IUserProfile UserProfile { get => userProfile; set => userProfile = value; }
    }
    public List<LeaderboardUserData> GetLeaderboardData(string id,LeaderboardStart position)
    {
        List<LeaderboardUserData> leaderboardUserData = new List<LeaderboardUserData>();
        PlayGamesPlatform.Instance.LoadScores(
        id,
        position,
        20,
        LeaderboardCollection.Public,
        LeaderboardTimeSpan.AllTime,
        (data) =>
        {
            List<string> UserId = new List<string>();
            List<IUserProfile> userProfile = new List<IUserProfile>();
  
            foreach (IScore score in data.Scores)
            {

                UserId.Add(score.userID);
            }

            string status = "";
            Social.LoadUsers(UserId.ToArray(), (users) =>
            {
                foreach (IScore score in data.Scores)
                {
                    IUserProfile user = FindUser(users, score.userID);

                    LeaderboardUserData userPos = new LeaderboardUserData(score.userID, score, user);
                    leaderboardUserData.Add(userPos);
                    status += "\n" + score.formattedValue + " by " +
                        (string)(
                            (user != null) ? user.userName : "**unk_" + score.userID + "**");
                }
                Debug.Log(status);
            });
            string mStatus = "Leaderboard data valid: " + data.Valid;
            mStatus += "\n approx:" + data.ApproximateCount + " have " + data.Scores.Length;;
            Debug.Log(mStatus);

        });
        return leaderboardUserData;
    }

    private IUserProfile FindUser(IUserProfile[] users, string userid)
    {
        foreach (IUserProfile user in users)
        {
            if (user.id == userid)
            {
                return user;
            }
        }

        return null;
    }

    public void LoadLeaderBoardData()
    {
        if (playGames.playerAuthenticated)
        {
            hallOfFameLeaderboardData = GetLeaderboardData(HOFleaderBoardID,LeaderboardStart.TopScores);
            reputationLeaderboardData = GetLeaderboardData(RleaderBoardID, LeaderboardStart.TopScores);
            meadowLandRecordData = GetLeaderboardData(MLTRleaderBoardID, LeaderboardStart.TopScores);
            playerRepCenteredData = GetLeaderboardData(RleaderBoardID, LeaderboardStart.PlayerCentered);
        }
    }

    IEnumerator LoadAchievementData()
    {
        yield return new WaitForSeconds(5f);
        //ProgressUpdator();

        achievementData = new AchievementData[]
        {
            new AchievementData("Journey Begins","You have started your journey the overthrow the emperor of Star Alliance.",5,false,iconSprites[0],1),
            new AchievementData("Sweet Victory","Win Your First race.",5,false,iconSprites[1],1),
            new AchievementData("Newbie Racer","Win 5 races in any planet of Star Alliance.",5,true,iconSprites[2],5),
            new AchievementData("Rookie Racer","Win 15 races in any planet of Star Alliance.",10,true,iconSprites[3],15),
            new AchievementData("Skilled Racer","Win 30 Racer in any planet of Star Alliance.",15,true,iconSprites[4],30),
            new AchievementData("Seasoned Racer","Win 60 Races in any planet of Star Alliance.",25,true,iconSprites[5],60),
            new AchievementData("Experienced Racer","Win over 100 Races in any planet of Star Alliance.",30,true,iconSprites[6],100),
            new AchievementData("Expert Racer","Win Over 250 races in any planet of Star Alliance.",50,true,iconSprites[7],250),
            new AchievementData("Fantasy Racer","Win over 1000 races in any planet of Star Alliance.",75,true,iconSprites[8],1000),
            new AchievementData("Bad Luck","First Car Totaled.",5,false,iconSprites[9],1),
            new AchievementData("Extra Bad Luck","10 Cars totaled.",15,true,iconSprites[10],10),
            new AchievementData("Got A New Car","Win a car from an opponent in any planet of Star Alliance.",5,false,iconSprites[11],1),
            new AchievementData("Car Dealer","Win over 10 car from an opponent in any planet of Star Alliance.",10,true,iconSprites[12],10),
            new AchievementData("First Friend","Make your first friend.",5,false,iconSprites[13],1),
            new AchievementData("Friend Circle","Make a total of 10 friends",10,true,iconSprites[14],10),
            new AchievementData("Friend Group","Make over 25 Friends.",25,true,iconSprites[15],25),
            new AchievementData("I AM Extrovert","Make over 50 Friends in Fantasy Racing.",75,true,iconSprites[16],50),
            new AchievementData("Charismatic Being","Make over 100 friends in Fantasy Racing.",100,true,iconSprites[17],100),
            new AchievementData("100,000 Star Coins","Have More than 100,000 Star Coins.",5,false,iconSprites[18],1),
            new AchievementData("Millionaire Racer","Have More than 1 million Star Coins.",15,false,iconSprites[19],1),
            new AchievementData("Billionaire Racer","Have More than 1 Billion Star Coins.",25,false,iconSprites[20],1),
            new AchievementData("Road to Gladiator","Win first level of Colosseum.",5,false,iconSprites[21],1),
            new AchievementData("Brave Warrior","Won 10 level of Colosseum.",15,true,iconSprites[22],10),
            new AchievementData("Righteous Knight","Cleared more than 25 levels in Colosseum",25,true,iconSprites[23],25),
            new AchievementData("Flashy Gladiator","Cleared more than 50 level of Colosseum.",35,true,iconSprites[24],50),
            new AchievementData("Level Evasion","Complete first level of Track Escape.",5,false,iconSprites[25],1),
            new AchievementData("Level Learner","Clear 10 Levels of Track Escape.",15,true,iconSprites[26],10),
            new AchievementData("Level Master","Clear all 25 of levels Track Escape.",25,true,iconSprites[27],25),
            new AchievementData("Interplanetary Citizen","Reach Reputation level 8.",15,true,iconSprites[28],8),
            new AchievementData("King Reputation","Reach Reputation level 12 in Fantasy Racing. Now you can Challenge The Emperor.",25,true,iconSprites[29],12),
            new AchievementData("Emperor Reputation","Reach Reputation level 99 in Fantasy Racing.",100,true,iconSprites[30],99),
            new AchievementData("The Conqueror","Defeat all the kings of Star Alliance System.",75,true,iconSprites[31],10),
            new AchievementData("End of line","The Evil Emperor of Star Alliance has been defeated. Peace and Justice will return to the Star System...or Not?",50,false,iconSprites[32],1),
            //new AchievementData("Car Lost!!!!","Lose your car to an opponent in any planet of Star Alliance.",5,false,iconSprites[33],1),

        };

    }
    public void ProgressUpdator()
    {
        int value = 100;
        progress.SetValue(value, journeyBegins);

        if(userdata.playerStats[userdata.NoOfRacesWon]>=1)
        {
            value = 100; 
        }
        else { value = 0; }
        progress.SetValue(value, sweetVictory);

        value = (int)(((double)userdata.playerStats[userdata.NoOfRacesWon] / 5f)*100.0f);
        progress.SetValue(value, newbieRacer);

        value = (int)(((double)userdata.playerStats[userdata.NoOfRacesWon] / 15f) * 100f);
        progress.SetValue(value, rookieRacer);

        value = (int)(((double)userdata.playerStats[userdata.NoOfRacesWon] / 30f) * 100f);
        progress.SetValue(value, skilledRacer);

        value = (int)(((double)userdata.playerStats[userdata.NoOfRacesWon] / 60f) * 100f);
        progress.SetValue(value, seasonedRacer);

        value = (int)(((double)userdata.playerStats[userdata.NoOfRacesWon] / 100f) * 100f);
        progress.SetValue(value, ExperiencedRacer);

        value = (int)(((double)userdata.playerStats[userdata.NoOfRacesWon] / 250f) * 100f);
        progress.SetValue(value, expertRacer);

        value = (int)(((double)userdata.playerStats[userdata.NoOfRacesWon] / 1000f) * 100f);
        progress.SetValue(value, fantasyRacer);

        if (userdata.playerStats[userdata.CarTotalled] >= 1)
        {
            value = 100;
        }
        else { value = 0; }
        progress.SetValue(value, badLuck);

        value = (int)(((double)userdata.playerStats[userdata.CarTotalled] / 10f) * 100f);
        progress.SetValue(value, extraBadLuck);

        if (userdata.playerStats[userdata.NoOfCarsWon] >= 1)
        {
            value = 100;
        }
        else { value = 0; }
        progress.SetValue(value, gotANewCar);

        value = (int)(((double)userdata.playerStats[userdata.NoOfCarsWon] / 10f) * 100f);
        progress.SetValue(value, carDealer);

        value = 0;
        progress.SetValue(value, firstFriend);

        value = 0;
        progress.SetValue(value, friendCircle);

        value = 0;
        progress.SetValue(value, friendGroup);

        value = 0;
        progress.SetValue(value, IAMExtrovert);

        value = 0;
        progress.SetValue(value, charismaticBeing);

        if (userdata.playerCoins >= 100000)
        {
            value = 100;
        }
        else { value = 0; }
        progress.SetValue(value, StarCoins100000);

        if (userdata.playerCoins >= 1000000)
        {
            value = 100;
        }
        else { value = 0; }
        progress.SetValue(value, millionaireRacer);

        if (userdata.playerCoins >= 1000000000)
        {
            value = 100;
        }
        else { value = 0; }
        progress.SetValue(value, billionaireRacer);

        if (userdata.playerStats[userdata.CarbattleWon] >= 1)
        {
            value = 100;
        }
        else { value = 0; }
        progress.SetValue(value, roadToGladiator);

        value = (int)(((double)userdata.playerStats[userdata.CarbattleWon]/10f) * 100f);
        progress.SetValue(value, braveWarrior);

        value = (int)(((double)userdata.playerStats[userdata.CarbattleWon] / 25f) * 100f);
        progress.SetValue(value, RighteousKnight);

        value = (int)(((double)userdata.playerStats[userdata.CarbattleWon] / 50f) * 100f);
        progress.SetValue(value, flashyGladiator);

        if (userdata.playerStats[userdata.CarEscapeLevel] >= 1)
        {
            value = 100;
        }
        else { value = 0; }
        progress.SetValue(value, levelEvasion);

        value = (int)(((double)userdata.playerStats[userdata.CarEscapeLevel] / 10f) * 100f);
        progress.SetValue(value, levelLearner);

        value = (int)(((double)userdata.playerStats[userdata.CarEscapeLevel] / 25f) * 100f);
        progress.SetValue(value, LevelMaster);

        value = (int)(((double)userdata.reputation[0]/ 8.0f) * 100.0f);
        progress.SetValue(value, interplanetoryCitizen);

        value = (int)(((double)userdata.reputation[0] / 12f) * 100f);
        progress.SetValue(value, kingReputation);

        value = (int)(((double)userdata.reputation[0] / 99f) * 100f);
        progress.SetValue(value, emperorReputation);

        value = (int)(((userdata.kingDefeated[0]+ userdata.kingDefeated[1]+ userdata.kingDefeated[2]+ userdata.kingDefeated[3]+
            userdata.kingDefeated[4]+ userdata.kingDefeated[5]+ userdata.kingDefeated[6]+ userdata.kingDefeated[7]
            + userdata.kingDefeated[8]+ userdata.kingDefeated[9]) / 10f) * 100f);
        progress.SetValue(value, theConqueror);

        value = (int)(((double)userdata.kingDefeated[10] / 1) * 100f);
        progress.SetValue(value, endOfLine);




    }
    private userData userdata;
    private PlayGames playGames;
    public int[] progress = new int[40];


    

    enum rewardTypes { COINS, CARBON };

    public class AchievementData
    {

        private string achievementName;
        private string achievementDescription;
        private int achievementReward;
        private bool isIncremental;
        private Sprite achievementIcon;
        private int steps;

        public AchievementData(string achievementName, string achievementDescription, int achievementReward, bool isIncremental, Sprite achievementIcon, int steps)
        {
            this.achievementName = achievementName;
            this.achievementDescription = achievementDescription;
            this.achievementReward = achievementReward;
            this.isIncremental = isIncremental;
            this.achievementIcon = achievementIcon;
            this.steps = steps;
        }

        public string AchievementName { get => achievementName; set => achievementName = value; }
        public string AchievementDescription { get => achievementDescription; set => achievementDescription = value; }
        public int AchievementReward { get => achievementReward; set => achievementReward = value; }
        public bool IsIncremental { get => isIncremental; set => isIncremental = value; }
        public Sprite AchievementIcon { get => achievementIcon; set => achievementIcon = value; }
        public int Steps { get => steps; set => steps = value; }
    }

}