using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManoJugador : MonoBehaviour
{

    private GameObject mano;
    private GameObject objetoEnMano = null;
    public GameObject objetoASostener = null;
    private GameObject objetoPadre;
    private Vector2 posInicialObjeto;
    private Animator animatorPlayer;
    private GameObject posSolucion;

    private Rigidbody2D rgbOpcion;

    private GameManagerGeneric gameManagerGeneric;

    private void Start()
    {
        mano = GameObject.Find("Mano");
        animatorPlayer = GameObject.Find("Jugador").GetComponent<Animator>();
        posSolucion = GameObject.Find("Solucion");
        gameManagerGeneric = GameObject.FindObjectOfType<GameManagerGeneric>();
    }

    private void Update()
    {
        // Verifica si esta frente a un objeto sostenible, si no hay objetos en la mano
        if (objetoASostener != null && objetoASostener.GetComponent<OpcionSostenible>().esSostenible && objetoEnMano == null)
        {
            //Verifica si se presiona la letra F en caso de haber en frente y no tener un objeto sostenible
            if (Input.GetKeyDown(KeyCode.F))
            {
                //Convierte el objeto a sostener en hijo de la mano
                objetoPadre = objetoASostener.transform.parent.gameObject;
                posInicialObjeto = objetoASostener.transform.position;

                objetoEnMano = objetoASostener;
                objetoEnMano.GetComponent<OpcionSostenible>().esSostenible = false;
                objetoEnMano.transform.position = mano.transform.GetChild(0).position;
                objetoEnMano.transform.SetParent(transform);
                //if (objetoEnMano.GetComponent<Rigidbody2D>() != null) objetoEnMano.GetComponent<Rigidbody2D>().simulated = false;
                objetoEnMano.GetComponent<SpriteRenderer>().sortingOrder = 1;
                //Inicia la animacion de Sostener
                animatorPlayer.SetBool("Sostener", true);
            }
        } // Verifica si ya hay un objeto en la mano
        else if (objetoEnMano != null)
        {
            //verificarSolucion si se presiona la letra f para soltar el objeto
            if (Input.GetKeyDown(KeyCode.F))
            {
                TraspazarObjetoSostenido.sueloElevado = false;
                objetoEnMano.GetComponent<OpcionSostenible>().esSostenible = true;
                if (objetoEnMano.GetComponent<Collider2D>().enabled == false) objetoEnMano.GetComponent<Collider2D>().enabled = true;
               // if (objetoEnMano.GetComponent<Rigidbody2D>() != null) objetoEnMano.GetComponent<Rigidbody2D>().simulated = true;
                objetoEnMano.transform.SetParent(objetoPadre.transform);
                objetoEnMano.GetComponent<SpriteRenderer>().sortingOrder = 0;
                if (Vector2.Distance(objetoEnMano.transform.position, posSolucion.transform.position) < 0.62f)
                {
                    objetoEnMano.transform.position = new Vector2(objetoEnMano.transform.position.x, objetoEnMano.transform.position.y - 0.3f);

                    string seleccion = objetoEnMano.name;
                    string seleccionOpcion = objetoEnMano.transform.GetComponentInChildren<TextMeshProUGUI>().text;

                    //Verifica la solucion seleccionada
                    verificarSolucion(seleccion, seleccionOpcion);
                }
                else
                {
                    //Restablece la posicion del objeto sostenido en caso de no ser una opcion valida
                    //objetoEnMano.transform.position = posInicialObjeto;
                }
                // El objeto sostenido deja de ser hijo (Se suelta)
                objetoEnMano = null;
                //Detiene la animacion de sostener
                animatorPlayer.SetBool("Sostener", false);
            }
        }

        //Verifica si se sostiene un objeto
        if (objetoEnMano != null)
        {
            //Verifica si se esta en un Suelo Elevado y permite traspazar el objeto sostenido por colisiones inecesarias
            objetoEnMano.GetComponent<Collider2D>().enabled = TraspazarObjetoSostenido.sueloElevado ? false : true;
        }
    }

    // Metodo que se encarga de verificar la respuesta y pasar al siguiente nivel o pregunta
    public void verificarSolucion(string solucion, string solucionOpcion)
    {
        string seleccion = solucion;
        string seleccionOpcion = solucionOpcion;

        gameManagerGeneric.responder(seleccion, seleccionOpcion);
    }
}
