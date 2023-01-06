using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip Clip;
    [Range(0f, 1f)]
    public float Volume;
    [Range(.1f, 3f)]
    public float Pitch;
    [Range(0f, 1f)]
    public float spatialBlend;
    [Range(1f, 500f)]
    public float MinDistance;
    [Range(1f, 500f)]
    public float MaxDistance;
    public bool loop;
    [HideInInspector]
    public AudioSource Source;
}
