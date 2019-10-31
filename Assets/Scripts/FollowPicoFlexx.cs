using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPicoFlexx : MonoBehaviour{
    public Transform picoFlexxTransform;

    void Start(){
        transform.parent.position = transform.parent.position+new Vector3(20,0,0);
    }

    void Update(){
        ///Camera follows the PicoFlexx.
        
        Vector3 thisPosition = this.transform.position;

        transform.position = picoFlexxTransform.position+transform.parent.position;
        
        //this.transform.position = new Vector3(picoFlexxTransform.position.x-0.1f,picoFlexxTransform.position.y,picoFlexxTransform.position.xz); ///0.1f, is added, because else the images will look a bit weird. Gotta look into this.
        
        //print("This Transform: "+thisPosition + " && "+"PicoFlexxTransoform"+picoFlexxTransform.position);
    }
}
