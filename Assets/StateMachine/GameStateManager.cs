using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{

    public GameState currentGameState;
    public static GameStateManager instance;

    public GameObject fadeOut;

    // Variable for number of tomatoes and onions that need to be ut
    public  int numOnions = 1;
    public int numTomato = 1;

    // Needed in order to delay the start of a GameState until the button has been pressed
    // public GameObject secondInCommand;
    // private ButtonScript buttonScript;


    // Create different game states here
    public enum GameState
    {

        // Note: I am using the names for two things: 
        // 1) the actual name of the GameState
        // 2) the functions that are being called at the beginning of the GameState
        StartingState,
        BeginOnionChop,
        BeginTomatoChopBook,
        BeginTomatoChop,
        BeginBowlStep,
        BeginBrownPorkBook,
        BeginBrownPork,
        BeginSizzlePork,
        BeginWashingBokChoyBook,
        BeginWashingBokChoy,
        EndingState
    }


    // Ignore the state function for now
    /*
    public override void Awake()
    {
        //base.Awake();
    }
    */

    // Start with the ChopOnion State
    // Things that need to be taken care of in ChopOnion:
        // start with sending a message to the onions and knives
    private void Start()
    {
        // buttonScript = secondInCommand.GetComponent<ButtonScript>();
        instance = this;
        ChangeGameState(GameState.StartingState);
        fadeOut.SetActive(true);
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

            
            case GameState.BeginTomatoChopBook:
                Debug.Log("BeginTomatoChopBook reached!");
                BroadcastMessage("BeginTomatoChopBook");
                break;
            

            case GameState.BeginTomatoChop:
                Debug.Log("BeginTomatoChop reached");
                BroadcastMessage("BeginTomatoChop");
                break;


            case GameState.BeginBowlStep:
                Debug.Log("BeginBowlStep reached!");
                BroadcastMessage("BeginBowlStep");
                break;
                
            case GameState.BeginBrownPorkBook:
                Debug.Log("BeginBrownPorkBook reached!");
                BroadcastMessage("BeginBrownPorkBook");
                break;
                

            case GameState.BeginBrownPork:
                Debug.Log("BeginBrownPork reached!");
                BroadcastMessage("BeginBrownPork");
                break;

            case GameState.BeginSizzlePork:
                Debug.Log("BeginSizzlePork reached.");
                BroadcastMessage("BeginSizzlePork");
                break;

            
            case GameState.BeginWashingBokChoyBook:
                Debug.Log("BeginWashingBokChoyBook");
                BroadcastMessage("BeginWashingBokChoyBook");
                break;
            

            case GameState.BeginWashingBokChoy:
                Debug.Log("BeginWashingBokChoy reached!");
                BroadcastMessage("BeginWashingBokChoy");
                break;

            case GameState.EndingState:
                Debug.Log("EndingState reached!  Everything should be working");
                fadeOut.GetComponent<FadeInFadeOut>().StartFadeToBlack();
                StartCoroutine(SwitchScenes());
                break;

        }
    }

    private IEnumerator SwitchScenes()
    {
        
        yield return new WaitForSeconds(6);
        // insert fade to black code here
        SceneManager.LoadScene(sceneName: "Dorm P1");
    }

    
}

