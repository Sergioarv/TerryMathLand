using UnityEngine;

public class VerificarSuelo : MonoBehaviour
{
    public static bool tocandoSuelo;
    public static bool tocandoOpcion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo"))
        {
            tocandoSuelo = true;
        }
        else if (collision.CompareTag("Opcion"))
        {
            tocandoOpcion = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo"))
        {
            tocandoSuelo = false;
        }
        else if (collision.CompareTag("Opcion"))
        {
            tocandoOpcion = false;
        }
    }
}
