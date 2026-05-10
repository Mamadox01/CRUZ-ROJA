using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
   [Header("Configuración")]
    public GameObject obstaclePrefab; // Tu Prefab de cactus/piedra
    public float minTime = 1.5f; // Tiempo mínimo entre obstáculos
    public float maxTime = 3f;   // Tiempo máximo entre obstáculos

    private RunnerManager manager;

    void Start()
    {
        manager = FindObjectOfType<RunnerManager>();
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (manager.isGameActive)
        {
            // Espera un tiempo al azar
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);

            // Invoca el obstáculo
            Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
        }
    }
}
