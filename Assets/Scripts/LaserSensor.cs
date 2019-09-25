using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSensor : MonoBehaviour{
    public float rotationSpeed = 20f;
    public int numberOfDetectedPoints;

    public Vector2[] arrayOfHitPositions;
    private int hitcounter;
    private LineRenderer visibleRaycast;

    void Start(){
        numberOfDetectedPoints = 5; 
        hitcounter = 0;
        visibleRaycast = GetComponent<LineRenderer>();
    }

    void Update(){
        arrayOfHitPositions = new Vector2[numberOfDetectedPoints];
        RotateSensor();
        LaserRaycast(); 
    }


    private void RotateSensor(){
        transform.Rotate(Vector3.right*rotationSpeed,Space.Self);
    }

    private void LaserRaycast(){
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.up),out hit, 100f)){
            arrayOfHitPositions[hitcounter] = hit.point;
            hitcounter++;
            if(hitcounter >= numberOfDetectedPoints){
                hitcounter = 0;
            }
            print("Hit Position: "+hit.point);

            //Make line from sensor to hit position of raycast
            var points = new Vector3[2];
            points[0] = Vector3.zero;
            points[1] =  hit.point;
            visibleRaycast.SetPositions(points);
            //Debug.DrawRay(transform.position,transform.TransformDirection(Vector3.up)*100,Color.red);
            
        }else{
            Debug.DrawRay(transform.position,transform.TransformDirection(Vector3.up)*100,Color.yellow);
        }
    }
}
