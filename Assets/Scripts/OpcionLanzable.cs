using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpcionLanzable : MonoBehaviour
{
    public bool esLanzable = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            collision.GetComponent<ManoLanzadora>().objetoASostener = this.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            collision.GetComponent<ManoLanzadora>().objetoASostener = null;
        }
    }
}
