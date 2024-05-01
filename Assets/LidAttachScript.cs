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

    void Start()
    {
        potTransform = Pot.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Lid")
        {
            Debug.Log("Put it down!");
            if (!pickedUp && !parentSet)
            {
                Debug.Log("Set parent!");
                other.gameObject.transform.SetParent(potTransform, true);
                other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                parentSet = true;
            }
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
