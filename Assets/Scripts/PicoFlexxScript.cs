using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicoFlexxScript : MonoBehaviour{

    Camera picoFlexCamera;
    public PointCloudScript pointCloud;
    Ray[,] rayArray = new Ray[Screen.width/10,Screen.height/10];

    void Start(){
        picoFlexCamera = this.transform.Find("Camera").GetComponent<Camera>();
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
                Ray newRay = picoFlexCamera.ScreenPointToRay(new Vector3(x*10,y*10,0));
                
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
