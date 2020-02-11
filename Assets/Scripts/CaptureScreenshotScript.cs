using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureScreenshotScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            ScreenCapture.CaptureScreenshot("Screenshot1" + Random.Range(1, 1000).ToString() + ".png");
        }
    }
}
