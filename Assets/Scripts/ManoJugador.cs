using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManoJugador : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Opcion"))
        {
            collision.transform.SetParent(transform);
        }
    }
}
