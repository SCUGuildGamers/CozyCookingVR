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

    private Sliceable SliceableComponent2;
    private XRGrabInteractable XRScript2;
    private Outline OutlineScript2;



    GameObject child;
    GameObject child2;

    private bool conCheck;

    private void Start()
    {

        child = transform.GetChild(1).gameObject;
        child2 = transform.GetChild(2).gameObject;


        // Define the script components (get them from the object derviced from the Sliceable)
        SliceableComponent = child.GetComponent<Sliceable>();
        XRScript = child.GetComponent<XRGrabInteractable>();
        OutlineScript = child.transform.GetChild(0).GetComponent<Outline>();

        SliceableComponent2 = child2.GetComponent<Sliceable>();
        XRScript2 = child2.GetComponent<XRGrabInteractable>();
        OutlineScript2 = child2.transform.GetChild(0).GetComponent<Outline>();


    }


    void Update()
    {
            if ((gameObject.transform.childCount == 13) && (conCheck == true))
            {
                // Might as well handle all the logic for the end of onion slice here
                // XRScript.enabled = false;
                // OutlineScript.enabled = false;
                GameStateManager.instance.ChangeGameState(GameStateManager.GameState.BeginBowlStep);

                conCheck = false;
            }
        
    }

    public void BeginTomatoChop()
    {
        conCheck = true;
        // On BeginOnionChop, set the GameObject to SliceableTag so that it interacts with the thing
        XRScript.enabled = true;
        OutlineScript.enabled = true;
        child.layer = 6;
        
        XRScript2.enabled = true;
        OutlineScript2.enabled = true;
        child2.layer = 6;
    }
}
