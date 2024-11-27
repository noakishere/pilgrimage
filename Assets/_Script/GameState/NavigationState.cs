using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationState : GameStateBase
{
    public override void EnterState(GameManager state)
    {
        Debug.Log("Entering Navigation State");
    }

    public override void ExitState(GameManager state)
    {
        Debug.Log("Exiting Navigation State");
    }

    public override void UpdateState(GameManager state)
    {

    }
}
