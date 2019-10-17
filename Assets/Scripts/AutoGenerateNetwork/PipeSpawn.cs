using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawn : MonoBehaviour
{
 
    public GameObject pipeToCreate;
 void SpawnPipes(int amount) {
     GameObject newPipe = Instantiate(pipeToCreate);
    
     for (int i = 0; i < amount; i++)
     {
         GameObject tempPipe = SpawnObject(newPipe);
         newPipe=tempPipe;
     }
 }  
 GameObject SpawnObject(GameObject preObject) {
     Vector3 startPosition = preObject.transform.Find("Start").position;  
     GameObject newPipe = Instantiate(pipeToCreate);
     newPipe.transform.position =  startPosition;
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
