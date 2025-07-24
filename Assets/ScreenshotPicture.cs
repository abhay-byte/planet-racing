using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotPicture : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Image box = GetComponent<Image>();
        box.sprite = Sprite.Create( ScreenCapture.CaptureScreenshotAsTexture(),new Rect(0f,0f,Screen.height, Screen.width),new Vector2(0.5f,.5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
