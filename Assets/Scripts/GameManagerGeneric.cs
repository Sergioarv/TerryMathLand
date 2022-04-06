using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class GameManagerGeneric : MonoBehaviour
{

    public GameObject preguntaPrefab;
    public TextMeshProUGUI enunciado;
    public TextMeshProUGUI optionA;
    public TextMeshProUGUI optionB;
    public TextMeshProUGUI optionC;
    public TextMeshProUGUI optionD;
    public int numPregunta;
    public int contPregunta;

    public Image imagen;

    public MyRutas.Rutass rutaList = new MyRutas.Rutass();

    public Texture2D[] img;

    private void Awake()
    {
        leerSimple();
    }

    public void leerSimple()
    {
        rutaList = DatosEntreEscenas.instace.rutaList;
        numPregunta = DatosEntreEscenas.instace.numPregunta;
        contPregunta = DatosEntreEscenas.instace.contPrguntas;
        img = DatosEntreEscenas.instace.img;
    }

    public void guardarSimple()
    {
        DatosEntreEscenas.instace.rutaList = rutaList;
        DatosEntreEscenas.instace.numPregunta = numPregunta + 1;
        DatosEntreEscenas.instace.contPrguntas = contPregunta + 1;
        DatosEntreEscenas.instace.img = img;
    }

    // Start is called before the first frame update
    void Start()
    {
        MostrarPregunta();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("MostrarPregunta")]
    private void MostrarPregunta()
    {

        GameObject pregunta = Instantiate(preguntaPrefab);
        enunciado = pregunta.GetComponentInChildren<TextMeshProUGUI>();
        enunciado.text = rutaList.data[numPregunta].idruta.ToString();

        GameObject oA = GameObject.FindGameObjectsWithTag("Opcion")[0];
        optionA = oA.GetComponentInChildren<TextMeshProUGUI>();
        optionA.text = rutaList.data[numPregunta].origen.nombreciudad;

        GameObject oB = GameObject.FindGameObjectsWithTag("Opcion")[1];
        optionB = oB.GetComponentInChildren<TextMeshProUGUI>();
        optionB.text = rutaList.data[numPregunta].origen.idciudad.ToString();

        GameObject oC = GameObject.FindGameObjectsWithTag("Opcion")[2];
        optionC = oC.GetComponentInChildren<TextMeshProUGUI>();
        optionC.text = rutaList.data[numPregunta].origen.visado.ToString();

        GameObject oD = GameObject.FindGameObjectsWithTag("Opcion")[3];
        optionD = oD.GetComponentInChildren<TextMeshProUGUI>();
        optionD.text = rutaList.data[numPregunta].origen.nombreciudad;

        GameObject oI = GameObject.FindGameObjectsWithTag("Imagen")[0];
        imagen = oI.GetComponentInChildren<Image>();

        imagen.sprite = Sprite.Create(img[numPregunta], new Rect(0, 0, img[numPregunta].width, img[numPregunta].height), Vector2.zero);

        //StartCoroutine(CorrutinaLeerImagen(rutaList.data[numPregunta].destino.nombreciudad));

        //optionA.transform.position = new Vector3(oA.transform.position.x + 750, optionA.transform.position.y + 10);
        /*
        GameObject oA = GameObject.FindGameObjectsWithTag("Opcion")[1];
        optionA = oA.GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(oA.transform.position + " " + optionA.transform.position);
        optionA.text = "Respuesta";
        optionA.transform.position = new Vector3(optionA.transform.position.x + oA.transform.position.x, optionA.transform.position.y + ( oA.transform.position.y * 2));
        */
    }

    public void cargarEscena()
    {
        guardarSimple();

        if (contPregunta == 5)
        {
            DatosEntreEscenas.instace.contPrguntas = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    private IEnumerator CorrutinaLeerImagen(string url)
    {

        UnityWebRequest reg = UnityWebRequestTexture.GetTexture(url);
        yield return reg.SendWebRequest();

        if (!reg.isNetworkError && !reg.isHttpError)
        {

            Texture2D img = DownloadHandlerTexture.GetContent(reg);
            //Sprite sprite = Sprite.Create(img, new Rect(0.0f, 0.0f, 100, 100), Vector2.zero);
            imagen.sprite = Sprite.Create(img, new Rect(0, 0, img.width, img.height), Vector2.zero);
            //imagen.sprite = sprite;
            //imagen.SetNativeSize();

            Debug.Log("Trabaja");
        }
        else
        {
            Debug.LogWarning("Hubo un error al leer");
        }
    }

}
