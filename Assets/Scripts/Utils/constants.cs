using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;
using Assets.Scripts.Utils;
using UnityEngine.UI;
using TMPro;


public class constants : MonoBehaviour
{

    public readonly static Sprite planet1;
    public readonly static Sprite planet2;
    public readonly static Sprite planet3;
    public readonly static Sprite planet4;
    public readonly static Sprite planet5;
    public readonly static Sprite planet6;
    public readonly static Sprite planet7;
    public readonly static Sprite planet8;
    public readonly static Sprite planet9;
    public readonly static Sprite planet10;
    public readonly static Sprite planet11;

    public readonly static Sprite planetT1;
    public readonly static Sprite planetT2;
    public readonly static Sprite planetT3;
    public readonly static Sprite planetT4;
    public readonly static Sprite planetT5;
    public readonly static Sprite planetT6;
    public readonly static Sprite planetT7;
    public readonly static Sprite planetT8;
    public readonly static Sprite planetT9;
    public readonly static Sprite planetT10;
    public readonly static Sprite planetT11;

    public readonly static Sprite planetK1;
    public readonly static Sprite planetK2;
    public readonly static Sprite planetK3;
    public readonly static Sprite planetK4;
    public readonly static Sprite planetK5;
    public readonly static Sprite planetK6;
    public readonly static Sprite planetK7;
    public readonly static Sprite planetK8;
    public readonly static Sprite planetK9;
    public readonly static Sprite planetK10;
    public readonly static Sprite planetK11;

    public readonly static Sprite planetKT1;
    public readonly static Sprite planetKT2;
    public readonly static Sprite planetKT3;
    public readonly static Sprite planetKT4;
    public readonly static Sprite planetKT5;
    public readonly static Sprite planetKT6;
    public readonly static Sprite planetKT7;
    public readonly static Sprite planetKT8;
    public readonly static Sprite planetKT9;
    public readonly static Sprite planetKT10;
    public readonly static Sprite planetKT11;
    public readonly static Sprite planetKT12;


}


namespace Assets.Scripts.Utils
{

    public class Constants
    {
         


        public static readonly List<int> timeRequired = new List<int>()
        {
            120,150,145,110,100,100,100,90,110,125,60,100
        };
        private Constants() { }

        public static readonly Dictionary<int, string> engines = new Dictionary<int, string>()
    {
        {0,"40 AP Basic"},
        {1,"60 AP Boost"},
        {2,"85 AP Charge"},
        {3,"135 AP Hybrid"},
        {4,"240 AP Twin Turbo"},
        {5,"350 AP Twin Charged"},
        {6,"500 AP Turbo X6"},
        {7,"750 AP Twin Turbo X6"},
        {8,"800 AP Turbo X8"},
        {9,"1050 AP Twin Turbo X8"},
        {10,"1250 AP Turbo X16"},
        {11,"1375 AP Twin Turbo X16"},
        {12,"1595 AP Qaud Turbo Charged X16"},
        {13,"1885 AP Quad Turbo Charged W16"},
    };
        /*        private string carPart;
                private int carPartHealth;
                private int carPartMass;
                private int carPartTopSpeed;
                private int carPartAcceleration;
                private float carPartSteerHelper;
                private float carPartTractionControl;
                private int carPartDownForce;
                private int carPartBreakTorque;*/
        public static List<carPartdata> enginesData = new List<carPartdata>()
    {
        new carPartdata("40 AP Basic",                      1000,100,30,2500,0,0,0,1000,25),
        new carPartdata("60 AP Boost",                      1100,125,40,3600,0,0,0,1050,50),
        new carPartdata("85 AP Charge",                     1200,150,50,4700,0,0,0,1010,85),
        new carPartdata("135 AP Hybrid",                    1300,200,60,5800,0,0,0,1100,125),
        new carPartdata("240 AP Twin Turbo",                1500,250,65,6900,0,0,0,1150,250),
        new carPartdata("350 AP Twin Charged",              1600,300,70,7000,0,0,0,1200,350),
        new carPartdata("500 AP Turbo X6",                  1800,350,75,8100,0,0,0,1250,450),
        new carPartdata("750 AP Twin Turbo X6",             2000,400,80,9200,0,0,0,1350,560),
        new carPartdata("800 AP Turbo X8",                  2200,450,85,10300,0,0,0,1400,685),
        new carPartdata("1050 AP Twin Turbo X8",            2500,425,90,11400,0,0,0,1500,750),
        new carPartdata("1250 AP Turbo X16",                3000,400,95,12500,0,0,0,1600,850),
        new carPartdata("1375 AP Twin Turbo X16",           4000,350,100,13600,0,0,0,1700,950),
        new carPartdata("1595 AP Qaud Turbo Charged X16",   4200,325,105,14700,0,0,0,1800,1050),
        new carPartdata("1885 AP Quad Turbo Charged W16",   3500,300,110,15800,0,0,0,2000,1250),

    };
        public static List<RandomData> allDataLimit = new List<RandomData>()
    {
           new RandomData(Tuple.Create(0,1),Tuple.Create(0,2),Tuple.Create(0,2),Tuple.Create(0,2)),
           new RandomData(Tuple.Create(1,2),Tuple.Create(2,3),Tuple.Create(2,3),Tuple.Create(0,2)),
           new RandomData(Tuple.Create(2,4),Tuple.Create(3,4),Tuple.Create(2,4),Tuple.Create(0,2)),
           new RandomData(Tuple.Create(4,5),Tuple.Create(4,5),Tuple.Create(2,4),Tuple.Create(0,3)),
           new RandomData(Tuple.Create(5,6),Tuple.Create(5,6),Tuple.Create(2,5),Tuple.Create(0,3)),
           new RandomData(Tuple.Create(6,7),Tuple.Create(6,7),Tuple.Create(3,6),Tuple.Create(0,3)),
           new RandomData(Tuple.Create(7,8),Tuple.Create(7,8),Tuple.Create(3,6),Tuple.Create(1,3)),
           new RandomData(Tuple.Create(8,9),Tuple.Create(8,9),Tuple.Create(3,6),Tuple.Create(1,3)),
           new RandomData(Tuple.Create(9,10),Tuple.Create(9,10),Tuple.Create(4,7),Tuple.Create(1,3)),
           new RandomData(Tuple.Create(10,11),Tuple.Create(7,10),Tuple.Create(4,7),Tuple.Create(2,3)),
           new RandomData(Tuple.Create(11,12),Tuple.Create(9,10),Tuple.Create(5,7),Tuple.Create(2,4)),
           new RandomData(Tuple.Create(12,13),Tuple.Create(10,10),Tuple.Create(5,7),Tuple.Create(2,4)),
    };

