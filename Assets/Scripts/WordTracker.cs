using TMPro;
using UnityEngine;

public class WordTracker : MonoBehaviour
{
    [SerializeField] private WordBank wordBank;
    [SerializeField] private TextMeshProUGUI levelDisplay;

    public int CurrentLevel => wordBank != null ? wordBank.Level : 1;

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
        UpdateLevelDisplay(CurrentLevel);
    }

    private void HandleLevelChanged(int level)
    {
        UpdateLevelDisplay(level);
    }

    private void UpdateLevelDisplay(int level)
    {
        if (levelDisplay != null)
            levelDisplay.text = $"Level {level}";
    }
}
