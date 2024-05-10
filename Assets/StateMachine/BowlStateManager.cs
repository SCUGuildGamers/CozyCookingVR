using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BowlStateManager : MonoBehaviour
{

    private Outline outlineScript;
    private BoxCollider hitbox;
    private int itemCount;
    private bool correctState;

    // hitbox might not even be necessary
    void Start()
    {

        // Get the relevant components for the outline script as well as the BoxCollider acting as the hitbox
        // two random variables (itemCount counting how many objects/vegetables making contact), and (correctState which only allows the State to be completed
        // if the conditions are right)
        outlineScript = gameObject.GetComponent<Outline>();
        hitbox = gameObject.GetComponent<BoxCollider>();

        outlineScript.enabled = false;
        correctState = false;
        itemCount = 0;
    }


    private void Update()
    {
        if((itemCount == 12) && (correctState == true))
        {
            GameStateManager.instance.ChangeGameState(GameStateManager.GameState.BeginBrownPork);
            correctState = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Onion") || (other.tag == "Tomato"))
        {
            itemCount++;
        }
      
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.tag == "Onion") || (other.tag == "Tomato"))
        {
            itemCount--;
        }
    }

    public void BeginBowlStep()
    {
        outlineScript.enabled = true;
        correctState = true;
    }

    public void BeginBrownPorkBook()
    {
        outlineScript.enabled = false;
    }
}
