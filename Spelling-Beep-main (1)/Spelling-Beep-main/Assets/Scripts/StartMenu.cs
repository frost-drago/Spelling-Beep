using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // "public" must be lowercase!
    public void PlayGame()
    {
        // Added the missing semicolon at the end
        SceneManager.LoadSceneAsync("DifficultyMenu"); 
    }

    public void QuitGame()
    {
        // If playing in the Unity Editor, stop playing
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // If running the actual built game application, close it
        Application.Quit();
        #endif
    }

    public void QuitGameSession()
    {
        // Added the missing semicolon at the end
        SceneManager.LoadSceneAsync("StartMenu"); 
    }
}