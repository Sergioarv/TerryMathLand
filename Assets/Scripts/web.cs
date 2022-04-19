using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class web : MonoBehaviour
{
    public ControladorCarga ctrCarga;

    public IEnumerator CorrutinaCargar()
    {

        ctrCarga.PantallaDeCarga.SetActive(true);
        ctrCarga.sliderLoad.value = 0f;
        ctrCarga.txtSliderLoad.text = ctrCarga.sliderLoad.value.ToString() + "%";

        UnityWebRequest web = UnityWebRequest.Get("http://localhost:8080/ruta/2");
        web.SendWebRequest();

        while (!web.isDone)
        {
            ctrCarga.sliderLoad.value += ctrCarga.sliderLoad.value < 0.85 ? 0.0005f : 0f;
            ctrCarga.txtSliderLoad.text = (int) (ctrCarga.sliderLoad.value * 100) + "%";
            yield return null;
        }

        if (!web.isNetworkError && !web.isHttpError)
        {
            ctrCarga.rutaList = JsonUtility.FromJson<MyRutas.Rutass>(web.downloadHandler.text);
            DatosEntreEscenas.instace.rutaList = ctrCarga.rutaList;
            DatosEntreEscenas.instace.numPregunta = 0;
            DatosEntreEscenas.instace.contPreguntas = 0;
            DatosEntreEscenas.instace.vida = ctrCarga.rutaList.data.Count;
            DatosEntreEscenas.instace.preguntasCorrectas = 0;

            ctrCarga.PantallaDeCarga.SetActive(false);

            StartCoroutine(CorrutinaCargarImagenes());
        }
        else
        {
            ctrCarga.errorTextObj.SetActive(true);
            ctrCarga.errorTextObj.GetComponent<Image>().enabled = true;
            ctrCarga.errorTextObj.GetComponentInChildren<TextMeshProUGUI>().text = "Hubo un error al leer, recargue la pagina por favor";
            ctrCarga.PantallaDeCarga.SetActive(false);
        }
    }

    public IEnumerator CorrutinaCargarImagenes()
    {
        ctrCarga.PantallaDeCarga.SetActive(true);

        ctrCarga.img = new Texture2D[ctrCarga.rutaList.data.Count];
        for (int i = 0; i < ctrCarga.rutaList.data.Count;)
        {
            UnityWebRequest reg = UnityWebRequestTexture.GetTexture(ctrCarga.rutaList.data[i].destino.nombreciudad);
            reg.SendWebRequest();

            while (!reg.isDone)
            {
                ctrCarga.sliderLoad.value += ctrCarga.sliderLoad.value < 0.85 ? 0.0005f : 0f;
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
                Debug.LogWarning("Hubo un error al leer");
            }
        }

        DatosEntreEscenas.instace.img = ctrCarga.img;

        ctrCarga.PantallaDeCarga.SetActive(false);
    }

    public IEnumerator CorrutinaVerificarUsuario(string id)
    {
        UnityWebRequest web = UnityWebRequest.Get("http://localhost:8080/pasajero?id=" + id);
        web.SendWebRequest();

        while (!web.isDone)
        {
            ctrCarga.sliderLoad.value += ctrCarga.sliderLoad.value < 0.85 ? 0.003f : 0f;
            ctrCarga.txtSliderLoad.text = (int)(ctrCarga.sliderLoad.value * 100) + "%";
            yield return null;
        }

        if (!web.isNetworkError && !web.isHttpError)
        {
            ctrCarga.p = JsonUtility.FromJson<pasajero>(web.downloadHandler.text);
            DatosEntreEscenas.instace.p = ctrCarga.p;
            ctrCarga.buscoP = true;
        }
        else
        {
            ctrCarga.errorTextObj.SetActive(true);
            ctrCarga.errorTextObj.GetComponent<Image>().enabled = false;
            ctrCarga.errorTextObj.GetComponentInChildren<TextMeshProUGUI>().text = "Por favor verifique el nombre";
            ctrCarga.buscoP = true;
        }
    }

}
