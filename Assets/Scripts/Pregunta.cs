using System;
using System.Collections.Generic;

[Serializable]
public class Pregunta
{
    public int idpregunta;
    public string enunciado;
    public string urlImg;
    public List<Opcion> opciones;
}

[Serializable]
public class ListPregunta
{
    public List<Pregunta> data;
}