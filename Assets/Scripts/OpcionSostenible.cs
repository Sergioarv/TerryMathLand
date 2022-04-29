using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpcionSostenible : MonoBehaviour
{
    public bool esSostenible = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            collision.GetComponent<ManoJugador>().objetoASostener = this.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            collision.GetComponent<ManoJugador>().objetoASostener = null;
        }
    }
}
