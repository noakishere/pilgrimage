using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventTypes", menuName = "ScriptableObjects/EventTypes", order = 1)]
public class EventCellTypeSO : ScriptableObject
{
    public List<EventTypeData> cellTypes = new List<EventTypeData>();
}

[Serializable]
public class EventTypeData
{
    public EventCellType type;
    public Sprite sprite;
}
