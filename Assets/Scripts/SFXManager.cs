using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    public AudioSource[] audioSources;

    [HideInInspector] public AudioSource soundsAudioSource;
    [HideInInspector] public AudioSource musicAudioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSources = GetComponents<AudioSource>();
        soundsAudioSource = audioSources[0];
        musicAudioSource = audioSources[1];
    }

    public void EjecutarSonido(AudioClip sonido)
    {
        soundsAudioSource.PlayOneShot(sonido);
    }

    public void CambiarM�sica(AudioClip musica, bool bucle)
    {
        if (soundsAudioSource.isPlaying) soundsAudioSource.Stop();
        musicAudioSource.clip = musica;
        if (bucle)
        {
            musicAudioSource.loop = true;
        }
        else
        {
            musicAudioSource.loop = false;
        }
        musicAudioSource.Play();
    }

    public void EfectsVolume(float value)
    {
        soundsAudioSource.volume = value;
    }

    public void MusicVolume(float value)
    {
        musicAudioSource.volume = value;
    }
}
