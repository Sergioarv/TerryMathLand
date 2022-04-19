using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ControladorCarga : MonoBehaviour
{
    public web web;


    public GameObject PantallaDeCarga;
    public Slider sliderLoad;
    public GameObject errorTextObj;
    public TMP_InputField userInput;

    public Texture2D[] img;

    public MyRutas.Rutass rutaList = new MyRutas.Rutass();

    public pasajero p = new pasajero();
    public bool busco = false;
    public bool buscoP = false;

    private void Awake()
    {
        leerSimple();
    }

    private void Start()
    {
        
    }

    [ContextMenu("Leer simple")]
    public void leerSimple()
    {
        StartCoroutine(web.CorrutinaCargar());
    }

    /*
    private IEnumerator CorrutinaLeerSimple()
    {
        UnityWebRequest web = UnityWebRequest.Get("http://localhost:8080/ruta/2");
        yield return web.SendWebRequest();

        if (!web.isNetworkError && !web.isHttpError)
        {
            rutaList = JsonUtility.FromJson<MyRutas.Rutass>(web.downloadHandler.text);
            DatosEntreEscenas.instace.rutaList = rutaList;
            DatosEntreEscenas.instace.numPregunta = 0;
            DatosEntreEscenas.instace.contPrguntas = 0;
            DatosEntreEscenas.instace.vida = rutaList.data.Count;
            DatosEntreEscenas.instace.preguntasCorrectas = 0;

            StartCoroutine(CorrutinaCargarImagenes());
        }
        else
        {
            errorTextObj.SetActive(true);
            errorTextObj.GetComponent<Image>().enabled = true;
            errorTextObj.GetComponentInChildren<TextMeshProUGUI>().text = "Hubo un error al leer, recargue la pagina por favor";
            buscoP = true;
        }
    }
    
    private IEnumerator CorrutinaCargarImagenes()
    {
        img = new Texture2D[rutaList.data.Count];
        for (int i = 0; i < rutaList.data.Count;)
        {
            UnityWebRequest reg = UnityWebRequestTexture.GetTexture(rutaList.data[i].destino.nombreciudad);
            yield return reg.SendWebRequest();

            if (!reg.isNetworkError && !reg.isHttpError)
            {
                img[i] = DownloadHandlerTexture.GetContent(reg);
                i++;
            }
            else
            {
                Debug.LogWarning("Hubo un error al leer");
            }
        }
        buscoP = true;
        DatosEntreEscenas.instace.img = img;
    }
    */
    /*
    private IEnumerator CorrutinaVerificarUsuario(string id)
    {
        UnityWebRequest web = UnityWebRequest.Get("http://localhost:8080/pasajero?id="+id);
        yield return web.SendWebRequest();

        if (!web.isNetworkError && !web.isHttpError)
        {
            p = JsonUtility.FromJson<pasajero>(web.downloadHandler.text);
            DatosEntreEscenas.instace.p = p;
        }
        else
        {
            errorTextObj.SetActive(true);
            errorTextObj.GetComponent<Image>().enabled = false;
            errorTextObj.GetComponentInChildren<TextMeshProUGUI>().text = "Por favor verifique el nombre";
            busco = true;
        }
    }
    */

    public void verificar()
    {
        StartCoroutine(Comenzar());
    }

    public IEnumerator Comenzar()
    {
        string txtUser = userInput.text;
        
        if (txtUser == "")
        {
            errorTextObj.SetActive(true);
            errorTextObj.GetComponent<Image>().enabled = false;
            errorTextObj.GetComponentInChildren<TextMeshProUGUI>().text = "Por favor ingrese el nombre";
            PantallaDeCarga.SetActive(false);
        }
        else
        {
            PantallaDeCarga.SetActive(true);
            sliderLoad.gameObject.SetActive(true);
            sliderLoad.value = 0.0f;

            if (rutaList.data.Count > 0)
            {
                StartCoroutine(web.CorrutinaVerificarUsuario(txtUser));

                if (p == null || p.nombre == "")
                {
                    PantallaDeCarga.SetActive(false);
                    errorTextObj.SetActive(true);
                    errorTextObj.GetComponent<Image>().enabled = false;
                    errorTextObj.GetComponentInChildren<TextMeshProUGUI>().text = "Por favor verifique el nombre";
                }
                else
                {
                    AsyncOperation loading = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

                    while (!loading.isDone)
                    {
                        float progress = Mathf.Clamp01(loading.progress / 0.9f);
                        sliderLoad.value += progress;
                        yield return null;
                    }
                }
            }
            else
            {
                PantallaDeCarga.SetActive(false);
            }
        }
    }
    /*
    private IEnumerator CargaEscenaSimple()
    {
        string txtUser = userInput.text;
        busco = false;

        if (txtUser == "")
        {
            errorTextObj.SetActive(true);
            errorTextObj.GetComponent<Image>().enabled = false;
            errorTextObj.GetComponentInChildren<TextMeshProUGUI>().text = "Por favor ingrese el nombre";
            PantallaDeCarga.SetActive(false);
        }
        else
        {
            PantallaDeCarga.SetActive(true);
            sliderLoad.value = 0.0f;

            while (rutaList.data.Count == 0 && !buscoP)
            {
                sliderLoad.value += sliderLoad.value > 0.9f ? 0f : 0.001f;
                Debug.Log("Esperando ...");
                yield return null;
            }

            while (rutaList.data != null && !buscoP)
            {
                sliderLoad.value += sliderLoad.value > 0.9f ? 0f : 0.001f;
                Debug.Log("Esperando img...");
                yield return null;
            }         

            if (rutaList.data.Count > 0 )
            {
                StartCoroutine(CorrutinaVerificarUsuario(txtUser));

                while (!busco && !buscoP)
                {
                    sliderLoad.value += sliderLoad.value > 0.9f ? 0f : 0.001f;
                    Debug.Log("Esperando pasajero...");
                    yield return null;
                }

                if (p == null || p.nombre == "")
                {
                    PantallaDeCarga.SetActive(false);
                    errorTextObj.SetActive(true);
                    errorTextObj.GetComponent<Image>().enabled = false;
                    errorTextObj.GetComponentInChildren<TextMeshProUGUI>().text = "Por favor verifique el nombre";
                    busco = false;
                }
                else { 
                    AsyncOperation loading = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

                    while (!loading.isDone)
                    {
                        float progress = Mathf.Clamp01(loading.progress / 0.9f);
                        sliderLoad.value += progress;

                        yield return null;
                    }
                }
            }
            else
            {
                PantallaDeCarga.SetActive(false);
            }
        }
    }
    */
}
