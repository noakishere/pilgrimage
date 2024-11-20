using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : SingletonMonoBehaviour<CardsManager>
{
    [Header("All Cards")]
    [SerializeField] private List<Card> allCardTypes;

    [Header("Route cards")]
    [SerializeField] private EventCellTypeSO eventCellTypeSO;
    [SerializeField] private List<GameObject> cards;

    [SerializeField] private List<EventCellType> possibleRouteTypes;

    [SerializeField] private GameObject cardsRootGameObject;
    [SerializeField] private GameObject cardImageGameObject;

    [SerializeField] private CardImageUI selectedRouteCard;
    public CardImageUI SelectedRouteCard { get { return selectedRouteCard; } }

    [SerializeField] private bool canSelectRouteCard = false;
    public bool CanSelectRouteCard { get { return canSelectRouteCard; } }

    public void BeginSelection(int amount = 6)
    {
        canSelectRouteCard = true;

        GenerateCards(10);
    }

    public void GenerateCards(int amount = 6)
    {
        //Clean all the other objects just in case
        for(int i = 0; i < cardsRootGameObject.transform.childCount; i++)
        {
            Destroy(cardsRootGameObject.transform.GetChild(i).gameObject);
        }

        cards.Clear();

        // generate cards
        for (int i = 0; i < amount; i++)
        {
            GameObject newImage = Instantiate(cardImageGameObject);
            newImage.transform.SetParent(cardsRootGameObject.transform);

            RouteCard cardType = newImage.GetComponent<RouteCard>();
            EventCellType eventCellType = possibleRouteTypes[UnityEngine.Random.Range(0, possibleRouteTypes.Count)];

            cardType.AssignTypeToCard(eventCellType);
            cardType.AssignSprite(eventCellTypeSO.cellTypes[eventCellType].sprite);

            newImage.GetComponent<CardImageUI>().UpdateText(cardType.EventType.ToString());

            cards.Add(newImage);
        }
    }

    public void SelectRouteCard(CardImageUI newCard)
    {
        if(canSelectRouteCard)
        {
            if (selectedRouteCard != null && selectedRouteCard != newCard)
            {
                selectedRouteCard.Deselect();
            }

            // Set the new card as selected
            selectedRouteCard = newCard;
        }
    }

    public void DeselectCurrentCard(CardImageUI targetCard)
    {
        if(canSelectRouteCard)
        {
            if (selectedRouteCard != null && targetCard == selectedRouteCard)
            {
                selectedRouteCard.Deselect();
                selectedRouteCard = null;
            }
        }
    }

    public void EndSelection()
    {
        canSelectRouteCard = false;

        for (int i = 0; i < cardsRootGameObject.transform.childCount; i++)
        {
            Destroy(cardsRootGameObject.transform.GetChild(i).gameObject, 0.1f);
        }
    }

}
