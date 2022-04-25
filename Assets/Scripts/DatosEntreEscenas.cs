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

    public Usuario usuario = new Usuario();

    public ListPregunta listaPreguntas = new ListPregunta();

    public Respuesta respuestaEst = new Respuesta();

    public Texture2D[] img;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}