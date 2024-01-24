using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * 5 * Time.deltaTime);
        if (transform.position.z  < -1)
        {
            Destroy(this);
        }
    }
}
