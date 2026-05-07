using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FirstAidManager : MonoBehaviour
{
    public float timer = 3f;
    public bool isGameActive = true;
    public bool isCleaned = false; // ¿Ya se limpió la herida?
    
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI statusText; // Para mostrar "¡Ganaste!" o "¡Perdiste!"

    void Start()
    {
        if(statusText) statusText.text = "¡Limpia y pon la curita!";
    }

    void Update()
    {
        if (!isGameActive) return;

        timer -= Time.deltaTime; // Resta el tiempo real
        
        if (timeText) 
            timeText.text = timer.ToString("F1") + "s"; // Muestra con 1 decimal (ej: 2.5s)

        if (timer <= 0)
        {
            Lose();
        }
    }

    public void Win()
    {
        isGameActive = false;
        if(statusText) statusText.text = "¡Salvado!";
        Debug.Log("¡Minijuego Superado!");
        Invoke("VolverAlMapa", 2.5f);
        // Aquí volverías al MapManager
    }

    public void Lose()
    {
        isGameActive = false;
        if(statusText) statusText.text = "¡Muy lento! Perdiste.";
        Debug.Log("¡Tiempo agotado!");
        Invoke("VolverAlMapa", 2.5f);
    }
    void VolverAlMapa()
{
    // Escribe aquí el nombre exacto de tu escena del mapa
    SceneManager.LoadScene("MapaCentral");
}
}
