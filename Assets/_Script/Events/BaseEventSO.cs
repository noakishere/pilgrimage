using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "BaseEvent", menuName = "ScriptableObjects/EventTypes", order = 1)]
public abstract class BaseEventSO : ScriptableObject
{
    public Sprite tileSprite; // Sprite representing the event
    public EventCellType eventType;  // Name of the event
    public string description; // Optional: Event description for UI

    // Abstract method for event behavior
    public abstract void TriggerEvent(EventContext context);
}
