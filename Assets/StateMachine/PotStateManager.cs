using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PotStateManager : MonoBehaviour
{

    private Outline outlineScript;
    private BoxCollider hitbox;
    private int itemCount;
    private bool correctState;

    private XRGrabInteractable porkPieceScript;

    void Start()
    {

        outlineScript = gameObject.GetComponent<Outline>();
        hitbox = gameObject.GetComponent<BoxCollider>();

        outlineScript.enabled = false;
        correctState = false;
        itemCount = 0;
    }


    private void Update()
    {

        if ((itemCount == 4) && (correctState == true))
        {
            GameStateManager.instance.ChangeGameState(GameStateManager.GameState.BeginWashingBokChoyBook);
            correctState = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Onion")
        {
            porkPieceScript = other.GetComponent<XRGrabInteractable>();
            porkPieceScript.enabled = false;
            
            itemCount++;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Onion")
        {
            porkPieceScript = other.GetComponent<XRGrabInteractable>();
            porkPieceScript.enabled = true;

            itemCount--;
        }
    }

    public void BeginSizzlePork()
    {
        outlineScript.enabled = true;
        correctState = true;
    }

    public void BeginWashingBokChoyBook()
    {
        outlineScript.enabled = false;
    }
}
