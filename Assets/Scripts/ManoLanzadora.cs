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
    private Vector2 posInicialObjeto;
    public Animator animatorPlayer;
    public GameObject posSolucion;

    private Rigidbody2D rgbOpcion;

    public GameManagerGeneric gameManagerGeneric;

    private void Update()
    {
        // Verifica si esta frente a un objeto sostenible, si no hay objetos en la mano
        if (objetoASostener != null && objetoASostener.GetComponent<OpcionLanzable>().esLanzable && objetoEnMano == null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                objetoPadre = objetoASostener.transform.parent.gameObject;
                posInicialObjeto = objetoASostener.transform.position;

                objetoEnMano = objetoASostener;
                objetoEnMano.GetComponent<OpcionLanzable>().esLanzable = false;
                objetoEnMano.transform.position = mano.transform.GetChild(0).position;
                objetoEnMano.transform.SetParent(transform);
                if (objetoEnMano.GetComponent<Rigidbody2D>() != null) objetoEnMano.GetComponent<Rigidbody2D>().simulated = false;
                objetoEnMano.GetComponent<SpriteRenderer>().sortingOrder = 1;

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
                /*
                if (Vector2.Distance(objetoEnMano.transform.position, posSolucion.transform.position) < 0.54f)
                {
                    objetoEnMano.transform.position = new Vector2(objetoEnMano.transform.position.x, objetoEnMano.transform.position.y - 0.3f);

                    /*
                    string seleccion = objetoEnMano.name;
                    string seleccionOpcion = objetoEnMano.transform.GetComponentInChildren<TextMeshProUGUI>().text;
                    verificarSolucion(seleccion, seleccionOpcion);
                    
                }
                else
                {
                    objetoEnMano.transform.position = posInicialObjeto;
                }*/
                
                objetoEnMano.transform.position = new Vector2(objetoEnMano.transform.position.x, objetoEnMano.transform.position.y);

                objetoEnMano = null;
                animatorPlayer.SetBool("Sostener", false);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Se disparo " + objetoEnMano);
            }
        }

        if (objetoEnMano != null)
        {
            objetoEnMano.GetComponent<Collider2D>().enabled = TraspazarObjetoSostenido.sueloElevado ? false : true;
        }
    }

    public void verificarSolucion(string solucion, string solucionOpcion)
    {
        string seleccion = solucion;
        string seleccionOpcion = solucionOpcion;

        gameManagerGeneric.responder(seleccion, seleccionOpcion);
    }
}
