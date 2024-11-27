using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StartEvent", menuName = "ScriptableObjects/Events/Start", order = 1)]
public class StartEventSO : BaseEventSO
{
    public GameObject TargetCanvas;
    public override void TriggerEvent(EventContext context)
    {
        EventsUI.Instance.NewEventProcess(this);
    }

}
