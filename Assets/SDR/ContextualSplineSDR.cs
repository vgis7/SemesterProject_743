using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextualSplineSDR : StructuredDomainRandomization
{

    public int numberOfControlPoints;
    public ControlPoint[] controlPoints;
    public bool contextIsDefect;
    public float defectProbability;

    //Defects
    public GameObject[] defectRubberRings;


    public void Setup(int numberOfControlPoints, ControlPoint[] controlPoints, bool contextIsDefect, float defectProbability){
        this.numberOfControlPoints = numberOfControlPoints;
        this.controlPoints = controlPoints;
        this.contextIsDefect = contextIsDefect;
        this.defectProbability = defectProbability;
    }

    void Start(){
        Random.seed = (int)System.DateTime.Now.Ticks;
        CreateObjects();
    }



    ///////////////////////
    // Objects
    //////////////////////

    private void CreateObjects(){
        if(!contextIsDefect){
            CreateSewerSystem();
        }

        if(contextIsDefect){
            CreateRubberRings();
        }
    }

    private void CreateRubberRings(){
        for (int i = 0; i < numberOfControlPoints; i++) {

            if (controlPoints[i].displacedPipe == false){ continue; }

            int randomValue = Random.Range(0, 5);
            GameObject newRubberRing = Instantiate(defectRubberRings[randomValue]);
            newRubberRing.transform.parent = this.transform.Find("Objects");

            Transform pipehead = controlPoints[i].pipe.transform.Find("Head");
            newRubberRing.transform.position = pipehead.position+new Vector3(0,0,-0.05f);
            newRubberRing.transform.rotation = pipehead.rotation;
            newRubberRing.transform.Rotate(new Vector3(90, 0, 0), Space.Self);
        }
    }

    private void CreateSewerSystem(){
        for (int i = 0; i < numberOfControlPoints; i++) {
            if (controlPoints[i].pipe != null) {
                GameObject newGameObject = Instantiate(controlPoints[i].pipe);
                newGameObject.transform.parent = this.transform.Find("Objects");
            }
            if (controlPoints[i].endBlock != null) {
                GameObject newGameObject = Instantiate(controlPoints[i].endBlock);
                newGameObject.transform.parent = this.transform.Find("Objects");
            }
        }
    }


}
