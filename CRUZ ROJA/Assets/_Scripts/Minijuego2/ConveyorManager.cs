using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class ConveyorManager : MonoBehaviour
{
    [Header("Configuración")]
    public float gameTime = 20f;
    public float spawnDelay = 1f; // Cada cuántos segundos cae un producto nuevo

    [Header("Referencias")]
    public GameObject productPrefab;
    public Transform[] spawnPoints; // Puntos arriba de la pantalla de donde caerán
    
    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    private int totalScore = 0;
    private bool isGameActive = false;
    public GameObject globalVolume;


    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        totalScore = 0;
        isGameActive = true;
        UpdateUI();
        
        // Iniciamos los generadores de tiempo y de productos
        StartCoroutine(SpawnRoutine());
        StartCoroutine(TimerRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnProduct();
        }
    }

    void SpawnProduct()
    {
        // Elige un punto de aparición al azar en la parte superior
        int randPoint = Random.Range(0, spawnPoints.Length);
        GameObject newProduct = Instantiate(productPrefab, spawnPoints[randPoint].position, Quaternion.identity);

        // 50% de probabilidad de que el producto esté vencido
        bool willBeExpired = Random.value > 0.5f; 
        newProduct.GetComponent<Product>().isExpired = willBeExpired;
    }

    IEnumerator TimerRoutine()
    {
        // Resta 1 segundo cada segundo real
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
    }

    public void SubtractScore(int amount)
    {
        if(!isGameActive) return;
        totalScore -= amount;   
        Activarpantallazo();
        Invoke("OcultarPantallazo", 1f);
        UpdateUI();
    }

    void Activarpantallazo()
    {
        globalVolume.SetActive(true);
    }
    void OcultarPantallazo()
    {
        globalVolume.SetActive(false);
    }
    void EndGame()
    {
        isGameActive = false;
        if (timeText) timeText.text = "¡FIN!";
        int recordAnterior = PlayerPrefs.GetInt("Record_Mini2", 0);
        if (totalScore > recordAnterior)
        {
            // ¡Nuevo récord! Lo guardamos con la MISMA clave que usamos en el mapa ("Record_Mini2")
            PlayerPrefs.SetInt("Record_Mini2", totalScore);
            PlayerPrefs.Save(); // Esto obliga al celular a guardar los datos de inmediato
            
            Debug.Log("¡Nuevo Récord Alcanzado!: " + totalScore);
            // Aquí podrías incluso activar un texto en pantalla que diga "¡NUEVO RÉCORD!"
        }
        else
        {
            Debug.Log("Tu récord sigue siendo: " + recordAnterior);
        }
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
