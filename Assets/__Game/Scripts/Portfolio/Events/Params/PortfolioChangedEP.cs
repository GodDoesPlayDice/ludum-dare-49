using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortfolioChangedEP : IGameEventParam
{

    public ResourceType type { get; private set; }
    public bool success { get; private set; }
    public int resourceDelta { get; private set; }
    public int moneyDelta { get; private set; }

    public PortfolioChangedEP(ResourceType type, bool success, int resourceDelta, int moneyDelta)
    {
        this.type = type;
        this.success = success;
        this.resourceDelta = resourceDelta;
        this.moneyDelta = moneyDelta;
    }
}
