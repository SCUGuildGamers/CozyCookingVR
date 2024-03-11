using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Respawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Cubert;
    public GameObject Cube2;
    public GameObject tempBottle;

    private bool tilt = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Instantiate(Cubert, new Vector3(0f,.83f,1.5f), transform.rotation);
            Instantiate(Cubert, new Vector3(-.4f, .83f, 1.5f), transform.rotation);
            Instantiate(Cubert, new Vector3(.4f, .83f, 1.5f), transform.rotation);
            Instantiate(Cubert, new Vector3(0f, 1.23f, 1.5f), transform.rotation);
            Instantiate(Cubert, new Vector3(0f, .43f, 1.5f), transform.rotation);
            Instantiate(Cubert, new Vector3(-.4f, .43f, 1.5f), transform.rotation);
            Instantiate(Cubert, new Vector3(-.4f, 1.23f, 1.5f), transform.rotation);
            Instantiate(Cubert, new Vector3(.4f, .43f, 1.5f), transform.rotation);
            Instantiate(Cubert, new Vector3(.4f, 1.23f, 1.5f), transform.rotation);
        }
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            StartCoroutine(startStream());
        }
        if (Keyboard.current.backslashKey.wasPressedThisFrame)
        {
            Debug.Log("tilting");
            if (!tilt)
            {
                tempBottle.transform.Rotate(0f, 0f, 120f, Space.Self);
                tilt = !tilt;
            }
            else
            {
                tempBottle.transform.Rotate(0f, 0f, -120f, Space.Self);
                tilt = !tilt;
            }
            
        }
    }
    public IEnumerator startStream()
    {
        for(int i = 0; i < 5; i++)
        {
            Instantiate(Cube2, new Vector3(.4f, .4f, 8f), transform.rotation);
            yield return new WaitForSeconds(.4f);
            Instantiate(Cube2, new Vector3(-.4f, .4f, 8f), transform.rotation);
            yield return new WaitForSeconds(.4f);
            Instantiate(Cube2, new Vector3(.4f, 1.2f, 8f), transform.rotation);
            yield return new WaitForSeconds(.4f);
            Instantiate(Cube2, new Vector3(-.4f, 1.2f, 8f), transform.rotation);
            yield return new WaitForSeconds(.4f);
            //yield return new WaitForSeconds(1f);
        }
    }
}
