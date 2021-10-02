using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlotValuesHolder : MonoBehaviour
{
    public List<float> values { get; private set; } = new List<float>();

    private int valuesCount;
    
    public void SetValuesCount(int valuesCount)
    {
        this.valuesCount = valuesCount;
    }

    public void Add(float value)
    {
        values.Add(value);
        if (values.Count > valuesCount)
        {
            values.RemoveAt(0);
        }
    }

    public List<float> GetValues()
    {
        return values;
    }

    public float GetMaxValue()
    {
        return values.Max();
    }

    public float GetMinValue()
    {
        return values.Min();
    }
}
