using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PipeSpawn : MonoBehaviour
{
    int preState = 0;
    public LineRenderer lineRenderer;
    public int AmountOfPipes = 10;
    public GameObject StraightPipe,Bend15Pipe,Bend30Pipe,Bend45Pipe,Bend15PipeR,Bend30PipeR,Bend45PipeR;

    public GameObject[] rubberDefects;

    public moveCamera MoveCamera;

    public bool defect;

    Vector3[] SpawnPipes(int amount) {
        int amountOfPoints= 5+((amount)*4);
        Vector3[] linePositions = new Vector3[amountOfPoints];
        lineRenderer.positionCount = amountOfPoints;
        Debug.Log("linePositions: "+ (linePositions.Length));
        GameObject prePipe = Instantiate(StraightPipe);
        prePipe.transform.name = "PipeStraight";

        linePositions[0] = prePipe.transform.position;
        linePositions[1] = prePipe.transform.Find("BendStart").position;
        linePositions[2] = prePipe.transform.Find("BendMid").position;
        linePositions[3] = prePipe.transform.Find("BendHead").position;
        linePositions[4] = prePipe.transform.Find("Head").position;

        for (int i = 0; i < amount; i++)
        {
            GameObject tempPipe = SpawnObject(prePipe);
            prePipe=tempPipe;
            ///Debug.Log(prePipe);
            ///Debug.Log(linePositions[(4*i)+5]);
            linePositions[(4*i)+5] = prePipe.transform.Find("BendStart").position;
            linePositions[(4*i)+6] = prePipe.transform.Find("BendMid").position;
            linePositions[(4*i)+7] = prePipe.transform.Find("BendHead").position;
            linePositions[(4*i)+8] = prePipe.transform.Find("Head").position;
        }
        return linePositions;
    }  
     
     GameObject SpawnObject(GameObject preObject){
        //Get Head position from point
        Vector3 startPosition = preObject.transform.Find("Head").position;
        Vector3 WorldRotation = preObject.transform.rotation.eulerAngles;


        
        
        ////////////////////
        /// Instantiate Pipes
        ////////////////////

        GameObject newPipe = null;
        int number;
        if(preState == 1){
            number = Random.Range(1,8);
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
                newPipe = Instantiate(Bend15PipeR);
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
                newPipe = Instantiate(Bend45PipeR);
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
            newPipe.transform.Rotate(WorldRotation + new Vector3(0,-15,-180), Space.World);
        }
        //Pipe 30
        else if(preObject.transform.name == "PipeBend30"){
            newPipe.transform.Rotate(WorldRotation + new Vector3(0,30,0), Space.World);
        }else if(preObject.transform.name == "PipeBend30R"){
            newPipe.transform.Rotate(WorldRotation + new Vector3(0,-30,-180), Space.World);
        }
        //Pipe 45
        else if(preObject.transform.name == "PipeBend45"){
            newPipe.transform.Rotate(WorldRotation + new Vector3(0,45,0), Space.World);
        }else if(preObject.transform.name == "PipeBend45R"){
            newPipe.transform.Rotate(WorldRotation + new Vector3(0,-45,-180), Space.World);
        }
        //Pipe Straight
        else{
            newPipe.transform.Rotate(WorldRotation, Space.World);
        }

        //int defect = Random.Range(0,2);
        float rotationDisplacement = 0;
        float translateDisplacement = 0;
        if(defect){
            int randomInt = Random.Range(0,5);
            //if(randomInt == 4){
                //rotationDisplacement = Random.Range(0f,0f);
                translateDisplacement = Random.Range(0.05f,0.2f);
                CreateRubberDefect(newPipe.transform);
            //}
        }

        newPipe.transform.Translate(new Vector3(0,0,-translateDisplacement),Space.Self);
        //newPipe.transform.localPosition -= new Vector3(0,0,translateDisplacement);
        //newPipe.transform.Rotate(transform.rotation.eulerAngles+new Vector3(0,rotationDisplacement,0));

        newPipe.transform.parent = transform.Find("PipeContainer");

        return newPipe;
    }

    void Start()
    {
        Vector3[] LP = SpawnPipes(AmountOfPipes);
        lineRenderer.SetPositions(LP);
        MoveCamera.moveTheCamera(LP);
    }

    private void CreateRubberDefect(Transform transformOfNewPipe){
        int randomInt = Random.Range(0,5);
        GameObject newRubberDefect = Instantiate(rubberDefects[randomInt]);
        
        newRubberDefect.transform.position = transformOfNewPipe.position;

        newRubberDefect.transform.rotation = transformOfNewPipe.rotation;
        float randomRotation = Random.Range(0f,100f);
        newRubberDefect.transform.Rotate(new Vector3(90,0,0),Space.Self);
        newRubberDefect.transform.Rotate(new Vector3(0,randomRotation,0),Space.Self);

        newRubberDefect.transform.parent = transformOfNewPipe;
        
    }

}
