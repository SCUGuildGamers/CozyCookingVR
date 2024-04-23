using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudio : MonoBehaviour
{
    [SerializeField]
    string soundName, loopName;
    [SerializeField]
    bool isOn = false;
    GameObject currentLoop = null;

    private void Start()
    {
        Debug.Log(soundName);
    }

    public void Activate()
    {
        if (soundName != "")
            AudioManager.instance.Play(soundName, transform);
        if (loopName != "")
        {
            if (!isOn)
            {
                //turn on
                currentLoop = AudioManager.instance.LerpLoopable(loopName, transform, 2.0f);
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
}
