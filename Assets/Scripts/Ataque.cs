using UnityEngine;

public class Ataque : MonoBehaviour
{
    private float velocidad = 10f;
    public Vector2 posObjetivo;

    public GameObject mano;

    private void Start()
    {
        mano = GameObject.Find("Mano");
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, posObjetivo, velocidad * Time.deltaTime);

        if (Vector2.Distance(transform.position, posObjetivo) <= 0.001f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {

            if (mano.GetComponent<ManoLanzadora>().objetoEnMano != null)
                mano.GetComponent<ManoLanzadora>().soltarObjeto();

            Destroy(gameObject);
        }
    }
}
