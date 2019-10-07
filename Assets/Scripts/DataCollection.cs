using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCollection : MonoBehaviour{
    private int incrementImageID;
    SceneSettings sceneSettings;
    public Camera data_camera;
    public Camera UI_camera;

    void Start(){
        incrementImageID = 0;
        sceneSettings = this.transform.GetComponent<SceneSettings>();
        
    }

    void Update(){
        data_camera.pixelRect = new Rect(0,0,sceneSettings.imageWidth,sceneSettings.imageHeight);
        //ScreenShotAtMousePress();
        NewScreenShot();
        Screen.fullScreen = false;
    }



    private void ScreenShotAtMousePress(){
        if(Input.GetMouseButton(0)){
            print(incrementImageID);
            
            ScreenCapture.CaptureScreenshot("C:\\Users\\schon\\Desktop\\Images\\"+"screenshot"+incrementImageID+".png");
            incrementImageID++;
        }
    }

    private void NewScreenShot(){
        if (Input.GetMouseButton(0)) {
            //Screen.SetResolution(sceneSettings.imageWidth,sceneSettings.imageHeight,false,60);
            UI_camera.aspect = data_camera.aspect;
            

            RenderTexture renderTexture = new RenderTexture(sceneSettings.imageWidth, sceneSettings.imageHeight, 24);
            data_camera.targetTexture = renderTexture;
            Texture2D screenShot = new Texture2D(sceneSettings.imageWidth, sceneSettings.imageHeight, TextureFormat.RGB24, false);
            data_camera.Render();
            RenderTexture.active = renderTexture;
            screenShot.ReadPixels(new Rect(0, 0, sceneSettings.imageWidth, sceneSettings.imageHeight), 0, 0);
            data_camera.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors
            Destroy(renderTexture);
            byte[] bytes = screenShot.EncodeToPNG();
            System.IO.File.WriteAllBytes("C:\\Users\\schon\\Desktop\\Images\\"+"screenshot"+incrementImageID+".png", bytes);
            Debug.Log(string.Format("Took screenshot to: {0}", "C:\\Users\\schon\\Desktop\\Images\\"+"screenshot"+incrementImageID+".png"));
            incrementImageID++;
         }
    }
}
