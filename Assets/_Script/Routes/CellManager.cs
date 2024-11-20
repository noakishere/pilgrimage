using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : SingletonMonoBehaviour<CellManager>
{
    [SerializeField] private List<EventCell> cellEvents = new List<EventCell>();
    public List<EventCell> CellEvents { get { return cellEvents; } }

    public event Action<EventCell> OnCellClicked;

    private void OnEnable()
    {
        OnCellClicked += ShowNextCells;
    }

    private void OnDisable()
    {
        OnCellClicked -= ShowNextCells;
    }

    public void CellClicked(EventCell cell)
    {
        OnCellClicked?.Invoke(cell);

        ShowNextCells(cell);
    }

    public void AddCell(EventCell eventCell)
    {
        cellEvents.Add(eventCell);
    }

    private void ShowNextCells(EventCell eventCell)
    {
        foreach(EventCell cell in eventCell.NextCells)
        {
            cell.EventCellVisualizer.Appear();
        }
    }

    public void AssignRouteCellsReference(List<EventCell> routeCells)
    {
        this.cellEvents.Clear();
        this.cellEvents = routeCells;
    }

    public void EventCellStateCheck()
    {
        bool allAssigned = true;
        foreach (EventCell rc in cellEvents)
        {
            if (rc.CurrentEventCellType == EventCellType.Empty)
            {
                allAssigned = false;
            }
        }

        if (allAssigned)
        {
            CardsManager.Instance.EndSelection();
            GameManager.Instance.ChangeGameState(GameState.Navigation);
        }
    }
}
