using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RunnerManager : MonoBehaviour
{
    [Header("Configuración de Mecánica")]
    public float pushDistance = 2f; // Cuánta distancia retrocede al chocar
    public float gameOverXPosition = -8f; // Límite izquierdo de la pantalla

    [Header("Referencias")]
    public Transform playerTransform; // Arrastra al Jugador aquí
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    public bool isGameActive = true;
    private float totalScore = 0f;

    void Start()
    {
        isGameActive = true;
        if(gameOverText) gameOverText.enabled = false;
        if(scoreText) scoreText.text = "Puntos: 0";
    }

    void Update()
    {
        if (!isGameActive) return;

        // El puntaje sube cada segundo que sobrevives
        totalScore += Time.deltaTime;
        if(scoreText) scoreText.text = "Puntos: " + (int)totalScore;

        // COMPROBACIÓN DE GAME OVER:
        // Si el jugador fue empujado más allá del límite izquierdo...
        if (playerTransform.position.x <= gameOverXPosition)
        {
            EndGame();
        }
    }

    // La función que aplica el castigo
    public void PushPlayerBack()
    {
        if (!isGameActive) return;

        // Movemos el Transform del jugador hacia atrás en el eje X
        Vector3 newPos = playerTransform.position;
        newPos.x -= pushDistance; // Restamos distancia
        playerTransform.position = newPos;

        Debug.Log("¡Empujado hacia atrás!");
    }

    void EndGame()
    {
        isGameActive = false;
        if(gameOverText) gameOverText.enabled = true;
        
        // Efecto visual: paramos el tiempo (opcional)
        Time.timeScale = 0.2f; // Cámara lenta para el drama

        Debug.Log("¡Game Over! Puntos final: " + (int)totalScore);

        // Volver al mapa en 2 segundos (tomando en cuenta la cámara lenta)
        Invoke("VolverAlMapa", 1.0f); 
    }

    void VolverAlMapa()
    {
        Time.timeScale = 1f; // Restauramos el tiempo normal
        SceneManager.LoadScene("MapaCentral"); // Pon el nombre exacto de tu escena
    }
}
