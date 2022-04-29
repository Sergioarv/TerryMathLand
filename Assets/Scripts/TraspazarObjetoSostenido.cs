using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraspazarObjetoSostenido : MonoBehaviour
{
    public static bool sueloElevado = false;
    
    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.CompareTag("SueloElevado"))
        {
            sueloElevado = true;
            Debug.Log(collision.name+" true");
        }
        else
        {
            sueloElevado = false;
            Debug.Log(collision.name + " false");
        }
    }
}
