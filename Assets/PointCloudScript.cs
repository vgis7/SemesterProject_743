﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCloudScript : MonoBehaviour{
    Transform pointsContainer;
    public GameObject pointPrefab;
    GameObject[,] pointArray = new GameObject[Screen.width/5,Screen.height/5];

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
    }

    public void SetPointPosition(Vector2 rayID, Vector3 hitPosition,float distanceFromCameraToImpact){
        int x = (int)rayID.x;
        int y = (int)rayID.y;
        pointArray[x,y].transform.position = pointsContainer.position + hitPosition;
        Color newColor = Color.HSVToRGB(Mathf.Clamp(distanceFromCameraToImpact-2,0f,0.7f), 1f,1f);
        pointArray[x,y].GetComponent<Renderer>().material.SetColor("_Color",newColor);
    }
}
