using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour{
    public GameObject pipeStraight, pipeBend;

    void Start(){
        for(int i = 0; i<4;i++){
            int random = Random.Range(0,1);

            GameObject newPipe = Instantiate(pipeStraight);
            newPipe.transform.position = new Vector3(0,0,3f*i);
            newPipe.transform.Rotate(new Vector3(-90,0,0));
        }
    }

    void Update(){
        
    }
}
