using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UpDownActionSource
{
    public Transform transform { get; }
    public void ExecuteAction(PlayerActionType type);
    public void IsCloseToPlayer(bool isClose);
}
