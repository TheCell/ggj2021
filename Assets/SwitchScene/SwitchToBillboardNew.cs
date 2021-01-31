using UnityEngine.SceneManagement;

public static class SwitchToBillboardNew
{
    public static int previousSceneIndex = 0;

    public static void SwitchToBillboard()
    {
        previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(5);
    }

    public static void SwitchToScene()
    {
        SceneManager.LoadScene(previousSceneIndex);
    }
}
