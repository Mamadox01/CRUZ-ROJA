using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [Header("Configuración")]
    public bool isHealthy; // True = Verdura, False = Chatarra
    public float fallSpeed = 5f;
    [Header("Colección de Sprites")]
    public Sprite[] goodSprites; // Lista de dibujos para ingredientes ricos (papa, pollo, tomate)
    public Sprite[] badSprites;  // Lista de dibujos para cosas asquerosas (mosca, zapato, basura)

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (isHealthy)
        {
            if (goodSprites.Length > 0)
            {
                int randomGood = Random.Range(0, goodSprites.Length);
                sr.sprite = goodSprites[randomGood];
                
            }
        }
        else
        {
            if (badSprites.Length > 0)
            {
                int randomBad = Random.Range(0, badSprites.Length);
                sr.sprite = badSprites[randomBad];
            }
        }
        sr.color = Color.white;

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
