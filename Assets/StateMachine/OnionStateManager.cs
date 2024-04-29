using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;



// NOTE: This script is actually stupid.  All cuttable objects' game mamangers are attached to an empty game object, which it then manages through GetChild(i)
// If I have the time I should look for a more elegant solution but this might need to work for now...

public class OnionStateManager : Sliceable 
{
    // Define the script components
    private Sliceable SliceableComponent;
    private XRGrabInteractable XRScript;
    private Outline OutlineScript;

    // I believe you can just add these GameObjects from the explorer so you do not have to deal with Get() bs
    GameObject child;

    private void Start()
    {

        child = transform.GetChild(0).gameObject;


        // Define the script components (get them from the object derviced from the Sliceable)
        SliceableComponent = child.GetComponent<Sliceable>();
        XRScript = child.GetComponent<XRGrabInteractable>();
        OutlineScript = child.GetComponent<Outline>();

    }


    void Update()
    {
        if(gameObject.transform.childCount == 6)
        {
            // Might as well handle all the logic for the end of onion slice here
            // XRScript.enabled = false;
           // OutlineScript.enabled = false;
            GameStateManager.instance.EndOnionChop();
        }
    }

    private void BeginOnionChop()
    {
        // On BeginOnionChop, set the GameObject to SliceableTag so that it interacts with the thing
        XRScript.enabled = true;
        OutlineScript.enabled = true;
        child.layer = 6;
    }
}