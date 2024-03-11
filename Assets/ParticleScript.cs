using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Material liquidFill;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Fillable")
        {
            Debug.Log("should be filling");
            liquidFill = collision.gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material;
            liquidFill.SetFloat("_fill", liquidFill.GetFloat("_fill") + 0.0005f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Fillable")
        {
            Debug.Log("hit fillable object");
        }
    }
}
