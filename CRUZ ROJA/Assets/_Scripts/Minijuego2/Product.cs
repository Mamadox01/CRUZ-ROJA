using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Product : MonoBehaviour
{
    public bool isExpired;
    public float fallSpeed = 4f; // Qué tan rápido caen

    [Header("Colección de Sprites")]
    public string[] goodAnimationStates;
    public string[] badAnimationStates;
    public Animator animator;
    private Light2D productLight;
    private SpriteRenderer sr;

    void Start()
    {
        animator = GetComponent<Animator>();
        productLight = GetComponentInChildren<Light2D>();

        if (productLight != null)
        {
            if (isExpired)
            {
                productLight.enabled = false;
            }
            else
            {
                productLight.enabled = true;
                productLight.color = Color.green;
            }
        }
        if (animator == null) return;
        sr = GetComponent<SpriteRenderer>();
        // Dependiendo de si está vencido o no, elegimos un sprite al azar de la lista correspondiente
        if (isExpired)
        {
            if (badAnimationStates.Length > 0)
            {
                int randomIndex = Random.Range(0, badAnimationStates.Length);
                // Reproduce la animación elegida al azar inmediatamente
                animator.Play(badAnimationStates[randomIndex]);
            }
        }
        else
        {
            if (goodAnimationStates.Length > 0)
            {
                int randomIndex = Random.Range(0, goodAnimationStates.Length);
                animator.Play(goodAnimationStates[randomIndex]);
            }
        }

        // Ya no necesitamos pintarlos de rojo o verde porque ahora tienen tus diseños reales, 
        // así que el sr.color se queda con su color normal (blanco/transparente).
        sr.color = Color.white;
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
