using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WaterPurifierManager : MonoBehaviour
{
    [Header("Configuración del Juego")]
    public int maxRounds = 3;
    public int pillsPerRound = 5;
    public int contaminatedPerRound = 2;

    [Header("Invocación (Prefabs y Spawns)")]
    public GameObject bottlePrefab; 
    public Transform[] spawnPoints;
    
    [Header("UI")]
    public List<Bottle> allBottles;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI pillsText;
    public TextMeshProUGUI roundText;

    private int currentRound = 1;
    private int totalScore = 0;
    private int currentPills;
    private int contaminatedBottlesLeft; // Cuántas botellas sucias
    private List<GameObject> spawnedBottles = new List<GameObject>();

    void Start()
    {
        StartRound();
    }

    void StartRound()
    {
        currentPills = pillsPerRound;
        contaminatedBottlesLeft = contaminatedPerRound;
        
        SpawnBottles();
        UpdateUI();
        
        Debug.Log("Ronda " + currentRound + " iniciada.");
    }

    void SpawnBottles()
    {
        // 1. Destruimos las botellas de la ronda anterior para limpiar la pantalla
        foreach (GameObject b in spawnedBottles)
        {
            Destroy(b);
        }
        spawnedBottles.Clear(); // Limpiamos la lista

        // 2. Elegimos al azar qué posiciones tendrán el agua contaminada
        List<int> contaminatedIndices = new List<int>();
        while (contaminatedIndices.Count < contaminatedPerRound)
        {
            int rand = Random.Range(0, spawnPoints.Length);
            if (!contaminatedIndices.Contains(rand))
            {
                contaminatedIndices.Add(rand);
            }
        }

        // 3. Invocamos (Instanciamos) las botellas en cada Spawn Point
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            // Crea la botella exactamente en la posición del Spawn Point
            GameObject newBottle = Instantiate(bottlePrefab, spawnPoints[i].position, Quaternion.identity);
            spawnedBottles.Add(newBottle);

            // Obtenemos el script de la botella para asignarle su tipo
            Bottle bottleScript = newBottle.GetComponent<Bottle>();

            if (contaminatedIndices.Contains(i))
            {
                bottleScript.ResetBottle(Bottle.BottleType.Contaminated);
            }
            else
            {
                bottleScript.ResetBottle(Bottle.BottleType.Potable);
            }
        }
    }

    public void UsePill()
    {
        currentPills--;
        UpdateUI();
    }

    public int GetCurrentPills() => currentPills;

    public void AddScore(int amount)
    {
        totalScore += amount;
        contaminatedBottlesLeft--;
        UpdateUI();
    }

    public void SubtractScore(int amount)
    {
        totalScore -= amount;
        UpdateUI();
    }

    public void CheckRoundCompletion()
    {
       if (contaminatedBottlesLeft <= 0)
        {
            Invoke("NextRound", 0.5f); // Pausa pequeña para que se vea el cambio
        }
        else if (currentPills <= 0)
        {
            Debug.Log("Sin pastillas. Fin del juego.");
        }
    }

    void NextRound()
    {
        currentRound++;
        if (currentRound > maxRounds)
        {
            Debug.Log("¡Minijuego Terminado! Puntaje Final: " + totalScore);
            Invoke("VolverAlMapa", 2.0f);
        }
        else
        {
            StartRound(); 
        }
    }

    void UpdateUI()
    {
        if(scoreText != null) scoreText.text = "Puntos: " + totalScore;
        if(pillsText != null) pillsText.text = "Pastillas: " + currentPills;
        if(roundText != null) roundText.text = "Ronda: " + currentRound + "/" + maxRounds;
    }
    void VolverAlMapa()
{
    // Escribe aquí el nombre exacto de tu escena del mapa
    SceneManager.LoadScene("MapaCentral");
}
}
