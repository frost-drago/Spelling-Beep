public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public static class GameSettings
{
    public static Difficulty SelectedDifficulty { get; set; } = Difficulty.Easy;
}
