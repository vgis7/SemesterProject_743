using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Window_Graph : MonoBehaviour
{
    public GameObject LaserSensorGameObj;
    
    private RectTransform graphContainer;
    [SerializeField] private Sprite circleSprite;

    private void Awake() {
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
    }

    private void CreatePoint(Vector2 Position){
        GameObject gameObject = new GameObject("point", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2 (Position.x*100,Position.y*100);
        rectTransform.sizeDelta = new Vector2(10,10);
        //rectTransform.anchorMin = new Vector2(0,0);
        //rectTransform.anchorMax = new Vector2(0,0);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //print("LS: " + LS.arrayOfHitPositions[0]);
        //LaserSensorGameObj.GetComponent<LaserSensor>().arrayOfHitPositions[0];
        CreatePoint(LaserSensorGameObj.GetComponent<LaserSensor>().arrayOfHitPositions[0]);

        if(transform.Find("GraphContainer").childCount > 150){
            GameObject tempGameObject = transform.Find("GraphContainer").GetChild(1).gameObject;
            Destroy(tempGameObject); 
            //Destroy(transform.Find("GraphContainer").GetChild(1));
            //print(transform.Find("GraphContainer").childCount);
        }
    }
}
