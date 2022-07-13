using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{

    private float velocidad = 3f;
    private float tiempoEspera;
    private float inicioTiempoEspera = 4f;
    private int i = 0;
    public Transform[] puntosMovimiento;

    void Start()
    {
        inicioTiempoEspera = Random.Range(1f, 4f);
        tiempoEspera = inicioTiempoEspera;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[i].transform.position, velocidad * Time.deltaTime);

        if (Vector2.Distance(transform.position, puntosMovimiento[i].transform.position) < 0.01f)
        {
            if (tiempoEspera <= 0)
            {
                if (puntosMovimiento[i] != puntosMovimiento[puntosMovimiento.Length - 1])
                {
                    i++;
                }
                else
                {
                    i = 0;
                }

                tiempoEspera = Random.Range(2.0f, 5.5f);
            }
            else
            {
                tiempoEspera -= Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Jugador"))
            collision.collider.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Jugador"))
            collision.collider.transform.SetParent(null);
    }
}
