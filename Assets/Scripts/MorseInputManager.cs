//MorseInputManager.cs

using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MorseInputManager : MonoBehaviour
{
    [Header("Timing Configuration")]
    public float dotThreshold = 0.25f;
    public float letterGapThreshold = 0.8f;

    [Header("Word System Link")]
    public Typer typer;
    public TextMeshProUGUI typingOutput;

    private float pressStartTime;
    private float releaseStartTime;
    private bool isPressed = false;
    private bool gapTracked = true;

    private string currentLetterSequence = "";
    private string lastCompletedDisplay = "";

    void Update()
    {
        bool spaceJustPressed = Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;
        bool mouseJustPressed = Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;

        if (spaceJustPressed || mouseJustPressed)
        {
            isPressed = true;
            pressStartTime = Time.time;
            gapTracked = false;
        }

        bool spaceJustReleased = Keyboard.current != null && Keyboard.current.spaceKey.wasReleasedThisFrame;
        bool mouseJustReleased = Mouse.current != null && Mouse.current.leftButton.wasReleasedThisFrame;

        if (isPressed && (spaceJustReleased || mouseJustReleased))
        {
            isPressed = false;
            releaseStartTime = Time.time;
            float holdDuration = Time.time - pressStartTime;

            if (holdDuration < dotThreshold)
                OnDotDetected();
            else
                OnDashDetected();
        }

        if (!isPressed && !gapTracked)
        {
            float idleDuration = Time.time - releaseStartTime;
            if (idleDuration >= letterGapThreshold)
            {
                ProcessCompletedSequence();
                gapTracked = true;
            }
        }
    }

    public void ClearTypingDisplay()
    {
        currentLetterSequence = string.Empty;
        lastCompletedDisplay = string.Empty;
        UpdateTypingDisplay();
    }

    void OnDotDetected()
    {
        BeginNextLetterIfNeeded();
        currentLetterSequence += ".";
        UpdateTypingDisplay();
        Debug.Log("Current Sequence: " + currentLetterSequence + " (Dot)");
    }

    void OnDashDetected()
    {
        BeginNextLetterIfNeeded();
        currentLetterSequence += "-";
        UpdateTypingDisplay();
        Debug.Log("Current Sequence: " + currentLetterSequence + " (Dash)");
    }

    void ProcessCompletedSequence()
    {
        if (string.IsNullOrEmpty(currentLetterSequence))
            return;

        string finishedSequence = currentLetterSequence;
        char translatedLetter = MorseDecoder.Translate(finishedSequence);
        currentLetterSequence = string.Empty;
        lastCompletedDisplay = MorseDecoder.FormatDecodedLetter(translatedLetter);
        UpdateTypingDisplay();

        Debug.Log($"[Morse] {finishedSequence}  =>  [Decoded] {translatedLetter}");

        if (typer != null)
            typer.ReceiveMorseLetter(translatedLetter);
        else
            Debug.LogWarning("Typer reference is missing on the MorseInputManager component!");
    }

    private void BeginNextLetterIfNeeded()
    {
        if (currentLetterSequence.Length > 0 || string.IsNullOrEmpty(lastCompletedDisplay))
            return;

        lastCompletedDisplay = string.Empty;
    }

    private void UpdateTypingDisplay()
    {
        if (typingOutput == null)
            return;

        typingOutput.text = currentLetterSequence.Length > 0
            ? currentLetterSequence
            : lastCompletedDisplay;
    }
}
