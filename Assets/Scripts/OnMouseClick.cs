using UnityEngine;
using TMPro;

public class OnMouseClick : MonoBehaviour
{
    private GameManagerGeneric gameManagerGeneric;
    private GameObject[] optiones;
    private bool activo = true;

    private void Awake()
    {
        gameManagerGeneric = GameObject.FindObjectOfType<GameManagerGeneric>();
    }

    private void OnMouseDown()
    {
        if (!activo) return;

        string seleccion = this.gameObject.name;
        string seleccionOpcion = this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
              
        optiones = GameObject.FindGameObjectsWithTag("Opcion");

        optiones[0].GetComponent<OnMouseClick>().activo = false;
        optiones[1].GetComponent<OnMouseClick>().activo = false;
        optiones[2].GetComponent<OnMouseClick>().activo = false;
        optiones[3].GetComponent<OnMouseClick>().activo = false;

        gameManagerGeneric.responder(seleccion, seleccionOpcion);
    }

}
