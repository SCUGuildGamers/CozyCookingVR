using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FaucetDetector : MonoBehaviour
{

    public Transform streamOrigin;
    public GameObject streamPrefab = null;
    public GameObject water;
    private Stream currentStream = null;
    public Material liqFill;
    public bool isPouring;
    GameObject currentLoop = null;
    [SerializeField] private GameObject Button;
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
        AudioManager.instance.Play(Random.value > 0.5 ? "faucetsqueak_1_oneshot" : "faucetsqueak_2_oneshot", transform);
        if (isPouring)
        {
            if (currentLoop != null)
            {
                StartCoroutine(currentLoop.GetComponent<Loopable>().LerpDestroySelf(0.0f, 2.0f));
                currentLoop = null;
            }
            isPouring = false;
            Button.GetComponent<TextMeshProUGUI>().text = "Poke to turn on faucet";
            EndPour();
            // turn off stream
        }
        else
        {
            currentLoop = AudioManager.instance.LerpLoopable("faucetrunning_loop", transform, 2.0f);
            isPouring = true;
            Button.GetComponent<TextMeshProUGUI>().text = "Poke to turn off faucet";
            StartPour(); 
        }
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
        return steamObj.GetComponent<Stream>();

    }
}
