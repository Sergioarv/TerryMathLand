using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraspazarPlataforma : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Jugador"))
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Collider2D colisionJugador = collision.collider.GetComponent<Collider2D>();

                StartCoroutine(IgnorarColison(colisionJugador));
            }
        }
    }

    IEnumerator IgnorarColison(Collider2D colisionJugador)
    {
        Physics2D.IgnoreCollision(colisionJugador, GetComponent<Collider2D>(), true);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(colisionJugador, GetComponent<Collider2D>(), false);
    }
}
