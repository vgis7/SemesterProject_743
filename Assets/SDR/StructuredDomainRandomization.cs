using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructuredDomainRandomization : MonoBehaviour{
    public struct ControlPoint{
        public GameObject pipe;
        public GameObject endBlock;
        public bool displacedPipe;
        public Vector3 position;
    };
}
