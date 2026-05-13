using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandAid : MonoBehaviour
{
    public Wound targetWound;
    public FirstAidManager manager;

    void OnMouseDrag()
    {
        // Solo deja mover la curita si el juego está activo y ya se limpió la herida
        if (!manager.isRoundActive || !manager.isCleaned) return;

        Vector3 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentMousePos.z = 0;
        transform.position = currentMousePos;
    }

    void OnMouseUp()
    {
        if (!manager.isRoundActive || !manager.isCleaned) return;

        // Comprueba si soltaste la curita cerca del centro de la herida
        float distanceToWound = Vector3.Distance(transform.position, targetWound.transform.position);
        
        if (distanceToWound < 1.5f)
        {
            // Hace "Snap" (se pega exactamente en el centro de la herida)
            transform.position = targetWound.transform.position;
            manager.WinRound();
        }
    }
}
