using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LidAttachScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Pot;

    private Transform potTransform;
    public bool pickedUp = false;
    public bool parentSet = false;

    private Vector3 potPosition = new Vector3(0, 0, 0);
    private PotDetector potScript;
    void Start()
    {
        potTransform = Pot.GetComponent<Transform>();
        potScript = Pot.GetComponent<PotDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Lid")
        {
     
            if (!pickedUp && !parentSet)
            {
             
                potScript.LidOn();
                parentSet = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Lid")
        {
        
                potScript.LidOff();
                parentSet = false;
            
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
