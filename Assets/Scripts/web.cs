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
    // url de la api
    private string urlBase = "https://bk-terrymathmand.herokuapp.com";
    // url para consultar la lista de preguntas
    private string urlPreguntas = "/pregunta";
    // url para verificar el estudiante por su nombre
    private string urlUsuario = "/estudiante/estudiantenombre?nombre=";
    // url para guardar la respuesta y soluciones del estudiante
    private string urlRespuesta = "/estudiante";
    // game object 'ContoladorCarga' en la escena Main Menu
    private ControladorCarga ctrCarga;

    // Método Awake, método propio de la clase que se ejecuta al cargar la escena que lo contiene
    private void Awake()
    {
        /* Se carga el game object de tipo 'ControladorCarga' que se 
         * encuentra dentro en la misma escena, este se encarga del 
         * canvas de la escena y sus hijos */
        ctrCarga = GameObject.FindObjectOfType<ControladorCarga>();
    }

    // Método encargado de cargar las preguntas de la base de dato en el prefab 'DatosEntreEscenas'
    public IEnumerator CorrutinaCargar()
    {
        // inicia la animacion de carga
        ctrCarga.PantallaDeCarga.SetActive(true);
        // se establece a cero la barra de carga en 'ControladorCarga'
        ctrCarga.sliderLoad.value = 0f;
        // se accede al Text porcentaje en 'ControladorCarga' para mostrar el porcentaje de carga
        ctrCarga.txtSliderLoad.text = ctrCarga.sliderLoad.value.ToString() + "%";

        // Servicio de Unity encargado de realizar la cominucación con el back-end
        UnityWebRequest web = UnityWebRequest.Get(urlBase + urlPreguntas);
        // Se encarga de enviar la solicitud
        web.SendWebRequest();

        // espera la respuesta de la petición
        while (!web.isDone)
        {
            // realiza una pequeña animación de carga
            ctrCarga.sliderLoad.value += ctrCarga.sliderLoad.value < 0.85 ? 0.005f : 0f;
            ctrCarga.txtSliderLoad.text = (int)(ctrCarga.sliderLoad.value * 100) + "%";
            yield return null;
        }

        // Verifica si la respuesta ha recibido un error de conexion o de http
        if (!web.isNetworkError && !web.isHttpError)
        {
            // Se encarga de convertir el json recibido por la peticion a Lista de preguntas
            ctrCarga.listaPreguntas = JsonUtility.FromJson<ListPregunta>(web.downloadHandler.text);
            // guarda en el prefab 'DatosEntreEscenas' los parametros necesarios entre escenas
            DatosEntreEscenas.instace.listaPreguntas = ctrCarga.listaPreguntas;
            DatosEntreEscenas.instace.numPregunta = 0;
            DatosEntreEscenas.instace.contPreguntas = 0;
            DatosEntreEscenas.instace.vida = ctrCarga.listaPreguntas.data.Count;
            DatosEntreEscenas.instace.preguntasCorrectas = 0;
            // detiene la animacion de carga
            ctrCarga.PantallaDeCarga.SetActive(false);
            // llamado del método encargado de cargar las imagenes
            StartCoroutine(CorrutinaCargarImagenes());
        }
        else // Si la peticion recibe un error
        {
            // activa el mensaje de error
            ctrCarga.errorTextObj.SetActive(true);
            ctrCarga.errorTextObj.GetComponent<Image>().enabled = true;
            ctrCarga.errorTextObj.GetComponentInChildren<TextMeshProUGUI>().text = "Hubo un error al cargar las preguntas, por favor recargue la pagina";
            // detiene la animacion de carga
            ctrCarga.PantallaDeCarga.SetActive(false);
        }
    }

    // Método encargado de cargar las imagenes de la base de dato en el prefab 'DatosEntreEscenas'
    public IEnumerator CorrutinaCargarImagenes()
    {
        // inicia la animacion de carga
        ctrCarga.PantallaDeCarga.SetActive(true);
        // inicializa un arreglo para guardar las imgenes
        ctrCarga.img = new Texture2D[ctrCarga.listaPreguntas.data.Count];
        // ciclo encargado de obtener las url de las imagenes en cada pregunta
        for (int i = 0; i < ctrCarga.listaPreguntas.data.Count;)
        {
            // Servicio de Unity encargado de realizar la cominucación con el back-end
            UnityWebRequest reg = UnityWebRequestTexture.GetTexture(ctrCarga.listaPreguntas.data[i].urlImg);
            // Se encarga de enviar la solicitud
            reg.SendWebRequest();

            // espera la respuesta de la petición
            while (!reg.isDone)
            {
                // realiza una pequeña animación de carga
                ctrCarga.sliderLoad.value += ctrCarga.sliderLoad.value < 0.92 ? 0.005f : 0f;
                ctrCarga.txtSliderLoad.text = (int)(ctrCarga.sliderLoad.value * 100) + "%";
                yield return null;
            }
            // Verifica si la respuesta ha recibido un error de conexion o de http
            if (!reg.isNetworkError && !reg.isHttpError)
            {
                // descarga y agrega la imagen recibida por la petición
                ctrCarga.img[i] = DownloadHandlerTexture.GetContent(reg);
                i++;
            }
            else // Si la peticion recibe un error
            {
                // activa el mensaje de error
                ctrCarga.errorTextObj.SetActive(true);
                ctrCarga.errorTextObj.GetComponent<Image>().enabled = true;
                ctrCarga.errorTextObj.GetComponentInChildren<TextMeshProUGUI>().text = "Hubo un error al cargar las imagenes, por favor recargue la pagina";
                // detiene la animacion de carga
                ctrCarga.PantallaDeCarga.SetActive(false);
            }
        }
        // guarda en el prefab 'DatosEntreEscenas' la lista de imagenes necesarios entre escenas
        DatosEntreEscenas.instace.img = ctrCarga.img;
        // detiene la animacion de carga
        ctrCarga.PantallaDeCarga.SetActive(false);
    }

    // Método encargado de verificar el estudiante de la base de dato
    public IEnumerator CorrutinaVerificarUsuario(string nombre)
    {
        // Servicio de Unity encargado de realizar la cominucación con el back-end
        UnityWebRequest web = UnityWebRequest.Get(urlBase + urlUsuario + nombre);
        // encargado de enviar la solicitud
        web.SendWebRequest();

        // espera la respuesta de la petición
        while (!web.isDone)
        {
            // realiza una pequeña animación de carga
            ctrCarga.sliderLoad.value += ctrCarga.sliderLoad.value < 0.85 ? 0.003f : 0f;
            ctrCarga.txtSliderLoad.text = (int)(ctrCarga.sliderLoad.value * 100) + "%";
            yield return null;
        }
        // Verifica si la respuesta ha recibido un error de conexion o de http
        if (!web.isNetworkError && !web.isHttpError)
        {
            // Se encarga de convertir el json recibido por la peticion a un Estudiante
            ctrCarga.usuario = JsonUtility.FromJson<Estudiante>(web.downloadHandler.text);
            // guarda el usuario con su nuevas respuestas
            DatosEntreEscenas.instace.usuario = ctrCarga.usuario;
        }
        else // Si la peticion recibe un error
        {
            // activa el mensaje de error
            ctrCarga.errorTextObj.SetActive(true);
            ctrCarga.errorTextObj.GetComponent<Image>().enabled = false;
            ctrCarga.errorTextObj.GetComponentInChildren<TextMeshProUGUI>().text = "Por favor recargue la pagina, no hemos encontrado la base de datos";
        }
    }

    // Método encargado de guardar la respuesta con sus soluciones en la base de dato
    public IEnumerator CorrutinaGuardarRespuesta(Respuesta respuesta, Estudiante usuario)
    {
        // Crea un usuario con los datos a guardar
        Usuario newUsuario = new Usuario();
        // se acciona el id del usuario que esta jugando
        newUsuario.idusuario = usuario.data.idusuario;
        newUsuario.nombre = usuario.data.nombre;
        // se agrega la respuesta con sus soluciones
        newUsuario.respuestas.Add(respuesta);
        // Se convierte el usuario a json
        string newRespuesta = JsonUtility.ToJson(newUsuario).ToString();
        // Servicio de Unity encargado de realizar la cominucación con el back-end
        UnityWebRequest web = UnityWebRequest.Put(urlBase + urlRespuesta, newRespuesta);
        web.SetRequestHeader("Content-Type", "application/json;charset=UTF-8;application/x-www-form-urlencoded");
        web.SetRequestHeader("Accept", "application/json");
        // Encargado de enviar la peticion
        yield return web.SendWebRequest();
        // Verifica si la respuesta ha recibido un error de conexion o de http
        if (!web.isNetworkError && !web.isHttpError)
        {
            // Se encarga de convertir el json recibido por la peticion a un Estudiante
            DatosEntreEscenas.instace.usuario = JsonUtility.FromJson<Estudiante>(web.downloadHandler.text);
            // Se encarga de obtener el game object GameOver en el cual se crea y llena los ultimos puntajes del jugador
            GameObject.FindObjectOfType<GameOver>().leerSimple();
            GameObject.FindObjectOfType<GameOver>().crearTabla();
        }
        else
        {
            Debug.LogWarning("Hubo un error al escribir ruta");
        }
    }
}

