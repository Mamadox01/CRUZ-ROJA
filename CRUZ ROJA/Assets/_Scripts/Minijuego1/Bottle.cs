using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]

public class Bottle : MonoBehaviour
{
    public enum BottleType { Contaminated, Vacia, Potable }
    public BottleType currentType;

    [Header("Asignación de Sprites")]
    public Sprite spritePotable; 
    public Sprite spriteContaminada;  
    public Sprite spriteVacia;
    
    private WaterPurifierManager manager;

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        manager = FindObjectOfType<WaterPurifierManager>();
    }

    void Start()
    {
        manager = FindObjectOfType<WaterPurifierManager>();
    }

    public void ResetBottle(BottleType type)
    {
        currentType = type;
        ActualizarSprite();
    }

    void ActualizarSprite()
    {
        switch (currentType)
        {
            case BottleType.Potable:
                sr.sprite = spritePotable;
                break;
            case BottleType.Contaminated:
                sr.sprite = spriteContaminada;
                break;
            case BottleType.Vacia:
                sr.sprite = spriteVacia;
                break;
        }
    }

    // Detecta el toque en móvil o el clic del mouse
    void OnMouseDown()
    {
        if (Time.timeScale == 0f) return;

        // Si ya está vacía, no tiene sentido gastar otra pastilla
        if (currentType == BottleType.Vacia) return;

        // Verificamos si el Manager todavía tiene pastillas disponibles
        if (manager.GetCurrentPills() > 0)
        {
            manager.UsePill(); // Gastamos una pastilla visualmente en la UI

            if (currentType == BottleType.Contaminated)
            {
                // ¡Acierto! Purificamos el agua sucia
                currentType = BottleType.Vacia;
                ActualizarSprite();
                manager.AddScore(100); // Sumamos puntos por hacerlo bien
            }
            else if (currentType == BottleType.Potable)
            {
                // ¡Error! Gastamos una pastilla en agua que ya estaba limpia
                currentType = BottleType.Vacia; 
                ActualizarSprite();
                manager.SubtractScore(50); // Castigo por desperdiciar
            }

            // Le avisamos al Manager que revise si ya limpiamos todas las de esta ronda
            manager.CheckRoundCompletion();
        }
        else
        {
            Debug.Log("¡No te quedan pastillas para usar!");
        }
    }
}