        public static readonly List<float> solidColorType = new List<float>() { 0, .8f };
        public static readonly List<float> pearlescentColorType = new List<float>() { .5f, 1 };
        public static readonly List<float> matteColorType = new List<float>() { .5f, .2f };
        public static readonly List<float> metallicColorType = new List<float>() { 1, .85f };
        public static readonly List<List<float>> colorType = new List<List<float>>()
    {
        solidColorType,pearlescentColorType,matteColorType,metallicColorType
    }
        ;

        public static readonly Dictionary<int, string> tires = new Dictionary<int, string>()
    {
        {0,"Low Grip"},
        {1,"Medium Grip"},
        {2,"High Grip"},
        {3,"Flow Grip"},
        {4,"Fast Grip"},
        {5,"Super Grip"},
        {6,"Max Grip"},
        {7,"Ultra Grip"},
    };

        public static List<carPartdata> tiresData = new List<carPartdata>()
    {
        new carPartdata("Low Grip",      750,25,0,0,0.4f,0.9f,0,12500,65),
        new carPartdata("Medium Grip",   1000,35,0,0,0.5f,.9f,0,15000,85),
        new carPartdata("High Grip",     1250,45,-2,0,.7f,0.9f,0,18000,105),
        new carPartdata("Flow Grip",     1500,30,5,200,.6f,.9f,0,12500,250),
        new carPartdata("Fast Grip",     1800,25,10,300,.65f,.9f,0,15000,385),
        new carPartdata("Super Grip",    2500,30,-5,-100,0.75f,0.9f,0,30000,450),
        new carPartdata("Max Grip",      3000,35,-10,-200,0.85f,0.8f,0,35000,560),
        new carPartdata("Ultra Grip",    4000,40,-15,-300,.9f,.85f,0,40000,750),

    };

