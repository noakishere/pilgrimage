using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EmptyEvent", menuName = "ScriptableObjects/Events/Empty", order = 1)]
public class EmptyEventSO : BaseEventSO
{
    public override void TriggerEvent(EventContext context)
    {

    }
}
