using UnityEngine;

public enum WorkerState
{
    selling,
    buying
}

public class WorkerController : MonoBehaviour, UpDownActionSource
{
    public ResourceType resourceType;
    public GameEventWithParam<WorkerOperationEP> operationEvent;
    public int resourcesBuyingMultiplier = 1;
    public int resourcesSellingMultiplier = 1;

    //private WorkerState state = WorkerState.buying;
    private AudioSource buySellAudioSource;
    /*
    private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    */
    private void Start()
    {
        buySellAudioSource = GameObject.FindGameObjectWithTag("SellAndBuyAudio").GetComponent<AudioSource>();
    }

    public void OnTick(WorkerTickEP workerTickEP)
    {
        //operationEvent.Raise(new WorkerOperationEP(resourceType, state == WorkerState.buying ? 1 * resourcesBuyingMultiplier : -1 * resourcesSellingMultiplier));
    }

    /*
    public void ToggleWorkerState(WorkerState newState)
    {
        if (state == newState) return;

        //audio
        if (buySellAudioSource != null)
        {
            buySellAudioSource.Play();
        }
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
    */

    public void ExecuteAction(PlayerActionType type)
    {
        if (type == PlayerActionType.UP) {
            operationEvent.Raise(new WorkerOperationEP(resourceType, resourcesBuyingMultiplier));
        } else
        {
            operationEvent.Raise(new WorkerOperationEP(resourceType, resourcesSellingMultiplier * -1));
        }
        buySellAudioSource.Play();
    }
}
