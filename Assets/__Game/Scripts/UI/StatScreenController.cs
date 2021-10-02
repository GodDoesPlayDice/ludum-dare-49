using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatScreenController : MonoBehaviour
{

    public Text textOilCount;
    public Text textWoodCount;
    public Text textGoldCount;
    public Text textAmount;
    public Text textNews;

    void Start()
    {
        textOilCount.text = "123";
        textWoodCount.text = "123";
        textGoldCount.text = "123";
        textAmount.text = "123 $";
        //textNews.text = "News! News! News!\nNews! News! News!\nNews! News! News!";
    }

    void Update()
    {
        
    }
}
