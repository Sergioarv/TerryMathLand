using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseClick : MonoBehaviour
{
    public GameManagerGeneric gameManagerGeneric;
    private GameObject[] optiones;
    public bool activo = true;

    private void OnMouseDown()
    {
        if (!activo) return;

        gameManagerGeneric.responder(this.name);

        optiones = GameObject.FindGameObjectsWithTag("Opcion");

        optiones[0].GetComponent<OnMouseClick>().activo = false;
        optiones[1].GetComponent<OnMouseClick>().activo = false;
        optiones[2].GetComponent<OnMouseClick>().activo = false;
        optiones[3].GetComponent<OnMouseClick>().activo = false;
    }

}
