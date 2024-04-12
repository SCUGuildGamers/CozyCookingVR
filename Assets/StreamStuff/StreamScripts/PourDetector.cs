using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int pourAngle;
    public Transform streamOrigin;
    public GameObject streamPrefab = null;
    public GameObject water;
    public bool notTiltedEnough;
    public float temp;
    public float ztemp;

    private Stream currentStream = null;
    public Material liqFill;

    public bool isPouring;

    void Start()
    {
        //liquidFill.SetFloat("_fill", 0.66f);
        liqFill = water.GetComponent<MeshRenderer>().material; 

    }

    // Update is called once per frame
    void Update()
    {
        temp = CalcPourAngle();
        ztemp = CalcPourAngleZ();
        notTiltedEnough = CalcPourAngle() < pourAngle && CalcPourAngleZ() < pourAngle; // false 
        if (!notTiltedEnough && isPouring == false)
        {
            if(liqFill.GetFloat("_fill") >= 0.445)
            {
                isPouring = true;
                StartPour();
            }    
        }
        else if(notTiltedEnough && isPouring == true)
        {
            isPouring = false;
            EndPour();     
        }
        else if(!notTiltedEnough && isPouring == true && liqFill.GetFloat("_fill") <= 0.445)
        {
            isPouring = false;
            EndPour();
        }
        if (Keyboard.current.commaKey.wasPressedThisFrame)
        {
            liqFill.SetFloat("_fill", .64f);
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
 
        StartCoroutine(decreaseFill());
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

    public IEnumerator decreaseFill()
    {
        float a;
        while (isPouring)
        {
            a = liqFill.GetFloat("_fill");
            if(a > 0.445f)
            {
                liqFill.SetFloat("_fill", a - 0.0005f); 
            }
            yield return null;
        }
    }
}
