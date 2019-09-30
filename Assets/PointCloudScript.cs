using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCloudScript : MonoBehaviour{
    Transform pointsContainer;
    public GameObject pointPrefab;
    GameObject[,] pointArray = new GameObject[Screen.width/10,Screen.height/10];

    void Start(){
        pointsContainer = this.transform.Find("PointsContainer");

        int columnAmount = pointArray.GetLength(0); //X
        int rowAmount = pointArray.GetLength(1); //Y

        for(int y = 0;y<rowAmount;y++){
            for(int x = 0; x<columnAmount;x++){
                GameObject newPoint = Instantiate(pointPrefab,pointsContainer);
                pointArray[x,y]=newPoint;
            }
        }
    }
    
    void Update(){
        
    }

    public void SetPointPosition(Vector2 rayID,Vector3 hitPosition){
        int x = (int) rayID.x;
        int y = (int) rayID.y;
        pointArray[x,y].transform.position = pointsContainer.position+hitPosition;
        //print("RayID: "+rayID+" "+"HitPosition: "+hitPosition);
    }
}
