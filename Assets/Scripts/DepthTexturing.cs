using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthTexturing : MonoBehaviour{
    public Shader shader;
    void Start(){
        Camera.main.SetReplacementShader(shader,"RenderType");
    }
    
    void Update(){
    }
}
