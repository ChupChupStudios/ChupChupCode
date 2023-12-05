using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour
{
    [SerializeField] Slider efectos;
    [SerializeField] Slider musica;

    void Start()
    {
        efectos.value = SFXManager.Instance.soundsAudioSource.volume;
        musica.value = SFXManager.Instance.musicAudioSource.volume;
        efectos.onValueChanged.AddListener(VolumenEfectos);
        musica.onValueChanged.AddListener(VolumenMusica);
    }

    private void VolumenEfectos(float volumen)
    {
        SFXManager.Instance.EfectsVolume(volumen);
    }

    private void VolumenMusica(float volumen)
    {
        SFXManager.Instance.MusicVolume(volumen);
    }
}
