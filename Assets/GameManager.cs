using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance;
    public GameState currentGameState;

    public GameObject Pot;
    public GameObject Sink;
    public GameObject Sauce;
    public GameObject Powder;
    public GameObject StoveArea;
    public GameObject VeggieBowl;
    public GameObject Lid;

    public GameObject fadeOut;

    public GameObject NoteBook;

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
        NotComplete, // this is the fill up water step pot and sink highlighted 
        AddSauce, // sauce and pot highlighted
        AddPowder,  // powder and pot highlighted 
        PlaceOnStove, // stove area and pot highlighted 
        AddVeggies, // veggie bowl and pot highlighted 
        AddLid, // Lid and pot highlighted
        DormTransition, // wait for seconds to transition 
    }

    // Ignore the state function for now
  
    private void Start()
    {
        fadeOut.SetActive(true);
        ChangeGameState(GameState.NotComplete);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(sceneName: "Dorm P1");
        }
    }

    // Handles what happens when we switch states
    public void ChangeGameState(GameState newGameState)
    {

        currentGameState = newGameState;
        // Implement game state functionality here
        switch (newGameState)
        {
            case GameState.NotComplete:
                Sink.GetComponent<Outline>().enabled = true;
                Pot.GetComponent<Outline>().enabled = true;
                break;
            case GameState.AddSauce:
                Sink.GetComponent<Outline>().enabled = false;
                //Pot.GetComponent<Outline>().enabled = false;
                Sauce.GetComponent<Outline>().enabled = true;
                NoteBook.GetComponent<NoteSwitcher>().SetPage(1);
                break;
            case GameState.AddPowder:
                Sauce.GetComponent<Outline>().enabled = false;
                Powder.GetComponent<Outline>().enabled = true;
                NoteBook.GetComponent<NoteSwitcher>().SetPage(2);
                break;
            case GameState.PlaceOnStove:
                Powder.GetComponent<Outline>().enabled = false;
                StoveArea.GetComponent<Outline>().enabled = true;
                NoteBook.GetComponent<NoteSwitcher>().SetPage(3);
                break;
            case GameState.AddVeggies:
                //StoveArea.GetComponent<Outline>().enabled = false;
                VeggieBowl.GetComponent<Outline>().enabled = true;

                // turn the notebook page 
                break;
            case GameState.AddLid:
                VeggieBowl.GetComponent<Outline>().enabled = false;
                Lid.GetComponent<Outline>().enabled = true;
                NoteBook.GetComponent<NoteSwitcher>().SetPage(4);
                // turn the notebook page?
                break;
            case GameState.DormTransition:
                // ON ChopOnion state (which is the first one, broadcast start)
                Lid.GetComponent<Outline>().enabled = false;
                Pot.GetComponent<Outline>().enabled = false;
                Debug.Log("Changing scenes to the next kitchen");
                fadeOut.GetComponent<FadeInFadeOut>().StartFadeToBlack();
                StartCoroutine(SwitchScenes());
                break;

        }
    }

    private void ToggleOutline(Outline targetObj)
    {
        targetObj.enabled = !targetObj.enabled;
    }

    private void OnionSliced()
    {
        
    }

    public void DormComplete()
    {
        Debug.Log("game state manager got the message and changing scenes");
    }

    public void WaterAdded()
    {
        Debug.Log("Water added to stove NotComplete -> AddSauce");
        if(currentGameState == GameState.NotComplete)
        {
            ChangeGameState(GameState.AddSauce);
        }
    }

    public void SauceAdded()
    {
        if(currentGameState == GameState.AddSauce)
        {
            ChangeGameState(GameState.AddPowder);
        }
    }

    public void PowderAdded()
    {
        if (currentGameState == GameState.AddPowder)
        {
            ChangeGameState(GameState.PlaceOnStove);
        }
    }

    public void PotOnStove()
    {
        if (currentGameState == GameState.PlaceOnStove)
        {
            ChangeGameState(GameState.AddVeggies);
        }
    }

    public void VeggiesAdded()
    {
        if(currentGameState == GameState.AddVeggies)
        {
            ChangeGameState(GameState.AddLid);
        }
    }

    public void LidAdded()
    {
        if(currentGameState == GameState.AddLid)
        {
            ChangeGameState(GameState.DormTransition);
        }
    }

    private IEnumerator SwitchScenes()
    {
        yield return new WaitForSeconds(6);
        // insert fade to black code here
        SceneManager.LoadScene(sceneName: "Home Kitchen 2");
    }
}
    