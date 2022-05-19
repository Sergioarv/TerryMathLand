﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosEntreEscenas : MonoBehaviour
{

    public static DatosEntreEscenas instace;

    public int numPregunta;
    public int contPreguntas;
    public int vida;
    public int preguntasCorrectas;

    public Estudiante usuario = new Estudiante();

    public ListPregunta listaPreguntas = new ListPregunta();

    public Respuesta respuestaEst = new Respuesta();

    public Texture2D[] img;

    public bool guardoRespuestas = false;

    private void Awake()
    {
        if( instace == null)
        {
            instace = this;
            DontDestroyOnLoad(instace);
        }
        else
        {
            if( instace != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
