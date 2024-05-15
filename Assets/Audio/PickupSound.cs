using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PickupSound : MonoBehaviour
{
    public List<string> soundList;

    public void OnGrabbed()
    {
        AudioManager.instance.RandomPlay(soundList, transform);
    }
}
