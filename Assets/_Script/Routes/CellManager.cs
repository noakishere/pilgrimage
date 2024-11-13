using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    [SerializeField] private List<EventCell> cellEvents = new List<EventCell>();
    public List<EventCell> CellEvents { get { return cellEvents; } }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCell(EventCell eventCell)
    {
        cellEvents.Add(eventCell);
    }
}
