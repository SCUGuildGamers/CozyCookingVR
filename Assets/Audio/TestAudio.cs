using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudio : MonoBehaviour
{
    [SerializeField]
    string soundName, loopName;
    [SerializeField]
    bool isOn = false;
    GameObject currentLoop = null; // have this line for scripts where you want audio to play

    private void Start()
    {
        Debug.Log(soundName);
    }

    public void Activate()
    {
        if (soundName != "")
            AudioManager.instance.Play(soundName, transform); // copy paste this line when you want a oneshot sound to play
        if (loopName != "")
        {
            if (!isOn)
            {
                //turn on
                currentLoop = AudioManager.instance.LerpLoopable(loopName, transform, 2.0f); // copy paste this line when you want a loopable sound to play 
                isOn = true;
            }
            else
            {
                //turn off
                if (currentLoop != null)
                {
                    StartCoroutine(currentLoop.GetComponent<Loopable>().LerpDestroySelf(0.0f, 2.0f)); // copy paste this line when you want to kill a loopable sound
                    currentLoop = null;
                }
                isOn = false;
            }
        }
    }
}
