using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotScript : MonoBehaviour
{
    void Update()
    {
        if(GameManager.inst.inputManager.Giocatore.UsoRum.triggered)
        {
            string percorsoFile = "Assets/z_Prova/Screen/";

            ScreenCapture.CaptureScreenshot(percorsoFile
                                            + "Screenshot"
                                            + System.DateTime.Now.ToString(" (dd-MM-yyyy, HH-mm-ss)")
                                            + ".png");
            Debug.Log("Screenshot preso!");
        }
    }
}
