﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSettings : MonoBehaviour{
    public int pixelsEachRayCovers = 10;
    public int imageWidth = 224; 
    public int imageHeight = 171;
    public string imagePath; 
    public string directory_path;
    public bool beginSimulation;

    void Start(){
        Screen.SetResolution(imageWidth,imageHeight,false);
        beginSimulation = false;
    }

    void Update(){
        
    }
}
