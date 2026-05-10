using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float speed = 8f; // Velocidad a la que viene el obstáculo

    void Update()
    {
        // Se mueve constantemente hacia la izquierda
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Si se sale de la pantalla por la izquierda, se destruye
        if (transform.position.x < -12f) // Ajusta este valor según tu cámara
        {
            Destroy(gameObject);
        }
    }
}
