using UnityEngine;
using TMPro;

public class OpcionLanzable : MonoBehaviour
{
    public bool esLanzable = true;
    public bool lanzado = false;
    public bool cae = true;

    private GameObject[] optiones;

    public float dirX;
    public bool respondio = false;

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

            optiones = GameObject.FindGameObjectsWithTag("Opcion");
            GameObject.FindGameObjectsWithTag("Solucion")[0].GetComponent<Collider2D>().enabled = false;

            optiones[0].GetComponent<OpcionLanzable>().esLanzable = false;
            optiones[1].GetComponent<OpcionLanzable>().esLanzable = false;
            optiones[2].GetComponent<OpcionLanzable>().esLanzable = false;
            optiones[3].GetComponent<OpcionLanzable>().esLanzable = false;

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
            if ( transform.position.y + 0.2f >= collision.transform.position.y)
            {
                respondio = true;
            }
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
