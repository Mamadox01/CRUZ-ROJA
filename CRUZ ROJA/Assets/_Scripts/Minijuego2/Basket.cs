using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    private ConveyorManager manager;

    void Start()
    {
        manager = FindObjectOfType<ConveyorManager>();
    }

    void Update()
    {
        // Movimiento táctil o con el clic sostenido del mouse
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Mueve la canasta solo en el eje X (horizontal), mantiene su altura (Y) original
            transform.position = new Vector3(mousePos.x, transform.position.y, 0);
        }
    }

    // Se activa cuando un objeto choca con la canasta
    void OnTriggerEnter2D(Collider2D col)
    {
        Product product = col.GetComponent<Product>();
        if (product != null)
        {
            if (product.isExpired)
            {
                manager.SubtractScore(50);
                Debug.Log("¡Atrapaste uno vencido! -50 puntos");
            }
            else
            {
                manager.AddScore(100);
                Debug.Log("¡Buen producto! +100 puntos");
            }
            
            // Destruimos el producto al atraparlo para que desaparezca
            Destroy(col.gameObject); 
        }
    }
}
