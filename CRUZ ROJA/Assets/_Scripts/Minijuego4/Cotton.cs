using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cotton : MonoBehaviour
{
    public Wound targetWound;
    public FirstAidManager manager;
    
    private Vector3 lastMousePos;

    void OnMouseDown()
    {
        // Guarda la posición inicial al tocar
        lastMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDrag()
    {
        if (!manager.isRoundActive || manager.isCleaned) return;

        // Seguir el dedo/mouse
        Vector3 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentMousePos.z = 0;
        transform.position = currentMousePos;

        // Calcular qué tanto se movió el dedo en este frame (Fricción)
        float distanceMoved = Vector3.Distance(currentMousePos, lastMousePos);

        // Si se está moviendo Y está lo suficientemente cerca de la herida
        float distanceToWound = Vector3.Distance(transform.position, targetWound.transform.position);
        
        if (distanceMoved > 0.01f && distanceToWound < 1.5f) 
        {
            targetWound.ReceiveCleaning(distanceMoved * 50f); // 50f es la velocidad de limpieza, ajústalo a tu gusto
        }

        lastMousePos = currentMousePos;
    }
}
