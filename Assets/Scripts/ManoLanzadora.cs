using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManoLanzadora : MonoBehaviour
{

    public GameObject mano;
    private GameObject objetoEnMano = null;
    public GameObject objetoASostener = null;
    private GameObject objetoPadre;

    public Animator animatorPlayer;
    public Animator animatorBomba;

    public GameObject posSolucion;
    private SpriteRenderer spriteBomba;

    private Rigidbody2D rgbOpcion;

    public float dirX;

    bool mirandoDerecha;

    public GameManagerGeneric gameManagerGeneric;

    private void Update()
    {
        // Verifica si esta frente a un objeto sostenible, si no hay objetos en la mano
        if (objetoASostener != null && objetoASostener.GetComponent<OpcionLanzable>().esLanzable && objetoEnMano == null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                objetoPadre = objetoASostener.transform.parent.gameObject;

                objetoEnMano = objetoASostener;
                animatorBomba = objetoASostener.GetComponent<Animator>();
                objetoEnMano.GetComponent<OpcionLanzable>().esLanzable = false;
                objetoEnMano.transform.position = mano.transform.GetChild(0).position;
                objetoEnMano.transform.SetParent(transform);
                if (objetoEnMano.GetComponent<Rigidbody2D>() != null) objetoEnMano.GetComponent<Rigidbody2D>().simulated = false;
                objetoEnMano.GetComponent<SpriteRenderer>().sortingOrder = 1;
                spriteBomba = objetoEnMano.GetComponent<SpriteRenderer>();
                mirandoDerecha = spriteBomba.flipX;

                animatorBomba.SetBool("Lanzable", false);

                animatorPlayer.SetBool("Sostener", true);
            }
        } // Verifica si sostenemos un objeto
        else if (objetoEnMano != null)
        {
            // Verifica si al sostener un objeto se presiona la tecla F para soltar
            if (Input.GetKeyDown(KeyCode.F))
            {
                TraspazarObjetoSostenido.sueloElevado = false;
                objetoEnMano.GetComponent<OpcionLanzable>().esLanzable = true;
                if (objetoEnMano.GetComponent<Collider2D>().enabled == false) objetoEnMano.GetComponent<Collider2D>().enabled = true;
                if (objetoEnMano.GetComponent<Rigidbody2D>() != null) objetoEnMano.GetComponent<Rigidbody2D>().simulated = true;
                objetoEnMano.transform.SetParent(objetoPadre.transform);
                objetoEnMano.GetComponent<SpriteRenderer>().sortingOrder = 0;
                spriteBomba = null;

                objetoEnMano.transform.position = new Vector2(objetoEnMano.transform.position.x, objetoEnMano.transform.position.y);

                animatorBomba.SetBool("Lanzable", false);

                objetoEnMano = null;
                animatorPlayer.SetBool("Sostener", false);
            } // Verifica si tenemos un objeto y lo lanzamos
            else if (Input.GetMouseButtonDown(0))
            {
                objetoEnMano.GetComponent<OpcionLanzable>().esLanzable = true;
                if (objetoEnMano.GetComponent<Collider2D>().enabled == false) objetoEnMano.GetComponent<Collider2D>().enabled = true;
                objetoEnMano.transform.SetParent(objetoPadre.transform);
                objetoEnMano.GetComponent<SpriteRenderer>().sortingOrder = 0;
                spriteBomba = null;

                if (objetoEnMano.GetComponent<Rigidbody2D>() != null) objetoEnMano.GetComponent<Rigidbody2D>().simulated = true;
                objetoEnMano.GetComponent<OpcionLanzable>().lanzarObjeto();

                animatorBomba.SetBool("Lanzable", true);

                objetoEnMano = null;
                animatorPlayer.SetBool("Sostener", false);
            }
        }

        if (objetoEnMano != null)
        {
            objetoEnMano.GetComponent<Collider2D>().enabled = TraspazarObjetoSostenido.sueloElevado ? false : true;
            animatorBomba.SetFloat("Mov", dirX);
        }
    }

    public void verificarSolucion(string solucion, string solucionOpcion)
    {
        string seleccion = solucion;
        string seleccionOpcion = solucionOpcion;

        gameManagerGeneric.responder(seleccion, seleccionOpcion);
    }
}
