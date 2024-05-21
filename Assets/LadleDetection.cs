using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadleDetection : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SoupObject;

    public bool hasSoup = false;

    GameObject currentLoop = null;
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
        if (other.gameObject.tag == "Ladle" && !hasSoup)
        {
            SoupObject.SetActive(true);
            hasSoup = true;
            AudioManager.instance.Play("sfx_ladlingsoupenter", transform);
        }   
        if(other.gameObject.tag == "Bowl" && hasSoup)
        {
            other.gameObject.GetComponent<BowlScript1>().FillBowl();
            SoupObject.SetActive(false);
            hasSoup = false;
            // call some function with getcomponent to increase the size/height of the soup level in the bowl
            AudioManager.instance.Play("sfx_ladlingsouppour", transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

}
