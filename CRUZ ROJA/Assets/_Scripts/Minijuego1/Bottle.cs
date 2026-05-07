using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public enum BottleType { Contaminated, Empty, Potable }
    public BottleType myType;
    
    private WaterPurifierManager manager;
    private bool alreadyTreated = false;
    private Color originalColor;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    void Start()
    {
        manager = FindObjectOfType<WaterPurifierManager>();
    }

    public void ResetBottle(BottleType newType)
    {
        myType = newType;
        alreadyTreated = false;
        if(sr != null) sr.color = originalColor; // Vuelve a su color normal
    }

    // Detecta el toque en móvil o el clic del mouse
    void OnMouseDown()
    {
        if (alreadyTreated || manager.GetCurrentPills() <= 0) return;

        manager.UsePill(); // Gastamos una pastilla al intentar

        if (myType == BottleType.Contaminated)
        {
            // Acierto
            alreadyTreated = true;
            manager.AddScore(100); 
            Debug.Log("¡Agua purificada!");
            // Check visual
            GetComponent<SpriteRenderer>().color = Color.cyan; 
        }
        else
        {
            // Falló
            manager.SubtractScore(50);
            Debug.Log("Desperdiciaste la pastilla");
        }

        manager.CheckRoundCompletion();
    }
}
