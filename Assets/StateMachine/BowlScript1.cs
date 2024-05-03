using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlScript1 : MonoBehaviour
{

    private Outline OutlineScript;

    // Start is called before the first frame update
    void Start()
    {
        OutlineScript = gameObject.GetComponent<Outline>();
    }

    public void StartingState()
    {
        OutlineScript.enabled = true;
    }

    public void EndingState()
    {
        gameObject.enabled = false;
    }
}
