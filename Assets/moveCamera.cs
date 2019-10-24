using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class moveCamera : MonoBehaviour
{
    private Vector3 nextPostition;
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
            float step =  1.0f * Time.deltaTime; // calculate distance to move
            Vector3 moveTowards = Vector3.MoveTowards(transform.position ,nextPostition,step);
            transform.position = moveTowards;
        }
    }

    public void moveTheCamera(Vector3[] positions){
        this.positions = positions;
        transform.position = positions[0];
        nextPostition = positions[1];
    }
}
