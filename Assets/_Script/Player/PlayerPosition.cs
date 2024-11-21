using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    [SerializeField] private EventCell currentCell;
    public EventCell CurrentCell { get { return currentCell; } }

    [SerializeField] private bool canMove = true;
    public bool CanMove { get { return canMove; } }

    private void OnEnable()
    {
        CellManager.Instance.OnCellClicked += UpdatePlayerPosition;
        GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        CellManager.Instance.OnCellClicked -= UpdatePlayerPosition;
        GameManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
    }

    public void UpdatePlayerPosition(EventCell newCell)
    {
        if(canMove)
        {
            if(currentCell != null)
            {
                currentCell.CellVisited();
            }

            if(currentCell.NextCells.Contains(newCell))
            {
                currentCell = newCell;
                transform.position = currentCell.Position;
                CellManager.Instance.PlayerMoved(currentCell);

                GameManager.Instance.ChangeGameState(new InEventState());
            }
        }
    }

    public void InitialPlayerPosition(EventCell newCell)
    {
        currentCell = newCell;
        transform.position = currentCell.Position;
    }

    private void HandleGameStateChanged(GameStateBase newState)
    {
        if(newState is NavigationState)
        {
            PlayerCanMove();
        }
        else
        {
            PlayerCantMove();
        }
    }

    public void PlayerCanMove()
    {
        canMove = true;
    }
    
    public void PlayerCantMove()
    {
        canMove = false;
    }
}
