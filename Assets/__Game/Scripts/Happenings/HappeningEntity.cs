using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Happenings/Happening", order = 51)]
public class HappeningEntity : ScriptableObject
{
    [SerializeField]
    public CrazyHappeningType crazyType;

    [SerializeField]
    public ResourceType[] resourceTypes;

    [SerializeField]
    public string newsString;

    [SerializeField]
    public float stockPercent;

}
