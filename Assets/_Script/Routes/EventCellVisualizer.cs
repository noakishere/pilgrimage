using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCellVisualizer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Material fogMaterial;
    [SerializeField] private Material defaultMaterial;

    private void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
    }

    public void Appear(Color cellColor)
    {
        //spriteRenderer.material = defaultMaterial;
        spriteRenderer.color = cellColor;
    }

    public void Disappear()
    {
        //spriteRenderer.material = fogMaterial;
        //fogMaterial.SetTexture("_MainTexture", spriteRenderer.sprite.texture);
        spriteRenderer.color = Color.red;
    }
}
