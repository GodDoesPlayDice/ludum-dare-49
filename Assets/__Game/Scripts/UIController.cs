using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Type type;

    public Text textTitle;
    public Text textCost;
    public Image imageResource;
    public Sprite spriteOil;
    public Sprite spriteWood;
    public Sprite spriteGold;

    void Start()
    {
        switch (type)
        {
            case Type.Oil:
                textTitle.text = "Oil";
                textCost.text = "300 $";
                imageResource.sprite = spriteOil;
                break;
            case Type.Wood:
                textTitle.text = "Wood";
                textCost.text = "200 $";
                imageResource.sprite = spriteWood;
                break;
            case Type.Gold:
                textTitle.text = "Oil";
                textCost.text = "400 $";
                imageResource.sprite = spriteGold;
                break;
        }
    }

    public void OnBuyButtonClick()
    {

    }

    public void OnSellButtonClick()
    {

    }

    public enum Type
    {
        Oil,
        Wood,
        Gold
    }
}
