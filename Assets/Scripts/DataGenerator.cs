using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataGenerator : MonoBehaviour{
    private int incrementImageID;
    SceneSettings sceneSettings;
    public Camera data_camera;
    public Camera UI_camera;

    public int numberOfImagesFine, numberOfImagesDefect;

    void Start(){
        incrementImageID = 0;
        sceneSettings = this.transform.GetComponent<SceneSettings>();   
    }

    /// <summary>
    /// Generates Image from the pointcloud camera by rendering the camera and then writes the image to the desired path.
    /// This function is called in PicoFlexxSensor.
    /// </summary>
    /// <param name="foundDefect"></param>
    /// <returns>Returns true if image is written successfully or enough images of a label has been created. Has to return true, else the camera will not move forward. </returns>
    public bool GenerateImage(bool foundDefect){
        ///Rendering Part
        RenderTexture renderTexture = new RenderTexture(sceneSettings.imageWidth, sceneSettings.imageHeight, 24);
        data_camera.targetTexture = renderTexture;
        Texture2D screenShot = new Texture2D(sceneSettings.imageWidth, sceneSettings.imageHeight, TextureFormat.RGB24, false);
        data_camera.Render();
        RenderTexture.active = renderTexture;
        screenShot.ReadPixels(new Rect(0, 0, sceneSettings.imageWidth, sceneSettings.imageHeight), 0, 0);

        //Checks if pixel is black, to see if depth shader is activated yet. Bad way of to do it though.
        if(screenShot.GetPixel(0,0)==new Color(0,0,0,1)){
            print("RESET");
            return false;
        }

        //print("YAY GOT THROUGH");

        ///Continue Rendering
        data_camera.targetTexture = null;
        RenderTexture.active = null; 
        Destroy(renderTexture);
        byte[] bytes = screenShot.EncodeToPNG();

        ///Obtain Path
        string path = ObtainPathDependingOnFoundDefect(foundDefect);
        if (path == "Done") { return true; }; ///Returns true, if the desired number of images has been created already.

        ///Write Image to path
        System.IO.File.WriteAllBytes(path + "/screenshot"+incrementImageID+".png", bytes);
        incrementImageID++;
        return true;
    }

    /// <summary>
    /// Check if a defect is found or not, which will return a path based on the value. 
    /// Moreover, if the desired number of images for a label is obtained, it will return "Done", which is used for skipping the rendering part.
    /// </summary>
    /// <param name="foundDefect"></param>
    /// <returns></returns>
    private string ObtainPathDependingOnFoundDefect(bool foundDefect){
        string path = "";
        if (foundDefect == true) {
            path = sceneSettings.directory_path + "/Defect";
            if (numberOfImagesDefect <= 0) {
                return "Done";
            }
            numberOfImagesDefect--;
        } else {
            path = sceneSettings.directory_path + "/Fine";
            if (numberOfImagesFine <= 0) {
                return "Done";
            }
            numberOfImagesFine--;
        }
        return path;
    }

    /// <summary>
    /// Sets the number of images for fine and defects.
    /// </summary>
    /// <param name="temp_numberOfImagesFine"></param>
    /// <param name="temp_numberOfImagesDefects"></param>
    public void SetNumberOfDesiredImages(int temp_numberOfImagesFine, int temp_numberOfImagesDefects){
        numberOfImagesFine = temp_numberOfImagesFine;
        numberOfImagesDefect = temp_numberOfImagesDefects;
    }
}
