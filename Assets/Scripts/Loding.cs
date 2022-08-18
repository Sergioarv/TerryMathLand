using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Loding : MonoBehaviour
{

    public Slider sliderLoad;
    public TextMeshProUGUI txtSliderLoad;
    public TextMeshProUGUI consejo;
    public GameObject mensajeContinuar;
    private int numPregunta;
    private int preguntasPorNivel;

    private int levelToLoad;
    private bool next = false;

    void Start()
    {
        levelToLoad = LevelLoading.nextLevel;

        numPregunta = DatosEntreEscenas.instace.numPregunta;
        preguntasPorNivel = DatosEntreEscenas.instace.preguntasPorNivel;

        StartCoroutine(LoadLevel());
    }

    private void Update()
    {
        if ((numPregunta == 0 || numPregunta == (preguntasPorNivel * 1) || numPregunta == (preguntasPorNivel * 2) 
            || numPregunta == (preguntasPorNivel * 3) || numPregunta == (preguntasPorNivel * 4)))
        {
            if (Input.GetKeyDown(KeyCode.Space) && next)
            {
                SceneManager.LoadScene(levelToLoad);
            }
        }
    }

    private IEnumerator LoadLevel()
    {
        sliderLoad.value = 0;

        if (numPregunta == 0)
        {
            consejo.text = "Lee la pregunta y selecciona con el mouse, el globo con la respuesta que considere correcta";
        }

        if (numPregunta == (preguntasPorNivel * 1))
        {
            consejo.text = "Lee la pregunta y selecciona con el mouse, la diana con la respuesta que considere correcta";
        }

        if (numPregunta == (preguntasPorNivel * 2))
        {
            consejo.text = "Lee la pregunta, usando las teclas A y D para moverte y Espacio para saltar, busca la puerta con la respuesta que considere correcta y parandote sobre ella presiona la tecla 'F'";
        }

        if (numPregunta == (preguntasPorNivel * 3))
        {
            consejo.text = "Lee la pregunta, usando las teclas A, D, W y S, para moverte, toma con la tecla 'F' la caja con la respuesta que considere correcta, llévala al altar y suéltala con la tecla de la letra 'F'";
        }

        if (numPregunta == (preguntasPorNivel * 4))
        {
            consejo.text = "Lee la pregunta, usando las teclas A, D, W y S, para moverte y Espacio para saltar, toma la bomba con la tecla de la letra 'F' sube lo más alto que puedas y mirando al enemigo lanza la bomba 'con Click izquierdo del mouse', recuerda esquivar los ataques";
        }

        while (sliderLoad.value < 1f && (numPregunta == 0 || numPregunta == (preguntasPorNivel * 1) || numPregunta == (preguntasPorNivel * 2)
            || numPregunta == (preguntasPorNivel * 3) || numPregunta == (preguntasPorNivel * 4)))
        {
            sliderLoad.value += 0.006f;
            txtSliderLoad.text = (int)(sliderLoad.value * 100) + "%";

            yield return null;
        }

        next = true;
        mensajeContinuar.SetActive(true);
    }
}
