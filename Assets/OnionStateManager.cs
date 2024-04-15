using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnionStateManager : Sliceable 
{

    private Sliceable SliceableComponent;
    // 
    void Start()
    {
        
    }

    void Update()
    {
        if(sliceCount == 0)
        {
            Debug.Log("Onion slice count reached 0");
            SendMessageUpwards("OnionSliced");
        }
    }

    private void OnionCutStart()
    {
        // Use this space to initilzie any objects that need to be initilized.  
        // Things like the bowl and the knife.
        Debug.Log("OnionCutStart sucessfully reached");

        // Get relevant components
        SliceableComponent = GetComponent<Sliceable>();
        sliceCount = SliceableComponent.sliceCount;
    }
}
