using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class web : MonoBehaviour
{
    private ControladorCarga ctrCarga;

    private string urlBase = "http://localhost:8080";
    //private string urlBase = "https://bk-terrymathmand.herokuapp.com";
    private string urlPreguntas = "/pregunta";
    private string urlUsuario = "/estudiante/estudiantenombre?nombre=";
    private string urlRespuesta = "/estudiante";

    private void Awake()
    {
        ctrCarga = GameObject.FindObjectOfType<ControladorCarga>();
    }

    public IEnumerator CorrutinaCargar()
    {
        ctrCarga.PantallaDeCarga.SetActive(true);
        ctrCarga.sliderLoad.value = 0f;
        ctrCarga.txtSliderLoad.text = ctrCarga.sliderLoad.value.ToString() + "%";

        UnityWebRequest web = UnityWebRequest.Get(urlBase + urlPreguntas);
        web.SendWebRequest();

        while (!web.isDone)
        {
            ctrCarga.sliderLoad.value += ctrCarga.sliderLoad.value < 0.85 ? 0.005f : 0f;
            ctrCarga.txtSliderLoad.text = (int)(ctrCarga.sliderLoad.value * 100) + "%";
            yield return null;
        }
        Debug.Log(web.downloadHandler.text);
        if (!web.isNetworkError && !web.isHttpError)
        {
            ctrCarga.listaPreguntas = JsonUtility.FromJson<ListPregunta>(web.downloadHandler.text);
            DatosEntreEscenas.instace.listaPreguntas = ctrCarga.listaPreguntas;
            DatosEntreEscenas.instace.numPregunta = 0;
            DatosEntreEscenas.instace.contPreguntas = 0;
            DatosEntreEscenas.instace.vida = ctrCarga.listaPreguntas.data.Count;
            DatosEntreEscenas.instace.preguntasCorrectas = 0;

            ctrCarga.PantallaDeCarga.SetActive(false);

            StartCoroutine(CorrutinaCargarImagenes());
        }
        else
        {
            ctrCarga.errorTextObj.SetActive(true);
            ctrCarga.errorTextObj.GetComponent<Image>().enabled = true;
            ctrCarga.errorTextObj.GetComponentInChildren<TextMeshProUGUI>().text = "Hubo un error al cargar las preguntas, por favor recargue la pagina";
            ctrCarga.PantallaDeCarga.SetActive(false);
        }
    }

    public IEnumerator CorrutinaCargarImagenes()
    {
        ctrCarga.PantallaDeCarga.SetActive(true);

        ctrCarga.img = new Texture2D[ctrCarga.listaPreguntas.data.Count];
        for (int i = 0; i < ctrCarga.listaPreguntas.data.Count;)
        {
            UnityWebRequest reg = UnityWebRequestTexture.GetTexture(ctrCarga.listaPreguntas.data[i].urlImg);
            reg.SendWebRequest();

            while (!reg.isDone)
            {
                ctrCarga.sliderLoad.value += ctrCarga.sliderLoad.value < 0.92 ? 0.005f : 0f;
                ctrCarga.txtSliderLoad.text = (int)(ctrCarga.sliderLoad.value * 100) + "%";
                yield return null;
            }

            if (!reg.isNetworkError && !reg.isHttpError)
            {
                ctrCarga.img[i] = DownloadHandlerTexture.GetContent(reg);
                i++;
            }
            else
            {
                ctrCarga.errorTextObj.SetActive(true);
                ctrCarga.errorTextObj.GetComponent<Image>().enabled = true;
                ctrCarga.errorTextObj.GetComponentInChildren<TextMeshProUGUI>().text = "Hubo un error al cargar las imagenes, por favor recargue la pagina";
                ctrCarga.PantallaDeCarga.SetActive(false);
            }
        }

        DatosEntreEscenas.instace.img = ctrCarga.img;

        ctrCarga.PantallaDeCarga.SetActive(false);
    }

    public IEnumerator CorrutinaVerificarUsuario(string nombre)
    {
        UnityWebRequest web = UnityWebRequest.Get(urlBase + urlUsuario + nombre);
        web.SendWebRequest();

        while (!web.isDone)
        {
            ctrCarga.sliderLoad.value += ctrCarga.sliderLoad.value < 0.85 ? 0.003f : 0f;
            ctrCarga.txtSliderLoad.text = (int)(ctrCarga.sliderLoad.value * 100) + "%";
            yield return null;
        }

        if (!web.isNetworkError && !web.isHttpError)
        {
            ctrCarga.usuario = JsonUtility.FromJson<Estudiante>(web.downloadHandler.text);
            DatosEntreEscenas.instace.usuario = ctrCarga.usuario;
        }
        else
        {
            ctrCarga.errorTextObj.SetActive(true);
            ctrCarga.errorTextObj.GetComponent<Image>().enabled = false;
            ctrCarga.errorTextObj.GetComponentInChildren<TextMeshProUGUI>().text = "Por favor recargue la pagina, no hemos encontrado la base de datos";
        }
    }

    public IEnumerator CorrutinaGuardarRespuesta(Respuesta respuesta, Estudiante usuario)
    {
        Debug.Log("Guardando creo");
        Usuario newUsuario = new Usuario();

        newUsuario.idusuario = usuario.data.idusuario;
        newUsuario.nombre = usuario.data.nombre;
        newUsuario.respuestas.Add(respuesta);

        string newRespuesta = JsonUtility.ToJson(newUsuario).ToString();

        UnityWebRequest web = UnityWebRequest.Put(urlBase + urlRespuesta, newRespuesta);
        web.SetRequestHeader("Content-Type", "application/json;charset=UTF-8;application/x-www-form-urlencoded");
        web.SetRequestHeader("Accept", "application/json");
        yield return web.SendWebRequest();
        if (!web.isNetworkError && !web.isHttpError)
        {
            DatosEntreEscenas.instace.usuario = JsonUtility.FromJson<Estudiante>(web.downloadHandler.text);

            GameObject.FindObjectOfType<GameOver>().leerSimple();
            GameObject.FindObjectOfType<GameOver>().crearTabla();
        }
        else
        {
            Debug.LogWarning("Hubo un error al escribir ruta");
        }
    }
}

