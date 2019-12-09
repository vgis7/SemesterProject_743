using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalParametersSDR : StructuredDomainRandomization{

    public GameObject contextualSpline;
    public GameObject blockCamera;
    public GameObject[] pipeModels;
    

    public void SetGlobalParameters(int numberOfControlPoints, float turnProbability, float defectProbability){
        //Control Point Shape
        ControlPoint[] controlPoints = GenerateSplineShape(numberOfControlPoints, turnProbability, defectProbability);

        //Pipe Spline
        CreateContextualSpline(numberOfControlPoints, controlPoints, 0f, false, defectProbability);

        //Defect
        if (defectProbability > 0) {
            CreateContextualSpline(numberOfControlPoints, controlPoints, 0.2f, true, defectProbability);
        }
    }

    private void CreateContextualSpline(int numberOfControlPoints, ControlPoint[] controlPoints, float amountOffset, bool contextIsDefect, float defectProbability){
        GameObject newContextualSplineGameObject = Instantiate(contextualSpline);
        ContextualSplineSDR newContextualSplineScript = newContextualSplineGameObject.GetComponent<ContextualSplineSDR>();
        newContextualSplineGameObject.transform.parent = this.transform.Find("ContextualSplines");


        newContextualSplineScript.Setup(numberOfControlPoints, controlPoints, contextIsDefect, defectProbability);

        LineRenderer lineRenderer = newContextualSplineGameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = numberOfControlPoints;

        Random.seed = (int)System.DateTime.Now.Ticks;

        for (int i = 0; i < numberOfControlPoints; i++) {
            lineRenderer.SetPosition(i, controlPoints[i].position+new Vector3(0,amountOffset,0));
        }
    }

    private ControlPoint[] GenerateSplineShape(int numberOfControlPoints, float turnProbability, float defectProbability){
        int numberOfPipes = numberOfControlPoints / 4;
        ControlPoint[] controlPoints = new ControlPoint[numberOfControlPoints];
        ///Spawn Pipes and Obtain ControlPoints
        for (int i = 0; i < numberOfPipes; i++) {
            GameObject newPipe = SpawnPipeObject(turnProbability, i);

            //Defect Displacement
            float randomValue = Random.Range(0f, 100f);
            if (randomValue <= defectProbability) {
                newPipe.transform.Translate(new Vector3(0, 0, -0.2f), Space.Self);
                controlPoints[(4 * i) + 0].displacedPipe = true;
            }

            controlPoints[(4 * i) + 0].pipe = newPipe;

            controlPoints[(4 * i) + 0].position = newPipe.transform.Find("BendStart").position;
            controlPoints[(4 * i) + 1].position = newPipe.transform.Find("BendMid").position;
            controlPoints[(4 * i) + 2].position = newPipe.transform.Find("BendHead").position;
            controlPoints[(4 * i) + 3].position = newPipe.transform.Find("Head").position;

            Destroy(newPipe);

            if(i == numberOfPipes-1){
                print("got here");
                blockCamera.transform.position = controlPoints[(4 * i) + 3].position;
                blockCamera.transform.LookAt(controlPoints[(4 * i) + 2].position);
                controlPoints[(4 * i) + 3].endBlock = blockCamera;
            }
        }
        return controlPoints;
    }


    private GameObject SpawnPipeObject(float turnProbability, int pipeIdx){
        GameObject newPipe;

        ///Decide which pipe to create
        float randomValue = Random.Range(0f, 100f);
        if (randomValue > turnProbability) {
            newPipe = Instantiate(pipeModels[0]);
        } else {
            int randomSelectTurnPipe = Random.Range(1, 7);
            newPipe = Instantiate(pipeModels[randomSelectTurnPipe]);
        }

        newPipe.transform.parent = this.transform.Find("TempPipeContainer");

        ///Get info about previous pipe
        Vector3 previousPipeRotation = new Vector3(0, 0, 0);
        Vector3 previousRotateOutput = new Vector3(0, 0, 0);
        Vector3 previousPipePosition = new Vector3(0, 0, 0);

        string previousPipeName = null;
        if (pipeIdx != 0) {
            Transform previousPipe = this.transform.Find("TempPipeContainer").GetChild(pipeIdx - 1);
            previousPipeName = this.transform.Find("TempPipeContainer").GetChild(pipeIdx - 1).name;
            previousPipeRotation = previousPipe.rotation.eulerAngles;
            previousPipePosition = previousPipe.transform.Find("Head").position;
        }

        ///Set Position
        newPipe.transform.position = previousPipePosition;

        ///Rotate
        switch (previousPipeName) {
            case "PipeStraight(Clone)":
                newPipe.transform.Rotate(previousPipeRotation, Space.World);
                break;

            case "PipeBend15L(Clone)":
                newPipe.transform.Rotate(previousPipeRotation + new Vector3(0, 15, 0), Space.World);
                break;
            case "PipeBend15R(Clone)":
                newPipe.transform.Rotate(previousPipeRotation + new Vector3(0, -15, -180), Space.World);
                break;

            case "PipeBend30L(Clone)":
                newPipe.transform.Rotate(previousPipeRotation + new Vector3(0, 30, 0), Space.World);
                break;
            case "PipeBend30R(Clone)":
                newPipe.transform.Rotate(previousPipeRotation + new Vector3(0, -30, -180), Space.World);
                break;

            case "PipeBend45L(Clone)":
                newPipe.transform.Rotate(previousPipeRotation + new Vector3(0, 45, 0), Space.World);
                break;
            case "PipeBend45R(Clone)":
                newPipe.transform.Rotate(previousPipeRotation + new Vector3(0, -45, -180), Space.World);
                break;
        }

        return newPipe;
    }

}
