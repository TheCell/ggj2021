using UnityEngine;

public class RestartGame : MonoBehaviour
{
    public void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void RestartGameNow()
    {
        // deprecated but this has to do for the ggj
        Application.LoadLevel(0);
    }
}
