using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaucetDetector : MonoBehaviour
{
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

    // both the faucet and the sauce bottles will be playing the same sound effect need to find a fix later

    void Start()
    {
        liqFill = water.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FaucetToggle()
    {
        if (isPouring)
        {
            Debug.Log("turn off faucet");
            isPouring = false;
            EndPour();
            // turn off stream
        }
        else
        {
            // turn on stream
            Debug.Log("turn on faucet");
            isPouring = true;
            StartPour(); 
        }
    }

    private void StartPour()
    {
        //StartCoroutine(decreaseFill());
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
