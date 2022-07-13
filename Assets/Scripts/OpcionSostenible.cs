using UnityEngine;

public class OpcionSostenible : MonoBehaviour
{
    public bool esSostenible = true;
    public Vector2 posInicialObjeto;

    private void Start()
    {
        posInicialObjeto = transform.position;
    }

    private void FixedUpdate()
    {
        if(esSostenible)
        {
            if ((transform.position.y <= 1.1f && transform.position.y >= -2f) && (transform.position.x >= -12.8f && transform.position.x <= -11f))
            {
                transform.position = posInicialObjeto;
            }
            else if ((transform.position.y <= -0.6f && transform.position.y >= -2f) && (transform.position.x >= -12.8f && transform.position.x <= -10f))
            {
                transform.position = posInicialObjeto;
            }
            else if ((transform.position.y <= -0.6f && transform.position.y >= -2f) && (transform.position.x >= -7.25f && transform.position.x <= 12.7f))
            {
                transform.position = posInicialObjeto;
            }
            else if (transform.position.y >= 1.8f && (transform.position.x >= -13f && transform.position.x <= 12.7f))
            {
                transform.position = posInicialObjeto;
            }
            else if (transform.position.y >= 0.7f && (transform.position.x > -5.4f && transform.position.x < -3.66f))
            {
                transform.position = posInicialObjeto;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            collision.GetComponent<ManoJugador>().objetoASostener = this.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            collision.GetComponent<ManoJugador>().objetoASostener = null;
        }
    }
}
