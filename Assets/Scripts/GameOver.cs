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

    public Respuesta respuestaEst = new Respuesta();
    public int preguntasCorrectas;
    public Usuario usuario = new Usuario();
    public int numPregunta;

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

    private void Update()
    {
        if (DatosEntreEscenas.instace.guardoRespuestas)
        {
            crearTabla();
            DatosEntreEscenas.instace.guardoRespuestas = false;
        }
    }

    public void leerSimple()
    {
        numPregunta = DatosEntreEscenas.instace.numPregunta;
        preguntasCorrectas = DatosEntreEscenas.instace.preguntasCorrectas;
        usuario = DatosEntreEscenas.instace.usuario;
        respuestaEst = DatosEntreEscenas.instace.respuestaEst;
    }

    [ContextMenu("Crear Tabla")]
    void crearTabla()
    {
        if (usuario.respuestas.Count < cantidadRegistros) cantidadRegistros = usuario.respuestas.Count;

        for (int i = 0; i < cantidadRegistros; i++)
        {
            GameObject inst = Instantiate(plantillaRegistros, tabla);
            inst.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, i * -95f);
        }

        llenarTabla();
    }

    [ContextMenu("LLenar")]
    void llenarTabla()
    {
        if (usuario.respuestas.Count < cantidadRegistros) cantidadRegistros = usuario.respuestas.Count;

        for (int i = 0; i < cantidadRegistros; i++)
        {
            tabla.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = usuario.respuestas[i].acertadas.ToString();
            tabla.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = usuario.respuestas[i].nota.ToString();
            System.DateTime fecha = usuario.respuestas[i].fecha;
            tabla.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text = fecha.ToString();
        }

        txtUsuario.text += usuario.nombre;
    }
}
