public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public static class GameSettings
{
    public static Difficulty SelectedDifficulty { get; set; } = Difficulty.Easy;
    public static int LastClearedLevel { get; set; }

    public static string GetDifficultyLabel()
    {
        return SelectedDifficulty.ToString();
    }
}
