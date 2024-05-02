using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PotDetector : MonoBehaviour
{
    // Start is called before the first frame update
    public int bokChoyCountRequirement = 2;
    public int tomatoRequirement = 1;
    public int onionRequirement = 1;
    public int sauceRequirement = 1;

    public int boyChoyCount = 0;
    public double sauceAmount = 0.0;

    public bool bokChoyComplete = false;
    public bool ingredientsComplete = false;
    public bool sauceComplete = false;
    public bool potComplete = false;
    public bool powderComplete = false;
    public bool waterComplete = false;

    public HashSet<GameObject> bokChoyInPot = new HashSet<GameObject>();
    public HashSet<GameObject> onionInPot = new HashSet<GameObject>();
    public HashSet<GameObject> tomatoInPot = new HashSet<GameObject>();
    public GameObject water;

    public Material liqFill;

    void Start()
    {
        liqFill = water.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(bokChoyInPot.Count == bokChoyCountRequirement)
        {
            // this correctly checks to see if theres enough bok choy in the pot 
            bokChoyComplete = true;
            
        }
        else
        {
            bokChoyComplete = false;
        }
        if(onionInPot.Count == onionRequirement && tomatoInPot.Count == tomatoRequirement)
        {
            ingredientsComplete = true;
        }
        else
        {
            ingredientsComplete = false;
        }
        if(sauceAmount >= sauceRequirement)
        {
            sauceComplete = true;
        }

        if(liqFill.GetFloat("_fill") >= 0.57)
        {
            waterComplete = true;
        }
        
        /*
        if(liqFill.GetFloat("_fill") >= 0.45)
        {
            liqFill.SetFloat("_fill", 0);
        }
        */
    }

    private void FixedUpdate()
    {
        
    }

    public void SetOnStove()
    {
        if(GameManager.Instance.currentGameState != GameManager.GameState.DormTransition)
        {
            if (bokChoyComplete && ingredientsComplete && sauceComplete && potComplete && powderComplete)
            {
                GameManager.Instance.DormComplete();
            }
        } 
    }

    public void AddSauce()
    {
        sauceAmount += 0.01;
    }
    
    public void LidOn()
    {
        potComplete = true;
    }

    public void LidOff()
    {
        potComplete = false;
    }

    public void PowderAdded()
    {
        powderComplete = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "BokChoy")
        {
            bokChoyInPot.Add(other.gameObject);
        }
        if (other.gameObject.tag == "Tomato")
        {
            onionInPot.Add(other.gameObject);
        }
        if (other.gameObject.tag == "Onion")
        {
            tomatoInPot.Add(other.gameObject);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BokChoy")
        {
            bokChoyInPot.Remove(other.gameObject);
        }
        if (other.gameObject.tag == "Tomato")
        {
            tomatoInPot.Remove(other.gameObject);
        }
        if (other.gameObject.tag == "Onion")
        {
            onionInPot.Remove(other.gameObject);
        }
    }
}
