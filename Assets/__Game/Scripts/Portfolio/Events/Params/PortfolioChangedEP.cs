using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortfolioChangedEP : IGameEventParam
{

    public ResourceType type { get; private set; }
    public bool success { get; private set; }
    public int resourceDelta { get; private set; }
    public int moneyDelta { get; private set; }
    public int currentResource { get; private set; }
    public int currentMoney { get; private set; }

    public PortfolioChangedEP(ResourceType type, bool success, int resourceDelta, int moneyDelta, int currentResource, int currentMoney)
    {
        this.type = type;
        this.success = success;
        this.resourceDelta = resourceDelta;
        this.moneyDelta = moneyDelta;
        this.currentResource = currentResource;
        this.currentMoney = currentMoney;
    }
}
