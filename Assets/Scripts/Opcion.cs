using System;
using System.Collections.Generic;

[Serializable]
public class Opcion
{
    public int idopcion;
    public string enunciadoopcion;
    public bool respuesta;
}

[Serializable]
public class ListOpcion
{
    public List<Opcion> data;
}
