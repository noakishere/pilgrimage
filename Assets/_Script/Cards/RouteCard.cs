using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteCard : Card
{
    [SerializeField] private EventCellType eventType;
    public EventCellType EventType { get { return eventType; } }

    public void AssignTypeToCard(EventCellType eventType)
    {
        this.eventType = eventType;
    }
}