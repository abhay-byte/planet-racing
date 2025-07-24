using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateGrassFallback : MonoBehaviour
{
    public GameObject originalGrassObject;
    public GameObject fallbackGrassObject;
    public string deviceGraphicVendor;

    private void Awake(){

        CheckIfFallbackNeeded();

    }

    private void CheckIfFallbackNeeded(){

        GetDeviceGraphicVendor();
        if (deviceGraphicVendor != "Qualcomm"){

            FallbackGrass();

        }

    }

    private void GetDeviceGraphicVendor(){

        string thisDeviceGraphicRender = SystemInfo.graphicsDeviceVendor;
        deviceGraphicVendor = thisDeviceGraphicRender;

    }

    private void FallbackGrass(){

        originalGrassObject.SetActive(false);
        fallbackGrassObject.SetActive(true);

    }


}
