using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class moveCamera : MonoBehaviour
{
    private Vector3 nextPostition;
    public float speedOfCamera = 1f;
    private int i;
    Vector3[] positions;
    // Start is called before the first frame update
    void Start()
    {
         i = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (positions.Length != 0){
           
            if(transform.position == nextPostition){
                i++;
                nextPostition = positions[i];
            }   
            float step =  (speedOfCamera + Random.Range(-0.1f,0.1f)) * Time.deltaTime; // calculate distance to move
            Debug.Log("step: " + step);
            Vector3 rotateTowards = Vector3.RotateTowards(transform.forward,(transform.position-nextPostition),step,0.0f);
            Vector3 moveTowards = Vector3.MoveTowards(transform.position ,nextPostition,step);
            transform.position = moveTowards;
            
            transform.rotation = Quaternion.LookRotation(rotateTowards);
            Quaternion rotationNoise = Quaternion.Euler(speedOfCamera*Random.Range(-0.05f,0.05f),speedOfCamera*Random.Range(-0.05f,0.05f),speedOfCamera*Random.Range(-0.05f,0.05f));
            transform.rotation *= rotationNoise;
            
        }
    }

    public void moveTheCamera(Vector3[] positions){
        this.positions = positions;
        transform.position = positions[0];
        nextPostition = positions[1];
    }
}
