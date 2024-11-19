using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private CardType cardType;
    public CardType CardType { get { return cardType; } }

    public virtual void ActivateCard()
    {

    }

    public virtual void AssignCardType(CardType cardType)
    {
        this.cardType = cardType;
    }
}

public enum CardType
{
    Route,
    Resource,
    Ability
}
