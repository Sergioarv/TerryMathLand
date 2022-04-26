using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificarSuelo : MonoBehaviour
{
    public static bool tocandoSuelo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo"))
        {
            tocandoSuelo = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo"))
        {
            tocandoSuelo = false;
        }
    }
}
