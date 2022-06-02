using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelLoading
{
    public static int nextLevel;

    public static void LoadLevel(int name)
    {
        nextLevel = name;

        SceneManager.LoadScene("PantallaDeCarga");
    }
}
