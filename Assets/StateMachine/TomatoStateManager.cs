using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class TomatoStateManager : Sliceable
{
    // Define the script components
    private Sliceable SliceableComponent;

    private XRGrabInteractable XRScript;
    private Outline OutlineScript;

    private void Start()
    {
        // Define the script components (get them from the object derviced from the Sliceable)
        SliceableComponent = GetComponent<Sliceable>();
        XRScript = GetComponent<XRGrabInteractable>();
        OutlineScript = GetComponent<Outline>();
    }


    void Update()
    {
        if (sliceCount == 0)
        {
            // Might as well handle all the logic for the end of TOMATO slice here
            SendMessageUpwards("EndTomatoChop");
            XRScript.enabled = false;
            OutlineScript.enabled = false;
            SliceableComponent.enabled = false;
        }
    }

    private void BeginTomatoChop()
    {
        XRScript.enabled = true;
        OutlineScript.enabled = true;
        SliceableComponent.enabled = true;
    }
}
