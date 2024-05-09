using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotSteamDetector : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Pot;

    public bool pickedUp = false;

    public GameObject Steam;
    void Start()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Lid")
        {
            // Steam.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lid")
        {
            //Steam.Stop();   
        }
    }

    public void PickedUp()
    {
        pickedUp = true;
    }
    public void PutDown()
    {
        pickedUp = false;
    }
}
