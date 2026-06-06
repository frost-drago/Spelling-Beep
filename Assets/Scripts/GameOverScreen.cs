using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI statsText;

    private void Start()
    {
        if (titleText != null)
            titleText.text = "Game Over";

        if (statsText == null)
            return;

        string stageLine = GameSettings.LastClearedLevel > 0
            ? $"Last stage cleared: {GameSettings.LastClearedLevel}"
            : "Last stage cleared: None";

        statsText.text = $"{stageLine}\nDifficulty: {GameSettings.GetDifficultyLabel()}";
    }

    public void GoToDifficultyMenu()
    {
        SceneManager.LoadScene("DifficultyMenu");
    }
}
