using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class web : MonoBehaviour
{
    // url de la api
    private string urlBase = "https://bk-terrymathland.herokuapp.com";
    //private string urlBase = "localhost:8080";
    // url para consultar la lista de preguntas
    private string urlPreguntas = "/cartilla/obtenerPreguntas?idcartilla=";
    // url para verificar el estudiante por su nombre
    private string urlUsuario = "/estudiante/estudiantenombre?nombre=";
    // url para guardar la respuesta y soluciones del estudiante
    private string urlRespuesta = "/respuesta/guardarRespuestaEstudiante";
    // url para listar las cartillas
    private string urlListarCartillas = "/cartilla/listarCartillas";
    // game object 'ContoladorCarga' en la escena Main Menu
    private ControladorCarga ctrCarga;
    // url para preguntas sin imagen
    private string urlSinImagen = "https://res.cloudinary.com/dj8sqmb8n/image/upload/v1655933832/Sin_Imagen_ngqu9q.png";

    // Método Awake, método propio de la clase que se ejecuta al cargar la escena que lo contiene
    private void Awake()
    {
        /* Se carga el game object de tipo 'ControladorCarga' que se 
         * encuentra dentro en la misma escena, este se encarga del 
         * canvas de la escena y sus hijos */
        ctrCarga = GameObject.FindObjectOfType<ControladorCarga>();
    }

    public IEnumerator CorrutinaListarCartillas()
    {
        // inicia la animacion de carga
        ctrCarga.PantallaDeCarga.SetActive(true);
        // se establece a cero la barra de carga en 'ControladorCarga'
        ctrCarga.sliderLoad.value = 0f;
        // se accede al Text porcentaje en 'ControladorCarga' para mostrar el porcentaje de carga
        ctrCarga.txtSliderLoad.text = ctrCarga.sliderLoad.value.ToString() + "%";

        // Servicio de Unity encargado de realizar la cominucación con el back-end
        UnityWebRequest web = UnityWebRequest.Get(urlBase + urlListarCartillas);
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
            ctrCarga.listaCartillas = JsonUtility.FromJson<ListaCartilla>(web.downloadHandler.text);

            if (!ctrCarga.listaCartillas.success)
            {
                ctrCarga.PantallaDeCarga.SetActive(false);
                ctrCarga.errorTextObj.SetActive(true);
                ctrCarga.errorTextObj.GetComponent<Image>().enabled = false;
                ctrCarga.errorTextObj.GetComponentInChildren<TextMeshProUGUI>().text = ctrCarga.listaCartillas.message;
            }

            List<string> cartillas = new List<string>();

            for (int i = 0; i < ctrCarga.listaCartillas.data.Count; i++)
            {
                cartillas.Add(ctrCarga.listaCartillas.data[i].nombre);
            }

            ctrCarga.comboBoxCartillas.AddOptions(cartillas);

            // detiene la animacion de carga
            ctrCarga.PantallaDeCarga.SetActive(false);
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

    // Método encargado de cargar las preguntas de la base de dato en el prefab 'DatosEntreEscenas'
    public IEnumerator CorrutinaCargar(int idCartilla)
    {
        // inicia la animacion de carga
        ctrCarga.PantallaDeCarga.SetActive(true);
        // se establece a cero la barra de carga en 'ControladorCarga'
        ctrCarga.sliderLoad.value = ctrCarga.sliderLoad.value;
        // se accede al Text porcentaje en 'ControladorCarga' para mostrar el porcentaje de carga
        ctrCarga.txtSliderLoad.text = ctrCarga.sliderLoad.value.ToString() + "%";

        // Servicio de Unity encargado de realizar la cominucación con el back-end
        UnityWebRequest web = UnityWebRequest.Get(urlBase + urlPreguntas+idCartilla);
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
            DatosEntreEscenas.instace.preguntasPorNivel = Mathf.RoundToInt(ctrCarga.listaPreguntas.data.Count / 5f);
            DatosEntreEscenas.instace.restante = calcularRestante();
            // detiene la animacion de carga
            ctrCarga.PantallaDeCarga.SetActive(false);
            // llamado del método encargado de cargar las imagenes
            StartCoroutine(CorrutinaCargarImagenes());
        }
        else // Si la peticion recibe un error
        {
            Debug.Log(urlBase + urlPreguntas);
            // activa el mensaje de error
            ctrCarga.errorTextObj.SetActive(true);
            ctrCarga.errorTextObj.GetComponent<Image>().enabled = true;
            ctrCarga.errorTextObj.GetComponentInChildren<TextMeshProUGUI>().text = "Hubo un error al cargar las preguntas, por favor recargue la pagina";
            // detiene la animacion de carga
            ctrCarga.PantallaDeCarga.SetActive(false);
        }
    }

    private int calcularRestante()
    {
        int entero = ctrCarga.listaPreguntas.data.Count/5;
        float decimas = ctrCarga.listaPreguntas.data.Count/5f;

        if (decimas - entero < 0.5f)
        {
          return (ctrCarga.listaPreguntas.data.Count - (entero * 5));
        }

        return 0;
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
            string urlImg = "";
            if (ctrCarga.listaPreguntas.data[i].urlImg == null || ctrCarga.listaPreguntas.data[i].urlImg == "") {
                urlImg = urlSinImagen;
            }
            else
            {
                urlImg = ctrCarga.listaPreguntas.data[i].urlImg;
            }
            
            // Servicio de Unity encargado de realizar la cominucación con el back-end
            UnityWebRequest reg = UnityWebRequestTexture.GetTexture(urlImg);
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
        LevelLoading.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
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
            Debug.Log(web.downloadHandler.text);
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
        Debug.Log(newRespuesta);
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
            DatosEntreEscenas.instace.puntajes = JsonUtility.FromJson<ListRespuesta>(web.downloadHandler.text);
            Debug.Log(DatosEntreEscenas.instace.puntajes);
            new WaitForSeconds(1);
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