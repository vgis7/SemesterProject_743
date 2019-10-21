using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawn : MonoBehaviour
{
    private Vector3 WorldRotation;
    public GameObject pipeToCreate;
 void SpawnPipes(int amount) {
     GameObject newPipe = Instantiate(pipeToCreate);
     WorldRotation = newPipe.transform.rotation.eulerAngles;
     for (int i = 0; i < amount; i++)
     {
         GameObject tempPipe = SpawnObject(newPipe);
         newPipe=tempPipe;
     }
 }  
 GameObject SpawnObject(GameObject preObject) {
     //Get Head position from point
     Vector3 startPosition = preObject.transform.Find("Head").position;   
     WorldRotation += preObject.transform.rotation.eulerAngles;
     GameObject newPipe = Instantiate(pipeToCreate);
        newPipe.transform.Rotate((WorldRotation),Space.Self);
     newPipe.transform.position =  startPosition;
     return newPipe; 
     
 }
     GameObject SpawnObject2(GameObject preObject){
        //Get Head position from point
        Vector3 startPosition = preObject.transform.Find("Head").position;
        Vector3 WorldRotation = preObject.transform.rotation.eulerAngles;

        GameObject newPipe = Instantiate(pipeToCreate);
        newPipe.transform.position = startPosition;
        newPipe.transform.Rotate(WorldRotation + new Vector3(0,15,0), Space.World);
        return newPipe;
    }

    void Start()
    {
        SpawnPipes(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
