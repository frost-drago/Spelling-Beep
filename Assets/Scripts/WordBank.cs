using System;
using UnityEngine;

public class WordBank : MonoBehaviour
{
    private int level = 1;

    public int Level => level;
    public event Action<int> OnLevelChanged;

    private void Awake()
    {
        level = 1;
    }

    public string GetWord()
    {
        return LevelScaler.GetWordForLevel(level).ToLowerInvariant();
    }

    public void AdvanceLevel()
    {
        level++;
        OnLevelChanged?.Invoke(level);
    }
}
