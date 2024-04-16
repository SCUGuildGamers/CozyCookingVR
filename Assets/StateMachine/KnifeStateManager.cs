using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class KnifeStateManager : MonoBehaviour
{
    // Define the script components
    private XRGrabInteractable XRScript;
    private Outline OutlineScript;

    private void Start()
    {
        // Define the script components (get them from the object derviced from the Sliceable)
        XRScript = GetComponent<XRGrabInteractable>();
        OutlineScript = GetComponent<Outline>();
    }


    void Update()
    {
       
    }

    private void BeginOnionChop()
    {
        XRScript.enabled = true;
       // OutlineScript.enabled = true;
    }
}
