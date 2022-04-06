using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ControladorCarga : MonoBehaviour
{
    public GameObject PantallaDeCarga;
    public Slider sliderLoad;
    public GameObject errorTextObj;
    public TMP_InputField userInput;

    public Texture2D[] img;

    public MyRutas.Rutass rutaList = new MyRutas.Rutass();

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
            rutaList = JsonUtility.FromJson<MyRutas.Rutass>(web.downloadHandler.text);
            DatosEntreEscenas.instace.rutaList = rutaList;
            DatosEntreEscenas.instace.numPregunta = 0;
            DatosEntreEscenas.instace.contPrguntas = 0;

            StartCoroutine(CorrutinaCargarImagenes());
        }
        else
        {
            errorTextObj.SetActive(true);
            errorTextObj.GetComponentInChildren<TextMeshProUGUI>().text = "Hubo un error al leer, recargue la pagina por favor";
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
                /*
                Sprite sprite = Sprite.Create(img, new Rect(0, 0, 100, 100), Vector2.zero);
                imagen.sprite = sprite;
                */
            }
            else
            {
                Debug.LogWarning("Hubo un error al leer");
            }
        }

        DatosEntreEscenas.instace.img = img;
    }


    public void cargarEscena()
    {
        StartCoroutine(CargaEscenaSimple());
    }
    
    private IEnumerator CargaEscenaSimple()
    {
        string txtUser = userInput.text;
        
        if(txtUser == "")
        {
            errorTextObj.SetActive(true);
            errorTextObj.GetComponent<Image>().enabled = false;
            errorTextObj.GetComponentInChildren<TextMeshProUGUI>().text = "Por favor ingrese el nombre";
        }
        else
        {
            PantallaDeCarga.SetActive(true);
            sliderLoad.value = 0.0f;

            while (rutaList.data.Count == 0 )
            {
                sliderLoad.value += sliderLoad.value > 0.9f ? 0f : 0.001f;
                Debug.Log("Esperando ...");
                yield return null;
            }

            while (img[rutaList.data.Count-1] == null)
            {
                sliderLoad.value += sliderLoad.value > 0.9f ? 0f : 0.001f;
                Debug.Log("Esperando img...");
                yield return null;
            }

            if (rutaList.data.Count > 0)
            {
                AsyncOperation loading = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

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
    }

}
