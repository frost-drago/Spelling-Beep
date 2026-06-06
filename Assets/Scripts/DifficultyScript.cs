using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyScript : MonoBehaviour
{
    public void EasyMode()
    {
        GameSettings.SelectedDifficulty = Difficulty.Easy;
        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void MediumMode()
    {
        GameSettings.SelectedDifficulty = Difficulty.Medium;
        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void HardMode()
    {
        GameSettings.SelectedDifficulty = Difficulty.Hard;
        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("StartMenu");
    }
}
