using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraspazarObjetoSostenido : MonoBehaviour
{
    public static bool sueloElevado = false;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("SueloElevado"))
        {
            sueloElevado = true;
        }
        else
        {
            sueloElevado = false;
        }
    }

}
