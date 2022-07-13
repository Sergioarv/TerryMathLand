using System;
using System.Collections.Generic;

[Serializable]
public class Respuesta
{
    public int idrespuesta;
    public List<Solucion> soluciones;
    public int acertadas;
    public int cantidadPreguntas;
    public double nota;
    public string fecha;
}

[Serializable]
public class ListRespuesta
{
    public List<Respuesta> data;
}