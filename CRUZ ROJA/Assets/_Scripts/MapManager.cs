using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class MapManager : MonoBehaviour
{
    public TextMeshProUGUI recordMini1;
    public TextMeshProUGUI recordMini2;
    public TextMeshProUGUI recordMini3;
    public TextMeshProUGUI recordMini4;
    public TextMeshProUGUI recordMini5;
    public GameObject panelAjustes;

    void Start()
    {
        ActualizarScoreboard();
    }

    void ActualizarScoreboard()
    {
        // PlayerPrefs.GetInt busca un número guardado. Si no existe (es la primera vez que juega), devuelve un 0.
        if (recordMini1 != null) recordMini1.text = "Récord: " + PlayerPrefs.GetInt("Record_Mini1", 0);
        if (recordMini2 != null) recordMini2.text = "Récord: " + PlayerPrefs.GetInt("Record_Mini2", 0);
        if (recordMini3 != null) recordMini3.text = "Récord: " + PlayerPrefs.GetInt("Record_Mini3", 0);
        if (recordMini4 != null)
        {
            float recordTime = PlayerPrefs.GetFloat("Record_Mini4", 0f);
            recordMini4.text = "Mejor: " + recordTime.ToString("F1") + "s";
        } 
        if (recordMini5 != null) recordMini5.text = "Récord: " + PlayerPrefs.GetInt("Record_Mini5", 0);
    }
    public void CargarMinijuego(string nombreEscena)
    {
        SceneTransition.instance.CambiarEscena(nombreEscena);
    }

    // Opcional: un botón para salir de la app
    public void SalirDelJuego()
    {
        Debug.Log("Cerrando el juego...");
        Application.Quit();
    }

    public void ToggleSettings(bool show)
    {
        if(panelAjustes != null) panelAjustes.SetActive(show);
    }

    public void CloseButton()
    {
        if(panelAjustes != null) panelAjustes.SetActive(false);
    }

    public void MenuBack()
    {
        SceneTransition.instance.CambiarEscena("MenuInicio");
    }
    public void ResetearRecords()
    {
        // Borramos cada clave que creamos
        PlayerPrefs.DeleteKey("Record_Mini1");
        PlayerPrefs.DeleteKey("Record_Mini2");
        PlayerPrefs.DeleteKey("Record_Mini3");
        PlayerPrefs.DeleteKey("Record_Mini4");
        PlayerPrefs.DeleteKey("Record_Mini5");

        // Opcional: Si quieres borrar ABSOLUTAMENTE TODO (incluyendo volumen y ajustes)
        // PlayerPrefs.DeleteAll(); 

        // Guardamos los cambios en el disco
        PlayerPrefs.Save();

        Debug.Log("Scoreboard reseteado con éxito.");

        // ¡Súper importante! Llamamos a la función que ya teníamos para que
        // los textos en el mapa se actualicen a 0 inmediatamente.
        ActualizarScoreboard();
    }

}
