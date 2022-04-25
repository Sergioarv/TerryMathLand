﻿using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

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

    public Usuario usuario = new Usuario();

    public Image imagen;

    public ListPregunta listaPreguntas = new ListPregunta();

    public Respuesta respuestaEst = new Respuesta();
    public Solucion solucionEst;

    public Texture2D[] img;

    private void Awake()
    {
        leerSimple();
    }

    public void leerSimple()
    {
        listaPreguntas = DatosEntreEscenas.instace.listaPreguntas;
        numPregunta = DatosEntreEscenas.instace.numPregunta;
        contPregunta = DatosEntreEscenas.instace.contPreguntas;
        img = DatosEntreEscenas.instace.img;
        vida = DatosEntreEscenas.instace.vida;
        preguntasCorrectas = DatosEntreEscenas.instace.preguntasCorrectas;
        usuario = DatosEntreEscenas.instace.usuario;
        respuestaEst = DatosEntreEscenas.instace.respuestaEst;
    }

    public void guardarSimple()
    {
        DatosEntreEscenas.instace.listaPreguntas = listaPreguntas;
        DatosEntreEscenas.instace.numPregunta = numPregunta + 1;
        DatosEntreEscenas.instace.contPreguntas = contPregunta + 1;
        DatosEntreEscenas.instace.img = img;
        DatosEntreEscenas.instace.vida = vida;
        DatosEntreEscenas.instace.preguntasCorrectas = preguntasCorrectas;
        DatosEntreEscenas.instace.usuario = usuario;
        DatosEntreEscenas.instace.respuestaEst = respuestaEst;
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
        if (numPregunta < listaPreguntas.data.Count)
        {

            enunciado.text = listaPreguntas.data[numPregunta].enunciado;

            optionA.text = listaPreguntas.data[numPregunta].opciones[0].enunciadoopcion;
            optionB.text = listaPreguntas.data[numPregunta].opciones[1].enunciadoopcion;
            optionC.text = listaPreguntas.data[numPregunta].opciones[2].enunciadoopcion;
            optionD.text = listaPreguntas.data[numPregunta].opciones[3].enunciadoopcion;

            imagen.sprite = Sprite.Create(img[numPregunta], new Rect(0, 0, img[numPregunta].width, img[numPregunta].height), Vector2.zero);

            txtVida.text = vida.ToString();
            txtPreguntasCorrectas.text = preguntasCorrectas.ToString();
        }
        else
        {
            respuestaEst.acertadas = preguntasCorrectas;
            respuestaEst.nota = (5.0f * preguntasCorrectas) / numPregunta;
        }
    }

    public void responder(string seleccion, string seleccioOpcion)
    {

        solucionEst = new Solucion();

        solucionEst.enunciadoPre = listaPreguntas.data[numPregunta].enunciado;
        solucionEst.respuestaEst = seleccioOpcion;

        for (int i = 0; i < listaPreguntas.data[numPregunta].opciones.Count; i++)
        {
            if (listaPreguntas.data[numPregunta].opciones[i].respuesta)
            {
                solucionEst.respuestaPre = listaPreguntas.data[numPregunta].opciones[i].enunciadoopcion;
                break;
            }
        }

        respuestaEst.soluciones.Add(solucionEst);

        switch (seleccion)
        {
            case "OpcionA":

                if (listaPreguntas.data[numPregunta].opciones[0].respuesta)
                {
                    acerto();
                }
                else
                {
                    fallo();
                }
                break;
            case "OpcionB":

                if (listaPreguntas.data[numPregunta].opciones[1].respuesta)
                {
                    acerto();
                }
                else
                {
                    fallo();
                }
                break;
            case "OpcionC":

                if (listaPreguntas.data[numPregunta].opciones[2].respuesta)
                {
                    acerto();
                }
                else
                {
                    fallo();
                }
                break;
            case "OpcionD":

                if (listaPreguntas.data[numPregunta].opciones[3].respuesta)
                {
                    acerto();
                }
                else
                {
                    fallo();
                }
                break;
        }
    }

    public void cargarEscena()
    {
        guardarSimple();

        if (contPregunta == 4)
        {
            DatosEntreEscenas.instace.contPreguntas = 0;
            LevelLoading.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            LevelLoading.LoadLevel(SceneManager.GetActiveScene().buildIndex);
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