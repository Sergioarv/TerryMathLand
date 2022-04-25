using System;
using System.Collections.Generic;

[Serializable]
public class Solucion
{
    public int idsolucion;
    public string enunciadoPre;
    public string respuestaPre;
    public string respuestaEst;
}

[Serializable]
public class ListSolucion
{
    public List<Solucion> data;
}