        public static readonly Dictionary<int, string> body = new Dictionary<int, string>()
    {
        {0,"Micro"},
        {1,"Basic"},
        {2,"Compact"},
        {3,"Blaze"},
        {4,"Coupe"},
        {5,"Roadster"},
        {6,"Spyder"},
        {7,"Sports"},
        {8,"Tuner"},
        {9,"Lightning"},
        {10,"Racing"},
    };
        public static List<carPartdata> bodyData = new List<carPartdata>()
    {
        new carPartdata("Micro",    2500,400,70,500,0,0,100,0,50),
        new carPartdata("Basic",    3000,450,80,600,0,0,110,0,105),
        new carPartdata("Compact",  3500,500,85,700,0,0,125,0,250),
        new carPartdata("Blaze",    4000,550,90,800,0,0,150,0,385),
        new carPartdata("Coupe",    4500,600,95,900,0,0,200,0,495),
        new carPartdata("Roadster", 5000,625,100,1000,0,0,250,0,685),
        new carPartdata("Spyder",   5500,600,106,1250,0,0,300,0,755),
        new carPartdata("Sports",   5900,550,117,1500,0,0,350,0,845),
        new carPartdata("Tuner",    5750,575,129,2000,0,0,400,0,1245),
        new carPartdata("Lightning",5250,525,135,2500,0,0,450,0,1520),
        new carPartdata("Racing",   5000,500,138,3000,0,0,500,0,1750),
    };
        public static List<carPartdata> nitrosData = new List<carPartdata>()
    {
        new carPartdata("1 Litre",     300,1,25,5000,0,0,0,0,10),
        new carPartdata("3 Litre",     700,3,25,5000,0,0,0,0,30),
        new carPartdata("5 Litre",     1000,5,25,5000,0,0,0,0,50),
        new carPartdata("10 Litre",    1600,10,25,5000,0,0,0,0,100),
        new carPartdata("18 Litre",    2500,28,25,5000,0,0,0,0,180),

    };
        public static readonly List<planetData> planetDescription = new List<planetData>()
        {
            new planetData(0,"Meadowlands",constants.planet1,constants.planetT1,1000,1,
                "Meadowlands is a lush, verdant planet, characterized by rolling hills and expansive grassy fields. The planet is dotted with gently rising elevations, providing scenic vistas and challenging terrain for racers. Despite being covered in a significant amount of water, the racetracks on Meadowlands are all located on solid ground, offering a mix of straightaways and winding turns that test a driver's skill and endurance. The mild, temperate climate of Meadowlands is perfect for outdoor activities, with occasional rain showers that keep the landscape green and thriving. The average temperature on Meadowlands is around 20-25°C (68-77°F) during the day, dropping to around 15°C (59°F) at night.",
                "Gravity: Normal Earth-like gravity","Weather: Mild, temperate climate with occasional rain showers.","Temperature: Average temperature of 20-25°C (68-77°F) during the day, dropping to around 15°C (59°F) at night.",
                1,new KingData("King Hillcrest, Ruler of the rolling hills and fields.",
                100000,2,constants.planetK1,GenerateCarData(1),"King Hillcrest is the ruler of Meadowlands, a planet known for its rolling hills and lush grassy fields. However, despite his status as the king, Hillcrest is not well-liked by the people of Meadowlands. He is known for his selfishness and lack of concern for the well-being of his subjects. King Hillcrest is a skilled racer, but uses his abilities to assert his dominance and control over the people of Meadowlands. He is a ruthless competitor and will stop at nothing to maintain his power, even if it means cheating or manipulating others. Despite his negative reputation, King Hillcrest remains in power, using his wealth and influence to maintain his rule over Meadowlands.",
                GenerateCarData(0),1,constants.planetKT1,1),2.15f,true,.75f,.5f,false,false,false,false,false,false,false,false,true,false,false,false,false),
            new planetData(6,"Dune Haven",constants.planet7,constants.planetT7,5000,2,
                "The planet Dune Heaven is known for its abundant resources hidden beneath the sand dunes. Despite the harsh climate, with its hot temperature, strong winds, low gravity, and frequent tornadoes, the planet is a hub for resource-seeking corporations and miners. The valuable resources that can be found on Dune Heaven include precious minerals and valuable ores, which are highly sought after by those in the galaxy. Despite the difficulties of extracting these resources, many are willing to brave the harsh conditions in order to profit from the treasures that the planet has to offer.",
                "Gravity: The gravity on this planet is very lower than that of Earth, making it easier for the racers to move around.","Weather: The weather is characterized by strong winds that often pick up sand and create dust storms, and tornadoes that swirl across the sand dunes.","Temperature: The temperature is extremely hot, with temperatures reaching well over 100°C (212°F) in some areas.",
                .5f,new KingData("King Sirocco, Ruler of the Dune Haven.",
                200000,3,constants.planetK7,GenerateCarData(2),"King Sirocco is a cunning ruler of the planet Dune Haven. Despite the harsh conditions of his desert kingdom, he has managed to maintain his grip on power by discovering valuable resources beneath the sand and using them to build a strong and prosperous empire. King Sirocco is known for his cunning tactics and his ability to navigate the treacherous winds of the Dune Haven, which are known for their fierce storms and destructive tornadoes. Despite his ruthless reputation, the people of Dune Haven look to King Sirocco as their protector, for he is the one who keeps them safe from the dangers of the sands.\nHe is a skilled racer, who uses his cunning and his expertise on the tracks of Dune Haven to dominate the competition. Despite his ruthless tactics, he is respected and feared by all who race on his planet. With his cunning mind and mastery of the sandy terrain, King Sirocco is a force to be reckoned with on the race circuits of the galaxy.",
                GenerateCarData(1),3,constants.planetKT7,2),4.5f,false,0,0,false,false,false,false,false,false,true,false,false,false,false,true,false),
            new planetData(8,"Pink Paradise",constants.planet9,constants.planetT9,10000,2,
                "Pink Paradise is a unique world, characterized by its vibrant pink hue that permeates its landscapes. The planet is dotted with hills and elevations, and its surface is dominated by lush vegetation and diverse plant life, including pink-colored trees. Despite its seemingly beautiful appearance, Pink Paradise is actually a dangerous place, as its water bodies contain green, toxic substances that can harm or even kill unsuspecting travelers. The gravity on this planet is extremely low, making it a challenge for visitors to navigate and traverse its terrain. Despite these challenges, Pink Paradise remains a fascinating and intriguing world, with a rich and diverse ecosystem waiting to be explored.",
                "Gravity: The gravity on Pink Paradise is extremely low, causing it to have a weak hold on its inhabitants and objects.","Weather: Constantly changing due to the planet's toxic and unstable atmosphere, with sudden outbursts of wind, rain, and lightning.","Temperature: Warm and humid, making it an hospitable place for most forms of life.",
                .2f,new KingData("King Venomvines, the Toxic Monarch of the Pink Paradise",
                3000000,4,constants.planetK9,GenerateCarData(3),"King Venomvines is a formidable alien ruler, deceitful ruler, with a reputation for cunning , strategic thinking and known for his treacherous ways and willingness to do whatever it takes to maintain his power over the Pink Paradise. He rules the Pink Paradise with an iron fist, using his mastery of the poisonous flora and fauna that thrives on the planet to maintain control and protect his kingdom.\nDespite his toxic reputation, King Venomvines is a charismatic leader who commands the loyalty of his subjects. He is feared by many, but revered by those who know him as a fair and just ruler who will do anything to protect his kingdom. With his imposing appearance and mastery of the elements, King Venomvines is a force to be reckoned with, and those who dare to cross him are sure to regret it. He is a formidable opponent on the racetrack, known for his aggressive driving style and willingness to bend the rules to get ahead.",
                GenerateCarData(2),1,constants.planetKT9,1),4.8f,true,.8f,.5f,true,false,false,false,false,false,false,false,false,true,false,true,false),

            new planetData(5,"Volcania",constants.planet6,constants.planetT6,20000,3,
                "Volcania is a fiery and intense planet, constantly bombarded by the showers of rock and ash from its numerous active volcanoes. The sky is filled with thick ash, making visibility low and causing a constant rainfall of ash. The ground is barren, with only a few hardy vegetation and dead trees scattered across the otherwise desolate landscape. Despite its inhospitable nature, the high gravity on Volcania makes it a challenge for racers, as they navigate the treacherous hills and valleys created by the volcanic activity. The temperature is extremely hot, with temperatures reaching over 1000 degrees Celsius near the active volcanoes. With such harsh conditions, only the bravest and most skilled racers can hope to survive the race on Volcania.",
                "Gravity: Strong gravitational pull compared to earth, making it difficult during meteor shower.","Weather: The weather is very harsh and unpredictable, with ash storms being a common occurrence due to constant volcanic eruptions.","Temperature: The temperature on the planet is extremely high, with temperatures near the active volcano hills reaching up to 1000°C.",
                1.5f,new KingData("King Blaze, the Fire Lord of Volcania",
                5000000,5,constants.planetK6,GenerateCarData(4),"King Blaze is the fiery ruler of the planet of Volcania. He is known for his passionate and intense personality, reflecting the constant volcanic activity on his planet. His aura is filled with the energy of molten lava and his voice booms like an eruption. Despite his fearsome reputation, King Blaze is respected by his subjects for his unwavering leadership and protection of their fiery world. He sits on a throne of molten rock and is attended by flocks of ash-covered birds, which soar through the ash-filled sky. With his fiery spirit and unbreakable resolve, King Blaze is a true force to be reckoned with.",
                GenerateCarData(3),3,constants.planetKT6,3),4.1f,false,0,0,false,true,true,true,false,false,false,false,false,false,true,false,false),

            new planetData(2,"Alpenglow",constants.planet3,constants.planetT3,30000,3,
                "Alpenglow is a planet filled with mountain regions, with wide, flat paths snaking through the peaks and valleys. Racers come from all over the galaxy to test their skills on Alpenglow's challenging terrain, navigating tight turns and dodging obstacles as they race to the finish line. The grassy slopes are dotted with rocky outcroppings, adding an extra layer of difficulty to each race.\nplanet's higher-than-Earth gravity gives racers a slight disadvantage in terms of speed and agility, but also means that the terrain is more eady to navigate. Heavier gravity will wear car down more quickly. Sudden changes in wind direction and intensity can make racing conditions unpredictable, and the harsh, hot climate is a true test of a racer's endurance and resolve.\nDespite the challenges posed by Alpenglow, it is considered one of the most beautiful planets in the galaxy, with towering peaks, rolling hills, and fields of waving grass that create a stunning backdrop for each race. Racers who can master the planet's terrain and withstand its harsh conditions will earn a place in the annals of racing history.",
                "Gravity: higher gravity compared to Earth, causing racers to slow down.","Weather: The weather on Alpenglow is always in flux, with sudden changes in wind direction and intensity that can make racing conditions unpredictable.","Temperature: The temperature on Alpenglow ranges from warm to scorching, with daytime temperatures often reaching 40 degrees Celsius or higher.",
                1.35f,new KingData("King Altair, Ruler of the Mountains.",
                700000,6,constants.planetK3,GenerateCarData(5),"King Altair, ruler of Alpenglow, is known for his unconventional and free-spirited approach to rule. Despite being the king of a mountainous planet, he is an expert in aerial acrobatics and often performs death-defying stunts while racing on the flat paths of Alpenglow's mountains. His flamboyant style and fearlessness have earned him a reputation as one of the most entertaining and charismatic kings in the galaxy. But despite his playful demeanor, King Altair takes his duties as ruler seriously and is always working to improve the lives of those living on Alpenglow.",
                GenerateCarData(4),1,constants.planetKT3,2),2.8f,true,.25f,.75f,false,true,false,false,false,false,false,false,true,false,false,false,false),
            new planetData(7,"Eco-Ridge",constants.planet8,constants.planetT8,100000,4,
                "The planet Eco-Ridge is a lush and verdant world covered in dense forests, meandering rivers, and swampy wetlands. At its center lies a towering and ancient tree, the likes of which has never been seen before. The air is fresh and pure, with a rich, earthy scent that permeates the entire planet.\nThe planet's climate is temperate, with warm summers and mild winters. The rainfall is abundant, fueling the growth of the dense vegetation that covers the planet. The gravity on Eco-Ridge is slightly lower than that of Earth, allowing for easier movement and exploration of the towering trees and sprawling rivers.",
                "Gravity: The gravity on this planet is slightly to that of Earth.","Weather: The weather is primarily influenced by the presence of the vast forests and rivers, which create a humid and rainy climate.","Temperature: The temperature on this planet is moderate, with warm temperatures during the day and cooler temperatures at night.",
                .8f,new KingData("King Thundertree, the Ruler of the Eco-Ridge.",
                8000000,7,constants.planetK8,GenerateCarData(6),"King Thundertree is the ruler of Eco-Ridge, a strong and wise leader who is revered by all the inhabitants of the planet. His power is drawn from the massive tree at the center of the planet, and he is said to be in tune with the rhythm of the forest itself. He is a cunning and strategic thinker, always one step ahead of his opponents, and his ability to read the landscape and the weather has earned him a reputation as one of the greatest racers in the galaxy. Despite his might, King Thundertree is a just and fair ruler, beloved by all who live on Eco-Ridge.\nKing Thundertree of Eco-Ridge is not just a ruler, but also a skilled racer who is known to be a worthy competitor to even the greatest racers, including the Emperor of the racing world. He is the protector of the massive, ancient tree at the center of the forest planet, and his knowledge of the twisting, winding rivers and swamps of Eco-Ridge make him a formidable force on the racetrack. With his swiftness, agility and unwavering focus, King Thundertree is a force to be reckoned with in any race.",
                GenerateCarData(5),1,constants.planetKT8,1),4.5f,true,1,1,false,true,false,false,false,false,false,true,true,false,false,false,false),

            new planetData(4,"Frostfall",constants.planet5,constants.planetT5,200000,5,
                "This cold and frosty planet is covered in a blanket of snow and ice. Everywhere you look, there are rolling hills and mountains of snow, with dense white fog rolling in and out. Despite the harsh conditions, there is still an abundance of life on this planet, with vegetation including tall trees, lush grasses, and a variety of brightly colored flowers. The temperature on this planet is well below freezing, and the constant snowfall adds to the feeling of being in a winter wonderland. The weather is constantly changing, with strong winds blowing snow around and whiteouts occurring frequently. Despite its inhospitable conditions, it is a truly stunning and unique planet that is a must-visit for those brave enough to venture into its frozen landscapes.",
                "Gravity: The gravity on this planet is similar to that of Earth.","Weather: The weather is constantly changing, with heavy snowfall and white, dense fog that make visibility difficult.","Temperature: Extremely cold, with an average temperature of -50°C (-58°F).",
                .95f,new KingData("King Frostbite, the Ice-Hearted Ruler of Frostfall.",
                1100000,8,constants.planetK5,GenerateCarData(7),"King Frostbite is known as the Ice-Hearted Ruler of Frostfall. He is a tall and imposing figure, with a piercing gaze and a cold demeanor. Despite his icy exterior, King Frostbite is a just ruler, feared but respected by his subjects. He has a deep connection to his planet and its frigid landscape, and is always seeking new ways to protect and preserve the delicate balance of the ecosystem. King Frostbite spends much of his time on the ice-covered hills of his kingdom, surveying the land and communing with nature. He is a formidable opponent on the racetrack, with a keen eye for the best lines and a masterful control of his speed and agility. Despite the harsh conditions of his home, King Frostbite is a proud and resilient leader, and his kingdom is a symbol of strength and endurance.",
                GenerateCarData(6),3,constants.planetKT5,2),4f,false,0,0,false,true,false,false,true,false,false,false,false,false,false,false,true),
            new planetData(1,"Oceanus",constants.planet2,constants.planetT2,450000,6,
                "Oceanus is a serene and tranquil planet, surrounded by calm, glassy waters. The surface of the planet is dotted with small islands and archipelagos, each offering a different type of terrain for racers to navigate through. From the towering mountains to the rolling hills and verdant forests, Oceanus offers a true test of skill and endurance. The weather is mild and temperate, with occasional rains that provide fresh water to the abundant sea life.Racing on Oceanus is a unique experience, with the calm waters allowing racers to reach high speeds and take tight turns without the fear of crashing. The low gravity of the planet adds an extra layer of excitement to the races, with racers able to soar through the air and perform daring stunts. Despite its tranquil appearance, the races on Oceanus are fiercely competitive, with racers from all over the galaxy coming to test their skills and challenge for the title of King or Queen of the Calm Waters.",
                "Gravity: Low gravity compared to Earth, causing racers to float and maneuver in a unique way while racing on the water.","Weather: Intermittent rainstorms that create massive waves and unpredictable currents, adding an extra layer of challenge to the race.","Temperature: Scorching hot during the day, reaching up to 40°C (104°F), but rapidly dropping to below freezing temperatures at night, requiring racers to adapt to the harsh conditions.",
                .8f,new KingData("King Current, Ruler of the calm waters.",
                15000000,9,constants.planetK2,GenerateCarData(8),"King Current is the ruler of the tranquil ocean planet, Oceanus. As the name suggests, he has a deep understanding of the currents and tides that flow through the planet's waters. This knowledge gives him a distinct advantage on the racetrack and has earned him the respect of racers from across the galaxy.\nKing Current's racing style is calculated and strategic, reflecting his mastery of the ocean's forces. He is not afraid to take bold risks when necessary, but is equally comfortable playing a waiting game and using his knowledge of the environment to outmaneuver his opponents.\nDespite his competitiveness on the racetrack, King Current is known for his fair play and sportsmanship. He is dedicated to maintaining the peace and stability on Oceanus, and works tirelessly to promote friendly competition among racers.\nWith his commanding presence and unwavering spirit, King Current is a true leader and a symbol of strength and determination on the racetrack and beyond.",
                GenerateCarData(7),1,constants.planetKT2,1),3.32f,true,1,1,false,false,false,false,false,true,false,false,false,false,false,true,false),
             new planetData(3,"Crystalline Mountains",constants.planet4,constants.planetT4,1250000,7,
                "The planet Crystalline Mountains is a stunning and unique world filled with towering crystal formations and lush, verdant mountains. The track for races winds through forests of shimmering blue crystal and over hills dotted with bushes and tall grasses. The atmosphere of the planet is thick and rich, with a warm and humid climate that provides ample moisture for the vegetation to thrive. The temperature is comfortable and mild, and the gravity is slightly lighter than Earth, allowing for a feeling of weightlessness during races. The crystal formations emit a soft, otherworldly glow that bathes the track in a cool blue light, making for a truly magical racing experience.",
                "Gravity: Slightly weaker than Earth's gravity, allowing racers to soar through the air with ease.","Weather: The air is filled with a constant, gentle breeze that cools the area. Due to the abundance of crystal, there are occasional, brief flashes of light that illuminate the landscape.","Temperature: Mild, with temperatures ranging from cool to warm throughout the day and night.",
                .65f,new KingData("King Sapphire, Ruler of the Crystalline Kingdom.",
                2000000,10,constants.planetK4,GenerateCarData(9),"King Sapphire is a regal figure who rules over the crystalline kingdom of Crystalline Mountains. With a keen sense of justice and a deep love for his people, King Sapphire is both respected and feared by the inhabitants of his planet. His impressive height and muscular build, combined with his shimmering blue armor, give him an intimidating presence, and his piercing eyes are said to be able to see into the hearts of even the most cunning of opponents. Despite his fearsome reputation, King Sapphire is a fair and just leader, always seeking to ensure that his subjects live in peace and prosperity. He is a formidable opponent on the race track, but it is his unwavering spirit and fierce determination that truly sets him apart from the rest.",
                GenerateCarData(8),2,constants.planetKT4,1),3.85f,false,0,0,false,true,false,false,false,false,false,false,false,false,false,true,false),

            new planetData(9,"Island Rush",constants.planet10,constants.planetT10,2000000,8,
                "Island Rush is a small tropical island covered in lush green vegetation and surrounded by clear blue waters. The island has many caves and tunnels that twist and turn, making it an ideal location for an adrenaline-fueled race. The island's inhabitants have built a racecourse through the caves, complete with obstacles, jumps, and challenging turns.",
                "Gravity: The gravity on Pink Paradise is Very High.","Weather: Windy and Rainy.","Temperature: Warm and humid, making it an hospitable place for most forms of life.",
                .2f,new KingData("King Reef Racer, Ruler of Island Rush",
                25000000,11,constants.planetK10,GenerateCarData(10),"King Reef Racer is the ruler of Island Rush and an accomplished racer. He's a daring and fearless competitor, always pushing himself to the limit. Reef Racer is highly respected on the island for his racing prowess, and he's a beloved leader who cares deeply for his people. He often joins the races himself and has been known to go to great lengths to ensure a fair competition.",
                GenerateCarData(9),4,constants.planetKT10,1),4.8f,true,.8f,.5f,true,false,false,false,false,false,false,false,false,true,false,true,false),

            new planetData(10,"High Lands",constants.planet11,constants.planetT11,1000000,8,
                "Island Rush is a small tropical island covered in lush green vegetation and surrounded by clear blue waters. The island has many caves and tunnels that twist and turn, making it an ideal location for an adrenaline-fueled race. The island's inhabitants have built a racecourse through the caves, complete with obstacles, jumps, and challenging turns.",
                "Gravity: The gravity on Pink Paradise is Very High.","Weather: Windy and Rainy.","Temperature: Warm and humid, making it an hospitable place for most forms of life.",
                .2f,new KingData("The Emperor Ru of Star Alliance",
                30000000,25,constants.planetK11,GenerateCarData(12),"Emperor Ru of Star Alliance, your final opponent, beat him and free this star system from his evil rule.",
                GenerateCarData(11),4,constants.planetKT11,1),4.8f,true,.8f,.5f,true,false,false,false,false,false,false,false,false,true,false,true,false),

        };

