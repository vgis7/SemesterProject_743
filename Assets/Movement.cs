using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour{
    void Start(){
        
    }

    void Update(){
        if(Input.GetKey(KeyCode.W)){
            this.gameObject.transform.Translate(Vector3.left*0.05f);
        }
    }


}
