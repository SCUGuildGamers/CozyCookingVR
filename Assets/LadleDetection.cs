using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadleDetection : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SoupObject;

    public bool hasSoup = false;
    void Start()
    {
        SoupObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ladle")
        {
            SoupObject.SetActive(true);
            hasSoup = true;
            Debug.Log("soup in ladle");
        }   
        if(other.gameObject.tag == "Bowl")
        {
            SoupObject.SetActive(false);
            hasSoup = false;
            // call some function with getcomponent to increase the size/height of the soup level in the bowl
            other.gameObject.GetComponent<BowlScript1>().FillBowl();
            Debug.Log("soup in bowl");
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

}
