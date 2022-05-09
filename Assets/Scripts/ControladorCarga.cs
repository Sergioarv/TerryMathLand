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

    private web web;
    public Texture2D[] img;

    public ListPregunta listaPreguntas = new ListPregunta();

    public Usuario usuario = new Usuario();

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
        StartCoroutine(web.CorrutinaCargar());
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
        }
        else
        {
            PantallaDeCarga.SetActive(true);
            sliderLoad.gameObject.SetActive(true);
            sliderLoad.value = 0.0f;

            if (listaPreguntas.data.Count > 0)
            {
                StartCoroutine(web.CorrutinaVerificarUsuario(txtUser));

                yield return new WaitForSeconds(2);

                if (usuario == null || usuario.nombre == "")
                {
                    PantallaDeCarga.SetActive(false);
                    errorTextObj.SetActive(true);
                    errorTextObj.GetComponent<Image>().enabled = false;
                    errorTextObj.GetComponentInChildren<TextMeshProUGUI>().text = "Por favor verifique el nombre";
                }
                else
                {
                    while (sliderLoad.value < 1f)
                    {
                        sliderLoad.value += 0.003f;
                        txtSliderLoad.text = (int)(sliderLoad.value * 100) + "%";

                        yield return null;
                    }

                    LevelLoading.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
            else
            {
                PantallaDeCarga.SetActive(false);
            }
        }
    }
}
