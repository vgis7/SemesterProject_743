using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicoFlexxScript : MonoBehaviour{

    Camera picoFlexCamera;
    public PointCloudScript pointCloud;
    Ray[,] rayArray = new Ray[Screen.width/5,Screen.height/5];

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
                Ray newRay = picoFlexCamera.ScreenPointToRay(new Vector3(x*5,y*5,0));
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
                Debug.DrawRay(currentRay.origin,currentRay.direction*5f,Color.red);

                RaycastHit hit;
                Vector2 rayID = new Vector2(x, y);
                if (Physics.Raycast(currentRay,out hit)){
                    pointCloud.SetPointPosition(rayID,hit.point,hit.distance);
                }else{
                    pointCloud.SetPointPosition(rayID, new Vector3(100f,0f,0f));
                }
            }
        }
    }
}