        List<string> allWeatherEffects = new List<string>()
        {
            "light rain","fog","rock shower","ash fall","dust","float"
        };

        List<string> worldTriggerEffect = new List<string>()
        {
            "wave water","tornado","swamp","wet surface","hail fall","heavy rain","hot surface","wind effect","poison rain","poison surface"
        };
        private static Car GenerateCarData(int i)
        {
            int engineindex = 1;
            int tireindex = 1;
            int bodyindex = 1;
            int nitroindex = 1;
            Color bodyColor = Color.white;
            Color vinylColor = Color.white;
            Color glassColor = Color.white;
            Color tireColor = Color.white;
            Color interiorColor = Color.white;
            Color spoilerColor = Color.white;

            List<float> colorTypeBody = new List<float>()
            {
                0,0
            };
            List<float> colorTypeVinyl = new List<float>()
            {
                0,0
            };

            if (i == 0) 
            { engineindex = 3; bodyindex = 1; tireindex = 2; nitroindex = 2;
                bodyColor = Color.magenta; vinylColor = Color.green; glassColor = Color.green; glassColor.a = .5f;
                tireColor = Color.magenta; interiorColor = Color.cyan; spoilerColor = Color.green;
                colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
            }

            else if (i == 1) { engineindex = 4; bodyindex = 2; tireindex = 2; nitroindex = 2;
                bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
                tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
                colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
            }

            else if(i == 2) { engineindex = 5; bodyindex = 3; tireindex = 2; nitroindex = 3;
                bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
                tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
                colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
            }
            else if(i == 3) { engineindex = 6; bodyindex = 3; tireindex = 2; nitroindex = 4;
                bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
                tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
                colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
            }

            else if(i == 4) { engineindex = 7; bodyindex = 4; tireindex = 3; nitroindex = 4;
                bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
                tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
                colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
            }

            else if(i == 5) { engineindex = 8; bodyindex = 4; tireindex = 7; nitroindex = 4;
                bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
                tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
                colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
            }

            else if(i == 6) { engineindex = 9; bodyindex = 5; tireindex = 7; nitroindex = 4;
                bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
                tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
                colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
            }

            else if(i == 7) { engineindex = 10; bodyindex = 6; tireindex = 7; nitroindex = 4;
                bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
                tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
                colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
            }

            else if(i == 8) { engineindex = 11; bodyindex = 7; tireindex = 6; nitroindex = 4;
                bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
                tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
                colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
            }

            else if(i == 9) { engineindex = 12; bodyindex = 8; tireindex = 7; nitroindex = 4;
                bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
                tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
                colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
            }

            else if(i == 10) { engineindex = 13; bodyindex = 9; tireindex = 4; nitroindex = 4;
                bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
                tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
                colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
            }

            else if(i == 11) { engineindex = 13; bodyindex = 10; tireindex = 7; nitroindex = 4;
                bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
                tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
                colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
            }

            else if(i == 12) { engineindex = 13; bodyindex = 10; tireindex = 7; nitroindex = 4;
                bodyColor = new Color(255, 92, 70); vinylColor = new Color(70, 233, 255); glassColor = new Color(70, 233, 255); glassColor.a = .5f;
                tireColor = new Color(255, 92, 70); interiorColor = Color.gray; spoilerColor = new Color(70, 233, 255);
                colorTypeBody[0] = 0.5f; colorTypeBody[0] = 0.8f;
            }

            int CarNitroHealth = nitrosData[nitroindex].CarPartHealth;
            int CarBodyHealth = bodyData[bodyindex].CarPartHealth;
            int CarEngineHealth = enginesData[engineindex].CarPartHealth;
            int CarTireHealth = tiresData[tireindex].CarPartHealth;


            decimal BodyHealth = CarBodyHealth;
            decimal EngineHealth = CarEngineHealth;
            decimal TireHealth = CarTireHealth;
            decimal NitroHealth = CarNitroHealth;

            int carMass = Constants.enginesData[engineindex].CarPartMass
                + Constants.bodyData[bodyindex].CarPartMass
                + Constants.tiresData[tireindex].CarPartMass
                + Constants.nitrosData[nitroindex].CarPartMass;

            int carTopSpeed = Constants.enginesData[engineindex].CarPartTopSpeed
                + Constants.bodyData[bodyindex].CarPartTopSpeed
                + Constants.tiresData[tireindex].CarPartTopSpeed;

            int carFullTorque = Constants.enginesData[engineindex].CarPartAcceleration
                + Constants.bodyData[bodyindex].CarPartAcceleration
                + Constants.tiresData[tireindex].CarPartAcceleration;

            float carSteerHelper = Constants.tiresData[tireindex].CarPartSteerHelper;
            float carTractionControl = Constants.tiresData[tireindex].CarPartTractionControl;
            int carDownForce = Constants.bodyData[bodyindex].CarPartDownForce;
            int carBreakTorque = Constants.enginesData[engineindex].CarPartBreakTorque
                + Constants.tiresData[tireindex].CarPartBreakTorque;

            decimal engine = decimal.Divide
        (EngineHealth, CarEngineHealth);
            decimal body = decimal.Divide
                (BodyHealth, CarBodyHealth);
            decimal eb = decimal.Divide(engine + body, 2);
            int topSpeed = (int)((eb * carTopSpeed) + carTopSpeed) / 2;
            int fullTorque = (int)((eb * carFullTorque) + carFullTorque) / 2;

            int spoilerindex = UnityEngine.Random.Range(0, 9);
            int vinylindex = 1;
            int bodyskitindex = 3;

            ColorTheme carPaint = new ColorTheme(bodyColor, vinylColor, glassColor, tireColor, interiorColor, spoilerColor, colorTypeBody, colorTypeVinyl);

            Car carData = new Car(engineindex, bodyindex, tireindex, nitroindex, carPaint, spoilerindex, bodyskitindex, vinylindex, (int)EngineHealth,
                (int)BodyHealth, (int)TireHealth, (int)NitroHealth, CarEngineHealth, CarBodyHealth, CarTireHealth, CarNitroHealth, carMass, carTopSpeed, carFullTorque
                , carSteerHelper, carTractionControl, carDownForce, carBreakTorque, topSpeed, fullTorque);
            return carData;
        }
        public static readonly Dictionary<int, string> nitro = new Dictionary<int, string>()
    {
        {0,"1 Litre"},
        {1,"3 Litre"},
        {2,"5 Litre"},
        {3,"10 Litre"},
        {4,"18 Litre"},

    };


