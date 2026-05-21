using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;

public class MapManager : MonoBehaviour
{
    [Header("UI Reset")]
    public GameObject panelConfirmacion;
    public TextMeshProUGUI recordMini1;
    public TextMeshProUGUI recordMini2;
    public TextMeshProUGUI recordMini3;
    public TextMeshProUGUI recordMini4;
    public TextMeshProUGUI recordMini5;

    [Header("Botones del Mapa (Imágenes)")]
    public Image botonMini1;
    public Image botonMini2;
    public Image botonMini3;
    public Image botonMini4;
    public Image botonMini5;

    [Header("Colores de Estado")]
    public Color colorPendiente = Color.red; // Color cuando no lo ha jugado
    public Color colorCompletado = Color.green;
    public GameObject panelAjustes;

    void Start()
    {
        if (panelConfirmacion != null) 
        {
            panelConfirmacion.SetActive(false);
        }
        ActualizarScoreboard();
        ActualizarColoresBotones();
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
    public void ActualizarColoresBotones()
    {
        // Usamos PlayerPrefs.HasKey para saber si el récord existe (es decir, si ya jugó)
        if (botonMini1 != null) 
            botonMini1.color = PlayerPrefs.HasKey("Record_Mini1") ? colorCompletado : colorPendiente;
            
        if (botonMini2 != null) 
            botonMini2.color = PlayerPrefs.HasKey("Record_Mini2") ? colorCompletado : colorPendiente;
            
        if (botonMini3 != null) 
            botonMini3.color = PlayerPrefs.HasKey("Record_Mini3") ? colorCompletado : colorPendiente;
            
        if (botonMini4 != null) 
            botonMini4.color = PlayerPrefs.HasKey("Record_Mini4") ? colorCompletado : colorPendiente;
            
        if (botonMini5 != null) 
            botonMini5.color = PlayerPrefs.HasKey("Record_Mini5") ? colorCompletado : colorPendiente;
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

        PlayerPrefs.Save();

        Debug.Log("Scoreboard reseteado con éxito.");

        ActualizarScoreboard();
        ActualizarColoresBotones();
        OcultarPanelConfirmacion();
    }
    public void MostrarPanelConfirmacion()
    {
        if (panelConfirmacion != null)
        {
            panelConfirmacion.SetActive(true);
        }
    }
    public void OcultarPanelConfirmacion()
    {
        if (panelConfirmacion != null)
        {
            panelConfirmacion.SetActive(false);
        }
    }

}
