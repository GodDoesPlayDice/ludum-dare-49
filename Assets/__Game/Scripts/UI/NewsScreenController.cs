using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsScreenController : MonoBehaviour
{

    public NewsTextController newsTextController;
    [Range(1f, 10f)]
    public float itemDuration = 3.0f;
    public string[] filterItems;

    float width;
    float pixelsPerSeconds;
    NewsTextController currentTextController;
    // Start is called before the first frame update
    void Start()
    {
        width = GetComponent<RectTransform>().rect.width;
        pixelsPerSeconds = width / itemDuration;
        AddNewsTextController(filterItems[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTextController.GetXPosition <= -currentTextController.GetWidth) 
        {
            AddNewsTextController(filterItems[Random.Range(0, filterItems.Length)]);
        }
    }

    void AddNewsTextController(string message) 
    {
        currentTextController = Instantiate(newsTextController, transform);
        currentTextController.Initialize(width, pixelsPerSeconds, message);
    }
}
