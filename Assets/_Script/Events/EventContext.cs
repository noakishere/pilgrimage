using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EventContext
{
    public Vector3 tilePosition;
    public PlayerStats playerStats;
    public GameManager gameManager;

    public EventContext(Vector3 tilePosition, PlayerStats playerStats, GameManager gameManager)
    {
        this.tilePosition = tilePosition;
        this.playerStats = playerStats;
        this.gameManager = gameManager;
    }
}