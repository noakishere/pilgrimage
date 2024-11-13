using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoutesPopulator : MonoBehaviour
{
    [SerializeField] private List<EventCell> eventCells;

    [SerializeField] private Transform startingPointTransform;
    [SerializeField] private Transform endingPointTransform;

    const int widthBound = 6;
    const int heightBound = 4;

    // TEST
    public EventCell testCell;

    [SerializeField] private List<List<Vector2>> grids;

    [SerializeField] private Transform CellsParent;

    public EventCellTypeSO EventCellTypeSO;


    // Start is called before the first frame update
    void Start()
    {
        grids = new();

        for (int x = -widthBound; x < widthBound; x++)
        {
            List<Vector2> newList = new List<Vector2>();
            for (int y = -heightBound; y < heightBound; y++)
            {
                newList.Add(new Vector2(x, y));
            }
            grids.Add(newList);
        }

        int index = 0;
        foreach (List<Vector2> list in grids)
        {
            Debug.Log($"Level {index}");
            foreach (Vector2 pos in list)
            {
                Debug.Log($"position {pos}");
            }
            index++;
        }

        //foreach(Vector2 pos in grids)
        //{
        //    Instantiate(testCell, pos, Quaternion.identity);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach (Transform child in CellsParent)
            {
                Destroy(child.gameObject);
            }
            Populate(testCell, testCell);
            DrawConnections();
        }
    }
    // To store all connections between cells
    private List<(EventCell parent, EventCell child)> cellConnections = new List<(EventCell, EventCell)>();

    private const int maxXStep = 2; // Maximum X-step between consecutive cells

    private void Populate(EventCell startingPoint, EventCell endingPoint, int potentialRoutes = 2, int totalNumberOfCells = 10)
    {
        // Clear previous data
        eventCells.Clear();
        cellConnections.Clear(); // Clear previous connections

        // To keep track of occupied positions
        HashSet<Vector2> usedPositions = new HashSet<Vector2>();

        // Track X progression
        int currentX = -widthBound;

        // Create a list to keep track of branchable cells
        List<EventCell> branchableCells = new List<EventCell>();

        // Instantiate starting point
        EventCell pointA = Instantiate(startingPoint, startingPointTransform.position, Quaternion.identity);
        pointA.transform.SetParent(CellsParent);
        eventCells.Add(pointA);
        usedPositions.Add(new Vector2(startingPointTransform.position.x, startingPointTransform.position.y));

        branchableCells.Add(pointA);

        EventCell previousCell = pointA;

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

            EventCell newCell = Instantiate(testCell, new Vector3(nextPos.x, nextPos.y, 0), Quaternion.identity);
            newCell.gameObject.name = i.ToString();
            newCell.transform.SetParent(CellsParent);

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
        EventCell pointB = Instantiate(endingPoint, endingPointTransform.position, Quaternion.identity);
        eventCells.Add(pointB);
        pointB.transform.SetParent(CellsParent);

        usedPositions.Add(new Vector2(endingPointTransform.position.x, endingPointTransform.position.y));
        previousCell.DefineNextCell(pointB);
        cellConnections.Add((previousCell, pointB));

        // Now, create branches from previously created cells
        CreateBranches(branchableCells, usedPositions, pointB);
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
                EventCell branchCell = Instantiate(testCell, new Vector3(branchPos.x, branchPos.y, 0), Quaternion.identity);
                branchCell.transform.SetParent(CellsParent);

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

    private void DrawConnections()
    {
        foreach (var connection in cellConnections)
        {
            connection.parent.DrawLineToNextCells();
        }
    }
}
