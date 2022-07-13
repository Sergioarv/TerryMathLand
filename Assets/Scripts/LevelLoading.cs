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
