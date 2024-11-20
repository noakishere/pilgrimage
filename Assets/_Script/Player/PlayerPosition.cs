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
    }

    private void OnDisable()
    {
        CellManager.Instance.OnCellClicked -= UpdatePlayerPosition;
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

                GameManager.Instance.ChangeGameState(GameState.InEvent);
            }
        }
    }

    private void ProcessCell(EventCell currentCell)
    {
        throw new NotImplementedException();
    }

    public void InitialPlayerPosition(EventCell newCell)
    {
        currentCell = newCell;
        transform.position = currentCell.Position;
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
