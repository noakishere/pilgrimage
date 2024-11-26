using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatEvent", menuName = "ScriptableObjects/Events/Combat", order = 1)]
public class CombatEventSO : BaseEventSO
{
    public int strength;
    public GameObject targetCanvas;

    public override void TriggerEvent(EventContext context)
    {
        EventsUI.Instance.NewEventProcess(this);
    }
}
