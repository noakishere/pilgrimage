using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : SingletonMonoBehaviour<CellManager>
{
    [SerializeField] private List<EventCell> cellEvents = new List<EventCell>();
    public List<EventCell> CellEvents { get { return cellEvents; } }

    public event Action<EventCell> OnCellClicked;

    public event Action<EventCell> OnPlayerMove;

    private void OnEnable()
    {
        OnPlayerMove += ShowNextCells;
        OnPlayerMove += ShowPreviousCells;
    }

    private void OnDisable()
    {
        OnPlayerMove -= ShowNextCells;
        OnPlayerMove -= ShowPreviousCells;
    }

    public void CellClicked(EventCell cell)
    {
        OnCellClicked?.Invoke(cell);

        //ShowNextCells(cell);
    }

    public void PlayerMoved(EventCell cell)
    {
        OnPlayerMove?.Invoke(cell);
    }

    public void AddCell(EventCell eventCell)
    {
        cellEvents.Add(eventCell);
    }

    public void ShowNextCells(EventCell eventCell)
    {
        foreach(EventCell cell in eventCell.NextCells)
        {
            cell.EventCellVisualizer.Appear(Color.blue);
        }
    }

    public void ShowPreviousCells(EventCell eventCell)
    {
        foreach(EventCell ec in cellEvents)
        {
            if(ec.HasBeenVisited && ec != eventCell)
            {
                ec.EventCellVisualizer.Appear(Color.gray);
            }
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
            //GameManager.Instance.ChangeGameState(GameState.Navigation);
            GameManager.Instance.ChangeGameState(new NavigationState());

        }
    }
}
