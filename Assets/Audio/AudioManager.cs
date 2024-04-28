using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance; //singleton
    public static AudioManager instance
    {
        get
        {
            if (!_instance)
            {
                Debug.Log("No instance!");
                var prefab = Resources.Load<GameObject>("AudioManager");
                var inScene = Instantiate(prefab);
                _instance = inScene.GetComponentInChildren<AudioManager>();
                if (!_instance) _instance = inScene.AddComponent<AudioManager>();
                DontDestroyOnLoad(_instance.transform.root.gameObject);
            }
            return _instance;
        }
    }

    private static System.Random rnd = new System.Random();

    [SerializeField]
    private GameObject[] sounds;
    [SerializeField]
    private GameObject[] musicsList;
    private List<AudioSource> musics;

    #region Sound Effects

    public float Play(string name, Transform parent)
    {
        GameObject audio = System.Array.Find(sounds, sound => sound.name == name);

        if(audio == null)
        {
            Debug.Log("Sound: " + name + " not found");
            return 0;
        }

        return Instantiate(audio, parent, false).GetComponent<AudioSource>().clip.length;
    }

    public IEnumerator Play(string name, Transform parent, float delay)
    {
        GameObject audio = System.Array.Find(sounds, sound => sound.name == name);

        if (audio == null)
        {
            Debug.Log("Sound: " + name + " not found");
            yield break;
        }

        yield return new WaitForSeconds(delay);
        GameObject prefab = Instantiate(audio, parent, false);
    }

    public IEnumerator PlayInOrder(List<string> soundList, Transform parent)
    {
        foreach (string name in soundList)
        {
            GameObject audio = System.Array.Find(sounds, sound => sound.name == name);

            if (audio == null)
            {
                Debug.Log("Sound: " + name + " not found");
                continue;
            }

            GameObject prefab = Instantiate(audio, parent, false);
            yield return new WaitForSeconds(prefab.GetComponent<AudioSource>().clip.length);
        }
    }

    public float RandomPlay(List<string> soundList, Transform parent)
    {
        int index = rnd.Next(soundList.Count);
        GameObject audio = System.Array.Find(sounds, sound => sound.name == soundList[index]);
        if (audio == null)
        {
            Debug.Log("Sound: " + name + " not found");
            return 0f;
        }
        return Instantiate(audio, parent, false).GetComponent<AudioSource>().clip.length;
    }

    #endregion

    #region Loopable

    public GameObject LerpLoopable(string name, Transform parent, float delay)
    {
        GameObject audio = System.Array.Find(sounds, sound => sound.name == name);

        if (audio == null)
        {
            Debug.Log("Loopable: " + name + " not found");
            return null;
        }

        Loopable loopable = Instantiate(audio, parent, false).GetComponent<Loopable>();
        StartCoroutine(loopable.Lerp(loopable.volume, delay));

        return loopable.gameObject;
    }

    #endregion

    #region Music

    public void InstantiateMusics()
    {
        foreach (GameObject music in musicsList)
        {
            AudioSource audio = Instantiate(music, transform, false).GetComponent<AudioSource>();
            audio.volume = 0f;
            musics.Add(audio);
        }
    }

    public void SetMusicVolume(string name, float volume)
    {
        AudioSource audio = musics.Find(music => music.name == name);
        if (audio == null)
        {
            Debug.Log("Music: " + name + " not found");
            return;
        }
        audio.volume = volume;
    }

    public IEnumerator LerpMusicVolume(string name, float volume, float delay)
    {
        AudioSource audio = musics.Find(music => music.name == name);
        if (audio == null)
        {
            Debug.Log("Music: " + name + " not found");
            yield break;
        }

        float currentTime = 0f;
        float prevVolume = audio.volume;

        while (currentTime < delay)
        {
            audio.volume = Mathf.Lerp(prevVolume, volume, currentTime / delay);
            currentTime += Time.unscaledDeltaTime;
            yield return null;
        }
        audio.volume = volume;
    }

    #endregion
}
