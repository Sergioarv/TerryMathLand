using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosEntreEscenas : MonoBehaviour
{
    // instancia de DatosEntreEscenas
    public static DatosEntreEscenas instace;
    // contador total de preguntas realizadas
    public int numPregunta;
    // contador de preguntas por nivel
    public int contPreguntas;
    // contador de vidas, permite llevar el número de vida restante
    public int vida;
    // contador de preguntas acertadas correctamente
    public int preguntasCorrectas;
    // usuario que se encuentra jugando
    public Estudiante usuario = new Estudiante();
    // lista de preguntas, guarda en memoria todas las preguntas de la base de datos
    public ListPregunta listaPreguntas = new ListPregunta();
    // La respuesta del jugador, en ella se almacena una lista con las soluciones
    public Respuesta respuestaEst = new Respuesta();
    // Lista de imágenes, guarda en memoria todas las imágenes cargadas de la base de datos
    public Texture2D[] img;

    public ListRespuesta puntajes = new ListRespuesta();

    public int preguntasPorNivel;
    public int restante;

    // Método Awake, método propio de la clase que se ejecuta al cargar la escena que lo contiene
    private void Awake()
    {
        // Se verifica si la instancia es nula
        // Si es nula toma como valor la primera instancia creada por la escena
        if (instace == null)
        {
            // 'this' es el primer prefab de DatosEntreEscenas creado en el proyecto
            instace = this;
            /* Se le especifica al proyecto Unity no borrar la instancia al 
             cambiar de escena o volver a cargar la escena*/
            DontDestroyOnLoad(instace);
        }
        else
        {
            // verifica si la instancia es el primer prefab creado por la escena
            /* Si la instancia contiene el primer prefab creado y 'this' es un 
             prefab distinto creado por una escena diferente a la primera la destruye
            y conserva la primera instancia */
            if (instace != this)
            {
                // Destruye el objecto que ejecuta el script para no crear copias o clones
                Destroy(gameObject);
            }
        }
    }

    internal void reiniciar()
    {
        numPregunta = 0;
        contPreguntas = 0;
        vida = 0;
        preguntasCorrectas = 0;
        usuario = new Estudiante();
        listaPreguntas = new ListPregunta();
        respuestaEst = new Respuesta();
        img = null;
    }
}
