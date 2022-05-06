using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorAtaque : MonoBehaviour
{
    public Transform controladorAtaque;
    public List<GameObject> ataque;

    public GameObject jugador;

    public float tiempoEspera = 5f;

    private void Start()
    {
        jugador = GameObject.Find("Jugador");
    }

    private void Update()
    {
        if(tiempoEspera <= 0)
        {
            int tiro = Random.Range(0, 9);
            GameObject bala = Instantiate(ataque[tiro], controladorAtaque);
            bala.GetComponent<Ataque>().posObjetivo = jugador.transform.position;
            tiempoEspera = Random.Range(1f, 5f);
        }
        else
        {
            tiempoEspera -= 1 * Time.deltaTime;
        }
    }
}
