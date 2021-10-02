using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text textTitle;
    public Text textNews;

    void Start() {
        textNews.text = "NEWS! NEWS NEWS!\nNEWS! NEWS NEWS!\nNEWS! NEWS NEWS!\n";
        textTitle.text = "Oil";
    }

    public void OnBuyButtonClick()
    {

    }

    public void OnSellButtonClick()
    {

    }
}
