using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance; 
    private AudioSource sfxSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
            
            sfxSource = GetComponent<AudioSource>();
            sfxSource.volume = PlayerPrefs.GetFloat("VolumenSFX", 0.5f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CambiarVolumenSFX(float nuevoVolumen)
    {
        if (sfxSource != null)
        {
            sfxSource.volume = nuevoVolumen;
            PlayerPrefs.SetFloat("VolumenSFX", nuevoVolumen);
            PlayerPrefs.Save();
        }
    }

    // Función universal para reproducir cualquier sonido
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip); 
        }
    }
}
