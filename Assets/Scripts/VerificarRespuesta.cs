using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VerficarRespuesta : MonoBehaviour
{
    public GameManagerGeneric gameManagerGeneric;
    private GameObject[] optiones;
    public bool activo = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            Debug.Log("Respondio" + this.name);

            if (!activo) return;

            string seleccion = this.gameObject.name;
            string seleccionOpcion = this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;

            optiones = GameObject.FindGameObjectsWithTag("Opcion");

            Debug.Log(optiones.Length);

            optiones[0].GetComponent<OnMouseClick>().activo = false;
            optiones[1].GetComponent<OnMouseClick>().activo = false;
            optiones[2].GetComponent<OnMouseClick>().activo = false;
            optiones[3].GetComponent<OnMouseClick>().activo = false;

            gameManagerGeneric.responder(seleccion, seleccionOpcion);
        }
    }
}
