using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManeger : MonoBehaviour
{
    public Sound[] sSounds;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (var s in sSounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.loop;
            s.Source.spatialBlend = s.spatialBlend;
            s.Source.minDistance = s.MinDistance;
            s.Source.maxDistance = s.MaxDistance;
        }
    }
    private void Start()
    {
       Play("Theme");
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sSounds, sound => sound.Name == name);
        if(s == null)
        {
            Debug.LogWarning($"Sound: {name} not found");
            return;
        }
        s.Source.Play();
    }
    public AudioSource AudioSound(string name)
    {
        Sound s = Array.Find(sSounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound: {name} not found");
            return null;
        }
        return s.Source;
    }
    public void Play(string name, Vector3 position)
    {
        Sound s = Array.Find(sSounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound: {name} not found");
            return;
        }
        s.Source.transform.position = position;
        s.Source.Play();
    }


}
