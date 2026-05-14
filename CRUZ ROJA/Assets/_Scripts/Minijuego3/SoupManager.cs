using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SoupManager : MonoBehaviour
{
    public AudioClip sonidoAcierto;
    public AudioClip sonidoError;
    public AudioClip olla;
    [Header("Configuración")]
    public float gameTime = 20f;
    public float spawnDelay = 1f; // Qué tan rápido caen los objetos

    [Header("Referencias")]
    public GameObject ingredientPrefab;
    public Transform[] spawnPoints; 
    
    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    private int totalScore = 0;
    private bool isGameActive = false;

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        totalScore = 0;
        isGameActive = true;
        UpdateUI();
        if (SFXManager.instance != null)
        {
            SFXManager.instance.PlaySFX(olla);
        }
        
        StartCoroutine(SpawnRoutine());
        StartCoroutine(TimerRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnIngredient();
        }
    }

    void SpawnIngredient()
    {
        // Elegimos un punto de aparición al azar arriba
        int randPoint = Random.Range(0, spawnPoints.Length);
        GameObject newIng = Instantiate(ingredientPrefab, spawnPoints[randPoint].position, Quaternion.identity);

        // 50% de probabilidad de ser sano o chatarra
        bool isHealthyItem = Random.value > 0.5f; 
        newIng.GetComponent<Ingredient>().isHealthy = isHealthyItem;
    }

    IEnumerator TimerRoutine()
    {
        while (gameTime > 0 && isGameActive)
        {
            yield return new WaitForSeconds(1f);
            gameTime--;
            UpdateUI();
        }
        EndGame();
    }

    public void AddScore(int amount)
    {
        if(!isGameActive) return;
        totalScore += amount;
        UpdateUI();
        if (SFXManager.instance != null)
        {
            SFXManager.instance.PlaySFX(sonidoAcierto);
        }
        
    }

    public void SubtractScore(int amount)
    {
        if(!isGameActive) return;
        totalScore -= amount;
        UpdateUI();
        if (SFXManager.instance != null)
        {
            SFXManager.instance.PlaySFX(sonidoError);
        }
        
    }

    void EndGame()
    {
        isGameActive = false;
        int recordAnterior = PlayerPrefs.GetInt("Record_Mini3", 0);
        if (totalScore > recordAnterior)
        {
            // ¡Nuevo récord! Lo guardamos con la MISMA clave que usamos en el mapa ("Record_Mini2")
            PlayerPrefs.SetInt("Record_Mini3", totalScore);
            PlayerPrefs.Save(); // Esto obliga al celular a guardar los datos de inmediato
            
            Debug.Log("¡Nuevo Récord Alcanzado!: " + totalScore);
            // Aquí podrías incluso activar un texto en pantalla que diga "¡NUEVO RÉCORD!"
        }
        else
        {
            Debug.Log("Tu récord sigue siendo: " + recordAnterior);
        }
        if (timeText) timeText.text = "¡FIN!";
        Invoke("VolverAlMapa", 3.0f);
    }

    void UpdateUI()
    {
        if (scoreText) scoreText.text = "Puntos: " + totalScore;
        if (timeText) timeText.text = "Tiempo: " + gameTime;
    }
    void VolverAlMapa()
    {
        SceneTransition.instance.CambiarEscena("MapaCentral");
    }
}
