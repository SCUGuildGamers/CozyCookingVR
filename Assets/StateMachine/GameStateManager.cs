using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{

    public GameState currentState;

    public override void Awake()
    {
        base.Awake();
    }

    public void Update()
    {
        // Implement game state functionality here
        switch (currentState)
        {
            case GameState.ChopOnion:
                break;
        }
    }
}

// Create different game states here
public enum GameState
{
    Begin = 1,
    ChopOnion = 2,
    ChopTomato = 3,
    BrownPork = 4,
    WashBockchoy = 5, 
}

