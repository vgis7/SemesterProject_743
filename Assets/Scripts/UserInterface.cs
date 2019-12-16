using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif



public class UserInterface : MonoBehaviour{
    public GameObject simulationManager;
    public GameObject picoFlexx;
    private SceneSettings sceneSettings;
    private DataGenerator dataGenerator;

    public InputField numberOfImagesFineInputField, numberOfImagesDefectInputField;
    public InputField pathToDirectory;

    public Text numberOfImagesTotalText;
    public int numberOfImagesFine, numberOfImagesDefect,numberOfImagesTotal;
    public Slider progressBar;

    string directory_path = "";

    void Start(){
        dataGenerator = simulationManager.GetComponent<DataGenerator>();
        sceneSettings = simulationManager.GetComponent<SceneSettings>();
    }
    
    void Update(){
        ChooseNumberOfImages();
        UpdateNumberOfImages();
        UpdateProgressBar();
    }

    /// <summary>
    /// Updates the progressbar by computing how many images that have been obtained compared to the total desired number of images.
    /// </summary>
    private void UpdateProgressBar(){
        if (picoFlexx.activeSelf == true) {
            progressBar.value = progressBar.maxValue-(numberOfImagesFine+numberOfImagesDefect);
        }
    }

    /// <summary>
    /// Updates how many images (fine, defect, total) that still needs to be generated.
    /// </summary>
    private void UpdateNumberOfImages(){
        if(picoFlexx.activeSelf == true){
            
            ///Fine
            numberOfImagesFine = dataGenerator.numberOfImagesFine;
            numberOfImagesFineInputField.text = numberOfImagesFine.ToString();
            
            ///Defects
            numberOfImagesDefect = dataGenerator.numberOfImagesDefect;
            numberOfImagesDefectInputField.text = numberOfImagesDefect.ToString();

            ///Total
            numberOfImagesTotal = numberOfImagesFine+ numberOfImagesDefect;
            numberOfImagesTotalText.text = numberOfImagesTotal.ToString();
        }
    }

    /// <summary>
    /// Before simulation begins, a desired number for images can be chosen. This function saves the numbers from the input fields (fine, defect) from the UI into integers. 
    /// Moreover, the total number of images is saved and sat for the total text in the UI.
    /// </summary>
    private void ChooseNumberOfImages(){
        if (picoFlexx.activeSelf == false) {
            numberOfImagesFine = ParseInputFieldToInt(numberOfImagesFineInputField);
            numberOfImagesDefect = ParseInputFieldToInt(numberOfImagesDefectInputField); 

            numberOfImagesTotal = (numberOfImagesFine + numberOfImagesDefect); 
            numberOfImagesTotalText.text = numberOfImagesTotal.ToString();
        }
    }

    /// <summary>
    /// Takes the text from the inputfield and tries to parse a number from string. 
    /// </summary>
    /// <param name="numberOfImagesInputField"></param>
    /// <returns>Int value of the string text from inputfield. </returns>
    private int ParseInputFieldToInt(InputField numberOfImagesInputField){
        int number;
        bool success = int.TryParse(numberOfImagesInputField.text, out number);
        if (success) {
            return number;
        }
        return 0;
    }

    /// <summary>
    /// Activates when the button "Generate" is pressed.
    /// Sets the desired number of images (fine, defect) in the DataGenerator(Manager), which is used for calculating how many images that should be generated.
    /// Moreover, this function activates the simulation and sets the max value for the progressbar.
    /// </summary>
    public void ButtonPressGenerate(){
        picoFlexx.SetActive(true);
        dataGenerator.SetNumberOfDesiredImages(numberOfImagesFine,numberOfImagesDefect);
        progressBar.maxValue = numberOfImagesTotal;
    }

    /// <summary>
    /// Activates when the button "Folder Icon" is pressed.
    /// Makes it possible for the user to select which folder to generate the data to. 
    /// If the chosen directory doesn't include two folders named "Fine" and "Defect", they will be created.
    /// The path from the chosen directory, will be saved in SceneSettings(Manager).
    /// </summary>
    public void ButtonSetDirectory(){
        #if UNITY_EDITOR
            EditorUtility.InstanceIDToObject(0);
            directory_path = EditorUtility.OpenFolderPanel("Set Directory",directory_path,"");
        #endif
        
        //sdirectory_path = EditorUtility.OpenFolderPanel("Set Directory",directory_path,"");
        Directory.CreateDirectory(directory_path+"/Fine");
        Directory.CreateDirectory(directory_path+"/Defect");
        sceneSettings.directory_path = directory_path;
        pathToDirectory.text = directory_path;
    }
}
