using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMiniManager : MonoBehaviour
{
    public GameObject panelAjustes;
    public bool juegoPausado = false;

    public void Reanudar()
    {
        panelAjustes.SetActive(false);
        Time.timeScale = 1;
        juegoPausado = false;
    }
    
    public void Pausar()
    {
        panelAjustes.SetActive(true);
        Time.timeScale = 0;
        juegoPausado = true;
    }

    public void MenuPrincipalButton()
    {
        SceneTransition.instance.CambiarEscena("MenuInicio");
    }

    public void MapCentral()
    {
        SceneTransition.instance.CambiarEscena("MapaCentral");
    }
}
