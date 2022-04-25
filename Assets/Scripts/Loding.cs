using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Loding : MonoBehaviour
{

    public Slider sliderLoad;
    public TextMeshProUGUI txtSliderLoad;
    public TextMeshProUGUI consejo;
    private int numPregunta;

    // Start is called before the first frame update
    void Start()
    {
        int levelToLoad = LevelLoading.nextLevel;

        numPregunta = DatosEntreEscenas.instace.numPregunta;

        StartCoroutine(LoadLevel(levelToLoad));
    }

    private IEnumerator LoadLevel(int level)
    {
        sliderLoad.value = 0;

        if (numPregunta == 0)
        {
            consejo.text = "Lee la pregunta y selecciona con el mouse el globo con la respuesta correcta";
        }

        if (numPregunta == 5)
        {
            consejo.text = "Lee la pregunta y tira al blanco con la respuesta correcta";
        }

        if (numPregunta == 10)
        {
            consejo.text = "Lee la pregunta y la al altar la caja con la respuesta correcta";
        }

        if (numPregunta == 15)
        {
            consejo.text = "Lee la pregunta y busca la puerta con la respuesta correcta";
        }

        if (numPregunta == 20)
        {
            consejo.text = "Lee la pregunta y ....";
        }

        while (sliderLoad.value < 1f && (numPregunta == 0 || numPregunta == 5 || numPregunta == 10 || numPregunta == 15 || numPregunta == 20))
        {
            sliderLoad.value += 0.0006f;
            txtSliderLoad.text = (int)(sliderLoad.value * 100) + "%";

            yield return null;
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(level);
    }
}
