using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCell : MonoBehaviour
{
    [SerializeField] private EventCellType eventCellType;
    [SerializeField] private bool hasBeenAssigned = false;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private List<EventCell> nextCells;
    public List<EventCell> NextCells { get { return nextCells; } }

    // Expose position for easier access
    public Vector3 Position { get { return transform.position; } }

    [SerializeField] private LineRenderer lineRendererPrefab;

    [SerializeField] private EventCellVisualizer eventCellVisualizer;
    public EventCellVisualizer EventCellVisualizer { get { return eventCellVisualizer; } }

    private void OnEnable()
    {

    }

    private void OnMouseDown()
    {
        //CellManager.Instance.CellClicked(this);
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DrawLineToNextCells()
    {
        // Clear previous LineRenderers (in case we're redrawing or updating)
        //ClearPreviousLineRenderers();

        // Draw one line per next cell
        foreach (EventCell nextCell in nextCells)
        {
            // Create a new child GameObject to hold the LineRenderer
            GameObject lineObject = new GameObject("LineToNextCell");
            lineObject.transform.SetParent(transform); // Set the current cell as parent
            lineObject.transform.position = Position;  // Set the position to the current cell's position

            // Add a LineRenderer component to the child GameObject
            LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.black;

            // Set line appearance
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));  // A basic material for the line

            lineRenderer.textureScale = new Vector2(0.01f, 0.01f);

            lineRenderer.numCornerVertices = 10;
            lineRenderer.numCapVertices = 10;
            lineRenderer.sortingOrder = -1;

            // Make sure the line renderer only has 2 positions (start and end)
            lineRenderer.positionCount = 2;

            // Set the start and end points of the line
            lineRenderer.SetPosition(0, Position);  // Start at the current cell's position
            lineRenderer.SetPosition(1, nextCell.Position);  // End at the next cell's position

            // Optionally: you can set line color dynamically if needed
            // lineRenderer.startColor = Color.white;
            // lineRenderer.endColor = Color.white;
        }
    }

    private void OnMouseEnter()
    {
        Debug.Log($"heyy from {gameObject.name}");
    }

    private void OnMouseUp()
    {
        if(!hasBeenAssigned)
        {
            if(CardsManager.Instance.SelectedRouteCard != null)
            {
                RouteCard routeCard = CardsManager.Instance.SelectedRouteCard.gameObject.GetComponent<RouteCard>();
                EventCellType assignedType = routeCard.EventType;
                eventCellType = assignedType;

                spriteRenderer.sprite = routeCard.CardSprite;
                hasBeenAssigned = true;

                Destroy(routeCard.gameObject, 0.1f);
            }
        }
    }

    public void DefineNextCell(List<EventCell> nextEventCell)
    {
        foreach(EventCell cell in nextEventCell)
        {
            nextCells.Add(cell);
        }
    }

    public void DefineNextCell(EventCell nextEventCell)
    {
        nextCells.Add(nextEventCell);
    }


    public void DefineData(EventCellType eventCellType, Sprite sprite)
    {
        this.eventCellType = eventCellType;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }
}

