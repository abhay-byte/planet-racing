using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityStandardAssets.CrossPlatformInput;


namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
        private Vector4 currentColor;
        private Vector4 whiteColor;
        private PlayerInputActions playerInputActions;
        [SerializeField] private RaceData raceData;
        private bool valueIncresed = false;
        private float nitroHealth ;
        private float curNitroHealth;
        [SerializeField] Volume NitrosEffect;
        [SerializeField] Volume SpeedEffects;
        [SerializeField] private GameObject nitrosParticleSystem;
        bool nitrosValue;
        float breakValue = 0;
        float h;
        float v;

        int nitrosSpeed;
        int currentController;
        int originalSpeed;

        int nitrosTorque;
        int originalTorque;
        private bool gotData;
        UnityEngine.Gyroscope gyroInput;

        public float NitroHealth { get => nitroHealth; set => nitroHealth = value; }
        public float CurNitroHealth { get => curNitroHealth; set => curNitroHealth = value; }

        private void Awake()
        {
            // get the car controller
            gotData = false;
            currentController = raceData.settings.controllerIndex;
            m_Car = GetComponent<CarController>();

            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();
            
            InputSystem.EnableDevice(Accelerometer.current);
            gyroInput.enabled = true;
            //Debug.Log(UnityEngine.InputSystem.Gyroscope.current.enabled);


            StartCoroutine(LoadStats());
        }

        public void ChangeController(int val)
        {
            currentController = val;
        }

        IEnumerator LoadStats()
        {
            yield return new WaitForSeconds(2f);
            if(raceData.RaceStart)
            {

            }

        }

        public void SetNitroHealth(int val)
        {
            nitroHealth = val;
            raceData.UIDataPlanet.NitrosSlider.value = curNitroHealth / nitroHealth;

            originalTorque = (int)m_Car.FullTorqueOverAllWheels;
            nitrosTorque = originalTorque + 4000;
            originalSpeed = (int)(m_Car.Topspeed);
            nitrosSpeed = originalSpeed + 50;
            rearLights = raceData.PlayerCar.GetComponent<CarObjects>().rearLight;
            gotData = true;
        }



        /*        public void Refresh(InputAction.CallbackContext context)
                {
                    bool pressed = playerInputActions.Player.Refresh.IsPressed();
                    Debug.Log("Pressed Refresh");

                    raceData.PlayerCar.transform.position = raceData.PathOneStepOne.GetChild(raceData.PlayerCheckpoint - 1).position;
                    raceData.PlayerCar.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                    transform.position += Vector3.up;
                    transform.rotation = Quaternion.LookRotation(transform.forward);


                }
        */
        private Material rearLights;
        public void Refresh()
        {
            if (raceData.CurrentRaceType != RaceData.RaceType.Reverse)
            {
                raceData.PlayerCar.transform.position = raceData.PathOneStepOne.GetChild(raceData.PlayerCheckpoint - 1).position;
            }
            else { raceData.PlayerCar.transform.position = raceData.PathOneStepOneReverse.GetChild(raceData.PlayerCheckpoint - 1).position; }

            raceData.PlayerCar.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            transform.position += Vector3.up;
            transform.rotation = Quaternion.LookRotation(transform.forward);
        }
        private void FixedUpdate()
        {
  
            
            bool nitrosPressed = playerInputActions.Player.Nitros.IsPressed();
            bool refreshPressed = playerInputActions.Player.Refresh.IsPressed();
            bool Break = playerInputActions.Player.Break.IsPressed();
            if (refreshPressed)
            {

                Refresh();
            }

            if (nitrosPressed && curNitroHealth > 0 && gotData)
            {
                //NitrosEffect.enabled = true;
                NitrosEffect.weight = 1f;
                raceData.UIDataPlanet.NitrosSlider.value = curNitroHealth / nitroHealth;
                curNitroHealth--;

                m_Car.Topspeed = nitrosSpeed;
                m_Car.FullTorqueOverAllWheels = nitrosTorque;
                nitrosParticleSystem.SetActive(true);

            }
            else if(gotData)
            {
                NitrosEffect.enabled = false;
                m_Car.Topspeed = originalSpeed;
                m_Car.FullTorqueOverAllWheels = originalTorque;
                nitrosParticleSystem.SetActive(false);
            }
            /*
                        //Debug.Log(nitrosValue);
                        if (valueIncresed)
                        {
                            NitrosEffect.weight += .1f;
                        }
                        else
                        {
                            NitrosEffect.weight -= .1f;
                        }*/

            //if (inputVector.y < 0)
            //{
            //    carMaterial.EnableKeyword("_EMISSION");
            //    carMaterial.SetColor("_EmissionColor", Color.yellow);//whiteLights.SetActive(true);

            //}
            //else
            //{
            //    carMaterial.DisableKeyword("_EMISSION");
            //}

            //if (breakValue > 0)
            //{
            //    carMaterial.EnableKeyword("_EMISSION");
            //    carMaterial.SetColor("_EmissionColor", Color.red);//redLights.SetActive(true);

            //}
            //else
            //{
            //    carMaterial.DisableKeyword("_EMISSION");
            //}
            // pass the input to the car!

            if (currentController == 0)
            {
                Vector3 accInputVal = Accelerometer.current.acceleration.ReadValue();
                h = accInputVal.x;
            } else
            {
                Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
                h = inputVector.x;
            }

            if (raceData.RaceStart)  v = 1;
            else v = 0;

            if (Break)
            {

                v = -1;
                rearLights.SetColor("_EmissionColor", Color.white);


            }
            else
            { 
                breakValue = 0;
                rearLights.SetColor("_EmissionColor", Color.red);

            }
            
            /*            if (breakValue == 1)
                        {
                            v = 0;

                        }*/
#if !MOBILE_INPUT
            //float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, breakValue);
#else
            m_Car.Move(h, v, v, breakValue);
#endif
        }
    }
}
