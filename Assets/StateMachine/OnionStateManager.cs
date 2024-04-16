using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class OnionStateManager : Sliceable 
{
    // Define the script components
    private Sliceable SliceableComponent;

    private XRGrabInteractable XRScript;
    private Outline OutlineScript;

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
        if(gameObject.transform.childCount == 4)
        {
            // Might as well handle all the logic for the end of onion slice here
            SendMessageUpwards("EndOnionChop");
            XRScript.enabled = false;
            OutlineScript.enabled = false;
        }
    }

    private void BeginOnionChop()
    {
        // On BeginOnionChop, set the GameObject to SliceableTag so that it interacts with the thing
        XRScript.enabled = true;
        //OutlineScript.enabled = true;
        child.tag = "Sliceable";
    }
}
