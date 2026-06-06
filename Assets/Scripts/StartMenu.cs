using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("DifficultyMenu");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void QuitGameSession()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
