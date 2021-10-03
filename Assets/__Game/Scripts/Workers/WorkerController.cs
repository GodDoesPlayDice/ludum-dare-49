using UnityEngine;

public enum WorkerState
{
    selling,
    buying
}

public class WorkerController : MonoBehaviour, ActionSource
{
    public ResourceType resourceType;
    public GameEventWithParam<WorkerOperationEP> operationEvent;
    public int resourcesBuyingMultiplier = 1;
    public int resourcesSellingMultiplier = 1;

    private WorkerState state = WorkerState.buying;

    /*
    private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    */

    public void OnTick(WorkerTickEP workerTickEP)
    {
        operationEvent.Raise(new WorkerOperationEP(resourceType, state == WorkerState.buying ? 1 * resourcesBuyingMultiplier : -1 * resourcesSellingMultiplier));
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

        Debug.Log(resourceType + " set to " + state);
    }

    public void ExecuteAction()
    {
        var newState = state == WorkerState.selling ? WorkerState.buying : WorkerState.selling;
        ToggleWorkerState(newState);
    }
}