        public static readonly Dictionary<int, string> carBodyMaterial = new Dictionary<int, string>()
    {
        {0,"Steel"},
        {1,"Aluminium"},
        {2,"Titanium"},
        {3,"Fibre Glass"},
        {4,"Carbon Fiber"},

    };

        public static readonly List<Color> carColors = new List<Color>()
    {
        Color.white,Color.black,Color.blue,Color.cyan,Color.gray,Color.green,Color.magenta,
        Color.red,Color.yellow
    };
    }


    public class RandomData
    {
        private Tuple<int, int> engine;
        private Tuple<int, int> body;
        private Tuple<int, int> tire;
        private Tuple<int, int> nitros;



        public RandomData(Tuple<int, int> engine, Tuple<int, int> body, Tuple<int, int> tire, Tuple<int, int> nitros)
        {
            this.engine = engine;
            this.body = body;
            this.tire = tire;
            this.nitros = nitros;
        }

        public Tuple<int, int> Engine { get => engine; set => engine = value; }
        public Tuple<int, int> Body { get => body; set => body = value; }
        public Tuple<int, int> Tire { get => tire; set => tire = value; }
        public Tuple<int, int> Nitros { get => nitros; set => nitros = value; }
    }

    public class carPartdata
    {
        private string carPart;
        private int carPartHealth;
        private int carPartMass;
        private int carPartTopSpeed;
        private int carPartAcceleration;
        private float carPartSteerHelper;
        private float carPartTractionControl;
        private int carPartDownForce;
        private int carPartBreakTorque;
        private int rating;

