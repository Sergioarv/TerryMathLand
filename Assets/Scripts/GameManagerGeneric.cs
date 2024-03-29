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

    public AudioSource aplausos;
    public AudioSource trompetas;

    private string[] opcionesName = { "OpcionA", "OpcionB", "OpcionC", "OpcionD" };

    public int numPregunta;
    public int contPregunta;
    public int restante;
    public int vida;
    public int preguntasCorrectas;
    public int preguntasPorNivel;
    public Estudiante usuario = new Estudiante();
    public Image imagen;
    public ListPregunta listaPreguntas = new ListPregunta();
    public Respuesta respuestaEst = new Respuesta();
    public Solucion solucionEst = new Solucion();
    public Texture2D[] img;

    private void Awake()
    {
        numPregunta = 0;
        contPregunta = 0;
        restante = 0;
        vida = 0;
        preguntasCorrectas = 0;
        preguntasPorNivel = 0;
        usuario = new Estudiante();
        listaPreguntas = new ListPregunta();
        respuestaEst = new Respuesta();
        solucionEst = new Solucion();

        leerEntreEscenas();
    }

    void Start()
    {
        MostrarPregunta();

    }

    public void leerEntreEscenas()
    {
        listaPreguntas = DatosEntreEscenas.instace.listaPreguntas;
        numPregunta = DatosEntreEscenas.instace.numPregunta;
        contPregunta = DatosEntreEscenas.instace.contPreguntas;
        img = DatosEntreEscenas.instace.img;
        vida = DatosEntreEscenas.instace.vida;
        preguntasCorrectas = DatosEntreEscenas.instace.preguntasCorrectas;
        usuario = DatosEntreEscenas.instace.usuario;
        respuestaEst = DatosEntreEscenas.instace.respuestaEst;
        preguntasPorNivel = DatosEntreEscenas.instace.preguntasPorNivel;
        restante = DatosEntreEscenas.instace.restante;
    }

    public void guardarEntreEscenas()
    {
        DatosEntreEscenas.instace.listaPreguntas = listaPreguntas;
        DatosEntreEscenas.instace.numPregunta = numPregunta;
        DatosEntreEscenas.instace.contPreguntas = contPregunta;
        DatosEntreEscenas.instace.img = img;
        DatosEntreEscenas.instace.vida = vida;
        DatosEntreEscenas.instace.preguntasCorrectas = preguntasCorrectas;
        DatosEntreEscenas.instace.usuario = usuario;
        DatosEntreEscenas.instace.respuestaEst = respuestaEst;
        DatosEntreEscenas.instace.preguntasPorNivel = preguntasPorNivel;
        DatosEntreEscenas.instace.restante = restante;
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
    }

    public void responder(string seleccion, string seleccioOpcion)
    {
        try
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

            for (int i = 0; i < opcionesName.Length; i++)
            {
                if (seleccion.Equals(opcionesName[i]))
                {
                    if (listaPreguntas.data[numPregunta].opciones[i].respuesta)
                    {
                        acerto();
                        break;
                    }
                    else
                    {
                        fallo();
                        break;
                    }
                }
            }
            numPregunta++;
            contPregunta++;
        }
        catch (ArgumentOutOfRangeException)
        {

        }
    }

    public void cargarEscena()
    {
        guardarEntreEscenas();

        if (numPregunta == listaPreguntas.data.Count)
        {
            float nota = (5.0f * preguntasCorrectas) / numPregunta;

            respuestaEst.acertadas = preguntasCorrectas;
            respuestaEst.nota = (Mathf.Round((float)(nota * 10.0f)) * 0.1f);
            DatosEntreEscenas.instace.respuestaEst = respuestaEst;

            SceneManager.LoadScene("PantallaFinal");
        }
        else if (numPregunta >= listaPreguntas.data.Count - restante)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            if (contPregunta == preguntasPorNivel)
            {
                DatosEntreEscenas.instace.contPreguntas = 0;
                LevelLoading.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }


    public void acerto()
    {
        preguntasCorrectas++;
        Instantiate(aplausos);
        Invoke("cargarEscena", 8);
    }

    public void fallo()
    {
        vida--;
        Instantiate(trompetas);
        Invoke("cargarEscena", 5);
    }
}
