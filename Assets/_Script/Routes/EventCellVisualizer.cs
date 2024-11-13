using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCellVisualizer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Appear()
    {
        spriteRenderer.color = Color.blue;
    }

    public void Disappear()
    {
        spriteRenderer.color = Color.red;
    }
}