        public carPartdata(string carPart, int carPartHealth, int carPartMass, int carPartTopSpeed, int carPartAcceleration, float carPartSteerHelper, float carPartTractionControl, int carPartDownForce, int carPartBreakTorque, int rating)
        {
            this.carPart = carPart;
            this.carPartHealth = carPartHealth;
            this.carPartMass = carPartMass;
            this.carPartTopSpeed = carPartTopSpeed;
            this.carPartAcceleration = carPartAcceleration;
            this.carPartSteerHelper = carPartSteerHelper;
            this.carPartTractionControl = carPartTractionControl;
            this.carPartDownForce = carPartDownForce;
            this.carPartBreakTorque = carPartBreakTorque;
            this.rating = rating;
        }

        public string CarPart { get => carPart; set => carPart = value; }
        public int CarPartHealth { get => carPartHealth; set => carPartHealth = value; }
        public int CarPartTopSpeed { get => carPartTopSpeed; set => carPartTopSpeed = value; }
        public int CarPartAcceleration { get => carPartAcceleration; set => carPartAcceleration = value; }
        public float CarPartSteerHelper { get => carPartSteerHelper; set => carPartSteerHelper = value; }
        public float CarPartTractionControl { get => carPartTractionControl; set => carPartTractionControl = value; }
        public int CarPartDownForce { get => carPartDownForce; set => carPartDownForce = value; }
        public int CarPartBreakTorque { get => carPartBreakTorque; set => carPartBreakTorque = value; }
        public int CarPartMass { get => carPartMass; set => carPartMass = value; }
        public int Rating { get => rating; set => rating = value; }
    }
    public class planetData
    {
        private int planetIndex;
        private float planetLenght;
        private string planetName;
        private Sprite planetImage;
        private Sprite planetTrackImage;
        private int planetEntryCost;
        private int planetEntryReputation;
        private string planetDescription;
        private string planetGravityDescription;
        private string planetWeatherDescription;
        private string planetTemperature;
        private float planetGravityFactor;
        private KingData kingData;
        //Weather Effects
        bool isRaining;
        float rainIntensity;
        float isRainFog;
        bool isRainPoisonous;

