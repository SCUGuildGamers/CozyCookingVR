using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonScript : MonoBehaviour
{

    // Script names 
    [SerializeField]
    private XRGrabInteractable grabInteractable;
    public  Outline OutlineScript;

    // Cheat code for delaying the process 
    // public bool buttonToContinue;

    private int stateNum;

    private void Start()
    {
        // Get reference to the XR Grab Interactable component
        grabInteractable = GetComponent<XRGrabInteractable>();

        // For below just add component
        OutlineScript = GetComponent<Outline>();

        // Subscribe to the grab and release events
        // grabInteractable.onSelectEntered.AddListener(OnGrabbed);
        grabInteractable.onSelectExited.AddListener(OnReleased);

        stateNum = 1;
        //buttonToContinue = true;
    }

    private void OnReleased(XRBaseInteractor interactor)
    {
        //buttonToContinue = true;
        //grabInteractable.enabled = false;
        //OutlineScript.enabled = false;
        
        if(stateNum == 1)
        {
            GameStateManager.instance.ChangeGameState(GameStateManager.GameState.BeginOnionChop);
            stateNum++;
        }
        /*
        else if (stateNum == 2)
        {
            GameStateManager.instance.ChangeGameState(GameStateManager.GameState.BeginTomatoChop);
            stateNum++;
        }
        else if (stateNum == 3)
        {
            GameStateManager.instance.ChangeGameState(GameStateManager.GameState.BeginBrownPork); 
            stateNum++;
        }
        else if (stateNum == 4)
        {
            GameStateManager.instance.ChangeGameState(GameStateManager.GameState.BeginWashingBokChoy);
            stateNum++;
        }
        */
    }



    // for the gamestates
    
    public void StartingState()
    {
        grabInteractable.enabled = true;
        OutlineScript.enabled = true;
    }
   
    /*
    private void BeginOnionChop()
    {
        grabInteractable.enabled = true;
        OutlineScript.enabled = true;
        // cheap way to get out of the multiple button touches
        //buttonToContinue = false;
    }
    */

    public void BeginTomatoChopBook()
    {

        grabInteractable.enabled = true;
        OutlineScript.enabled = true;
  
        // cheap way to get out of the multiple button touches
       // buttonToContinue = false;
    }


    public void BeginBrownPorkBook()
    {
        grabInteractable.enabled = true;
        OutlineScript.enabled = true;
        // cheap way to get out of the multiple button touches
        //buttonToContinue = false;
    }

    public void BeginWashingBokChoyBook()
    {
        grabInteractable.enabled = true;
        OutlineScript.enabled = true;
        // cheap way to get out of the multiple button touches
        //buttonToContinue = false;
    }
}
