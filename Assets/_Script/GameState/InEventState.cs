using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class InEventState : GameStateBase
{
    private BaseEventSO eventData;
    private EventContext context;

    public InEventState()
    {
    }

    public InEventState(BaseEventSO eventData, EventContext context)
    {
        this.eventData = eventData;
        this.context = context;
    }

    public override void EnterState(GameManager gameManager)
    {
        eventData.TriggerEvent(context);
        //EventsUI.Instance.NewEventProcess(gameManager.PlayerPosition.CurrentCell);
    }

    public override void ExitState(GameManager state)
    {

    }

    public override void UpdateState(GameManager state)
    {

    }
}
