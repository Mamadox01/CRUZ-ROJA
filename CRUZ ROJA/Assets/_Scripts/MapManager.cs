using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public void CargarMinijuego(string nombreEscena)
    {
        Debug.Log("Viajando a la estación: " + nombreEscena);
        SceneManager.LoadScene(nombreEscena);
    }

    // Opcional: un botón para salir de la app
    public void SalirDelJuego()
    {
        Debug.Log("Cerrando el juego...");
        Application.Quit();
    }
}
