using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSensor : MonoBehaviour{
    public float rotationSpeed = 20f;
    void Start(){
        
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
            print("Distance: "+hit.distance);
            Debug.DrawRay(transform.position,transform.TransformDirection(Vector3.up)*100,Color.red);
        }else{
            Debug.DrawRay(transform.position,transform.TransformDirection(Vector3.up)*100,Color.yellow);
        }
    }
}
