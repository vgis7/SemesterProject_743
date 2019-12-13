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
        int scenario = Random.Range(0,3);
        switch(scenario) {
            case 0:
                nameOfScenario = "Straight Sewer (No Defects)";
                numberOfControlPoints = 140;
                turnProbability = 15f;
                defectProbability = 0;
                break;
            case 1:
                nameOfScenario = "Straight Sewer (No Defects)";
                numberOfControlPoints = 140;
                turnProbability = 15f;
                defectProbability = 0;
                break;
            case 2:
                nameOfScenario = "Straight Sewer (No Defects)";
                numberOfControlPoints = 140;
                turnProbability = 15f;
                defectProbability = 0;
                break;
        }

        transform.GetComponent<GlobalParametersSDR>().SetGlobalParameters(numberOfControlPoints, turnProbability, defectProbability);
    }

}
