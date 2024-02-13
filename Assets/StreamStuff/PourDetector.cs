using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int pourAngle;
    public Transform streamOrigin;
    public GameObject streamPrefab = null;
    public bool noPour;
    public float temp;
    public float ztemp;

    private Stream currentStream = null;


    public bool isPouring;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        temp = CalcPourAngle();
        ztemp = CalcPourAngleZ();
        noPour = CalcPourAngle() < pourAngle && CalcPourAngleZ() < pourAngle; // false 
        if (!noPour && isPouring == false)
        {
             isPouring = true;
             StartPour();
        }
        else if(noPour && isPouring == true)
        {
             isPouring = false;
             EndPour();
        }
        
        
        
     
    }

    private float CalcPourAngle()
    {
        return Math.Abs(transform.forward.y * Mathf.Rad2Deg);
    }

    private float CalcPourAngleZ()
    {
        return Math.Abs(transform.rotation.z * Mathf.Rad2Deg);
    }

    private void StartPour()
    {

        currentStream = CreateStream();
        currentStream.Begin();
    }

    private void EndPour()
    {
        currentStream.End();
        currentStream = null;
        
    }

    private Stream CreateStream()
    {
        GameObject steamObj = Instantiate(streamPrefab, streamOrigin.position, Quaternion.identity, transform);
       // currentStream = steamObj;
        return steamObj.GetComponent<Stream>();

    }
}
