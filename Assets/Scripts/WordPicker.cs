using System;
using System.IO;
using UnityEngine;

public static class WordPicker
{
    static string[] words;
    static System.Random rng = new System.Random();

    static void EnsureWordsLoaded()
    {
        if (words != null)
            return;

        string path = Path.Combine(Application.streamingAssetsPath, "Word Dictionary", "output.txt");
        words = File.ReadAllLines(path);
    }

    static readonly (int start, int end)[] ranges =
    {
        (0, 0),
        (0, 0),

        (0, 100),           //2 letters
        (101, 1115),        //3 letters
        (1116, 5145),       //4 letters
        (5146, 14083),      //5 letters
        (14084, 29871),     //6 letters
        (29872, 53900),     //7 letters
        (53901, 83666),     //8 letters
        (83667, 112816),    //9 letters
        (112817, 135142),   //10 letters
        (135143, 151307),   //11 letters
        (151308, 162724),   //12 letters
        (162725, 170474),   //13 letters
        (170475, 175533),   //14 letters
        (175534, 178690)    //15 letters
    };

    public static string GetRandomWord(int length)
    {
        EnsureWordsLoaded();

        // Check if the requested length is valid
        if (length < 2 || length >= ranges.Length)
        {
            throw new ArgumentException(
                $"Word length must be between 2 and {ranges.Length - 1}"
            );
        }

        // Get the start and end index for this word length
        var (start, end) = ranges[length];

        // Pick a random index inside the range
        int randomIndex = rng.Next(start, end + 1);

        // Return the word at that index
        return words[randomIndex];
    }

    public static string GetRandomWordRange(int minLength, int maxLength)
    {
        EnsureWordsLoaded();

        // Check if lengths are valid
        if (minLength < 2 || maxLength >= ranges.Length)
        {
            throw new ArgumentException(
                $"Lengths must be between 2 and {ranges.Length - 1}"
            );
        }

        // Check if min is larger than max
        if (minLength > maxLength)
        {
            throw new ArgumentException(
                "Minimum length cannot be greater than maximum length"
            );
        }

        // Get the starting index from minLength
        var (start, _) = ranges[minLength];

        // Get the ending index from maxLength
        var (_, end) = ranges[maxLength];

        // Pick a random index inside the combined range
        int randomIndex = rng.Next(start, end + 1);

        // Return the word at that index
        return words[randomIndex];
    }
} // WordPicker ends here

public static class LevelScaler
{
    public static (int minLength, int maxLength) GetWordLengthRange(int level)
    {
        if (level <= 2)
            return (2, 3);

        int group = (level - 3) / 2;

        int min = 2 + group;
        int max = 4 + group;

        min = Math.Min(min, 15);
        max = Math.Min(max, 15);

        return (min, max);
    }

    // this method is the one getting exported, make sure to add level number
    public static string GetWordForLevel(int level)
    {
        var (min, max) = GetWordLengthRange(level);

        if (min == max)
        {
            return WordPicker.GetRandomWord(min);
        }

        return WordPicker.GetRandomWordRange(min, max);
    }
} // LevelScaler ends here