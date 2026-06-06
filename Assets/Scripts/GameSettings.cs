public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public static class GameSettings
{
    public const int WinStageLevel = 8;
    public const int EndlessStartLevel = 9;

    public static Difficulty SelectedDifficulty { get; set; } = Difficulty.Easy;
    public static int LastClearedLevel { get; set; }
    public static bool EndlessMode { get; set; }
    public static int PendingStartLevel { get; set; } = 1;

    public static string GetDifficultyLabel()
    {
        return SelectedDifficulty.ToString();
    }

    public static void PrepareNewRun()
    {
        EndlessMode = false;
        PendingStartLevel = 1;
    }

    public static void PrepareEndlessContinue()
    {
        EndlessMode = true;
        PendingStartLevel = EndlessStartLevel;
    }
}
