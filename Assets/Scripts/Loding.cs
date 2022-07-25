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
            consejo.text = "Lee la pregunta y selecciona con el mouse el globo con la respuesta correcta";
        }

        if (numPregunta == (preguntasPorNivel * 1))
        {
            consejo.text = "Lee la pregunta y selecciona la diana con la respuesta correcta";
        }

        if (numPregunta == (preguntasPorNivel * 2))
        {
            consejo.text = "Lee la pregunta, busca la puerta con la respuesta correcta y presiona la tecla 'F'";
        }

        if (numPregunta == (preguntasPorNivel * 3))
        {
            consejo.text = "Lee la pregunta, toma con la letra 'F' la caja con la respuesta correcta y llevala al altar y sueltala con la letra 'F'";
        }

        if (numPregunta == (preguntasPorNivel * 4))
        {
            consejo.text = "Lee la pregunta, toma la bomba con la letra 'F' sube lo mas que puedas y mirando al enemigo lanza la bomba 'con click del mouse', recuerda esquivar sus ataques";
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
