using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{

    public Transform tabla;
    public GameObject plantillaRegistros;

    public TextMeshProUGUI txtUsuario;

    web web;

    private Respuesta respuestaEst = new Respuesta();
    private int preguntasCorrectas;
    private Estudiante usuario = new Estudiante();
    private int numPregunta;

    int cantidadRegistros = 5;

    void Awake()
    {
        web = GameObject.FindObjectOfType<web>();
        leerSimple();
    }

    private void Start()
    {
        StartCoroutine(web.CorrutinaGuardarRespuesta(respuestaEst, usuario));
    }

    public void leerSimple()
    {
        numPregunta = DatosEntreEscenas.instace.numPregunta;
        preguntasCorrectas = DatosEntreEscenas.instace.preguntasCorrectas;
        usuario = DatosEntreEscenas.instace.usuario;
        respuestaEst = DatosEntreEscenas.instace.respuestaEst;
    }

    public void crearTabla()
    {
        DatosEntreEscenas.instace.guardoRespuestas = false;

        if (usuario.data.respuestas.Count < cantidadRegistros) cantidadRegistros = usuario.data.respuestas.Count;

        for (int i = 0; i < cantidadRegistros; i++)
        {
            GameObject inst = Instantiate(plantillaRegistros, tabla);
            inst.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, i * -95f);
        }

        llenarTabla();
    }

    void llenarTabla()
    {
        if (usuario.data.respuestas.Count < cantidadRegistros) cantidadRegistros = usuario.data.respuestas.Count;

        for (int i = 0; i < cantidadRegistros; i++)
        {
            tabla.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = usuario.data.respuestas[i].acertadas.ToString();
            tabla.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = usuario.data.respuestas[i].nota.ToString();
            System.DateTime fecha = usuario.data.respuestas[i].fecha;
            tabla.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text = fecha.ToString();
        }

        txtUsuario.text += usuario.data.nombre;
    }
}
