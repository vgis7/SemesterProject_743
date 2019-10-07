using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthTexturing : MonoBehaviour{
    public Shader shader;
    void Start(){
        this.transform.GetComponent<Camera>().SetReplacementShader(shader,"RenderType");

    }
    
    void Update(){
    }
}
