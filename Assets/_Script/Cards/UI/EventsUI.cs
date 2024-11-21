using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventsUI : SingletonMonoBehaviour<EventsUI>
{
    [SerializeField] private GameObject eventsContainer;
    public GameObject EventsContainer { get { return eventsContainer; } }

    [SerializeField] private TextMeshProUGUI eventText;

    public void NewEventProcess(EventCell eventCell)
    {
        eventText.text = $"This is a {eventCell.CurrentEventCellType} event";

        eventsContainer.SetActive(true);
    }

    public void EventDone()
    {
        eventsContainer.SetActive(false);
        //GameManager.Instance.ChangeGameState(GameState.Navigation);
        GameManager.Instance.ChangeGameState(new NavigationState());
    }
}
