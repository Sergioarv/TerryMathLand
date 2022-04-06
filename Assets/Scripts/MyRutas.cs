using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRutas : MonoBehaviour
{
    [System.Serializable]
    public class Rutass
    {
        public List<Ruta> data;
    }

    [System.Serializable]
    public class Origen
    {
        public int idciudad;
        public string nombreciudad;
        public bool visado;
    }

    [System.Serializable]
    public class Destino
    {
        public int idciudad;
        public string nombreciudad;
        public bool visado;
    }

    [System.Serializable]
    public class Ruta
    {
        public int idruta;
        public bool escala;
        public int duracion;
        public Origen origen;
        public Destino destino;
    }
}
