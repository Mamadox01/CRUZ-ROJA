using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    private Animator anim;
    private SoupManager manager;
    public bool isCovered = false;

    
    [Header("Visuales (Temporal)")]
    public Color openColor = Color.white;
    public Color coveredColor = Color.gray; // Simulamos que está tapado cambiándolo a gris

    void Start()
    {
        manager = FindObjectOfType<SoupManager>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;
        // Detecta si el jugador mantiene presionado el dedo/clic
        if (Input.GetMouseButtonDown(0))
        {
            isCovered = true;
            anim.SetBool("Tapada", true);
        }
        else if (Input.GetMouseButtonUp(0)) // Al soltar
        {
            isCovered = false;
            anim.SetBool("Tapada", false);
        }
    }

    // Cuando un ingrediente choca con el tazón
    void OnTriggerEnter2D(Collider2D col)
    {
        Ingredient ing = col.GetComponent<Ingredient>();
        
        if (ing != null)
        {
            if (isCovered)
            {
                // El tazón estaba tapado. El objeto rebotó.
                if (ing.isHealthy) {
                    manager.SubtractScore(50);
                    Debug.Log("¡Mal! Tapaste una verdura.");
                } else {
                    manager.AddScore(100);
                    Debug.Log("¡Bien! Evitaste la chatarra.");
                }
            }
            else
            {
                // El tazón estaba abierto. El objeto entró a la sopa.
                if (ing.isHealthy) {
                    manager.AddScore(100);
                    Debug.Log("¡Bien! Sopa más nutritiva.");
                } else {
                    manager.SubtractScore(50);
                    Debug.Log("¡Mal! Cayó chatarra a la sopa.");
                }
            }
            
            // Sea lo que sea, el ingrediente desaparece tras tocar el tazón
            Destroy(col.gameObject);
        }
    }
}
