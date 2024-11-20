using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoutesPopulator : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private float offset;
    [SerializeField] private float widthBound;
    [SerializeField] private float heightBound;
    [SerializeField] private Transform startingPointTransform;
    [SerializeField] private Transform endingPointTransform; 
    HashSet<Vector2> usedPositions = new HashSet<Vector2>();


    private List<(EventCell parent, EventCell child)> cellConnections = new List<(EventCell, EventCell)>();

    // TEST
    public EventCell testCell;

    [SerializeField] private Transform CellsParent;

    public EventCellTypeSO EventCellTypeSO;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach (Transform child in CellsParent)
            {
                Destroy(child.gameObject);
            }

            ActivateCells(testCell, 10);
            DrawConnections();
        }
    }

    private void ActivateCells(EventCell eventCell, int cellAmounts)
    {
        List<EventCell> eventCells = new();
        cellConnections.Clear(); // Clear previous connections
        usedPositions.Clear();


        // beginning cell
        EventCell startCell = CreateNewCell(eventCell, startingPointTransform.position, eventCells);
        startCell.EventCellVisualizer.Appear();
        startCell.gameObject.name = "Start";
        startCell.AssignCell();

        startCell.DefineData(EventCellType.Start, EventCellTypeSO.cellTypes[EventCellType.Start].sprite);

        Vector3 nextPos = new Vector3(startCell.Position.x + offset, 
            UnityEngine.Random.Range(-heightBound, heightBound));

        usedPositions.Add(nextPos);

        EventCell parent = startCell;
        for (int i = 0; i < cellAmounts - 2; i++)
        {
            EventCell newCell = CreateNewCell(eventCell, nextPos, eventCells);
            //newCell.EventCellVisualizer.Disappear();
            newCell.gameObject.name = $"cell number {i+1}";

            cellConnections.Add((parent, newCell));
            parent.DefineNextCell(newCell);

            newCell.DefineData(EventCellType.Empty, EventCellTypeSO.cellTypes[EventCellType.Empty].sprite);

            nextPos = new Vector3(newCell.Position.x + offset,
                UnityEngine.Random.Range(-heightBound, heightBound));
            
            while(usedPositions.Contains(nextPos) && nextPos.x <= widthBound)
            {
                nextPos.x = newCell.Position.x + offset;
                nextPos.y = UnityEngine.Random.Range(-heightBound, heightBound);
                
                if (nextPos.x > widthBound)
                {
                    nextPos.x = widthBound;
                }
            }

            parent = newCell;
        }

        EventCell endCell = CreateNewCell(eventCell, endingPointTransform.position, eventCells);
        endCell.EventCellVisualizer.Disappear();
        endCell.gameObject.name = "End";
        cellConnections.Add((parent, endCell));
        parent.DefineNextCell(endCell);

        endCell.DefineData(EventCellType.End, EventCellTypeSO.cellTypes[EventCellType.End].sprite);
        endCell.AssignCell();

        //CardsManager.Instance.GenerateCards();

        // temporary, just to put the player in the first tile
        CellManager.Instance.CellClicked(startCell);

        CellManager.Instance.AssignRouteCellsReference(eventCells);
        GameManager.Instance.ChangeGameState(GameState.RouteSelection);
    }

    private void DrawConnections()
    {
        foreach (var connection in cellConnections)
        {
            connection.parent.DrawLineToNextCells();
        }
    }

    private EventCell CreateNewCell(EventCell eventCell, Vector3 pos, List<EventCell> eventCells)
    {
        EventCell cell = Instantiate(eventCell, pos, Quaternion.identity);
        //cell.EventCellVisualizer.Disappear();
        cell.transform.SetParent(CellsParent);
        eventCells.Add(cell);
        usedPositions.Add(new Vector2(startingPointTransform.position.x, startingPointTransform.position.y));

        return cell;
    }
}



