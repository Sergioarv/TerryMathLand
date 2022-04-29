using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManoJugador : MonoBehaviour
{

    public GameObject mano;
    private GameObject objetoEnMano = null;
    private GameObject objetoPadre;
    private Vector2 posInicialObjeto;
    public Animator animatorPlayer;
    public GameObject posSolucion;

    public GameManagerGeneric gameManagerGeneric;

    private void Update()
    {

        if (objetoEnMano != null)
        {
            //Debug.Log(Vector2.Distance(objetoEnMano.transform.position, posSolucion.transform.position));
            if (Input.GetKey(KeyCode.R))
            {
                TraspazarObjetoSostenido.sueloElevado = false;
                if (objetoEnMano.GetComponent<Collider2D>().enabled == false) objetoEnMano.GetComponent<Collider2D>().enabled = true;
                objetoEnMano.transform.SetParent(objetoPadre.transform);
                objetoEnMano.GetComponent<SpriteRenderer>().sortingOrder = 0;
                if (Vector2.Distance(objetoEnMano.transform.position, posSolucion.transform.position) < 0.54f)
                {
                    objetoEnMano.transform.position = new Vector2(objetoEnMano.transform.position.x, objetoEnMano.transform.position.y - 0.3f);

                    string seleccion = objetoEnMano.name;
                    string seleccionOpcion = objetoEnMano.transform.GetComponentInChildren<TextMeshProUGUI>().text;

                    verificarSolucion(seleccion, seleccionOpcion);
                }
                else
                {
                    objetoEnMano.transform.position = posInicialObjeto;
                }
                objetoEnMano = null;
                animatorPlayer.SetBool("Sostener", false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Opcion"))
        {
            if (Input.GetKey(KeyCode.E) && objetoEnMano == null)
            {
                objetoPadre = collision.transform.parent.gameObject;
                posInicialObjeto = collision.transform.position;
                collision.transform.position = mano.transform.GetChild(0).position;
                collision.transform.SetParent(transform);
                objetoEnMano = collision.gameObject;
                objetoEnMano.GetComponent<SpriteRenderer>().sortingOrder = 1;

                animatorPlayer.SetBool("Sostener", true);
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

        Debug.Log("respondi: " + seleccion + " " + seleccionOpcion);

        gameManagerGeneric.responder(seleccion, seleccionOpcion);
    }

}
