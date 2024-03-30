using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{

    public GameState currentGameState;

    // Create different game states here
    public enum GameState
    {
        Begin,
        ChopOnion,
        ChopTomato,
        BrownPork,
        WashBockchoy, 
    }


    // Ignore the state function for now
    public void Awake()
    {
        
    }


    // Start with the ChopOnion State
    // Things that need to be taken care of in ChopOnion:
        // start with sending a message to the onions and knives
    private void Start()
    {
        currentGameState = ChopOnion;
    }


    public void ChangeGameState(GameState newGameState)
    {

        currentGameState = newGameState;
        // Implement game state functionality here
        switch (newState)
        {
            case GameState.Begin:
                BroadcastMessage("OnionCut");
                break;

            case GameState.ChopOnion:
                break;

            case GameState.ChopTomato:
                break;

            case GameState.BrownPork:
                break;

            case GameState.WashBockchoy:
                break;

        }
    }
}

