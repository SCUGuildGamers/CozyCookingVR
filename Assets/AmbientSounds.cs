using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSounds : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject currentLoop = null;
    void Start()
    {
        currentLoop = AudioManager.instance.LerpLoopable("sfx_natureambience", transform, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
