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
        //OutlineScript.enabled = true;
        //XRScript.enabled = true;

        XRScript.onSelectEntered.AddListener(OnGrabbed);
    }


    private void OnGrabbed(XRBaseInteractor interactor)
    {
        if(GameStateManager.instance.currentGameState != GameStateManager.GameState.EndingState)
        {
            GameStateManager.instance.ChangeGameState(GameStateManager.GameState.EndingState);
        }
        OutlineScript.enabled = false;
    }
        

    public void BeginWashingBokChoy()
    {
        OutlineScript.enabled = true;
        XRScript.enabled = true;
    }

}