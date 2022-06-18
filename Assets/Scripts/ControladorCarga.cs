using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ControladorCarga : MonoBehaviour
{
    public GameObject PantallaDeCarga;
    public Slider sliderLoad;
    public GameObject errorTextObj;
    public TextMeshProUGUI txtSliderLoad;
    public TMP_InputField userInput;
    public TMP_Dropdown comboBoxCartillas;

    private web web;
    public Texture2D[] img;

    public ListPregunta listaPreguntas = new ListPregunta();

    public Estudiante usuario = new Estudiante();

    public ListaCartilla listaCartillas = new ListaCartilla();

    private int idCartilla;

    private void Awake()
    {
        web = GameObject.FindObjectOfType<web>();
    }

    private void Start()
    {
        leerSimple();
    }

    public void leerSimple()
    {
        //StartCoroutine(web.CorrutinaCargar());
        StartCoroutine(web.CorrutinaListarCartillas());
    }

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
        } else if (idCartilla == -1)
        {
            errorTextObj.SetActive(true);
            errorTextObj.GetComponent<Image>().enabled = false;
            errorTextObj.GetComponentInChildren<TextMeshProUGUI>().text = "Por favor seleccione una cartilla";
            PantallaDeCarga.SetActive(false);
        }
        else
        {
            PantallaDeCarga.SetActive(true);
            sliderLoad.gameObject.SetActive(true);
            sliderLoad.value = 0.0f;

            //if (listaPreguntas.data.Count > 0)
            //{
                StartCoroutine(web.CorrutinaVerificarUsuario(txtUser));
                yield return new WaitForSeconds(3);

                if (usuario.data == null || usuario.data.nombre == "" || !usuario.success)
                {
                    PantallaDeCarga.SetActive(false);
                    errorTextObj.SetActive(true);
                    errorTextObj.GetComponent<Image>().enabled = false;
                    errorTextObj.GetComponentInChildren<TextMeshProUGUI>().text = usuario.message;
                }
                else
                {
                    //while (sliderLoad.value < 1f)
                    //{
                    //    sliderLoad.value += 0.003f;
                    //    txtSliderLoad.text = (int)(sliderLoad.value * 100) + "%";

                    //    yield return null;
                    //}

                    StartCoroutine(web.CorrutinaCargar(idCartilla));
                    //LevelLoading.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
                }
            //}
            //else
            //{
            //    PantallaDeCarga.SetActive(false);
            //}
        }
    }

    public void comboBoxCambioDeItem(int index)
    {
        if (index == 0)
        {
            idCartilla = -1;
        }
        else
        {
            idCartilla = listaCartillas.data[index-1].idcartilla;
        }

    }
}
