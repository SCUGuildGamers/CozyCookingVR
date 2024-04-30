using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class KnifeStateManager : MonoBehaviour
{
    // Define the script components
    private XRGrabInteractable XRScript;
    private Outline OutlineScript;
    //private Outline otherOutlineScript;

    // other GameObjects that might contain the outline (they need to be removed)
    //private GameObject sliceableObject;

    //private bool finishState;


    // On start, need to get the components for both the Interactble script and the Outline script
    private void Start()
    {
        // Define the script components (get them from the object derviced from the Sliceable)
        XRScript = GetComponent<XRGrabInteractable>();
        OutlineScript = GetComponent<Outline>();

        XRScript.onSelectEntered.AddListener(OnGrabbed);
        XRScript.onSelectExited.AddListener(OnReleased);

        //finishState = false;
    }

    
    // This logic SHOULD interact with a slieceable and permanetly deactive its outline so that it can be cut
    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.gameObject.layer == 6)
        {
            otherOutlineScript = other.GetComponent<Outline>();
            otherOutlineScript.enabled = false;
        }
        */
    }

    // The following functions will handle the logic for the outline while doing through BeginOnionCHop, BeginTomatoChop, and BeginBrownPork
    private void OnGrabbed(XRBaseInteractor interactor)
    {
        OutlineScript.enabled = false;
    }

    // OnRealased will include the logic to completely deactivate after BeginWashuingBokChoy is reached, but it must trigger after we let go
    private void OnReleased(XRBaseInteractor interactor)
    {
        OutlineScript.enabled = true;
        /*
        if(finishState == true)
        {
            XRScript.enabled = false;
            OutlineScript.enabled = false;
        }
        */
    }


    // On the first state of BeginOnionChop, it will now be interacteable + outline enabled to demonstrate such
    public void BeginOnionChop()
    {
        XRScript.enabled = true;
        OutlineScript.enabled = true;
    }

    public void BeginBowlStep()
    {
        OutlineScript.enabled = false;
        XRScript.enabled = false;
    }

    public void BeginBrownPork()
    {
        XRScript.enabled = true;
        OutlineScript.enabled = true;
    }

    public void BeginWashingBokChoy()
    {
        OutlineScript.enabled = false;
        XRScript.enabled = false;
    }
}
