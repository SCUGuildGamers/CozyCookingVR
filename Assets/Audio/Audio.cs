using UnityEngine;

public class Audio : ScriptableObject
{
    public AudioClip clip;
    public bool loop;
    public float pitch;
    public bool playOnAwake;
    [Range(0f,1f)]
    public float volume;
}
