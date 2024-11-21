using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteSelectionState : GameStateBase
{
    public override void EnterState(GameManager state)
    {
        CardsManager.Instance.BeginSelection();
    }

    public override void ExitState(GameManager state)
    {
        
    }

    public override void UpdateState(GameManager state)
    {
        
    }
}
