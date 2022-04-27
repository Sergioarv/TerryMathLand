using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VerificarRespuesta : MonoBehaviour
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E) && activo)
        {
            string seleccion = this.gameObject.name;
            string seleccionOpcion = this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;

            Debug.Log(collision.name + " -- " + seleccion + " -- " + seleccionOpcion);

            optiones = GameObject.FindGameObjectsWithTag("Opcion");

            optiones[0].GetComponent<VerificarRespuesta>().activo = false;
            optiones[1].GetComponent<VerificarRespuesta>().activo = false;
            optiones[2].GetComponent<VerificarRespuesta>().activo = false;
            optiones[3].GetComponent<VerificarRespuesta>().activo = false;

            gameManagerGeneric.responder(seleccion, seleccionOpcion);
        }

        return;
    }
}
