using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance;
    public GameState currentGameState;

    // Variable for number of tomatoes and onions that need to be ut
    public int numOnions = 1;
    public int numTomato = 1;

    void Awake()
    {
        Instance = this;
    }

    // Create different game states here
    public enum GameState
    {
        NotComplete,
        DormTransition,
    }

    // Ignore the state function for now
   


    // Start with the ChopOnion State
    // Things that need to be taken care of in ChopOnion:
    // start with sending a message to the onions and knives
    private void Start()
    {
        ChangeGameState(GameState.NotComplete);
    }


    // Handles what happens when we switch states
    public void ChangeGameState(GameState newGameState)
    {

        currentGameState = newGameState;
        // Implement game state functionality here
        switch (newGameState)
        {
            case GameState.NotComplete:
                break;

            case GameState.DormTransition:
                // ON ChopOnion state (which is the first one, broadcast start)
                Debug.Log("Changing scenes to the next kitchen");
                break;

        }
    }

    private void OnionSliced()
    {
        
    }

    public void DormComplete()
    {
        Debug.Log("game state manager got the message and changing scenes");
        if(currentGameState == GameState.NotComplete)
        {
            ChangeGameState(GameState.DormTransition);
        } 
    }
}
