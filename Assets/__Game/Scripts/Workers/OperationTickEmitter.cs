using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationTickEmitter : MonoBehaviour
{
    [SerializeField]
    private GameEventWithParam<WorkerTickEP> tickEvent;

    [SerializeField]
    private float tickDeltaTime = 1f;

    private float prevTickTime;
    
    void Update()
    {
        if (prevTickTime + tickDeltaTime < Time.time)
        {
            tickEvent.Raise(new WorkerTickEP());
            prevTickTime = Time.time;
        }
    }
}
