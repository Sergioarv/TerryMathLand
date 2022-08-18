using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
            errorTextObj.GetComponentInChildren<TextMeshProUGUI>().text = "Por favor ingrese el documento";
            PantallaDeCarga.SetActive(false);
        }
        else if (idCartilla == -1 || idCartilla == 0)
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
                StartCoroutine(web.CorrutinaCargar(idCartilla));
            }
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
            idCartilla = listaCartillas.data[index - 1].idcartilla;
        }
        
    }
}
