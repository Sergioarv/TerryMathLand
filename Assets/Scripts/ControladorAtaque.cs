using System.Collections.Generic;
using UnityEngine;

public class ControladorAtaque : MonoBehaviour
{
    private GameObject controladorAtaque;
    public List<GameObject> ataque;
    private GameObject jugador;

    private Animator animatorEnemigo;

    private float tiempoEspera = 5f;

    private void Start()
    {
        jugador = GameObject.Find("Jugador");
        animatorEnemigo = GetComponent<Animator>();
        controladorAtaque = GameObject.Find("ControladorAtaque");
    }

    private void Update()
    {
        if(tiempoEspera <= 0)
        {
            int tiro = Random.Range(0, 9);
            GameObject bala = Instantiate(ataque[tiro], controladorAtaque.transform);
            bala.GetComponent<Ataque>().posObjetivo = jugador.transform.position;

            animatorEnemigo.SetTrigger("Ataque");

            tiempoEspera = Random.Range(1f, 5f);
        }
        else
        {
            tiempoEspera -= 1 * Time.deltaTime;
        }
    }
}
