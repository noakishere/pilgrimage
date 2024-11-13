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
}
