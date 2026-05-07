using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [Header("Configuración")]
    public bool isHealthy; // True = Verdura, False = Chatarra
    public float fallSpeed = 5f;
    
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        // Solo visual para las pruebas: Verde es sano, Rojo es chatarra
        sr.color = isHealthy ? Color.green : Color.red; 
    }

    void Update()
    {
        // El ingrediente cae constantemente
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // Si se pasa de largo (cae al vacío), se destruye
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
}
