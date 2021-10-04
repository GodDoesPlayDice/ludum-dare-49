using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxesEmitter : MonoBehaviour
{
    [SerializeField]
    private float deltaTime;
    [SerializeField]
    private int taxesCount;
    [SerializeField]
    private PortfolioController portfolio;

    [SerializeField]
    private TaxesEvent taxesEvent;

    private float prevTime;


    void Update()
    {
        if (prevTime + deltaTime < Time.time)
        {
            portfolio.RemoveTaxesMoney(taxesCount);
            taxesEvent.Raise(new TaxesEP(taxesCount, portfolio.GetCurrentMoney()));
            prevTime = Time.time;
        }
    }
}
