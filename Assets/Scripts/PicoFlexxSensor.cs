﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicoFlexxSensor : MonoBehaviour{

    public SceneSettings sceneSettings;
    public PointCloud pointCloud;
    Camera picoFlexCamera;
    Ray[] rayArray;


    void Start(){
        picoFlexCamera = this.transform.Find("Camera").GetComponent<Camera>();
        rayArray = new Ray[(Screen.width/sceneSettings.pixelsEachRayCovers)*(Screen.height/sceneSettings.pixelsEachRayCovers)];
    }

    void Update(){
        CastRays();
        pointCloud.SetAllParticlesPositions(GetRayHitPositions());
    }

    private void CastRays(){
        int indexCounter = 0;
        for(int y = 0; y<Screen.height/sceneSettings.pixelsEachRayCovers;y++){
            for(int x = 0; x<Screen.width/sceneSettings.pixelsEachRayCovers;x++){
                rayArray[indexCounter] = picoFlexCamera.ScreenPointToRay(new Vector3(x*sceneSettings.pixelsEachRayCovers,y*sceneSettings.pixelsEachRayCovers,0));
                indexCounter++;
            }
        }
    }

    Vector3[] GetRayHitPositions(){
        Vector3[] hitPositions = new Vector3[rayArray.Length];
        RaycastHit hit;
        for(int i = 0; i < rayArray.Length;i++){
            if(Physics.Raycast(rayArray[i],out hit)){
                //Debug.DrawRay(rayArray[i].origin,rayArray[i].direction*2,Color.red);
                hitPositions[i] = hit.point;
            }else{
                hitPositions[i] = new Vector3(0,-10,0);
            }
        }
        return hitPositions;
    }
}
