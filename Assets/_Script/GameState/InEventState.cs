using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InEventState : GameStateBase
{
    public override void EnterState(GameManager gameManager)
    {
        EventsUI.Instance.NewEventProcess(gameManager.PlayerPosition.CurrentCell);
    }

    public override void ExitState(GameManager state)
    {

    }

    public override void UpdateState(GameManager state)
    {

    }
}
