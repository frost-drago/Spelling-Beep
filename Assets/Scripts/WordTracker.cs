// WordTracker.cs

using UnityEngine;
using TMPro; // Crucial for talking to TextMeshPro

public class WordTracker : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI wordTextDisplay;

    [Header("Game Configuration")]
    public string targetWord = "APPLE";
    
    private string currentTypedWord = "";

    void Start()
    {
        targetWord = targetWord.ToUpper(); // Keep it clean and capitalized
        UpdateVisuals();
    }

    // This is the function our MorseInputManager will call every time a letter is decoded
    public void ReceiveLetter(char letter)
    {
        // 1. Figure out what the NEXT letter needs to be
        int nextLetterIndex = currentTypedWord.Length;

        if (nextLetterIndex < targetWord.Length)
        {
            // 2. Check if the player typed the correct letter
            if (letter == targetWord[nextLetterIndex])
            {
                currentTypedWord += letter;
                Debug.Log($"Correct! Word progress: {currentTypedWord}");
                
                // Vibe Check: Perfect spot to trigger a successful 'ding' sound or light burst!
            }
            else
            {
                Debug.Log($"Wrong letter! Expected {targetWord[nextLetterIndex]}, but got {letter}");
                // Vibe Check: Trigger a screen shake or minor red neon glitch here
            }
        }

        UpdateVisuals();

        // 3. Check if the whole word is finished
        if (currentTypedWord == targetWord)
        {
            OnWordCompleted();
        }
    }

    void UpdateVisuals()
    {
        if (wordTextDisplay == null) return;

        // Vibe-coding trick: Color code the letters the player has already finished!
        string completedPart = $"<color=#00FFCC>{currentTypedWord}</color>"; // Clean neon teal
        string remainingPart = targetWord.Substring(currentTypedWord.Length);

        wordTextDisplay.text = completedPart + remainingPart;
    }

    void OnWordCompleted()
    {
        Debug.Log("🎉 WORD COMPLETED! VICTORY! 🎉");
        // Next steps: Load a new word, give points, flash the screen, etc.
    }
}