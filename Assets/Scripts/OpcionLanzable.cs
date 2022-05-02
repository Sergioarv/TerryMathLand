using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpcionLanzable : MonoBehaviour
{
    public bool esLanzable = true;
    public bool lanzar = false;
    public bool lanzado = false;
    public bool cae = true;

    public GameObject objetivo;
    public float dirX;

    Rigidbody2D rbOpcion;

    private void Start()
    {
        rbOpcion = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {

    }

    public void lanzarObjeto()
    {
        rbOpcion.AddForce(new Vector2((dirX * 2), 1.67f), ForceMode2D.Impulse);
        lanzado = true;

        if (cae && lanzado)
        {
            Debug.Log("Boom");
            lanzar = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ManoJugador"))
        {
            collision.GetComponent<ManoLanzadora>().objetoASostener = this.gameObject;
        }

        if (collision.CompareTag("Suelo"))
        {
            cae = true;
            Debug.Log("Cayo");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ManoJugador"))
        {
            collision.GetComponent<ManoLanzadora>().objetoASostener = null;
        }

        if (collision.CompareTag("Suelo"))
        {
            cae = false;
            Debug.Log("No Cayo");
        }

    }
}
