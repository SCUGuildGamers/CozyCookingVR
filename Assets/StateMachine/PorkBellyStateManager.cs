using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class PorkBellyStateManager : Sliceable
{
    // Define the script components
    private Sliceable SliceableComponent;

    private XRGrabInteractable XRScript;
    private Outline OutlineScript;

    GameObject child;

    private bool conCheck;

    private void Start()
    {

        child = transform.GetChild(3).gameObject;


        // Define the script components (get them from the object derviced from the Sliceable)
        SliceableComponent = child.GetComponent<Sliceable>();
        XRScript = child.GetComponent<XRGrabInteractable>();
        OutlineScript = child.transform.GetChild(0).GetComponent<Outline>();


    }


    void Update()
    {
        if (conCheck)
        {
            if ((gameObject.transform.childCount == 16) && conCheck == true)
            {
                // Might as well handle all the logic for the end of onion slice here
                // XRScript.enabled = false;
                // OutlineScript.enabled = false;
                GameStateManager.instance.ChangeGameState(GameStateManager.GameState.BeginSizzlePork);
                conCheck = false;
            }
        }
        
    }

    public void BeginBrownPork()
    {
        // On BeginOnionChop, set the GameObject to SliceableTag so that it interacts with the thing
        XRScript.enabled = true;
        OutlineScript.enabled = true;
        child.layer = 6;

        conCheck = true;
    }
}
