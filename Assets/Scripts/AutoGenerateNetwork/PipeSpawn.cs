using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawn : MonoBehaviour
{
    int preState = 0;
    public GameObject StraightPipe;
    public GameObject Bend15Pipe;


    public GameObject Bend30Pipe,Bend45Pipe;
 void SpawnPipes(int amount) {
     GameObject newPipe = Instantiate(StraightPipe);
     newPipe.transform.name = "PipeStraight";
     for (int i = 0; i < amount; i++)
     {
         GameObject tempPipe = SpawnObject2(newPipe);
         newPipe=tempPipe;
     }
 }  
     
     GameObject SpawnObject2(GameObject preObject){
        //Get Head position from point
        Vector3 startPosition = preObject.transform.Find("Head").position;
        Vector3 WorldRotation = preObject.transform.rotation.eulerAngles;

        GameObject newPipe = new GameObject();
        int number;
        if(preState == 1){
            number = Random.Range(1,5);
        } else {
            number = 1;
        }
        preState = number;
        switch(number){
            case 1:
                newPipe = Instantiate(StraightPipe);
                newPipe.transform.position = startPosition;
                newPipe.transform.name = "PipeStraight";
                break;
            case 2:
                newPipe = Instantiate(Bend15Pipe);
                newPipe.transform.position = startPosition;
                newPipe.transform.name = "PipeBend15";
                break;
            case 3:
                newPipe = Instantiate(Bend30Pipe);
                newPipe.transform.position = startPosition;
                newPipe.transform.name = "PipeBend30";
                break;
            case 4:
                newPipe = Instantiate(Bend45Pipe);
                newPipe.transform.position = startPosition;
                newPipe.transform.name = "PipeBend45";
                break;
        }

        int rotationAroundZ = 0;
        int randomNumber = Random.Range(0,2);
        if(randomNumber == 0){
            rotationAroundZ = 0;
        }else{
            rotationAroundZ = -180;
        }

         if(preObject.transform.name == "PipeBend15"){
                    newPipe.transform.Rotate(WorldRotation + new Vector3(0,15,rotationAroundZ), Space.World);
         }
        else if(preObject.transform.name == "PipeBend30"){
                    newPipe.transform.Rotate(WorldRotation + new Vector3(0,30,rotationAroundZ), Space.World);
        }
        else if(preObject.transform.name == "PipeBend45"){
                    newPipe.transform.Rotate(WorldRotation + new Vector3(0,45,rotationAroundZ), Space.World);
         }       
         else {
            newPipe.transform.Rotate(new Vector3 (0,WorldRotation.y*-1,0), Space.World);
         }
        return newPipe;
    }

    void Start()
    {
        SpawnPipes(10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
