using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHolder : MonoBehaviour
{
    [SerializeField]
    private ResourceType type;

    [SerializeField]
    private int value;

    public void Add(int delta)
    {
        value += delta;
    }

    public int Remove(int delta)
    {
        var removed = HowManyCanRemove(delta);
        value -= removed;
        return removed;
    }

    public int HowManyCanRemove(int delta)
    {
        var result = Mathf.Min(value, delta);
        return result;
    }

    public ResourceType GetResourceType()
    {
        return type;
    }
}