        bool isDenseFogPresent;

        bool isAshfalling;

        bool isMeteorShowerPresent;

        bool isHailfallPresent;

        //Environmental Triggers
        bool isWavesPresent;
        bool isTornadoPresent;
        bool isSwaampPresent;
        bool isWaterSurfacePresent;
        bool isPoisonousSurfacePresent;
        bool isHotSurfacePresent;
        bool isWindEffectsPresent;
        bool isIceSurface;

        public planetData(int planetIndex, string planetName, Sprite planetImage, Sprite planetTrackImage, int planetEntryCost, int planetEntryReputation, string planetDescription, string planetGravityDescription, string planetWeatherDescription, string planetTemperature, float planetGravityFactor, KingData kingData, float planetLenght, bool isRaining, float rainIntensity, float isRainFog, bool isRainPoisonous, bool isDenseFogPresent, bool isAshfalling, bool isMeteorShowerPresent, bool isHailfallPresent, bool isWavesPresent, bool isTornadoPresent, bool isSwaampPresent, bool isWaterSurfacePresent, bool isPoisonousSurfacePresent, bool isHotSurfacePresent, bool isWindEffectsPresent, bool isIceSurface)
        {
            this.planetIndex = planetIndex;
            this.planetName = planetName;
            this.planetImage = planetImage;
            this.planetTrackImage = planetTrackImage;
            this.planetEntryCost = planetEntryCost;
            this.planetEntryReputation = planetEntryReputation;
            this.planetDescription = planetDescription;
            this.planetGravityDescription = planetGravityDescription;
            this.planetWeatherDescription = planetWeatherDescription;
            this.planetTemperature = planetTemperature;
            this.planetGravityFactor = planetGravityFactor;
            this.kingData = kingData;
            this.planetLenght = planetLenght;
            this.isRaining = isRaining;
            this.rainIntensity = rainIntensity;
            this.isRainFog = isRainFog;
            this.isRainPoisonous = isRainPoisonous;
            this.isDenseFogPresent = isDenseFogPresent;
            this.isAshfalling = isAshfalling;
            this.isMeteorShowerPresent = isMeteorShowerPresent;
            this.isHailfallPresent = isHailfallPresent;
            this.isWavesPresent = isWavesPresent;
            this.isTornadoPresent = isTornadoPresent;
            this.isSwaampPresent = isSwaampPresent;
            this.isWaterSurfacePresent = isWaterSurfacePresent;
            this.isPoisonousSurfacePresent = isPoisonousSurfacePresent;
            this.isHotSurfacePresent = isHotSurfacePresent;
            this.isWindEffectsPresent = isWindEffectsPresent;
            this.isIceSurface = isIceSurface;
        }

