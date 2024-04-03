using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Deriving another class becuase I can I guess?
public class OnionStateManager : Sliceable 
{
    // Ignore
    void Start()
    {
        
    }

    void Update()
    {
        if(sliceCount == 0)
        {
            Debug.Log("Onion Slicer reached 0");
            SendMessageUpwards("OnionSliced");
        }
    }

    private void OnionCutStart()
    {
        // Use this space to initilzie any objects that need to be initilized.  
        // Things like the bowl and the knife.
        
    }
}
