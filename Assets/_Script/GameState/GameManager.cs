using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [Header("General Game State")]
    [SerializeField] private GameState currentGameState;
    public GameState CurrentGameState { get { return currentGameState; } }

    public event Action<GameState> OnGameStateChanged;

    [SerializeField] private PlayerPosition playerPosition;


    // Start is called before the first frame update
    void Start()
    {
        currentGameState = GameState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        //HandleGameState();
    }

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
            
        }
    }

    public void ChangeGameState(GameState newState)
    {
        currentGameState = newState;

        //OnGameStateChanged?.Invoke(newState);
        HandleGameState();
    }
}
