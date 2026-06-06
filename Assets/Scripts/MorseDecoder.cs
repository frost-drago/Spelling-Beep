using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public static class MorseDecoder
{
    public const char InvalidLetter = '!';

    // A static dictionary means any script can access it instantly without needing a GameObject
    private static readonly Dictionary<string, char> MorseDictionary = new Dictionary<string, char>
    {
        { ".-", 'A' }, { "-...", 'B' }, { "-.-.", 'C' }, { "-..", 'D' }, 
        { ".", 'E' }, { "..-.", 'F' }, { "--.", 'G' }, { "....", 'H' }, 
        { "..", 'I' }, { ".---", 'J' }, { "-.-", 'K' }, { ".-..", 'L' }, 
        { "--", 'M' }, { "-.", 'N' }, { "---", 'O' }, { ".--.", 'P' }, 
        { "--.-", 'Q' }, { ".-.", 'R' }, { "...", 'S' }, { "-", 'T' }, 
        { "..-", 'U' }, { "...-", 'V' }, { ".--", 'W' }, { "-..-", 'X' }, 
        { "-.--", 'Y' }, { "--..", 'Z' },
        
        { "-----", '0' }, { ".----", '1' }, { "..---", '2' }, { "...--", '3' }, 
        { "....-", '4' }, { ".....", '5' }, { "-....", '6' }, { "--...", '7' }, 
        { "---..", '8' }, { "----.", '9' },

        { ".-.-.-", '.' }, { "--..--", ',' }, { "..--..", '?' }
    };

    private static readonly Dictionary<char, string> LetterToMorseDictionary = BuildLetterToMorseDictionary();

    private static Dictionary<char, string> BuildLetterToMorseDictionary()
    {
        var letterToMorse = new Dictionary<char, string>();
        foreach (KeyValuePair<string, char> entry in MorseDictionary)
            letterToMorse[entry.Value] = entry.Key;

        return letterToMorse;
    }

    /// <summary>
    /// Translates a sequence of dots and dashes into a character.
    /// </summary>
    public static char Translate(string morseSequence)
    {
        if (MorseDictionary.TryGetValue(morseSequence, out char letter))
        {
            return letter;
        }
        
        return InvalidLetter;
    }

    /// <summary>
    /// Returns the decoded letter in lowercase, or "???" when the sequence is not valid morse.
    /// </summary>
    public static string FormatDecodedLetter(char letter)
    {
        if (letter == InvalidLetter)
            return "???";

        return char.ToLowerInvariant(letter).ToString();
    }

    /// <summary>
    /// Returns the morse pattern for a single letter, or null if unknown.
    /// </summary>
    public static string EncodeLetter(char letter)
    {
        char key = char.ToUpperInvariant(letter);
        return LetterToMorseDictionary.TryGetValue(key, out string morse) ? morse : null;
    }

    /// <summary>
    /// Formats a word as morse letter groups separated by " / ", e.g. ".- / .--. / .-.. / ." for "apl".
    /// </summary>
    public static string EncodeWord(string word)
    {
        if (string.IsNullOrEmpty(word))
            return string.Empty;

        var builder = new StringBuilder();
        for (int i = 0; i < word.Length; i++)
        {
            string morse = EncodeLetter(word[i]);
            if (morse == null)
                continue;

            if (builder.Length > 0)
                builder.Append(" / ");

            builder.Append(morse);
        }

        return builder.ToString();
    }

    public static HashSet<int> PickRandomObfuscatedIndices(int wordLength)
    {
        var obfuscated = new HashSet<int>();
        int count = wordLength / 2;

        if (count <= 0)
            return obfuscated;

        var available = new List<int>(wordLength);
        for (int i = 0; i < wordLength; i++)
            available.Add(i);

        for (int i = 0; i < count; i++)
        {
            int pick = Random.Range(0, available.Count);
            obfuscated.Add(available[pick]);
            available.RemoveAt(pick);
        }

        return obfuscated;
    }

    public static string EncodeWordWithObfuscation(string word, int startIndex, HashSet<int> obfuscatedIndices)
    {
        if (string.IsNullOrEmpty(word))
            return string.Empty;

        var builder = new StringBuilder();
        for (int i = 0; i < word.Length; i++)
        {
            if (builder.Length > 0)
                builder.Append(" / ");

            int fullIndex = startIndex + i;
            if (obfuscatedIndices != null && obfuscatedIndices.Contains(fullIndex))
            {
                builder.Append("?");
                continue;
            }

            string morse = EncodeLetter(word[i]);
            if (morse != null)
                builder.Append(morse);
        }

        return builder.ToString();
    }
}