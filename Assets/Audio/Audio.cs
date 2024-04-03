using System.Collections;
using UnityEngine;

public class Audio : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("DestroySelf", GetComponent<AudioSource>().clip.length);
    }

    private IEnumerator DestroySelf(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}
