using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabDetection : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    private void Start()
    {
        // Get reference to the XR Grab Interactable component
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Subscribe to the grab and release events
        grabInteractable.onSelectEntered.AddListener(OnGrabbed);
        grabInteractable.onSelectExited.AddListener(OnReleased);
    }

    private void OnGrabbed(XRBaseInteractor interactor)
    {
        SendMessageUpwards("EndState1");
        Debug.Log("The button was toutched");
        // Add your custom functionality here when the object is grabbed
    }

    private void OnReleased(XRBaseInteractor interactor)
    {
        Debug.Log("Object released!");
        // Add your custom functionality here when the object is released
    }
}
