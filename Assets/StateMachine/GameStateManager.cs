using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{

    public GameState currentGameState;

    // Variable for number of tomatoes and onions that need to be ut
    public  int numOnions = 1;
    public int numTomato = 1;


    // Create different game states here
    public enum GameState
    {

        // Note: I am using the names for two things: 
        // 1) the actual name of the GameState
        // 2) the functions that are being called at the beginning of the GameState
        StartingState,
        BeginOnionChop,
        BeginTomatoChop,
        BeginBrownPork,
        BeginWashingBokchoy
    }


    // Ignore the state function for now
    public override void Awake()
    {
        base.Awake();
    }


    // Start with the ChopOnion State
    // Things that need to be taken care of in ChopOnion:
        // start with sending a message to the onions and knives
    private void Start()
    {
        ChangeGameState(GameState.StartingState);
    }


    // Handles what happens when we switch states
    public void ChangeGameState(GameState newGameState)
    {

        currentGameState = newGameState;
        // Implement game state functionality here
        switch (newGameState)
        {
            case GameState.StartingState:
                Debug.Log("StartingState reached!");
                BroadcastMessage("StartingState");
                break;

            case GameState.BeginOnionChop:
                // ON ChopOnion state (which is the first one, broadcast start)
                Debug.Log("BeginOnionChop reached!");
                BroadcastMessage("BeginOnionChop");
                break;

            case GameState.BeginTomatoChop:
                Debug.Log("BeginTomatoChop reached!");
                BroadcastMessage("BeginTomatoChop");
                break;

            case GameState.BeginBrownPork:
                Debug.Log("BeginBrownPork reached!");
                break;

            case GameState.BeginWashingBokchoy:
                break;

        }
    }

    public void EndStartingState()
    {
        ChangeGameState(GameState.BeginOnionChop);
    }

    public void EndOnionChop()
    {
        ChangeGameState(GameState.BeginTomatoChop);
    }

    public void EndTomatoChop()
    {
        ChangeGameState(GameState.BeginBrownPork);
    }

    void IsThisWorking()
    {
        Debug.Log("Debugging step!");
    }
}

