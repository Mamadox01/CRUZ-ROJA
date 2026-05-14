using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; 
    
    private AudioSource bgmSource;

    void Awake()
    {
        // La misma magia que usamos para la transición: si no existe, este se vuelve el oficial
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ¡Se vuelve inmortal!
            
            bgmSource = GetComponent<AudioSource>();
            
            // Cargamos el volumen que el jugador haya guardado antes. 
            // Si es la primera vez que juega, arranca en 0.5f (50% de volumen) para no reventarle los oídos.
            bgmSource.volume = PlayerPrefs.GetFloat("VolumenMusica", 0.5f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Esta función la va a llamar nuestro Slider
    public void CambiarVolumen(float nuevoVolumen)
    {
        if (bgmSource != null)
        {
            bgmSource.volume = nuevoVolumen;
            
            // Guardamos el nuevo valor en el celular al instante
            PlayerPrefs.SetFloat("VolumenMusica", nuevoVolumen);
            PlayerPrefs.Save(); 
        }
    }
}
