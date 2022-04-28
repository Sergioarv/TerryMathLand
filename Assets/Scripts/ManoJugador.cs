using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManoJugador : MonoBehaviour
{

    public GameObject mano;
    private GameObject objetoEnMano = null;
    private GameObject objetoPadre;
    private Vector2 posInicialObjeto;

    private void Update()
    {
        if (objetoEnMano != null)
        {
            if (Input.GetKey(KeyCode.R))
            {
                objetoEnMano.transform.position = posInicialObjeto;
                objetoEnMano.transform.SetParent(objetoPadre.transform);
                //objetoEnMano.transform.position = new Vector2(objetoEnMano.transform.position.x, objetoEnMano.transform.position.y - 0.3f);
                objetoEnMano = null;
            }
        }

    }

    /*
     * private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Opcion"))
        {

            if (Input.GetKey(KeyCode.E) && objetoEnMano == null)
            {
                objetoPadre = collision.transform.parent.gameObject;
                posInicialObjeto = collision.transform.position;
                collision.transform.position = mano.transform.position;
                collision.transform.SetParent(transform);
                objetoEnMano = collision.gameObject;
            }

        }
    }
    */
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Opcion"))
        {
            if (Input.GetKey(KeyCode.E) && objetoEnMano == null)
            {
                objetoPadre = collision.transform.parent.gameObject;
                posInicialObjeto = collision.transform.position;
                collision.transform.position = mano.transform.position;
                collision.transform.SetParent(transform);
                objetoEnMano = collision.gameObject;
            }
        }
    }
}
