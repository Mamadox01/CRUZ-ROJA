using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wound : MonoBehaviour
{
    public float cleanProgress = 0f;
    public float cleanTarget = 100f; // Cuánto hay que frotar
    public FirstAidManager manager;
    
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // El algodón llamará a esta función cuando lo froten encima
    public void ReceiveCleaning(float amount)
    {
        if (manager.isCleaned || !manager.isGameActive) return;

        cleanProgress += amount;

        // Efecto visual: La mancha se va haciendo transparente
        Color c = sr.color;
        c.a = 1f - (cleanProgress / cleanTarget);
        sr.color = c;

        if (cleanProgress >= cleanTarget)
        {
            manager.isCleaned = true;
            sr.enabled = false; // Desaparece la suciedad por completo
            Debug.Log("¡Limpio! Ahora la curita.");
        }
    }
}
