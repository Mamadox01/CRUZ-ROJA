using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Paneles UI")]
    public GameObject panelCreditos; // Arrastra un panel con los nombres de tu equipo aquí

    void Start()
    {
        // Aseguramos que los créditos estén ocultos al empezar
        if(panelCreditos != null) panelCreditos.SetActive(false);
    }

    // Función para el botón START
    public void StartGame()
    {
        // Cambia "MapaCentral" por el nombre exacto de tu escena de mapa
        SceneManager.LoadScene("MapaCentral");
    }

    // Función para el botón CREDITS
    public void ToggleCredits(bool show)
    {
        if(panelCreditos != null) panelCreditos.SetActive(show);
    }

    public void CloseButton()
    {
        SceneManager.LoadScene("MenuInicio");
    }

    // Función para el botón EXIT
    public void ExitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
