using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(CanvasGroup))]

public class PantallaInstrucciones : MonoBehaviour
{
    [Header("Configuración de Instrucción")]
    public float tiempoVisible = 1.5f; // Cuántos segundos se queda en pantalla
    public float velocidadTransicion = 4f; // Qué tan rápido desaparece

    private CanvasGroup canvasGroup;

    void Start()
    {
        // 1. ¡CONGELAMOS EL JUEGO AL INSTANTE!
        // Como el timeScale es 0, los cronómetros de tus Managers ni se van a enterar.
        Time.timeScale = 0f; 
        
        canvasGroup = GetComponent<CanvasGroup>();
        
        // Arrancamos la rutina mágica
        StartCoroutine(SecuenciaInstruccion());
    }

    IEnumerator SecuenciaInstruccion()
    {
        // 2. Esperamos el tiempo definido
        // Usamos Realtime porque el tiempo normal de Unity está congelado en 0
        yield return new WaitForSecondsRealtime(tiempoVisible);

        // 3. Transición de Salida (Efecto Dumb Ways to Die)
        float t = 0;
        Vector3 escalaInicial = transform.localScale;
        Vector3 escalaFinal = escalaInicial * 1.5f; // Se agranda un 50%

        while (t < 1)
        {
            // Usamos unscaledDeltaTime para poder animar aunque el juego esté pausado
            t += Time.unscaledDeltaTime * velocidadTransicion; 
            
            // Va bajando la opacidad de 1 a 0
            canvasGroup.alpha = Mathf.Lerp(1, 0, t); 
            
            // Va creciendo un poquito para dar ese efecto de "¡Hacia tu cara!"
            transform.localScale = Vector3.Lerp(escalaInicial, escalaFinal, t);
            
            yield return null;
        }

        // 4. ¡A JUGAR! Descongelamos el juego y apagamos este panel
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
