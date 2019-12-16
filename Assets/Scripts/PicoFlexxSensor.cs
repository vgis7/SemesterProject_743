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
    public bool activateGenerateCSV;

    Vector3[] directions;
    bool unsureToLabelImageAsDefect;
    
    public GameObject rayCubeInitializer;

    void Start(){
        picoFlexCamera = this.transform.Find("Camera").GetComponent<Camera>();
        rayArray = new Ray[(sceneSettings.imageWidth/sceneSettings.pixelsEachRayCovers)*(sceneSettings.imageHeight/sceneSettings.pixelsEachRayCovers)]; ///Size of the ray
        foundDefect = false;
        directions = null;
        unsureToLabelImageAsDefect = true;
    }

    public struct Point{
        public Vector2 position;
        public Vector3 direction;
        public float distance;
        public Vector3 impactPoint;
        public Vector3 normal;
    }

    void Update(){
        Point[] points = CastRays();
        //pointCloud.SetAllParticlesPositions(GetRayHitPositions()); ///Sends the ray hit positions towards the point cloud, that transforms the position of each particle inside the pointcloud to these hit positions.
        //Point[] points = GetRayHitPointStruct();
        points = GetRayHitPointStruct(points);
        float[] distance = GetDistanceFromPointStruct(points);

        GeneratorCSV(points);

        directions = GetDirectionsFromPointStruct(points);

        rayCubeInitializer.SetActive(false); ///Disables the cube which used to initialize rays. Else a black circle will come.

        pointCloud.SetAllParticlesPositions(picoFlexCamera.transform.position,directions,distance);
        TakeScreenShotAndMoveCamera();
        
    }

    private void GeneratorCSV(Point[] points){
        if(activateGenerateCSV){
            DepthArrayToCSV.ConvertArrayToCSV(points);
        }
    }

    /// <summary>
    /// Waits untill the point cloud has initialized all its particles. Each time an image is successfully taken, the camera will move.
    /// </summary>
    private void TakeScreenShotAndMoveCamera(){
        bool imageTaken = false;
        if (pointCloud.pointCloudIsReady && unsureToLabelImageAsDefect == false){
            imageTaken = dataGenerator.GenerateImage(foundDefect);
        }
        if(imageTaken || unsureToLabelImageAsDefect == true) {
            foundDefect = false;
            unsureToLabelImageAsDefect = false;
            //transform.GetComponent<moveCamera>().UpdateCameraMovement();
            this.transform.GetComponent<moveCamera>().UpdateCameraMovement();
            float walklength = 0.05f;
            //this.gameObject.transform.Translate(Vector3.forward* walklength);
        }
    }

    /// <summary>
    /// Casts rays towards screen points and returns the rays. The screen points are based upon the stride (CHANGE COMMENT)
    /// </summary>
    private Point[] CastRays(){
        Point[] points = new Point[rayArray.Length];
        int indexCounter = 0;
        for(int y = 0; y<sceneSettings.imageHeight/sceneSettings.pixelsEachRayCovers;y++){
            for(int x = 0; x<sceneSettings.imageWidth/sceneSettings.pixelsEachRayCovers;x++){
                rayArray[indexCounter] = picoFlexCamera.ScreenPointToRay(new Vector3(x*sceneSettings.pixelsEachRayCovers,y*sceneSettings.pixelsEachRayCovers,-0.5f));
                points[indexCounter].position = new Vector2(x,y);
                indexCounter++;
            }
        }

        //picoFlexCamera.fieldOfView = 62;
        //picoFlexCamera.focalLength = 3.994271f;
        //picoFlexCamera.gateFit = Camera.GateFitMode.Horizontal;
        return points;
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
            if(Physics.Raycast(rayArray[i],out hit,4)){
                ///Checks for tag named defect
                if(hit.transform.gameObject.tag == "Defect" && foundDefect == false){
                    foundDefect = true;
                }
                ///Saves the position of each ray hit
                hitPositions[i] = hit.point;
                //Debug.DrawRay(rayArray[i].origin,rayArray[i].direction*2,Color.red); ///DEBUG RAY
            } else {
                ///Move particles out of the screen, if a ray has not hit.
                hitPositions[i] = new Vector3(0,-10,0);
            }
        }
        return hitPositions;
    }



    Point[] GetRayHitPointStruct(Point[] points){
        RaycastHit hit;

        for(int i = 0; i < rayArray.Length;i++){
            if(Physics.Raycast(rayArray[i],out hit,4)){
                ///Checks for tag named defect
                if(hit.transform.gameObject.tag == "Defect" && foundDefect == false){
                    foundDefect = true;
                }

                ///Saves the position of each ray hit
                points[i].direction = rayArray[i].direction;

                ///Checks if pixel includes defect
                float defectAmount = CheckIfPixelDefectAtHitUVCoordinate(hit);
                points[i].distance = hit.distance + defectAmount;
          
                ///ImpactPoint save to point
                points[i].impactPoint = hit.point;

                points[i].normal = hit.normal;
                

                //Debug.DrawRay(rayArray[i].origin,rayArray[i].direction*hit.distance,Color.red); ///DEBUG RAY
            } else {
                ///Move particles out of the screen, if a ray has not hit.
                points[i].direction = new Vector3(0,0,0);
            }
        }

        return points;
    }

    /// <summary>
    /// Takes the directions from the point struct and returns a new array of directions.
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    Vector3[] GetDirectionsFromPointStruct(Point[] points){
        if(directions != null){ return directions; }

        Vector3[] new_directions = new Vector3[points.Length];
        for(int i = 0; i<points.Length;i++){
            new_directions[i] = points[i].direction;
        }
        return new_directions;
    }

    /// <summary>
    /// Takes the distance from the point struct and returns a new array of distances.
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    float[] GetDistanceFromPointStruct(Point[] points){
        float[] distance = new float[points.Length];
        for(int i = 0; i<points.Length;i++){
            distance[i] = points[i].distance;
        }
        return distance;
    }


    float CheckIfPixelDefectAtHitUVCoordinate(RaycastHit hit){
        Texture2D texmap = hit.transform.GetComponent<Renderer>().material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= texmap.width;
        pixelUV.y *= texmap.height;

        Color nonDefectColor = new Vector4(0, 0, 0, 1);
        Color hitPixel = texmap.GetPixel((int)pixelUV.x, (int)pixelUV.y);
        if (hitPixel != nonDefectColor) {
            float defectAmount = ((hitPixel.r+hitPixel.g+hitPixel.b)/3)+0.2f;

            ///To make sure that the defect can be seen
            /////REALLY IMPORTANT TO CHANGE//////
            if(hit.distance < 3 && hit.distance > 1f){
                foundDefect = true;
            }

            ///If defect is already close enough to be seen, don't tell the program that it's unsure of how to label the image.
            if(foundDefect == false){
                unsureToLabelImageAsDefect = true;
            }

            return defectAmount;
        }
        return 0f;
    }
}
