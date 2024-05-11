using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeSound : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject currentLoop = null;
    void Start()
    {
        currentLoop = AudioManager.instance.LerpLoopable("sfx_fridgesound", transform, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
