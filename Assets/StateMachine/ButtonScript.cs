using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonScript : MonoBehaviour
{

    // Script names 
    private XRGrabInteractable grabInteractable;
    private Outline OutlineScript;

    // Cheat code for delaying the process 
    // public bool buttonToContinue;

    private int stateNum;

    private void Start()
    {
        // Get reference to the XR Grab Interactable component
        grabInteractable = GetComponent<XRGrabInteractable>();
        OutlineScript = GetComponent<Outline>();

        // Subscribe to the grab and release events
        // grabInteractable.onSelectEntered.AddListener(OnGrabbed);
        grabInteractable.onSelectExited.AddListener(OnReleased);

        stateNum = 0;
        //buttonToContinue = true;
    }

    private void OnReleased(XRBaseInteractor interactor)
    {
        //buttonToContinue = true;
        grabInteractable.enabled = false;
        OutlineScript.enabled = false;
        if(stateNum == 1)
        {
            GameStateManager.instance.EndStartingState();
        }
        if (stateNum == 2)
        {
            GameStateManager.instance.EndOnionChop();
        }
        if (stateNum == 4)
        {
            GameStateManager.instance.EndTomatoChop();
        }
        if (stateNum == 6)
        {
            GameStateManager.instance.EndBrownPork();
        }
    }



    // for the gamestates
    
    private void StartingState()
    {
        grabInteractable.enabled = true;
        OutlineScript.enabled = true;
        stateNum++;
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
        stateNum++;
        // cheap way to get out of the multiple button touches
       // buttonToContinue = false;
    }

    public void BeginBrownPorkBook()
    {
        grabInteractable.enabled = true;
        OutlineScript.enabled = true;
        stateNum++;
        // cheap way to get out of the multiple button touches
        //buttonToContinue = false;
    }

    public void BeginWashingBokChoyBook()
    {
        grabInteractable.enabled = true;
        OutlineScript.enabled = true;
        stateNum++;
        // cheap way to get out of the multiple button touches
        //buttonToContinue = false;
    }
}
