using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager manager;

    public Slider musicVolumeAdjust, soundVolumeAdjust; //Reference to our volume sliders

    public Audio[] getAudio;

    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }

        foreach (Audio a in getAudio)
        {
            a.source = gameObject.AddComponent<AudioSource>();

            a.source.clip = a.clip;

            a.source.volume = a.volume;
            a.source.pitch = a.pitch;
            a.source.loop = a.enableLoop;
        }
    }

    public void Play(string name)
    {
        Audio a = Array.Find(getAudio, sound => sound.name == name);
        if (a == null)
        {
            Debug.LogWarning("Sound name " + name + " was not found.");
            return;
        } else
        {
            a.source.Play();
        }
    }
    public void Stop(string name)
    {
        Audio a = Array.Find(getAudio, sound => sound.name == name);
        if (a == null)
        {
            Debug.LogWarning("Sound name " + name + " was not found.");
            return;
        }
        else
        {
            a.source.Stop();
        }
    }

    public void Volume(string name, float value)
    {
        Audio a = Array.Find(getAudio, sound => sound.name == name);
        if (a == null)
        {
            Debug.LogWarning("Sound name " + name + " was not found.");
        } else
        {
            a.source.volume = value;
        }
    }
}
