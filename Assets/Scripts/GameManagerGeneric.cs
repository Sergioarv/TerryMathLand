using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class GameManagerGeneric : MonoBehaviour
{
    public TextMeshProUGUI enunciado;
    public TextMeshProUGUI optionA;
    public TextMeshProUGUI optionB;
    public TextMeshProUGUI optionC;
    public TextMeshProUGUI optionD;

    public TextMeshProUGUI txtVida;
    public TextMeshProUGUI txtPreguntasCorrectas;

    public int numPregunta;
    public int contPregunta;
    public int vida;
    public int preguntasCorrectas;

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
        vida = DatosEntreEscenas.instace.vida;
        preguntasCorrectas = DatosEntreEscenas.instace.preguntasCorrectas;
    }

    public void guardarSimple()
    {
        DatosEntreEscenas.instace.rutaList = rutaList;
        DatosEntreEscenas.instace.numPregunta = numPregunta + 1;
        DatosEntreEscenas.instace.contPrguntas = contPregunta + 1;
        DatosEntreEscenas.instace.img = img;
        DatosEntreEscenas.instace.vida = vida;
        DatosEntreEscenas.instace.preguntasCorrectas = preguntasCorrectas;
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

    [ContextMenu("Next")]
    private void next()
    {
        cargarEscena();
    }

    private void MostrarPregunta()
    {
        enunciado.text = rutaList.data[numPregunta].idruta.ToString();

        optionA.text = rutaList.data[numPregunta].origen.nombreciudad;
        optionB.text = rutaList.data[numPregunta].origen.idciudad.ToString();
        optionC.text = rutaList.data[numPregunta].origen.visado.ToString();
        optionD.text = rutaList.data[numPregunta].origen.nombreciudad;

        imagen.sprite = Sprite.Create(img[numPregunta], new Rect(0, 0, img[numPregunta].width, img[numPregunta].height), Vector2.zero);

        txtVida.text = vida.ToString();
        txtPreguntasCorrectas.text = preguntasCorrectas.ToString();
    }

    public void responder(string opcion)
    {
        switch (opcion)
        {
            case "OpcionA":
                if (rutaList.data[numPregunta].origen.nombreciudad == "")
                {
                    acerto();
                }
                else
                {
                    fallo();
                }
                break;
            case "OpcionB": break;
            case "OpcionC":
                if (!rutaList.data[numPregunta].origen.visado)
                {
                    acerto();
                }
                else
                {
                    fallo();
                }
                break; ;
            case "OpcionD": break;
        }
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

    public void acerto()
    {
        preguntasCorrectas++;
        cargarEscena();
    }

    public void fallo()
    {
        vida--;
        cargarEscena();
    }
}
