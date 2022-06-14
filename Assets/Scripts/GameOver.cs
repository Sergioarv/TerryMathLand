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
    private ListRespuesta puntajes = new ListRespuesta();

    int cantidadRegistros = 4;

    void Awake()
    {
        web = GameObject.FindObjectOfType<web>();
        leerSimple();
    }

    private void Start()
    {
        StartCoroutine(web.CorrutinaGuardarRespuesta(respuestaEst, usuario));
    }

    [ContextMenu("Leer")]
    public void leerSimple()
    {
        numPregunta = DatosEntreEscenas.instace.numPregunta;
        preguntasCorrectas = DatosEntreEscenas.instace.preguntasCorrectas;
        usuario = DatosEntreEscenas.instace.usuario;
        respuestaEst = DatosEntreEscenas.instace.respuestaEst;
        puntajes = DatosEntreEscenas.instace.puntajes;
    }

    
    [ContextMenu("Crear Tabla")]
    public void crearTabla()
    {
        if (puntajes.data.Count < cantidadRegistros) cantidadRegistros = puntajes.data.Count;

        for (int i = 0; i <= cantidadRegistros; i++)
        {
            GameObject inst = Instantiate(plantillaRegistros, tabla);
            inst.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, i * -95f);
        }

        llenarTabla();
    }

    void llenarTabla()
    {
        if (puntajes.data.Count < cantidadRegistros) cantidadRegistros = puntajes.data.Count;

        int total = (int)((respuestaEst.acertadas * 5) / respuestaEst.nota);
        tabla.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = respuestaEst.acertadas.ToString() + " de " + total;
        tabla.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = (Mathf.Round((float)(respuestaEst.nota * 10.0f)) * 0.1f).ToString();
        tabla.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Puntaje de hoy";

        for (int i = 0; i < cantidadRegistros; i++)
        {
            total = (int)((puntajes.data[i].acertadas * 5) / puntajes.data[i].nota);
            tabla.GetChild(i+1).GetChild(0).GetComponent<TextMeshProUGUI>().text = puntajes.data[i].acertadas.ToString() + " de " + total;
            tabla.GetChild(i+1).GetChild(1).GetComponent<TextMeshProUGUI>().text = puntajes.data[i].nota.ToString();
            tabla.GetChild(i+1).GetChild(2).GetComponent<TextMeshProUGUI>().text = puntajes.data[i].fecha.ToString();
        }

        txtUsuario.text += usuario.data.nombre;
    }

    public void reiniciarJuego()
    {
        DatosEntreEscenas.instace.reiniciar();
        SceneManager.LoadScene("PantallaPrincipal");
    }
}
