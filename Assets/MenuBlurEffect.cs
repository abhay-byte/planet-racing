using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBlurEffect : MonoBehaviour
{

    void Start()
    {
        Image box = GetComponent<Image>();
        box.sprite = Sprite.Create(ScreenCapture.CaptureScreenshotAsTexture(), new Rect(1f, 1f, 0, 0), new Vector2(0.5f, .5f));
    }


    void SaveCameraView(Camera cam)
    {
        RenderTexture screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
        cam.targetTexture = screenTexture;
        RenderTexture.active = screenTexture;
        cam.Render();
        Texture2D renderedTexture = new Texture2D(Screen.width, Screen.height);
        renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        RenderTexture.active = null;
        byte[] byteArray = renderedTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + "/cameracapture.png", byteArray);
    }




}
