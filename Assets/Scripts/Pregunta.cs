using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pregunta
{
    public int idpregunta;
    public string enunciado;
    public string urlImg;
    public List<Opcion> opciones;
}

[Serializable]
public class listPregunta
{
    public List<Pregunta> data;
}