// ARCHIVE
/*
    #region mixnmatch

    private void Populate2(EventCell startingPoint, EventCell endingPoint, int potentialRoutes = 4, int totalNumberOfCells = 20)
    {
        // Clear everything
        eventCells.Clear();
        cellConnections.Clear(); // Clear previous connections
        usedPositions.Clear();

        EventCell startCell = CreateNewCell(startingPoint, startingPointTransform.position);
        Dictionary<int, Vector2> routesDict = new();

        //Tracking
        int currentX = -widthBound;
        int currentY = 0;

        // distribute cell counts to each route
        List<int> routesCellsCount = new List<int>();
        for(int i = 0; i < potentialRoutes; i++)
        {
            routesCellsCount.Add(0);
        }
        
        for(int i = 0; i < totalNumberOfCells-2; i++)
        {
            int route = UnityEngine.Random.Range(0, routesCellsCount.Count);

            routesCellsCount[route]++;
        }

        EventCell newCell;
        // make every route
        for(int route = 0; route < potentialRoutes; route++)
        {
            currentX = UnityEngine.Random.Range(-widthBound, -widthBound + offset);
            currentY = UnityEngine.Random.Range(-heightBound, -heightBound + offset);
            for (int i = 0; i < routesCellsCount[i]; i++)
            {
                //int x = 
                //newCell = CreateNewCell(startingPoint, startingPointTransform.position);

                currentX += UnityEngine.Random.Range(offset, offset + 1);
                if(currentX > widthBound)
                {
                    currentX = widthBound;
                }
            }
        }

    }



    // To store all connections between cells
    

    private void Populate(EventCell startingPoint, EventCell endingPoint, int potentialRoutes = 2, int totalNumberOfCells = 10)
    {
        // Clear previous data
        eventCells.Clear();
        cellConnections.Clear(); // Clear previous connections

        // To keep track of occupied positions
        usedPositions.Clear();

        // Track X progression
        int currentX = -widthBound;

        // Create a list to keep track of branchable cells
        List<EventCell> branchableCells = new List<EventCell>();

        // Instantiate starting point
        EventCell startCell = CreateNewCell(startingPoint, startingPointTransform.position);
        startCell.EventCellVisualizer.Appear();

        branchableCells.Add(startCell);

        EventCell previousCell = startCell;

        // Create the main path (this ensures no dead ends on the main path)
        for (int i = 0; i < totalNumberOfCells - 2; i++)
        {
            Vector2 nextPos;
            currentX++;

            if (currentX > widthBound)
                currentX = widthBound;

            // Generate a new position with a different Y value and within maxXStep distance for X
            do
            {
                int randomXStep = UnityEngine.Random.Range(1, maxXStep + 1); // Step forward by 1 or 2 in X
                int nextX = (int)previousCell.Position.x + randomXStep;

                if (nextX > widthBound)
                    nextX = widthBound;

                int randomY = UnityEngine.Random.Range(-heightBound, heightBound);
                nextPos = new Vector2(nextX, randomY);
            }
            while (usedPositions.Contains(nextPos) || Mathf.Approximately(nextPos.y, previousCell.Position.y)); // Ensure new Y is different

            //EventCell newCell = Instantiate(testCell, new Vector3(nextPos.x, nextPos.y, 0), Quaternion.identity);
            EventCell newCell = CreateNewCell(testCell, new Vector3(nextPos.x, nextPos.y, 0));
            newCell.gameObject.name = i.ToString();
            //newCell.transform.SetParent(CellsParent);

            EventTypeData newData = EventCellTypeSO.cellTypes[UnityEngine.Random.Range(0, EventCellTypeSO.cellTypes.Count)];
            newCell.DefineData(newData.type, newData.sprite);

            // Link the previous cell to the new one
            previousCell.DefineNextCell(newCell);

            // Record the connection
            cellConnections.Add((previousCell, newCell));

            // Add to the list of branchable cells for future branches
            branchableCells.Add(newCell);

            eventCells.Add(newCell);
            usedPositions.Add(nextPos);

            previousCell = newCell;
        }

        // Instantiate and connect the ending point
        EventCell endCell = CreateNewCell(endingPoint, endingPointTransform.position);
        endCell.EventCellVisualizer.Disappear();


        previousCell.DefineNextCell(endCell);
        cellConnections.Add((previousCell, endCell));

        // Now, create branches from previously created cells
        CreateBranches(branchableCells, usedPositions, endCell);

        CellManager.Instance.CellClicked(startCell);
    }

    // Create branches from the main path, ensuring they reconnect to cells within a reasonable X range or to the ending cell
    private void CreateBranches(List<EventCell> branchableCells, HashSet<Vector2> usedPositions, EventCell endingCell)
    {
        foreach (EventCell parentCell in branchableCells)
        {
            // Randomly decide if this cell should branch
            if (UnityEngine.Random.Range(0f, 1f) < 0.3f) // 30% chance of branching
            {
                Vector2 branchPos;

                // Generate the branch position ensuring X step is within maxXStep
                do
                {
                    int randomXStep = UnityEngine.Random.Range(1, maxXStep + 1); // Step forward by 1 or 2 in X
                    int branchX = (int)parentCell.Position.x + randomXStep;

                    if (branchX > widthBound)
                        branchX = widthBound;

                    int randomY = UnityEngine.Random.Range(-heightBound, heightBound);
                    branchPos = new Vector2(branchX, randomY);
                }
                while (usedPositions.Contains(branchPos) || Mathf.Approximately(branchPos.y, parentCell.Position.y)); // Ensure new Y is different

                // Instantiate the branch cell
                EventCell branchCell = CreateNewCell(testCell, new Vector3(branchPos.x, branchPos.y, 0));
                //branchCell.transform.SetParent(CellsParent);

                EventTypeData newData = EventCellTypeSO.cellTypes[UnityEngine.Random.Range(0, EventCellTypeSO.cellTypes.Count)];
                branchCell.DefineData(newData.type, newData.sprite);

                // Link the branch cell back to the parent
                parentCell.DefineNextCell(branchCell);
                cellConnections.Add((parentCell, branchCell));

                eventCells.Add(branchCell);
                usedPositions.Add(branchPos);

                // Ensure the branch reconnects to a cell ahead in the path or to the end
                ConnectBranchToMainPath(branchCell, branchableCells, endingCell);
            }
        }
    }

    // Helper method to connect branches to cells ahead in the main path or to the ending cell
    private void ConnectBranchToMainPath(EventCell branchCell, List<EventCell> branchableCells, EventCell endingCell)
    {
        // Filter only the cells that are ahead of the branchCell in the X axis
        List<EventCell> forwardCells = branchableCells.FindAll(cell => cell.Position.x > branchCell.Position.x);

        if (forwardCells.Count == 0)
        {
            // If no cells are ahead, connect to the ending cell
            branchCell.DefineNextCell(endingCell);
            cellConnections.Add((branchCell, endingCell));
        }
        else
        {
            // Randomly choose a forward cell that is within the next few levels (X-wise)
            EventCell reconnectCell = forwardCells[UnityEngine.Random.Range(0, forwardCells.Count)];
            branchCell.DefineNextCell(reconnectCell);
            cellConnections.Add((branchCell, reconnectCell));
        }
    }
    */