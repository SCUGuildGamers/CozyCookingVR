using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonScript : MonoBehaviour
{

    // Script names 
    private XRGrabInteractable grabInteractable;
    private Outline OutlineScript;

    private void Start()
    {
        // Get reference to the XR Grab Interactable component
        grabInteractable = GetComponent<XRGrabInteractable>();
        OutlineScript = GetComponent<Outline>();

        // Subscribe to the grab and release events
        // grabInteractable.onSelectEntered.AddListener(OnGrabbed);
        grabInteractable.onSelectExited.AddListener(OnReleased);
    }
        
    private void OnReleased(XRBaseInteractor interactor)
    {
        GameStateManager.instance.EndStartingState();
    }

 

    // for the gamestates
    private void StartingState()
    {
        grabInteractable.enabled = true;
       OutlineScript.enabled = true;
    }

    private void BeginOnionChop()
    {
        grabInteractable.enabled = false;
        OutlineScript.enabled = false;
    }
}
