using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum WorkerState
{
    selling,
    buying
}

public class WorkerController : MonoBehaviour
{
    public ResourceType resourceType;
    public GameEventWithParam<WorkerOperationEP> operationEvent;


    private WorkerState state = WorkerState.buying;


    public void OnTick(WorkerTickEP workerTickEP)
    {
        operationEvent.Raise(new WorkerOperationEP(resourceType, state == WorkerState.buying ? 1 : -1));
    }

    public void ToggleWorkerState(WorkerState newState)
    {
        if (state == newState) return;
        if (state == WorkerState.selling)
        {
            state = WorkerState.buying;
        }
        else if (state == WorkerState.buying)
        {
            state = WorkerState.selling;
        }
    }
}
