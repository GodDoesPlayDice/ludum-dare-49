using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ActionSource
{
    public Transform transform { get; }
    public void ExecuteAction();
}
