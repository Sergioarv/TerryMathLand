using System;
using System.Collections.Generic;

[Serializable]
public class Respuesta
{
    public int idrespuesta;
    public List<Solucion> soluciones;
    public int acertadas;
    public double nota;
    public DateTime fecha;
}

[Serializable]
public class ListRespuesta
{
    public List<Respuesta> data;
}