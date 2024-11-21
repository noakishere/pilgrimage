using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameStateBase
{
    public abstract void EnterState(GameManager state);
    public abstract void UpdateState(GameManager state); //used for update functions
    public abstract void ExitState(GameManager state);
}
