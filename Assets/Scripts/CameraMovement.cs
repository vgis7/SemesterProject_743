using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour{
    void Start(){
        
    }

    void Update(){
        if(Input.GetKey(KeyCode.W)){
            this.gameObject.transform.Translate(Vector3.left*0.05f);
        }
        if(Input.GetKey(KeyCode.S)){
            this.gameObject.transform.Translate(Vector3.right*0.05f);
        }
    }


}
