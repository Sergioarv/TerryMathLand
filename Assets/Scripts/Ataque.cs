using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{
    public float velocidad;
    public Vector2 posObjetivo;

    GameManagerGeneric gameManagerGeneric;

    private void Start()
    {
        gameManagerGeneric = GameObject.FindObjectOfType<GameManagerGeneric>();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, posObjetivo, velocidad * Time.deltaTime);

        if(Vector2.Distance(transform.position, posObjetivo) <= 0.001f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            gameManagerGeneric.vida -= 1;
            gameManagerGeneric.txtVida.text = gameManagerGeneric.vida.ToString();
            Destroy(gameObject);
        }
    }
}
