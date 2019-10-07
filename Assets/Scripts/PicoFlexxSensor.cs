using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicoFlexxSensor : MonoBehaviour{

    public SceneSettings sceneSettings;
    public PointCloud pointCloud;
    Camera picoFlexCamera;
    Ray[] rayArray;

    bool foundDefect;
    public DataGenerator dataGenerator;

    void Start(){
        picoFlexCamera = this.transform.Find("Camera").GetComponent<Camera>();
        rayArray = new Ray[(sceneSettings.imageWidth/sceneSettings.pixelsEachRayCovers)*(sceneSettings.imageHeight/sceneSettings.pixelsEachRayCovers)];

        foundDefect = false;
    }

    void Update(){
        if(sceneSettings.beginSimulation){
            CastRays();
            pointCloud.SetAllParticlesPositions(GetRayHitPositions());
            MoveCamera();
        }
    }

    private void MoveCamera(){
        bool imageTaken = dataGenerator.GenerateImage(foundDefect);
        if(imageTaken){
            foundDefect = false;
            this.gameObject.transform.Translate(Vector3.left*0.05f);
        }
    }

    private void CastRays(){
        int indexCounter = 0;
        for(int y = 0; y<sceneSettings.imageHeight/sceneSettings.pixelsEachRayCovers;y++){
            for(int x = 0; x<sceneSettings.imageWidth/sceneSettings.pixelsEachRayCovers;x++){
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
                if(hit.transform.gameObject.tag == "Defect" && foundDefect == false){
                    foundDefect = true;
                    print("FOUND DEFECT");
                }

                //Debug.DrawRay(rayArray[i].origin,rayArray[i].direction*2,Color.red);
                hitPositions[i] = hit.point;
            }else{
                hitPositions[i] = new Vector3(0,-10,0);
            }
        }
        return hitPositions;
    }
}
