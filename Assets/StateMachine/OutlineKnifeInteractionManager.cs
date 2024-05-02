using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineKnifeInteractionManager : MonoBehaviour
{

    private Outline OutlineScript;
    public GameObject theKnife;

    // Start is called before the first frame update
    void Start()
    {
        OutlineScript = gameObject.GetComponent<Outline>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == theKnife)
        {
            OutlineScript.enabled = false;
        }
    }

    /*
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject == theKnife)
        {
            OutlineScript.enabled = true;
        }
    }
    */
}
