using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour{
    bool pictureTaken;

    void Start(){
        pictureTaken = false;
    }

    void Update(){
        
        if(Input.GetMouseButton(0)){
            NormalScreenShot();
        }

    }


    void RenderScreenShot(){
        pictureTaken = true;
        RenderTexture renderTexture = this.gameObject.GetComponent<Camera>().targetTexture;
        print(renderTexture);
        Texture2D texture2D = new Texture2D(renderTexture.width,renderTexture.height,TextureFormat.ARGB32,false);
        Rect frame = new Rect(0,0,renderTexture.width,renderTexture.height);
        texture2D.ReadPixels(frame,0,0);

        byte[] byteArray = texture2D.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath+"",byteArray);
            
        RenderTexture.ReleaseTemporary(renderTexture);
        Camera.main.targetTexture = null;
    }

    void NormalScreenShot(){
        ScreenCapture.CaptureScreenshot( System.IO.Path.Combine(System.Environment.GetFolderPath( System.Environment.SpecialFolder.MyDocuments ),"screenshot.png") );
    }
}
