using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem; 

public class Typer : MonoBehaviour
{
    public WordBank wordBank = null;
    public TextMeshProUGUI wordOutput;

    private string remainingWord = string.Empty;
    private string currentWord;

    private void Start()
    {
        SetCurrentWord();
    }

    private void SetCurrentWord()
    {
        currentWord = wordBank.GetWord();
        SetRemainingWord(currentWord);
    }

    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        wordOutput.text = remainingWord;
    }

    // --- UNITY 6 NEW INPUT SYSTEM EVENT MANAGEMENT ---
    private void OnEnable()
    {
        // Hook into Unity's global text input event when this script wakes up
        Keyboard.current.onTextInput += OnCharacterTyped;
    }

    private void OnDisable()
    {
        // Unhook the event when the object is disabled/destroyed to prevent memory leaks
        if (Keyboard.current != null)
        {
            Keyboard.current.onTextInput -= OnCharacterTyped;
        }
    }

    private void OnCharacterTyped(char character)
    {
        // Convert the typed char to a string and pass it to your logic
        string typedLetter = character.ToString();
        EnterLetter(typedLetter);
    }
    // -----------------------------------------------------

    private void EnterLetter(string typedLetter)
    {
        if(IsCorrectLetter(typedLetter))
        {
            RemoveLetter();

            if (IsWordComplete())
            {
                SetCurrentWord(); // Handle word completion (e.g., display success message, load next word)
            }
        }
    }

    private bool IsCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;
    }

    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }
}