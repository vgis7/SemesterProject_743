using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioSDR : StructuredDomainRandomization{

    private void Start(){
        GenerateScenario();
    }

    [SerializeField]
    private string nameOfScenario;
    [SerializeField]
    private int numberOfControlPoints;
    [SerializeField]
    private float turnProbability;
    [SerializeField]
    private float defectProbability;

    ////////////////////
    //Scenario
    ////////////////////

    /// <summary>
    /// Uniform distribution for selecting a scenario
    /// </summary>
    private void GenerateScenario(){
        int scenario = Random.Range(0,4);
        switch(scenario) {
            case 0:
                nameOfScenario = "Straight Sewer (Small Amount of Defects)";
                numberOfControlPoints = 300;
                turnProbability = 10f;
                defectProbability = 10f;
                break;
            case 1:
                nameOfScenario = "Turning Sewer (Small Amount of Defects)";
                numberOfControlPoints = 300;
                turnProbability = 50f;
                defectProbability = 10f;
                break;
            case 2:
                nameOfScenario = "Straight Sewer (Large Amount of Defects)";
                numberOfControlPoints = 300;
                turnProbability = 60f;
                defectProbability = 40f;
                break;
             case 3:
                nameOfScenario = "Turning Sewer (Large Amount of Defects)";
                numberOfControlPoints = 300;
                turnProbability = 60f;
                defectProbability = 40f;
                break;
        }

        transform.GetComponent<GlobalParametersSDR>().SetGlobalParameters(numberOfControlPoints, turnProbability, defectProbability);
    }

}
