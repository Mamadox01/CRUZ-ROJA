using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WaterPurifierManager : MonoBehaviour
{
    public int maxRounds = 3;
    public int pillsPerRound = 5;
    public int contaminatedPerRound = 2;
    
    [Header("Invocación (Prefabs y Spawns)")]
    public GameObject bottlePrefab;     
    public Transform[] spawnPoints;     

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI pillsText;
    public TextMeshProUGUI roundText;

    private int currentRound = 1;
    private int totalScore = 0;
    private int currentPills;
    private int contaminatedBottlesLeft;
    private bool isGameOver = false; // Para evitar que se llame al final dos veces
    
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
    }

    void SpawnBottles()
    {
        foreach (GameObject b in spawnedBottles)
        {
            Destroy(b);
        }
        spawnedBottles.Clear(); 

        List<int> contaminatedIndices = new List<int>();
        while (contaminatedIndices.Count < contaminatedPerRound)
        {
            int rand = Random.Range(0, spawnPoints.Length);
            if (!contaminatedIndices.Contains(rand))
            {
                contaminatedIndices.Add(rand);
            }
        }

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject newBottle = Instantiate(bottlePrefab, spawnPoints[i].position, Quaternion.identity);
            spawnedBottles.Add(newBottle);

            Bottle bottleScript = newBottle.GetComponent<Bottle>();
            if (contaminatedIndices.Contains(i))
                bottleScript.ResetBottle(Bottle.BottleType.Contaminated);
            else
                bottleScript.ResetBottle(Bottle.BottleType.Potable);
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
        if (isGameOver) return;

        if (contaminatedBottlesLeft <= 0)
        {
            Invoke("NextRound", 0.5f); 
        }
        else if (currentPills <= 0)
        {
            EndGame(); // Llamamos al final del juego porque perdimos
        }
    }

    void NextRound()
    {
        currentRound++;
        if (currentRound > maxRounds)
        {
            EndGame(); // Llamamos al final del juego porque ganamos
        }
        else
        {
            StartRound();
        }
    }

    // --- NUEVA LÓGICA DE RÉCORD Y MAPA ---
    void EndGame()
    {
        isGameOver = true;
        
        // 1. Buscamos el récord anterior
        int recordAnterior = PlayerPrefs.GetInt("Record_Mini1", 0); 

        // 2. Comparamos y guardamos
        if (totalScore > recordAnterior)
        {
            PlayerPrefs.SetInt("Record_Mini1", totalScore);
            PlayerPrefs.Save(); 
            Debug.Log("¡Nuevo Récord Alcanzado!: " + totalScore);
        }
        else
        {
            Debug.Log("No superaste tu récord. Tu récord sigue siendo: " + recordAnterior);
        }

        // 3. Volvemos al mapa después de 2.5 segundos
        Invoke("VolverAlMapa", 2.5f);
    }

    void VolverAlMapa()
    {
        SceneTransition.instance.CambiarEscena("MapaCentral"); 
    }

    void UpdateUI()
    {
        if(scoreText) scoreText.text = "Puntos: " + totalScore;
        if(pillsText) pillsText.text = "Pastillas: " + currentPills;
        if(roundText) roundText.text = "Ronda: " + currentRound + "/" + maxRounds;
    }
}
