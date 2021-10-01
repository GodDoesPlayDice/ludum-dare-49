using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface Loadable
{
    public int loadCost { get; }

    public void Load(Action onLoad);
}
