using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    //public static AudioManager singleton;

    public Sound[] sounds;
    //AudioSource audioSource;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.am;
        }
        //singleton = this;
        //audioSource = GetComponent<AudioSource>();
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.Stop();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.Pause();
    }

    public void SetVolume(string name, float volume)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.volume = volume;
    }

    /*public void PlaySound(int clipIndex)
    {
        audioSource.PlayOneShot(clips[clipIndex]);
    }*/
}
