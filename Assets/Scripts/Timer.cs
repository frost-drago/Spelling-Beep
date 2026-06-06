using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private const float MaxTime = 30f;
    private const float LevelBonus = 10f;
    private const string GameOverScene = "GameOver";

    [SerializeField] private Image fillImage;
    [SerializeField] private WordBank wordBank;

    private float timeRemaining;
    private bool isRunning;
    private bool gameEnded;

    private void Awake()
    {
        if (wordBank == null)
            wordBank = FindFirstObjectByType<WordBank>();
    }

    private void OnEnable()
    {
        if (wordBank != null)
            wordBank.OnLevelChanged += HandleLevelChanged;
    }

    private void OnDisable()
    {
        if (wordBank != null)
            wordBank.OnLevelChanged -= HandleLevelChanged;
    }

    private void Start()
    {
        timeRemaining = MaxTime;
        isRunning = true;
        UpdateBar();
    }

    private void Update()
    {
        if (!isRunning || gameEnded)
            return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            isRunning = false;
            gameEnded = true;
            UpdateBar();
            SceneManager.LoadScene(GameOverScene);
            return;
        }

        UpdateBar();
    }

    private void HandleLevelChanged(int level)
    {
        if (level <= 1)
            return;

        timeRemaining = Mathf.Min(MaxTime, timeRemaining + LevelBonus);
        UpdateBar();
    }

    private void UpdateBar()
    {
        if (fillImage != null)
            fillImage.fillAmount = timeRemaining / MaxTime;
    }
}
