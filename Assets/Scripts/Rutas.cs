using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class Rutas : MonoBehaviour
{

    public GameObject PantallaDeCarga;
    public Slider sliderLoad;
    public bool errorWeb = false;
    public GameObject errorTextObj;

    //public MyRutas.Rutass rutaList = new MyRutas.Rutass();

    private void Awake()
    {
        leerSimple();
    }
       

    [ContextMenu("Leer simple")]
    public void leerSimple()
    {
        StartCoroutine(CorrutinaLeerSimple());
    }

    private IEnumerator CorrutinaLeerSimple()
    {
        UnityWebRequest web = UnityWebRequest.Get("http://localhost:8080/ruta/2");
        yield return web.SendWebRequest();

        if (!web.isNetworkError && !web.isHttpError)
        {
            //rutaList = JsonUtility.FromJson<MyRutas.Rutass>(web.downloadHandler.text);
            //DatosEntreEscenas.instace.rutaList = rutaList;
            DatosEntreEscenas.instace.numPregunta = 0;
            DatosEntreEscenas.instace.contPreguntas = 0;
        }
        else
        {
            Debug.LogWarning("Hubo un error al leer");
            errorWeb = true;
            errorTextObj.SetActive(true);
        }
    }

    [ContextMenu("Escribir simple")]
    public void escribirSimple()
    {
        StartCoroutine(CorrutinaEscribirSimple());
    }

    private IEnumerator CorrutinaEscribirSimple()
    {

        MyRutas.Origen newCiudad = new MyRutas.Origen();
        newCiudad.idciudad = 14;

        MyRutas.Destino newCiudadD = new MyRutas.Destino();
        newCiudadD.idciudad = 8;

        MyRutas.Ruta newRuta = new MyRutas.Ruta();
        newRuta.idruta = 14;
        newRuta.escala = true;
        newRuta.duracion = 120;
        newRuta.origen = newCiudad;
        newRuta.destino = newCiudadD;

        string newRutaStr = JsonUtility.ToJson(newRuta).ToString();

        UnityWebRequest web = UnityWebRequest.Put("http://localhost:8080/ruta", newRutaStr);
        web.SetRequestHeader("Content-Type", "application/json;charset=UTF-8;application/x-www-form-urlencoded");
        web.SetRequestHeader("Accept", "application/json");
        yield return web.SendWebRequest();
        if (!web.isNetworkError && !web.isHttpError)
        {
            Debug.Log(web.downloadHandler.text);
        }
        else
        {
            Debug.LogWarning("Hubo un error al escribir ruta");
        }
    }
    /*
    public void cargarEscena()
    {
        StartCoroutine(CargaEscenaSimple());
    }

    private IEnumerator CargaEscenaSimple()
    {

        PantallaDeCarga.SetActive(true);
        sliderLoad.value = 0.0f;
        
        while (rutaList.Count == 0 && !errorWeb )
        {
            sliderLoad.value += sliderLoad.value > 0.9f ? 0f : 0.01f;
            Debug.Log("Esperando ...");
            yield return null;
        }
        
        if (rutaList.data.Count > 0 )
        {
            AsyncOperation loading = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);

            while (!loading.isDone)
            {
                float progress = Mathf.Clamp01(loading.progress / .09f);
                Debug.Log(progress);
                sliderLoad.value += progress;

                yield return null;
            }
        }
        else
        {
            PantallaDeCarga.SetActive(false);
        }
        
    }
    */
}
