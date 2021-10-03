using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappeningEP : IGameEventParam
{
    public HappeningType type { get; private set;}
    public HappeningEntity happening { get; private set; }
    public bool isOn { get; private set; }

    public HappeningEP(HappeningType type, HappeningEntity happening, bool isOn)
    {
        this.type = type;
        this.happening = happening;
        this.isOn = isOn;
    }
}
