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
        Begin,
        ChopOnion,
        ChopTomato,
        BrownPork,
        WashBockchoy
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
        ChangeGameState(GameState.ChopOnion);
    }


    public void ChangeGameState(GameState newGameState)
    {

        currentGameState = newGameState;
        // Implement game state functionality here
        switch (newGameState)
        {
            case GameState.Begin:
                break;

            case GameState.ChopOnion:
                BroadcastMessage("OnionCutStart");
                break;

            case GameState.ChopTomato:
                Debug.Log("We got to this stage");
                break;

            case GameState.BrownPork:
                break;

            case GameState.WashBockchoy:
                break;

        }
    }

    private void OnionSliced()
    {
        Debug.Log("OnionSliced Reached");
        numOnions--;
        if(numOnions == 0)
        {
            ChangeGameState(GameState.ChopTomato);
        }
    }
}

