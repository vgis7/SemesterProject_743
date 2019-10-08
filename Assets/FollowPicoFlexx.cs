using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPicoFlexx : MonoBehaviour{
    public Transform picoFlexxTransform;

    void Start(){
        
    }

    void Update(){
        ///Camera follows the PicoFlexx.
        Vector3 thisTransform = this.transform.position;
        this.transform.position = new Vector3(picoFlexxTransform.position.x-0.1f, thisTransform.y, thisTransform.z); ///0.1f, is added, because else the images will look a bit weird. Gotta look into this.
    }
}
