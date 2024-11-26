using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EndEvent", menuName = "ScriptableObjects/Events/End", order = 1)]
public class EndEventSO : BaseEventSO
{
    public GameObject targetCanvas;

    public override void TriggerEvent(EventContext context)
    {
        EventsUI.Instance.NewEventProcess(this);
    }
}
