using System.Collections.Generic;
using System;

[Serializable]
public class Usuario
{
    public int idusuario;
    public string nombre;
    public List<Respuesta> respuestas;
}

[Serializable]
public class ListUsuario
{
    public List<Usuario> data;
}