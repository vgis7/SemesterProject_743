using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataGenerator : MonoBehaviour{
    private int incrementImageID;
    SceneSettings sceneSettings;
    public Camera data_camera;
    public Camera UI_camera;

    private int numberOfImagesFine, numberOfImagesDefects;

    void Start(){
        incrementImageID = 0;
        sceneSettings = this.transform.GetComponent<SceneSettings>();
        
    }

    void Update(){
    }

    public bool GenerateImage(bool foundDefect){
        string temp_path = "";
        if(foundDefect == true){
            temp_path = sceneSettings.directory_path+"/Defect";
        }else{
            temp_path = sceneSettings.directory_path+"/Fine";
        }

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
        print(temp_path+"/screenshot"+incrementImageID+".png");
        System.IO.File.WriteAllBytes(temp_path+"/screenshot"+incrementImageID+".png", bytes);
        incrementImageID++;
        return true;
    }

    public void SetNumberOfDesiredImages(int temp_numberOfImagesFine, int temp_numberOfImagesDefects){
        numberOfImagesFine = temp_numberOfImagesFine;
        numberOfImagesDefects = temp_numberOfImagesDefects;
    }
    /*
    public void GenerateImages(int numberOfImages){
        for(int i = 0; i < numberOfImages;i++){
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
            System.IO.File.WriteAllBytes(sceneSettings.directory_path+"/screenshot"+incrementImageID+".png", bytes);
            incrementImageID++;
        }
    }*/
}
