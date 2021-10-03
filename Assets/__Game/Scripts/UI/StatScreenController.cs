using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatScreenController : MonoBehaviour
{

    public TextMeshProUGUI textGoldCount;
    public TextMeshProUGUI textWoodCount;
    public TextMeshProUGUI textOilCount;
    public TextMeshProUGUI textAmount;

    public TextMeshProUGUI textGoldCurrent;
    public TextMeshProUGUI textWoodCurrent;
    public TextMeshProUGUI textOilCurrent;

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
