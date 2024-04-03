using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetChamberVolume : MonoBehaviour
{
    public int index; //cartridge number
    //public CartridgeState state;

    //height values should be 0 <= h <= 1
    public float currentHeight; //used to store current height
    public float prevHeight; //used to slide height up smoothly

    public float lerpSpeed = 1;

    public Material thisMaterial;

    //the shader operates on an topCoord <= h <= bottomCoord
    public float topCoord;//coordinate for the top of an object
    public float bottomCoord;//coordinate for the bottom of an object
    int check = -1;

    void Start()
    {
        //retrieve the value of the top and bottom of the shape

        topCoord = gameObject.GetComponent<MeshRenderer>().bounds.max.y;
        bottomCoord = gameObject.GetComponent<MeshRenderer>().bounds.min.y;

        //Debug.Log("Index: "+index+" Top: " + topCoord + ", Bottom: " + bottomCoord);

        //initialize the height of the liquid
        thisMaterial = gameObject.GetComponent<MeshRenderer>().material;
        //Debug.Log(index+", "+thisMaterial.GetFloat("_Fill"));

        //currentHeight = convertHeight(state.chamberVol[index]);//check the scriptable object for this chamber's height
        prevHeight = currentHeight;
        thisMaterial.SetFloat("_Fill", currentHeight);//set chamber physical height
    }

    void Update()
    {
        //if (currentHeight != convertHeight(state.chamberVol[index]))
        {
            prevHeight = currentHeight;
            //currentHeight = convertHeight(state.chamberVol[index]);
            check = 1;
            //Debug.Log(index+" start time: "+Time.time);
        }

        if (thisMaterial.GetFloat("_Fill") < currentHeight)
        {
            //thisMaterial.SetFloat("_Fill", thisMaterial.GetFloat("_Fill") + Time.deltaTime * state.chamberLerpSpeed[index] * (topCoord - bottomCoord));
        }
        if (thisMaterial.GetFloat("_Fill") > currentHeight)
        {
            //thisMaterial.SetFloat("_Fill", thisMaterial.GetFloat("_Fill") - Time.deltaTime * state.chamberLerpSpeed[index] * (topCoord - bottomCoord));
        }
        if (Mathf.Abs(thisMaterial.GetFloat("_Fill") - currentHeight) <= 0.05f && check == 1)
        {
            //Debug.Log(index + " end time: " + Time.time);
            check = -1;
        }
    }

    float convertHeight(float percentage)//put in the fill percentage, receive a worldspace coordinate
    {
        return ((topCoord - bottomCoord) * (percentage - 0.5f));
    }
}