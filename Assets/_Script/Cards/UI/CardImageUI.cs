using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using PrimeTween;
using TMPro;

public class CardImageUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 initialPosition; // To track the starting position
    private bool isClicked = false; // State to track if the card is clicked


    [Header("Animation params")]
    [SerializeField] private float moveDuration;
    [SerializeField] private float upValue;
    [SerializeField] private float downValue;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI titleText;

    private void Start()
    {
        initialPosition = transform.position; // Save the initial position of the card
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CardsManager.Instance.canSelectRouteCard)
        {
            isClicked = !isClicked; // Set the card as clicked

            if (isClicked)
            {
                CardsManager.Instance.SelectRouteCard(this);
                Tween.PositionY(transform, endValue: upValue, duration: moveDuration, ease: Ease.InOutSine);
            }
        }
        else
        {
            CardsManager.Instance.DeselectCurrentCard(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isClicked) // Only animate if the card is not clicked
        {
            Tween.PositionY(transform, endValue: upValue, duration: moveDuration, ease: Ease.InOutSine);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isClicked) // Only animate if the card is not clicked
        {
            Tween.PositionY(transform, endValue: downValue, duration: moveDuration, ease: Ease.InOutSine);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //isClicked = false; // Reset the clicked state when the mouse button is released
        //Tween.PositionY(transform, endValue: initialPosition.y, duration: 0.5f, ease: Ease.InOutSine);
    }

    public void Deselect()
    {
        // Reset state and position when deselected
        isClicked = false;
        Tween.PositionY(transform, endValue: downValue, duration: moveDuration, ease: Ease.InOutSine);
    }

    public void UpdateText(string newText)
    {
        titleText.text = newText;
    }
}
