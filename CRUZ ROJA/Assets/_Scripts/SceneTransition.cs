using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance; 

    [Header("Configuración Visual")]
    public Image fadeImage; // La imagen a todo color que tapará la pantalla
    public float fadeSpeed = 3f; // Qué tan rápido hace la transición

    void Awake()
    {
        // Si no hay transicionador, este se vuelve el oficial y sobrevive a los viajes.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            // Si ya hay uno viajando con nosotros y volvemos al menú, destruimos el clon.
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Al arrancar, si la pantalla está tapada, la destapamos suavemente
        if (fadeImage != null)
        {
            StartCoroutine(FadeIn());
        }
    }

    // ESTA ES LA FUNCIÓN QUE LLAMAREMOS PARA VIAJAR
    public void CambiarEscena(string nombreEscena)
    {
        StartCoroutine(FadeOutAndLoad(nombreEscena));
    }

    IEnumerator FadeIn()
    {
        fadeImage.raycastTarget = true; // Bloquea clics mientras se destapa
        float t = 1f;
        while (t > 0f)
        {
            // unscaledDeltaTime ignora si el juego está pausado (Time.timeScale = 0)
            t -= Time.unscaledDeltaTime * fadeSpeed;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, t);
            yield return null;
        }
        fadeImage.raycastTarget = false; // Permite hacer clic de nuevo
    }

    IEnumerator FadeOutAndLoad(string nombreEscena)
    {
        fadeImage.raycastTarget = true; // Bloquea clics para que no toquen nada más
        float t = 0f;
        
        // 1. Tapamos la pantalla (Fade Out)
        while (t < 1f)
        {
            t += Time.unscaledDeltaTime * fadeSpeed;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, t);
            yield return null;
        }

        // 2. Viajamos en el tiempo (Cargamos la escena)
        SceneManager.LoadScene(nombreEscena);

        // 3. Destapamos la pantalla en el nuevo destino (Fade In)
        StartCoroutine(FadeIn());
    }
}
