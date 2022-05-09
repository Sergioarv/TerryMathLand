using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpcionLanzable : MonoBehaviour
{
    public bool esLanzable = true;
    private bool lanzado = false;
    private bool cae = true;

    public float dirX;
    private bool respondio = false;

    Rigidbody2D rbOpcion;
    Animator animatorOpcion;

    GameManagerGeneric gameManagerGeneric;

    private void Start()
    {
        rbOpcion = GetComponent<Rigidbody2D>();
        animatorOpcion = GetComponent<Animator>();
        gameManagerGeneric = GameObject.FindObjectOfType<GameManagerGeneric>();
    }

    private void Update()
    {
        if (cae && lanzado && respondio)
        {
            string seleccion = this.name;
            string seleccionOpcion = this.transform.GetComponentInChildren<TextMeshProUGUI>().text;
            gameManagerGeneric.responder(seleccion, seleccionOpcion);

            respondio = false;

            animatorOpcion.SetBool("Lanzado", true);
            Invoke("destruirBomba", 0.8f);
        }
    }

    public void lanzarObjeto()
    {
        rbOpcion.AddForce(new Vector2((dirX * 1.2f), 2.2f), ForceMode2D.Impulse);
        lanzado = true;
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
        }

        if (collision.CompareTag("Solucion"))
        {
            respondio = true;
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
        }

        if (collision.CompareTag("Solucion"))
        {
            respondio = false;
        }
    }

    public void destruirBomba()
    {
        this.gameObject.SetActive(false);
        lanzado = false;
    }
}
