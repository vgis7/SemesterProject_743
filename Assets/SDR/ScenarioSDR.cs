using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioSDR : StructuredDomainRandomization{

    void Start(){
        GenerateScenario();
    }

    ////////////////////
    //Scenario
    ////////////////////

    /// <summary>
    /// Uniform distribution for selecting a scenario
    /// </summary>
    private void GenerateScenario(){
        int scenario = Random.Range(0,2);
        switch(scenario) {
            case 0:
                print("Straight Sewer (No Defects)");
                transform.GetComponent<GlobalParametersSDR>().SetGlobalParameters(140, 15f, 0f);
                break;

            case 1:
                print("Straight Sewer (Many Defects)");
                transform.GetComponent<GlobalParametersSDR>().SetGlobalParameters(140, 15f, 0f);
                break;
        }
    }

}
