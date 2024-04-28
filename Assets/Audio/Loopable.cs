using System.Collections;
using UnityEngine;

public class Loopable : MonoBehaviour
{
    public float volume = 1f;

    public IEnumerator Lerp(float volume, float delay)
    {
        AudioSource loopable = GetComponent<AudioSource>();

        float currentTime = 0f;
        float prevVolume = loopable.volume;

        while (currentTime < delay)
        {
            loopable.volume = Mathf.Lerp(prevVolume, volume, currentTime / delay);
            currentTime += Time.deltaTime;
            yield return null;
        }
        loopable.volume = volume;
    }

    public IEnumerator DestroySelf(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }

    public IEnumerator LerpDestroySelf(float volume, float delay)
    {
        AudioSource loopable = GetComponent<AudioSource>();

        float currentTime = 0f;
        float prevVolume = loopable.volume;

        while (currentTime < delay)
        {
            loopable.volume = Mathf.Lerp(prevVolume, volume, currentTime / delay);
            currentTime += Time.deltaTime;
            yield return null;
        }
        loopable.volume = volume;

        Destroy(this.gameObject);
    }
}
