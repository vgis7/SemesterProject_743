using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour{
    public GameObject pipeStraight, pipeBend;

    void Start(){
        Vector3 createNextObjectAtVector = new Vector3(0,0,3);

        for(int i = 0; i<3;i++){
            int random = Random.Range(0,2);
            print(random);
            if(random == 0){
                GameObject newPipe = Instantiate(pipeStraight);
                newPipe.transform.Rotate(new Vector3(-90, 0, 0));
                newPipe.transform.position = createNextObjectAtVector;
                createNextObjectAtVector += new Vector3(0,0,3);
            }
            if(random == 1){
                GameObject newPipe = Instantiate(pipeBend);
                newPipe.transform.Rotate(new Vector3(0, 0, 0));
                newPipe.transform.position = createNextObjectAtVector;
                createNextObjectAtVector += new Vector3(0, 0, 1.5f);
            }
            //GameObject newPipe = Instantiate(pipeStraight);
            //newPipe.transform.position = new Vector3(0,0,3f*i);
            //newPipe.transform.Rotate(new Vector3(-90,0,0));
        }
    }

    void Update(){
        
    }
}
