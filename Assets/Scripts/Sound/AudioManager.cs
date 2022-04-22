using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region singleton
    static private AudioManager _instance;
    static public AudioManager Instance
    {
        get
        {
            return _instance;
        }
        
    }
    #endregion

    public Sound[] sounds;

    void Awake()
    {
        _instance = this;

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) Debug.LogError($"ERROR: El sonido {name} no se encuentra o no existe (¿error al escribir el nombre del sonido?)");
        else s.source.Play();
    }

    public void StopPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) Debug.LogError($"ERROR: No se puede parar el sonido {name} porque no existe o no se encuentra (¿error al escribir el nombre del sonido?)");
        else
        {
            s.source.Stop();
        }
    }

    public void PlayInterval(string name, float toSeconds, float fromSeconds = 0f)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) Debug.LogError($"ERROR: El sonido {name} no se encuentra o no existe (¿error al escribir el nombre del sonido?)");
        else
        {
            if(!s.source.isPlaying)
            {
                s.source.Play();
                s.source.SetScheduledEndTime(AudioSettings.dspTime + (toSeconds - fromSeconds));
            }       
        }
    }

    public void PlayUntilEnd(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) Debug.LogError($"ERROR: El sonido {name} no se encuentra o no existe (¿error al escribir el nombre del sonido?)");
        else
        {
            if(!s.source.isPlaying)
            {
                s.source.Play();
            }
        }
    }

    public void PlayAfter(float seconds, string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) Debug.LogError($"ERROR: El sonido {name} no se encuentra o no existe (¿error al escribir el nombre del sonido?)");
        else s.source.PlayDelayed(seconds);
    }
}
