using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;


public class UserInteractions : MonoBehaviour{
    public GameObject sceneManager;
    private SceneSettings sceneSettings;
    private DataGenerator dataGenerator;

    public Text numberOfImagesFineText, numberOfImagesDefectText,numberOfImagesTotalText;
    private int numberOfImagesFine, numberOfImagesDefect,numberOfImagesTotal;

    string directory_path = "";

    void Start(){
        dataGenerator = sceneManager.GetComponent<DataGenerator>();
        sceneSettings = sceneManager.GetComponent<SceneSettings>();
    }
    
    void Update(){
        ChooseNumberOfImages();
    }

    private int ParseTextToInt(Text numberOfImagesText){
        int number;
        bool success = int.TryParse(numberOfImagesText.text,out number);
        if(success){
             return number;
        }
        return 0;
    }

    private void ChooseNumberOfImages(){
        numberOfImagesFine = ParseTextToInt(numberOfImagesFineText);
        numberOfImagesDefect = ParseTextToInt(numberOfImagesDefectText);
        numberOfImagesTotal = (numberOfImagesFine + numberOfImagesDefect);
        numberOfImagesTotalText.text =numberOfImagesTotal.ToString();
    }

    public void ButtonPressGenerate(){
        dataGenerator.SetNumberOfDesiredImages(numberOfImagesFine,numberOfImagesDefect);
        sceneSettings.beginSimulation = true;
        //dataGenerator.GenerateImages(numberOfImagesTotal);
    }

    public void ButtonSetDirectory(){
        directory_path = EditorUtility.OpenFolderPanel("Set Directory",directory_path,"");
        Directory.CreateDirectory(directory_path+"/Fine");
        Directory.CreateDirectory(directory_path+"/Defect");
        sceneSettings.directory_path = directory_path;
    }
}
