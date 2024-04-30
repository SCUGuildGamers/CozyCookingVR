using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faucet : MonoBehaviour
{
    [SerializeField]
    bool isOn = false;
    GameObject currentLoop = null;

    public void Activate()
    {
        AudioManager.instance.Play(Random.value > 0.5 ? "faucetsqueak_1_oneshot" : "faucetsqueak_2_oneshot", transform);
        if (!isOn)
        {
            //turn on
            currentLoop = AudioManager.instance.LerpLoopable("faucetrunning_loop", transform, 2.0f);
            isOn = true;
        }
        else
        {
            //turn off
            if (currentLoop != null)
            {
                StartCoroutine(currentLoop.GetComponent<Loopable>().LerpDestroySelf(0.0f, 2.0f));
                currentLoop = null;
            }
            isOn = false;
        }
    }
}