        public int PlanetIndex { get => planetIndex; set => planetIndex = value; }
        public string PlanetName { get => planetName; set => planetName = value; }
        public Sprite PlanetImage { get => planetImage; set => planetImage = value; }
        public Sprite PlanetTrackImage { get => planetTrackImage; set => planetTrackImage = value; }
        public int PlanetEntryCost { get => planetEntryCost; set => planetEntryCost = value; }
        public int PlanetEntryReputation { get => planetEntryReputation; set => planetEntryReputation = value; }
        public string PlanetDescription { get => planetDescription; set => planetDescription = value; }
        public string PlanetGravityDescription { get => planetGravityDescription; set => planetGravityDescription = value; }
        public string PlanetWeatherDescription { get => planetWeatherDescription; set => planetWeatherDescription = value; }
        public string PlanetTemperature { get => planetTemperature; set => planetTemperature = value; }
        public float PlanetGravityFactor { get => planetGravityFactor; set => planetGravityFactor = value; }
        public KingData KingData { get => kingData; set => kingData = value; }
        public float PlanetLenght { get => planetLenght; set => planetLenght = value; }
        public bool IsRaining { get => isRaining; set => isRaining = value; }
        public float RainIntensity { get => rainIntensity; set => rainIntensity = value; }
        public float IsRainFog { get => isRainFog; set => isRainFog = value; }
        public bool IsRainPoisonous { get => isRainPoisonous; set => isRainPoisonous = value; }
        public bool IsDenseFogPresent { get => isDenseFogPresent; set => isDenseFogPresent = value; }
        public bool IsAshfalling { get => isAshfalling; set => isAshfalling = value; }
        public bool IsMeteorShowerPresent { get => isMeteorShowerPresent; set => isMeteorShowerPresent = value; }
        public bool IsHailfallPresent { get => isHailfallPresent; set => isHailfallPresent = value; }
        public bool IsWavesPresent { get => isWavesPresent; set => isWavesPresent = value; }
        public bool IsTornadoPresent { get => isTornadoPresent; set => isTornadoPresent = value; }
        public bool IsSwaampPresent { get => isSwaampPresent; set => isSwaampPresent = value; }
        public bool IsWaterSurfacePresent { get => isWaterSurfacePresent; set => isWaterSurfacePresent = value; }
        public bool IsPoisonousSurfacePresent { get => isPoisonousSurfacePresent; set => isPoisonousSurfacePresent = value; }
        public bool IsHotSurfacePresent { get => isHotSurfacePresent; set => isHotSurfacePresent = value; }
        public bool IsWindEffectsPresent { get => isWindEffectsPresent; set => isWindEffectsPresent = value; }
        public bool IsIceSurface { get => isIceSurface; set => isIceSurface = value; }
    }
    public class KingData
    {
        private string kingTitle;
        private string kingDescription;
        private int offer;
        private int reputationRequired;
        private Sprite kingImage;
        private Car kingCar;
        private Car henchmanCar;
        private int noOfHenchmen;
        private Sprite customCarTexture;
        private int laps;


        public KingData(string kingTitle, int offer, int reputationRequired, Sprite kingImage, Car kingCar, string kingDescription, Car henchmanCar, int noOfHenchmen, Sprite customCarTexture, int laps)
        {
            this.kingTitle = kingTitle;
            this.offer = offer;
            this.reputationRequired = reputationRequired;
            this.kingImage = kingImage;
            this.kingCar = kingCar;
            this.kingDescription = kingDescription;
            this.henchmanCar = henchmanCar;
            this.noOfHenchmen = noOfHenchmen;
            this.customCarTexture = customCarTexture;
            this.laps = laps;
        }

        public string KingTitle { get => kingTitle; set => kingTitle = value; }
        public int Offer { get => offer; set => offer = value; }
        public int ReputationRequired { get => reputationRequired; set => reputationRequired = value; }
        public Sprite KingImage { get => kingImage; set => kingImage = value; }
        public Car KingCar { get => kingCar; set => kingCar = value; }
        public string KingDescription { get => kingDescription; set => kingDescription = value; }
        public Car HenchmanCar { get => henchmanCar; set => henchmanCar = value; }
        public int NoOfHenchmen { get => noOfHenchmen; set => noOfHenchmen = value; }
        public Sprite CustomCarTexture { get => customCarTexture; set => customCarTexture = value; }
        public int Laps { get => laps; set => laps = value; }
    }



}