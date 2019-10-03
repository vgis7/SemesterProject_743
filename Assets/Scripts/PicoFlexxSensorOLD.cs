using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicoFlexxSensorOLD : MonoBehaviour{

    public SceneSettings sceneSettings;
    Camera picoFlexCamera;
    public PointCloudOLD pointCloud;
    Ray[,] rayArray;

    void Start(){
        picoFlexCamera = this.transform.Find("Camera").GetComponent<Camera>();
        rayArray = new Ray[Screen.width/sceneSettings.pixelsEachRayCovers,Screen.height/sceneSettings.pixelsEachRayCovers];
    }

    void Update(){
        CreateRays();
        CastRays();
    }
    
    private void CreateRays(){
        int columnAmount = rayArray.GetLength(0); //X
        int rowAmount = rayArray.GetLength(1); //Y

        for(int y = 0;y<rowAmount;y++){
            for(int x = 0; x<columnAmount;x++){
                Ray newRay = picoFlexCamera.ScreenPointToRay(new Vector3(x*sceneSettings.pixelsEachRayCovers,y*sceneSettings.pixelsEachRayCovers,0));
                
                rayArray[x,y]= newRay;
            }
        }
    }

    private void CastRays(){
        int columnAmount = rayArray.GetLength(0); //X
        int rowAmount = rayArray.GetLength(1); //Y
        for(int y = 0;y<rowAmount;y++){
            for(int x = 0; x<columnAmount;x++){
                Ray currentRay = rayArray[x,y];

                RaycastHit hit;
                Vector2 rayID = new Vector2(x, y);
                if (Physics.Raycast(currentRay,out hit)){
                    //Debug.DrawRay(currentRay.origin,currentRay.direction*2,Color.red);
                    pointCloud.SetPointPosition(rayID,hit.point,true);
                }else{
                    pointCloud.SetPointPosition(rayID,hit.point,false);
                }
            }
        }

    }
}
