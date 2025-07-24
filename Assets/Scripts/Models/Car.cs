using System.Collections;
using UnityEngine;
using Assets.Scripts.Utils;
using System.Collections.Generic;

namespace Assets.Scripts.Models
{
    public class Car
    {

        // Car Data
        private int engineIndex;
        private int bodyIndex;
        private int tireIndex;
        private int nitroIndex;
        private int spoilerIndex;
        private int bodySkitIndex;
        private int vinylIndex;
        private int engineHealth;
        private int bodyHealth;
        private int tireHealth;
        private int nitroHealth;
        private int carEngineHealth;
        private int carBodyHealth;
        private int carTireHealth;
        private int carNitroHealth;
        private int carMass;
        private int carTopSpeed;
        private int carFullTorque;
        private float carSteerHelper;
        private float carTractionControl;
        private int carDownForce;
        private int carBreakTorque;
        private int topSpeed;
        private int fullTorque;


        // Color theme
        private ColorTheme colorTheme;

        public Car(int engineIndex, int bodyIndex, int tireIndex, int nitroIndex, ColorTheme colorTheme, int spoilerIndex, int bodySkitIndex, int vinylIndex, int engineHealth, int bodyHealth, int tireHealth, int nitroHealth, int carEngineHealth, int carBodyHealth, int carTireHealth, int carNitroHealth, int carMass, int carTopSpeed, int carFullTorque, float carSteerHelper, float carTractionControl, int carDownForce, int carBreakTorque, int topSpeed, int fullTorque)
        {
            this.engineIndex = engineIndex;
            this.bodyIndex = bodyIndex;
            this.tireIndex = tireIndex;
            this.nitroIndex = nitroIndex;
            this.colorTheme = colorTheme;
            this.spoilerIndex = spoilerIndex;
            this.bodySkitIndex = bodySkitIndex;
            this.vinylIndex = vinylIndex;
            this.EngineHealth = engineHealth;
            this.BodyHealth = bodyHealth;
            this.TireHealth = tireHealth;
            this.NitroHealth = nitroHealth;
            this.carEngineHealth = carEngineHealth;
            this.carBodyHealth = carBodyHealth;
            this.carTireHealth = carTireHealth;
            this.carNitroHealth = carNitroHealth;
            this.carMass = carMass;
            this.carTopSpeed = carTopSpeed;
            this.carFullTorque = carFullTorque;
            this.carSteerHelper = carSteerHelper;
            this.carTractionControl = carTractionControl;
            this.carDownForce = carDownForce;
            this.carBreakTorque = carBreakTorque;
            this.topSpeed = topSpeed;
            this.fullTorque = fullTorque;
        }



        public static Car FromLevel(int Level)
        {
            Level--;
            int engineIndex = PRUtils.randomNumbers.Next(Constants.allDataLimit[Level].Engine.Item1, Constants.allDataLimit[Level].Engine.Item2);
            int bodyIndex = PRUtils.randomNumbers.Next(Constants.allDataLimit[Level].Body.Item1, Constants.allDataLimit[Level].Body.Item2);
            int tireIndex = PRUtils.randomNumbers.Next(Constants.allDataLimit[Level].Tire.Item1, Constants.allDataLimit[Level].Tire.Item2);
            int nitroIndex = PRUtils.randomNumbers.Next(Constants.allDataLimit[Level].Nitros.Item1, Constants.allDataLimit[Level].Nitros.Item2);
            int spoilerIndex = PRUtils.randomNumbers.Next(0, 10);
            int bodySkitIndex = PRUtils.randomNumbers.Next(0, 3);
            int vinylIndex = PRUtils.randomNumbers.Next(0, 5);


            int carEngineHealth = Constants.enginesData[engineIndex].CarPartHealth;
            int carBodyHealth = Constants.bodyData[bodyIndex].CarPartHealth;
            int carTireHealth = Constants.enginesData[tireIndex].CarPartHealth;
            int carNitroHealth = Constants.nitrosData[nitroIndex].CarPartHealth;

            decimal eHealth = decimal.Divide(
                PRUtils.randomNumbers.Next(25, 100) * carEngineHealth,
                100);
            decimal eBody = decimal.Divide(
                PRUtils.randomNumbers.Next(25, 100) * carBodyHealth,
                100);
            decimal eTire = decimal.Divide(
                PRUtils.randomNumbers.Next(25, 100) * carTireHealth,
                100);
            decimal eNitros = decimal.Divide(
                PRUtils.randomNumbers.Next(25, 100) * carNitroHealth,
                100);
            int engineHealth = (int)eHealth;
            int bodyHealth = (int)eBody;
            int tireHealth = (int)eTire;
            int nitroHealth = (int)eNitros;

            int carMass = Constants.enginesData[engineIndex].CarPartMass
                + Constants.bodyData[bodyIndex].CarPartMass
                + Constants.tiresData[tireIndex].CarPartMass
                + Constants.nitrosData[nitroIndex].CarPartMass;

            int carTopSpeed = Constants.enginesData[engineIndex].CarPartTopSpeed
                + Constants.bodyData[bodyIndex].CarPartTopSpeed
                + Constants.tiresData[tireIndex].CarPartTopSpeed;

            int carFullTorque= Constants.enginesData[engineIndex].CarPartAcceleration
                + Constants.bodyData[bodyIndex].CarPartAcceleration
                + Constants.tiresData[tireIndex].CarPartAcceleration;

            float carSteerHelper = Constants.tiresData[tireIndex].CarPartSteerHelper;
            float carTractionControl = Constants.tiresData[tireIndex].CarPartTractionControl;
            int carDownForce = Constants.bodyData[bodyIndex].CarPartDownForce;
            int carBreakTorque = Constants.enginesData[engineIndex].CarPartBreakTorque
                + Constants.tiresData[tireIndex].CarPartBreakTorque;

            decimal engine = decimal.Divide
                (engineHealth, carEngineHealth);
            decimal body = decimal.Divide
                (bodyHealth, carBodyHealth);
            decimal eb = decimal.Divide(engine+body,2);
            int topSpeed = (int)((eb * carTopSpeed) + carTopSpeed) / 2;
            int fullTorque = (int)((eb * carFullTorque) + carFullTorque)/2;

            ColorTheme colorTheme = ColorTheme.Random();

            return new Car(engineIndex, bodyIndex, tireIndex, nitroIndex, colorTheme,
                spoilerIndex,bodySkitIndex,vinylIndex,engineHealth,bodyHealth,tireHealth,nitroHealth,
                carEngineHealth, carBodyHealth, carTireHealth, carNitroHealth, carMass,
                carTopSpeed, carFullTorque, carSteerHelper, carTractionControl, carDownForce,
                carBreakTorque, topSpeed, fullTorque);
        }

