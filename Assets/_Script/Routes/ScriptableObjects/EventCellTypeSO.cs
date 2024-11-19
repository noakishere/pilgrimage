using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventTypes", menuName = "ScriptableObjects/EventTypes", order = 1)]
public class EventCellTypeSO : ScriptableObject
{
    [SerializedDictionary("Event Cell Type", "Sprite")]
    public SerializedDictionary<EventCellType, EventTypeData> cellTypes;
}

[Serializable]
public class EventTypeData
{
    public EventCellType type;
    public Sprite sprite;
}
