using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Window_Graph : MonoBehaviour
{
    LaserSensor LS;
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
        rectTransform.anchoredPosition = Position;
        rectTransform.sizeDelta = new Vector2(11,11);
        rectTransform.anchorMin = new Vector2(0,0);
        rectTransform.anchorMax = new Vector2(0,0);

    }
    // Start is called before the first frame update
    void Start()
    {
        LS = GetComponent<LaserSensor>();
    }

    // Update is called once per frame
    void Update()
    {
        CreatePoint((LS.arrayOfHitPositions[0]));
    }
}
