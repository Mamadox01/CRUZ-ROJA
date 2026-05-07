using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    public bool isExpired;
    public float fallSpeed = 4f; // Qué tan rápido caen
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        // Para pruebas: Si está vencido se pinta de rojo, si está bueno de verde
        sr.color = isExpired ? Color.red : Color.green;
    }

    void Update()
    {
        // Caída constante hacia abajo
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // Si cae al vacío (fuera de la cámara por abajo), se destruye solito
        if (transform.position.y < -6f) // Ajusta este -6f dependiendo del tamaño de tu cámara
        {
            Destroy(gameObject);
        }
    }
}
