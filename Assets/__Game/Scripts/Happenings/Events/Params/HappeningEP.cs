using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappeningEP : IGameEventParam
{
    public HappeningType type { get; private set;}
    public HappeningEntity happening { get; private set; }

}
