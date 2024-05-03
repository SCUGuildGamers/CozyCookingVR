using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowderPourDetector : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public int pourAngle;
    
    public bool notTiltedEnough;
    private float temp;
    private float ztemp;

    private double powderRequirement = 1.0;
    private double powderAmount = 0.0;

    public bool isPouring;

    public GameObject particles;

    private ParticleSystem tamarindPowder;

    private LayerMask myLayerMask;

    void Start()
    {
        tamarindPowder = particles.GetComponent<ParticleSystem>();  
        tamarindPowder.Stop();
        myLayerMask = LayerMask.GetMask("Pot");
        myLayerMask = ~myLayerMask;
    }

    // Update is called once per frame
    void Update()
    {
        temp = CalcPourAngle();
        ztemp = CalcPourAngleZ();
        notTiltedEnough = CalcPourAngle() < pourAngle && CalcPourAngleZ() < pourAngle && CalcPourAngleX() < pourAngle; // false 
        if (!notTiltedEnough && isPouring == false)
        {
            isPouring = true;
            StartPour();
        }
        else if (notTiltedEnough && isPouring == true)
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

    private float CalcPourAngleX()
    {
        return Math.Abs(transform.rotation.x * Mathf.Rad2Deg);
    }

    private void StartPour()
    {
        tamarindPowder.Play();
        StartCoroutine(PowderFill());
    }

    private void EndPour()
    {
        tamarindPowder.Stop();
    }

    private IEnumerator PowderFill()
    {
        while (isPouring)
        {
            CheckForPot();
            yield return null;
        }
    }

    private void CheckForPot()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        Physics.Raycast(ray, out hit, 2.0f, myLayerMask); // 2.0 is the max distance the water can go
        if (hit.collider.tag == "Fillable")
        {
            powderAmount += 0.05;
            if(powderAmount >= powderRequirement)
            {
                hit.collider.gameObject.GetComponent<PotDetector>().PowderAdded();
            }
        }

    }

}
