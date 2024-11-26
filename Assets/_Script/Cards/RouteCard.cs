using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteCard : Card
{
    [SerializeField] private EventCellType eventType;
    public EventCellType EventType { get { return eventType; } }

    [SerializeField] private Sprite cardSprite;

    public Sprite CardSprite { get { return cardSprite; } }

    [SerializeField] private BaseEventSO eventDetails;
    public BaseEventSO EventDetails { get { return eventDetails; } }

    public void AssignTypeToCard(EventCellType eventType)
    {
        this.eventType = eventType;
    }

    public void AssignSprite(Sprite sprite)
    {
        cardSprite = sprite;
    }

    public void AssignEventDetails(BaseEventSO eventDetails)
    {
        this.eventDetails = eventDetails;
    }
}
