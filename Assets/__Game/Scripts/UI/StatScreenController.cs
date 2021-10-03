using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatScreenController : MonoBehaviour
{

    public Text textGoldCount;
    public Text textWoodCount;
    public Text textOilCount;
    public Text textAmount;

    public Text textGoldCurrent;
    public Text textWoodCurrent;
    public Text textOilCurrent;

    //public Text textNews;

    void Start()
    {
        textGoldCount.text = "123";
        textWoodCount.text = "123";
        textOilCount.text = "123";

        textAmount.text = "123 $";
        //textNews.text = "News! News! News!\nNews! News! News!\nNews! News! News!";

        textGoldCurrent.text = "123";
        textWoodCurrent.text = "123";
        textOilCurrent.text = "123";
    }

    void Update()
    {
        
    }
}
