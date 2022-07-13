using System.Collections.Generic;

[System.Serializable]
public class Cartilla
{
    public string nombre;
    public int idcartilla;
}

[System.Serializable]
public class ListaCartilla
{
    public List<Cartilla> data = new List<Cartilla>();
    public bool success;
    public string message;
}
