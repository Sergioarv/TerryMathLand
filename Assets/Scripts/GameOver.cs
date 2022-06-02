using UnityEngine;
using TMPro;
using System.Globalization;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public Transform tabla;
    public GameObject plantillaRegistros;

    public TextMeshProUGUI txtUsuario;

    private web web;

    private Respuesta respuestaEst = new Respuesta();
    private int preguntasCorrectas;
    private Estudiante usuario = new Estudiante();
    private int numPregunta;

    int cantidadRegistros = 4;

    void Awake()
    {
        web = GameObject.FindObjectOfType<web>();
        leerSimple();
    }

    private void Start()
    {
        Debug.Log(respuestaEst.nota + "__" + DatosEntreEscenas.instace.respuestaEst.nota);
        StartCoroutine(web.CorrutinaGuardarRespuesta(respuestaEst, usuario));
    }

    [ContextMenu("Leer")]
    public void leerSimple()
    {
        numPregunta = DatosEntreEscenas.instace.numPregunta;
        preguntasCorrectas = DatosEntreEscenas.instace.preguntasCorrectas;
        usuario = DatosEntreEscenas.instace.usuario;
        respuestaEst = DatosEntreEscenas.instace.respuestaEst;
    }

    
    [ContextMenu("Crear Tabla")]
    public void crearTabla()
    {
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
            tabla.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text = usuario.data.respuestas[i].fecha.ToString();
        }

        txtUsuario.text += usuario.data.nombre;
    }

    public void reiniciarJuego()
    {
        DatosEntreEscenas.instace.reiniciar();
        SceneManager.LoadScene("PantallaPrincipal");
    }
}
