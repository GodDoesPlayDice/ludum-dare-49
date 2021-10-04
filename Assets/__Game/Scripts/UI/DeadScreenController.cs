using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeadScreenController : MonoBehaviour
{
    [SerializeField]
    private LoadSceneEvent loadSceneEvent;
    [SerializeField]
    private Text titleEl;
    [SerializeField]
    private Text descriptionEl;
    [SerializeField]
    private Text goldEl;
    [SerializeField]
    private Text woodEl;
    [SerializeField]
    private Text oilEl;
    [SerializeField]
    private Text moneyEl;

    [SerializeField]
    private PortfolioController portfolio;

    private Action continueCallback;

    public void OnNewGameClick() 
    {
        //loadSceneEvent.Raise(new LoadSceneEP(SceneEnum.GAME, SceneEnum.GAME, true, active: true));
        SceneManager.LoadScene((int) SceneEnum.GAME);
    }

    public void OnContinueClick()
    {
        if (continueCallback != null)
        {
            continueCallback();
        }
    }

    public void SetContuinueCallback(Action continueCallback)
    {
        this.continueCallback = continueCallback;
    }

    public void SetDeadScreenData(bool won, string title, string description)
    {
        titleEl.text = title;
        descriptionEl.text = description;
        FillGoodsValues();
    }

    private void FillGoodsValues()
    {
        goldEl.text = portfolio.GetCurrentResource(ResourceType.GOLD).ToString();
        woodEl.text = portfolio.GetCurrentResource(ResourceType.WOOD).ToString();
        oilEl.text = portfolio.GetCurrentResource(ResourceType.OIL).ToString();
        moneyEl.text = portfolio.GetCurrentMoney().ToString();
    }
}
