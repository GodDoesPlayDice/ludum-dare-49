using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxesEP : IGameEventParam
{
    public int amount { get; private set; }
    public int currentMoney { get; private set; }

    public TaxesEP(int amount, int currentMoney)
    {
        this.amount = amount;
        this.currentMoney = currentMoney;
    }
}
