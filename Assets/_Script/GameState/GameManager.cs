using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{

    //[Header("General Game State")]
    private GameStateBase currentGameState;
    //[SerializeField] private GameState currentGameState;
    public GameStateBase CurrentGameState { get { return currentGameState; } }

    public event Action<GameStateBase> OnGameStateChanged;

    [SerializeField] private PlayerPosition playerPosition;
    public PlayerPosition PlayerPosition { get { return playerPosition; } }


    // Start is called before the first frame update
    void Start()
    {
        ChangeGameState(new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
        currentGameState?.UpdateState(this);
    }

    public void ChangeGameState(GameStateBase newState)
    {
        currentGameState?.ExitState(this);
        currentGameState = newState;
        currentGameState.EnterState(this);

        OnGameStateChanged?.Invoke(currentGameState);
    }

    /*

    private void HandleGameState()
    {
        switch (currentGameState)
        {
            case GameState.RouteSelection:
                playerPosition.PlayerCantMove();
                CardsManager.Instance.BeginSelection();
                break;

            case GameState.Navigation:
                playerPosition.PlayerCanMove();
                break;

            case GameState.InEvent:
                playerPosition.PlayerCantMove();
                EventsUI.Instance.NewEventProcess(playerPosition.CurrentCell);
                break;
            
        }
    }
    */

    /*
    public void ChangeGameState(GameState newState)
    {
        currentGameState = newState;

        //OnGameStateChanged?.Invoke(newState);
        HandleGameState();
    }*/
}
