using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWinScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;

    private void Start()
    {
        if (titleText != null)
            titleText.text = "You Win";
    }

    public void ContinueEndless()
    {
        GameSettings.PrepareEndlessContinue();
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
