using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceChangedEP : IGameEventParam
{
    public float value { get; private set; }

    public ResourceChangedEP(float value)
    {
        this.value = value;
    }
}
