using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawn : MonoBehaviour
{
    int preState = 0;

    public GameObject StraightPipe,Bend15Pipe,Bend30Pipe,Bend30PipeR,Bend45Pipe;
    
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

        
        
        ////////////////////
        /// Instantiate Pipes
        ////////////////////

        GameObject newPipe = new GameObject();
        int number;
        if(preState == 1){
            number = Random.Range(1,6);
        } else {
            number = 1;
        }
        preState = number;
        switch(number){
            //Pipe Straight
            case 1:
                newPipe = Instantiate(StraightPipe);
                newPipe.transform.position = startPosition;
                newPipe.transform.name = "PipeStraight";
                break;
            //Pipe 15
            case 2:
                newPipe = Instantiate(Bend15Pipe);
                newPipe.transform.position = startPosition;
                newPipe.transform.name = "PipeBend15";
                break;
            case 3:
                newPipe = Instantiate(Bend15Pipe);
                newPipe.transform.position = startPosition;
                newPipe.transform.name = "PipeBend15R";
                break;
            //Pipe 30
            case 4:
                newPipe = Instantiate(Bend30Pipe);
                newPipe.transform.position = startPosition;
                newPipe.transform.name = "PipeBend30";
                break;
            case 5:
                newPipe = Instantiate(Bend30PipeR);
                newPipe.transform.position = startPosition;
                newPipe.transform.name = "PipeBend30R";
                break;
            //Pipe 45
            case 6:
                newPipe = Instantiate(Bend45Pipe);
                newPipe.transform.position = startPosition;
                newPipe.transform.name = "PipeBend45";
                break;
            case 7:
                newPipe = Instantiate(Bend45Pipe);
                newPipe.transform.position = startPosition;
                newPipe.transform.name = "PipeBend45R";
                break;
        }

        
        ////////////////////
        /// Rotatate Pipes
        ////////////////////

        //Pipe 15
        if(preObject.transform.name == "PipeBend15"){
            newPipe.transform.Rotate(WorldRotation + new Vector3(0,15,0), Space.World);
        }else if(preObject.transform.name == "PipeBend15R"){
            newPipe.transform.Rotate(WorldRotation + new Vector3(0,-15,0), Space.World);
        }
        //Pipe 30
        else if(preObject.transform.name == "PipeBend30"){
            newPipe.transform.Rotate(WorldRotation + new Vector3(0,30,0), Space.World);
        }else if(preObject.transform.name == "PipeBend30R"){
            newPipe.transform.Rotate(WorldRotation + new Vector3(0,-30,0), Space.World);
        }
        //Pipe 45
        else if(preObject.transform.name == "PipeBend45"){
            newPipe.transform.Rotate(WorldRotation + new Vector3(0,45,0), Space.World);
        }else if(preObject.transform.name == "PipeBend45R"){
            newPipe.transform.Rotate(WorldRotation + new Vector3(0,-45,0), Space.World);
        }
        //Pipe Straight
        else{
            newPipe.transform.Rotate(WorldRotation, Space.World);
        }

        /*
        if(preObject.transform.name == "PipeBend15"){
                newPipe.transform.Rotate(WorldRotation + new Vector3(0,15,0), Space.World);
        }
        else if(preObject.transform.name == "PipeBend30"){
                    newPipe.transform.Rotate(WorldRotation + new Vector3(0,30,0), Space.World);
        }
        else if(preObject.transform.name == "PipeBend45"){
                    newPipe.transform.Rotate(WorldRotation + new Vector3(0,45,0), Space.World);
            }    
        else if(preObject.transform.name == "PipeBend30R"){
                    newPipe.transform.Rotate(WorldRotation + new Vector3(0,-30,0), Space.World);
        }       
        else {
        newPipe.transform.Rotate(WorldRotation, Space.World);
        }*/
        return newPipe;
    }

    void Start()
    {
        SpawnPipes(20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
