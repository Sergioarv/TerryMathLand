using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class web : MonoBehaviour
{

    public Image imagen;

    private void Awake()
    {
        CargarImagen();
    }

    public IEnumerator CargarImagen()
    {
        GameObject oI = GameObject.FindGameObjectsWithTag("Imagen")[0];
        imagen = oI.GetComponentInChildren<Image>();

        Debug.Log(imagen.name);
        UnityWebRequest reg = UnityWebRequestTexture.GetTexture("https://images.vexels.com/media/users/3/155414/isolated/lists/6c5e852026e7fd11f79cf45a65299016-silueta-de-vista-lateral-de-coche-suv.png");
        yield return reg.SendWebRequest();

        if (!reg.isNetworkError && !reg.isHttpError)
        {
            Texture2D img = DownloadHandlerTexture.GetContent(reg);
            Sprite sprite = Sprite.Create(img, new Rect(0, 0, 100, 100), Vector2.zero);
            imagen.sprite = sprite;
        }
        else
        {
            Debug.LogWarning("Hubo un error al leer");
        }
    }

    private IEnumerator CorrutinaLeerImagen(string url)
    {
        Debug.Log(imagen.name);
        UnityWebRequest reg = UnityWebRequestTexture.GetTexture(url);
        yield return reg.SendWebRequest();

        if (!reg.isNetworkError && !reg.isHttpError)
        {
            Texture2D img = DownloadHandlerTexture.GetContent(reg);
            Sprite sprite = Sprite.Create(img, new Rect(0, 0, 100, 100), Vector2.zero);
            imagen.sprite = sprite;
        }
        else
        {
            Debug.LogWarning("Hubo un error al leer");
        }
    }

    /*
    [System.Serializable]
    public class Ciudades
    {
        public List<Ciudad> data;
    }

    [System.Serializable]
    public class Ciudad
    {
        public int idciudad;
        public string nombreciudad;
        public bool visado;
    }

    public Ciudades ciudadesList = new Ciudades();


    [ContextMenu("Leer simple")]
    public void leerSimple()
    {
        StartCoroutine(CorrutinaLeerSimple());
    }

    private IEnumerator CorrutinaLeerSimple()
    {
        UnityWebRequest web = UnityWebRequest.Get("https://bk-aerolinea-bajocosto.herokuapp.com/ciudad");
        yield return web.SendWebRequest();
        //Debug.Log(web.downloadHandler.text);
        if (!web.isNetworkError && !web.isHttpError)
        {
            ciudadesList = JsonUtility.FromJson<Ciudades>(web.downloadHandler.text);
            foreach (Ciudad c in ciudadesList.data)
            {
                Debug.Log(c.nombreciudad);
            }
        }
        else
        {
            Debug.LogWarning("Hubo un error al leer");
        }
    }

    [ContextMenu("Escribir simple")]
    public void escribirSimple()
    {
        StartCoroutine(CorrutinaEscribirSimple());
    }

    private IEnumerator CorrutinaEscribirSimple()
    {

        Ciudad newCiudad = new Ciudad();
        newCiudad.idciudad = 14;
        newCiudad.nombreciudad = "SALENTO";
        newCiudad.visado = true;
        string newCiudadStr = JsonUtility.ToJson(newCiudad).ToString();

        Debug.LogWarning(newCiudadStr.ToString());

        UnityWebRequest web = UnityWebRequest.Put("http://localhost:8080/ciudad", newCiudadStr);
        web.SetRequestHeader("Content-Type", "application/json;charset=UTF-8;application/x-www-form-urlencoded");
        web.SetRequestHeader("Accept", "application/json");
        yield return web.SendWebRequest();
        if (!web.isNetworkError && !web.isHttpError)
        {
            Debug.Log(web.downloadHandler.text);
        }
        else
        {
            Debug.LogWarning("Hubo un error al leer");
        }
    }
    */
}
