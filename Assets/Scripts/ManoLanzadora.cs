using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManoLanzadora : MonoBehaviour
{

    private GameObject mano;
    public GameObject objetoEnMano = null;
    public GameObject objetoASostener = null;
    private GameObject objetoPadre;

    private Animator animatorPlayer;
    private Animator animatorBomba;

    private SpriteRenderer spriteBomba;

    private Rigidbody2D rgbOpcion;

    public float dirX;

    private GameManagerGeneric gameManagerGeneric;

    private void Start()
    {
        mano = GameObject.Find("Mano");
        animatorPlayer = GameObject.Find("Jugador").GetComponent<Animator>();
        gameManagerGeneric = GameObject.FindObjectOfType<GameManagerGeneric>();
    }
    private void Update()
    {
        // Verifica si esta frente a un objeto sostenible, si no hay objetos en la mano
        if (objetoASostener != null && objetoASostener.GetComponent<OpcionLanzable>().esLanzable && objetoEnMano == null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                recogerObjeto();
            }
        } // Verifica si sostenemos un objeto
        else if (objetoEnMano != null)
        {
            // Verifica si al sostener un objeto se presiona la tecla F para soltar
            if (Input.GetKeyDown(KeyCode.F))
            {
                soltarObjeto();

            } // Verifica si tenemos un objeto y lo lanzamos
            else if (Input.GetMouseButtonDown(0))
            {
                lanzarObjeto();
            }
        }

        if (objetoEnMano != null)
        {
            animatorBomba.SetFloat("Mov", dirX);
        }
    }

    public void recogerObjeto()
    {
        objetoPadre = objetoASostener.transform.parent.gameObject;

        objetoEnMano = objetoASostener;
        animatorBomba = objetoASostener.GetComponent<Animator>();
        objetoEnMano.GetComponent<OpcionLanzable>().esLanzable = false;
        objetoEnMano.transform.position = mano.transform.GetChild(0).position;
        objetoEnMano.transform.SetParent(transform);
        if (objetoEnMano.GetComponent<Rigidbody2D>() != null) objetoEnMano.GetComponent<Rigidbody2D>().simulated = false;
        objetoEnMano.GetComponents<Collider2D>()[0].enabled = false;
        objetoEnMano.GetComponent<SpriteRenderer>().sortingOrder = 1;
        spriteBomba = objetoEnMano.GetComponent<SpriteRenderer>();

        animatorPlayer.SetBool("Sostener", true);
    }

    public void soltarObjeto()
    {
        objetoEnMano.GetComponent<OpcionLanzable>().esLanzable = true;
        if (objetoEnMano.GetComponents<Collider2D>()[0].enabled == false) objetoEnMano.GetComponents<Collider2D>()[0].enabled = true;
        if (objetoEnMano.GetComponent<Rigidbody2D>() != null) objetoEnMano.GetComponent<Rigidbody2D>().simulated = true;
        objetoEnMano.transform.SetParent(objetoPadre.transform);
        objetoEnMano.GetComponent<SpriteRenderer>().sortingOrder = 0;
        spriteBomba = null;

        objetoEnMano.transform.position = new Vector2(objetoEnMano.transform.position.x, objetoEnMano.transform.position.y);

        dirX = 0;
        animatorBomba.SetFloat("Mov", dirX);

        animatorBomba = null;
        objetoEnMano = null;

        animatorPlayer.SetBool("Sostener", false);
    }

    public void lanzarObjeto()
    {
        objetoEnMano.GetComponent<OpcionLanzable>().esLanzable = true;
        if (objetoEnMano.GetComponents<Collider2D>()[0].enabled == false) objetoEnMano.GetComponents<Collider2D>()[0].enabled = true;
        objetoEnMano.transform.SetParent(objetoPadre.transform);
        objetoEnMano.GetComponent<SpriteRenderer>().sortingOrder = 0;
        spriteBomba = null;

        if (objetoEnMano.GetComponent<Rigidbody2D>() != null) objetoEnMano.GetComponent<Rigidbody2D>().simulated = true;
        objetoEnMano.GetComponent<OpcionLanzable>().lanzarObjeto();

        dirX = 0;
        animatorBomba.SetFloat("Mov", dirX);

        animatorBomba = null;
        objetoEnMano = null;
        animatorPlayer.SetBool("Sostener", false);
        animatorPlayer.SetBool("Tirar", true);

        StartCoroutine(GetComponentInParent<ControladorPersonaje>().congelarMovimiento());
    }

    public void verificarSolucion(string solucion, string solucionOpcion)
    {
        string seleccion = solucion;
        string seleccionOpcion = solucionOpcion;

        gameManagerGeneric.responder(seleccion, seleccionOpcion);
    }
}
