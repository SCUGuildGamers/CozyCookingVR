using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveDetector : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Fillable")
        {
            other.gameObject.GetComponent<PotDetector>().SetOnStove();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Fillable")
        {
            other.gameObject.GetComponent<PotDetector>().TakeOffStove();
        }
    }
}
