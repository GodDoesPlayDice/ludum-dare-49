using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerOperationEP : IGameEventParam
{
    public ResourceType type { get; private set; }
    public int resourceCount { get; private set; }

    public WorkerOperationEP(ResourceType type, int resourceCount)
    {
        this.type = type;
        this.resourceCount = resourceCount;
    }
}
