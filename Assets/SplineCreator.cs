using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineCreator : MonoBehaviour{

    private LineRenderer lineRenderer;
    public GameObject objectContainer;

    void Start(){
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update(){
        int pointPerObject = 10;
        lineRenderer.positionCount = objectContainer.transform.childCount* pointPerObject;

        //Get Objects from object container
        GameObject[] objects = new GameObject[objectContainer.transform.childCount];
        for (int i = 0; i < objectContainer.transform.childCount; i++) {
            objects[i] = objectContainer.transform.GetChild(i).gameObject;
        }

        //CURVES
        int pointCounter = 0;
        for (int i = 0; i < objectContainer.transform.childCount; i++) {
            Vector3 startPosition = objects[i].transform.Find("Start").transform.position;
            Vector3 middlePosition = objects[i].transform.Find("Middle").transform.position;
            Vector3 endPosition = objects[i].transform.Find("End").transform.position;

            for(int j = 0; j < pointPerObject; j++){
                float t = pointCounter / (lineRenderer.positionCount - 1.0f);
                Vector3 position = (1.0f - t) * (1.0f - t) * startPosition + 2.0f * (1.0f - t) * t * middlePosition + t * t * endPosition;
                lineRenderer.SetPosition(pointCounter, position);
                pointCounter++;
            }
        }
    }
}
