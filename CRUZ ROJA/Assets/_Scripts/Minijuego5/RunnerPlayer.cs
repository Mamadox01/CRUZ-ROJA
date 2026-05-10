using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerPlayer : MonoBehaviour
{
[Header("Configuración de Salto")]
    public float jumpForce = 12f; // Fuerza hacia arriba
    public float groundedRadius = 0.2f; // Radio para detectar el suelo
    public LayerMask whatIsGround; // Capa que es el suelo
    public Transform groundCheck; // Objeto vacío bajo los pies

    private Rigidbody2D rb;
    private bool isGrounded; // ¿Está tocando el suelo?
    private RunnerManager manager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<RunnerManager>();
    }

    void Update()
    {
        if (!manager.isGameActive) return;

        // Detección matemática del suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);

        // Si tocas la pantalla (o clic) Y estás en el suelo -> SALTA
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // Resetea velocidad vertical antes de saltar
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    // Detectar colisión con un obstáculo
    void OnCollisionEnter2D(Collision2D col)
    {
        if (!manager.isGameActive) return;

        if (col.gameObject.CompareTag("Obstaculo"))
        {
            Debug.Log("¡Te estrellaste!");
            
            // Avisamos al manager para que nos empuje hacia atrás
            manager.PushPlayerBack(); 

            // Efecto visual temporal: cambiamos de color al estrellarnos
            GetComponent<SpriteRenderer>().color = Color.gray; 
            Invoke("ResetColor", 0.3f); // Vuelve al color normal en 0.3 segundos

            // Destruimos el obstáculo con el que chocamos para no chocar dos veces
            Destroy(col.gameObject); 
        }
    }

    void ResetColor()
    {
        GetComponent<SpriteRenderer>().color = Color.green; // O tu color original
    }

    // Dibuja el radio de detección de suelo en el editor para que lo veas
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundedRadius);
        }
    }
}
