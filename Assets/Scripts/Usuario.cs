using System.Collections.Generic;
using System;

[Serializable]
public class Usuario
{
    public int idusuario;
    public string nombre;
    public List<Respuesta> respuestas = new List<Respuesta>();
}

[Serializable]
public class Estudiante
{
    public Usuario data = new Usuario();
}