        public int EngineIndex { get => engineIndex; set => engineIndex = value; }
        public int BodyIndex { get => bodyIndex; set => bodyIndex = value; }
        public int TireIndex { get => tireIndex; set => tireIndex = value; }
        public int NitroIndex { get => nitroIndex; set => nitroIndex = value; }
        public int SpoilerIndex { get => spoilerIndex; set => spoilerIndex = value; }
        public int BodySkitIndex { get => bodySkitIndex; set => bodySkitIndex = value; }
        public int VinylIndex { get => vinylIndex; set => vinylIndex = value; }
        public ColorTheme ColorTheme { get => colorTheme; set => colorTheme = value; }
        public int EngineHealth { get => engineHealth; set => engineHealth = value; }
        public int BodyHealth { get => bodyHealth; set => bodyHealth = value; }
        public int TireHealth { get => tireHealth; set => tireHealth = value; }
        public int NitroHealth { get => nitroHealth; set => nitroHealth = value; }
        public int CarEngineHealth { get => carEngineHealth; set => carEngineHealth = value; }
        public int CarBodyHealth { get => carBodyHealth; set => carBodyHealth = value; }
        public int CarTireHealth { get => carTireHealth; set => carTireHealth = value; }
        public int CarNitroHealth { get => carNitroHealth; set => carNitroHealth = value; }
        public int CarMass { get => carMass; set => carMass = value; }
        public int CarTopSpeed { get => carTopSpeed; set => carTopSpeed = value; }
        public int CarFullTorque { get => carFullTorque; set => carFullTorque = value; }
        public float CarSteerHelper { get => carSteerHelper; set => carSteerHelper = value; }
        public float CarTractionControl { get => carTractionControl; set => carTractionControl = value; }
        public int CarDownForce { get => carDownForce; set => carDownForce = value; }
        public int CarBreakTorque { get => carBreakTorque; set => carBreakTorque = value; }
        public int TopSpeed { get => topSpeed; set => topSpeed = value; }
        public int FullTorque { get => fullTorque; set => fullTorque = value; }
    }

    public class ColorTheme
    {
        private Color bodyColor;
        private Color vinylColor;
        private Color glassColor;
        private Color tireColor;
        private Color interiorColor;
        private Color spoilerColor;
        private List<float> colorTypeBody;
        private List<float> colorTypeVinyl;

        public ColorTheme(Color bodyColor, Color vinylColor, Color glassColor, Color tireColor, Color interiorColor, Color spoilerColor, List<float> colorTypeBody, List<float> colorTypeVinyl)
        {
            this.bodyColor = bodyColor;
            this.vinylColor = vinylColor;
            this.glassColor = glassColor;
            this.tireColor = tireColor;
            this.interiorColor = interiorColor;
            this.spoilerColor = spoilerColor;
            this.colorTypeBody = colorTypeBody;
            this.colorTypeVinyl = colorTypeVinyl;
        }

        public static ColorTheme Random()
        {
            Color bodyColor = PRUtils.GetSingle(Constants.carColors);
            Color vinylColor = PRUtils.GetSingle(Constants.carColors);
            Color glassColor = PRUtils.GetSingle(Constants.carColors);
            glassColor.a = .75f;
            Color tireColor = PRUtils.GetSingle(Constants.carColors);
            Color interiorColor = PRUtils.GetSingle(Constants.carColors);
            Color spoilerColor = PRUtils.GetSingle(Constants.carColors);
            List<float> bodyColorType = PRUtils.GetSingle(Constants.colorType);
            List<float> vinylColorType = PRUtils.GetSingle(Constants.colorType);
            return new ColorTheme(bodyColor, vinylColor,  glassColor, tireColor, interiorColor, spoilerColor, bodyColorType, vinylColorType);
        }

        public Color BodyColor { get => bodyColor; set => bodyColor = value; }
        public Color VinylColor { get => vinylColor; set => vinylColor = value; }
        public Color GlassColor { get => glassColor; set => glassColor = value; }
        public Color TireColor { get => tireColor; set => tireColor = value; }
        public Color InteriorColor { get => interiorColor; set => interiorColor = value; }
        public Color SpoilerColor { get => spoilerColor; set => spoilerColor = value; }
        public List<float> ColorTypeBody { get => colorTypeBody; set => colorTypeBody = value; }
        public List<float> ColorTypeVinyl { get => colorTypeVinyl; set => colorTypeVinyl = value; }
    }
}