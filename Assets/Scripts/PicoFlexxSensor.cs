﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicoFlexxSensor : MonoBehaviour{

    public SceneSettings sceneSettings;
    public PointCloud pointCloud;
    Camera picoFlexCamera;
    Ray[] rayArray;

    bool foundDefect;
    public DataGenerator dataGenerator;

    Vector3[] directions;

    void Start(){
        picoFlexCamera = this.transform.Find("Camera").GetComponent<Camera>();
        rayArray = new Ray[(sceneSettings.imageWidth/sceneSettings.pixelsEachRayCovers)*(sceneSettings.imageHeight/sceneSettings.pixelsEachRayCovers)]; ///Size of the ray
        foundDefect = false;
        directions = null;
    }

    

    public struct Point{
        public Vector3 direction;
        public float distance;
    }

    void Update(){
        CastRays();
        //pointCloud.SetAllParticlesPositions(GetRayHitPositions()); ///Sends the ray hit positions towards the point cloud, that transforms the position of each particle inside the pointcloud to these hit positions.
        Point[] points = GetRayHitPointStruct();
        float[] distance = GetDistanceFromPointStruct(points);
        if(directions == null){
            directions = GetDirectionsFromPointStruct(points);
        }
        
        pointCloud.SetAllParticlesPositions(picoFlexCamera.transform.position,directions,distance);
        TakeScreenShotAndMoveCamera();
    }

    /// <summary>
    /// Waits untill the point cloud has initialized all its particles. Each time an image is successfully taken, the camera will move.
    /// </summary>
    private void TakeScreenShotAndMoveCamera(){
        bool imageTaken = false;
        if (pointCloud.pointCloudIsReady){
            imageTaken = dataGenerator.GenerateImage(foundDefect);
        }
        if(imageTaken){
            foundDefect = false;
            float walklength = 0.05f;
            this.gameObject.transform.Translate(Vector3.left* walklength);
        }
    }

    /// <summary>
    /// Casts rays towards screen points and returns the rays. The screen points are based upon the stride (CHANGE COMMENT)
    /// </summary>
    private void CastRays(){
        int indexCounter = 0;
        for(int y = 0; y<sceneSettings.imageHeight/sceneSettings.pixelsEachRayCovers;y++){
            for(int x = 0; x<sceneSettings.imageWidth/sceneSettings.pixelsEachRayCovers;x++){
                rayArray[indexCounter] = picoFlexCamera.ScreenPointToRay(new Vector3(x*sceneSettings.pixelsEachRayCovers,y*sceneSettings.pixelsEachRayCovers,0));
                indexCounter++;
            }
        }
    }

    /// <summary>
    /// Goes through all rays and saves the position of where each ray hits. 
    /// Moreover, if ray hits an object which is tagged "Defect", it will change the current status to have found a defect.
    /// </summary>
    /// <returns>Vector3 array that consists of the positions where rays has hit any colliders.</returns>
    Vector3[] GetRayHitPositions(){
        Vector3[] hitPositions = new Vector3[rayArray.Length];
        RaycastHit hit;
        for(int i = 0; i < rayArray.Length;i++){
            if(Physics.Raycast(rayArray[i],out hit)){
                ///Checks for tag named defect
                if(hit.transform.gameObject.tag == "Defect" && foundDefect == false){
                    foundDefect = true;
                }
                ///Saves the position of each ray hit
                hitPositions[i] = hit.point;
                ///Debug.DrawRay(rayArray[i]. ,rayArray[i].direction*2,Color.red); ///DEBUG RAY
            } else {
                ///Move particles out of the screen, if a ray has not hit.
                hitPositions[i] = new Vector3(0,-10,0);
            }
        }
        return hitPositions;
    }

    bool test = false;
    Point[] GetRayHitPointStruct(){
        Point[] points = new Point[rayArray.Length];
        RaycastHit hit;
        for(int i = 0; i < rayArray.Length;i++){
            if(Physics.Raycast(rayArray[i],out hit)){
                ///Checks for tag named defect
                if(hit.transform.gameObject.tag == "Defect" && foundDefect == false){
                    foundDefect = true;
                }
                ///Saves the position of each ray hit
                points[i].direction = rayArray[i].direction;
                points[i].distance = hit.distance;

                if(test == false){
                    Texture2D texmap = (Texture2D)hit.transform.GetComponent<Renderer>().material.mainTexture;
                    Vector2 pixelUV = hit.textureCoord;
                    pixelUV.x = texmap.width;
                    pixelUV.y = texmap.height;

                    print(texmap.GetPixel((int)pixelUV.x,(int)pixelUV.y));
                    test = true;
                }

                ///Debug.DrawRay(rayArray[i]. ,rayArray[i].direction*2,Color.red); ///DEBUG RAY
            } else {
                ///Move particles out of the screen, if a ray has not hit.
                points[i].direction = new Vector3(0,0,0);
            }
        }
        return points;
    }

    Vector3[] GetDirectionsFromPointStruct(Point[] points){
        Vector3[] directions = new Vector3[points.Length];
        for(int i = 0; i<points.Length;i++){
            directions[i] = points[i].direction;
        }
        return directions;
    }

    float[] GetDistanceFromPointStruct(Point[] points){
        float[] distance = new float[points.Length];
        for(int i = 0; i<points.Length;i++){
            distance[i] = points[i].distance;
        }
        return distance;
    }
}
