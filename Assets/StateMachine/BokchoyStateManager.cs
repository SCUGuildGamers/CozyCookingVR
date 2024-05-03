using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class BokchoyStateManager : MonoBehaviour
{
    private Outline OutlineScript;
    private XRGrabInteractable XRScript;

    void Start()
    {
        OutlineScript = gameObject.GetComponent<Outline>();
        XRScript = gameObject.GetComponent<XRGrabInteractable>();
        OutlineScript.enabled = false;
        XRScript.enabled = false;

        XRScript.onSelectEntered.AddListener(OnGrabbed);
        XRScript.onSelectExited.AddListener(OnReleased);
    }


    private void OnGrabbed(XRBaseInteractor interactor)
    {
        GameStateManager.instance.ChangeGameState(GameStateManager.GameState.EndingState);
        OutlineScript.enabled = false;
    }

    private void OnReleased(XRBaseInteractor interactor)
    {
        SceneManager.LoadScene("Dorm P1");
    }

    public void BeginWashingBokChoy()
    {
        OutlineScript.enabled = true;
        XRScript.enabled = true;
    }

}