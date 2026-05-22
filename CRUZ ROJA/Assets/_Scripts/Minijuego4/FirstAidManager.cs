using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FirstAidManager : MonoBehaviour
{
   [Header("Configuración de Rondas")]
    public int maxRounds = 3;
    public float[] timesPerRound;
    private int currentRound = 1;
    private float totalTimeSaved = 0f;

    [Header("Elementos a Randomizar")]
    public GameObject herida;
    public GameObject algodon;
    public GameObject curita;

    [Header("Límites de Pantalla (Rango Aleatorio)")]
    public float minX = -6f;
    public float maxX = 6f;
    public float minY = -3f;
    public float maxY = 3f;

    [Header("UI")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI roundText;

    private float currentTimer;
    public bool isRoundActive = false;
    public bool isCleaned = false;
    private bool isGameOver = false;

    void Start()
    {
        Time.timeScale = 1f;
        currentRound = 1;
        totalTimeSaved = 0f;
        if (timesPerRound == null || timesPerRound.Length < maxRounds)
        {
            Debug.LogError("¡Oye mi rey! Falta configurar los tiempos en el arreglo 'Times Per Round'.");
        }
        StartNewRound();
    }

    void StartNewRound()
    {
        isRoundActive = true;
        isCleaned = false;

        currentTimer = timesPerRound[currentRound - 1];
        
        if (roundText) roundText.text = "Ronda: " + currentRound + "/" + maxRounds;
        if (statusText) statusText.text = "¡Veloz!";

        algodon.SetActive(true);
        curita.SetActive(true);

        // En rondas 2 y 3, movemos las cosas de lugar
        if (currentRound > 1)
        {
            RandomizeElements();
        }

        // Resetear estado de la herida
        herida.GetComponent<Wound>().ResetWound();
    }

    void Update()
    {
        if (!isRoundActive || isGameOver) return;

        currentTimer -= Time.deltaTime;
        if (timeText) timeText.text = currentTimer.ToString("F1") + "s";

        if (currentTimer <= 0)
        {
            LoseGame();
        }
    }

    // Se llama desde la curita cuando se pone bien
    public void WinRound()
    {
        if (!isRoundActive) return;
        isRoundActive = false;
        herida.GetComponent<Wound>().AplicarCurita();

        totalTimeSaved += currentTimer; // Acumulamos el tiempo que sobró
        statusText.text = "¡Bien!";
        
        if (currentRound >= maxRounds)
        {
            Invoke("WinGame", 1.0f);
        }
        else
        {
            currentRound++;
            Invoke("StartNewRound", 0.8f);
        }
    }

    void RandomizeElements()
    {
        // Movemos la herida a un punto al azar
        herida.transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
        
        // Movemos el algodón y la curita a otros puntos para que no estorben
        algodon.transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
        curita.transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
    }

    void WinGame()
    {
        isGameOver = true;
        statusText.text = "¡Excelente!";

        // GUARDAR SCOREBOARD (Tiempo total ahorrado en las 3 rondas)
        float mejorRecord = PlayerPrefs.GetFloat("Record_Mini4", 0f);
        if (totalTimeSaved > mejorRecord)
        {
            PlayerPrefs.SetFloat("Record_Mini4", totalTimeSaved);
            PlayerPrefs.Save();
        }

        Invoke("VolverAlMapa", 2.0f);
    }

    void LoseGame()
    {
        isGameOver = true;
        statusText.text = "¡Muy lento!";
        Invoke("VolverAlMapa", 2.0f);
    }

    void VolverAlMapa()
    {
        SceneTransition.instance.CambiarEscena("MapaCentral");
    }
}
