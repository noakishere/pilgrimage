using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    [SerializeField] private EventCell currentCell;
    // Start is called before the first frame update

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
        currentCell = newCell;
        transform.position = currentCell.Position;
    }
}
