using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSensor : MonoBehaviour{
    public float rotationSpeed = 20f;
    public Vector3[] hitposition;
    private LineRenderer visibleRaycast;

    void Start(){
        visibleRaycast = GetComponent<LineRenderer>();
    }

    void Update(){
        RotateSensor();
        LaserRaycast(); 
    }


    private void RotateSensor(){
        transform.Rotate(Vector3.right*rotationSpeed,Space.Self);
    }

    private void LaserRaycast(){
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.up),out hit, 100f)){
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
