using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyScript : MonoBehaviour
{
    public void EasyMode()
    {
        GameSettings.SelectedDifficulty = Difficulty.Easy;
        StartNewRun();
    }

    public void MediumMode()
    {
        GameSettings.SelectedDifficulty = Difficulty.Medium;
        StartNewRun();
    }

    public void HardMode()
    {
        GameSettings.SelectedDifficulty = Difficulty.Hard;
        StartNewRun();
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("StartMenu");
    }

    private static void StartNewRun()
    {
        GameSettings.PrepareNewRun();
        SceneManager.LoadSceneAsync("SampleScene");
    }
}
