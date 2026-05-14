using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider sliderMusica;
    public Slider sliderSFX;

    void Start()
    {
        // Cuando abres el menú de pausa/ajustes, el Slider debe ponerse en la posición correcta
        if (sliderMusica != null)
        {
            sliderMusica.value = PlayerPrefs.GetFloat("VolumenMusica", 0.5f);
            sliderMusica.onValueChanged.AddListener(ActualizarVolumenMusica);
        }
        if (sliderSFX != null)
        {
            sliderSFX.value = PlayerPrefs.GetFloat("VolumenSFX", 0.5f);
            sliderSFX.onValueChanged.AddListener(ActualizarVolumenSFX);
        }
    }

    // Función que recibe el número del slider (entre 0 y 1)
    public void ActualizarVolumenMusica(float valor)
    {
        // Buscamos a nuestro inmortal y le pasamos el nuevo volumen
        if (AudioManager.instance != null)
        {
            AudioManager.instance.CambiarVolumen(valor);
        }
    }
    public void ActualizarVolumenSFX(float valor)
    {
        if (SFXManager.instance != null)
            SFXManager.instance.CambiarVolumenSFX(valor);
    }
}
