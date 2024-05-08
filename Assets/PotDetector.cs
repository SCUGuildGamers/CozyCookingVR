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

    public bool isOnStove = false;

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
        
        if(GameManager.Instance.currentGameState == GameManager.GameState.AddVeggies)
        {
            if (onionInPot.Count == onionRequirement && tomatoInPot.Count == tomatoRequirement && bokChoyInPot.Count == bokChoyCountRequirement)
            {
                ingredientsComplete = true;
                GameManager.Instance.VeggiesAdded();
            }
            else
            {
                ingredientsComplete = false;
            }
        }
        
        if(GameManager.Instance.currentGameState == GameManager.GameState.AddSauce)
        {
            if (sauceAmount >= sauceRequirement)
            {
                sauceComplete = true;
                GameManager.Instance.SauceAdded();
            }
        }
        if(GameManager.Instance.currentGameState == GameManager.GameState.NotComplete)
        {
            if (liqFill.GetFloat("_fill") >= 0.57)
            {
                waterComplete = true;
                GameManager.Instance.WaterAdded();
            }
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
        /*
        if(GameManager.Instance.currentGameState != GameManager.GameState.DormTransition)
        {
            if (bokChoyComplete && ingredientsComplete && sauceComplete && potComplete && powderComplete)
            {
                GameManager.Instance.DormComplete();
            }
        } 
        */
       isOnStove = true;
       if(GameManager.Instance.currentGameState == GameManager.GameState.PlaceOnStove)
       {
            GameManager.Instance.PotOnStove();
       }
    }

    public void TakeOffStove()
    {
        isOnStove = false;
    }

    public void AddSauce()
    {
        sauceAmount += 0.01;
    }
    
    public void LidOn()
    {
        potComplete = true;
        if(GameManager.Instance.currentGameState == GameManager.GameState.AddLid)
        {
            GameManager.Instance.LidAdded();
        }
    }

    public void LidOff()
    {
        potComplete = false;
    }

    public void PowderAdded()
    {
        if (GameManager.Instance.currentGameState == GameManager.GameState.AddPowder)
        {
            powderComplete = true;
            GameManager.Instance.PowderAdded();
        }
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
