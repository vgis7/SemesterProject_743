using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCloudOLD : MonoBehaviour{
    public SceneSettings sceneSettings;
    Transform pointsContainer;
    public GameObject pointPrefab;
    GameObject[,] pointArray;

    void Start(){

        pointArray = new GameObject[Screen.width/sceneSettings.pixelsEachRayCovers,Screen.height/sceneSettings.pixelsEachRayCovers];
        InitializePoints();
    }
    
    void Update(){
        
    }

    private void InitializePoints(){
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

    public void SetPointPosition(Vector2 rayID,Vector3 hitPosition,bool isPointHit){
        int x = (int) rayID.x;
        int y = (int) rayID.y;
        
        if(isPointHit){ 
            pointArray[x,y].SetActive(true);
            pointArray[x,y].transform.position = pointsContainer.position+hitPosition;
        }else{
            pointArray[x,y].SetActive(false);
        }
    }
}
