using System.Collections.Generic;
using UnityEngine;

public static class MorseDecoder
{
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

    /// <summary>
    /// Translates a sequence of dots and dashes into a character.
    /// </summary>
    public static char Translate(string morseSequence)
    {
        if (MorseDictionary.TryGetValue(morseSequence, out char letter))
        {
            return letter;
        }
        
        // Return a fallback character if someone enters a sequence that doesn't exist
        return '!'; 
    }
}