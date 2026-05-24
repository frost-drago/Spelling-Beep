//MorseInputManager.cs

using UnityEngine;
// 1. Crucial: Include the new Input System namespace
using UnityEngine.InputSystem; 

public class MorseInputManager : MonoBehaviour
{
    [Header("Timing Configuration")]
    public float dotThreshold = 0.25f;
    public float letterGapThreshold = 0.8f;

    // 🚀 NEW: This links our Input Manager to your Word Tracker component
    [Header("Word System Link")]
    public WordTracker wordTracker;

    private float pressStartTime;
    private float releaseStartTime;
    private bool isPressed = false;
    private bool gapTracked = true;

    private string currentLetterSequence = "";

    void Update()
    {
        // 2. Modern Unity 6 way to check if Spacebar or Left Mouse Button was JUST pressed
        bool spaceJustPressed = Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;
        bool mouseJustPressed = Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;

        if (spaceJustPressed || mouseJustPressed)
        {
            isPressed = true;
            pressStartTime = Time.time;
            gapTracked = false;
        }

        // 3. Modern Unity 6 way to check if Spacebar or Left Mouse Button was JUST released
        bool spaceJustReleased = Keyboard.current != null && Keyboard.current.spaceKey.wasReleasedThisFrame;
        bool mouseJustReleased = Mouse.current != null && Mouse.current.leftButton.wasReleasedThisFrame;

        if (isPressed && (spaceJustReleased || mouseJustReleased))
        {
            isPressed = false;
            releaseStartTime = Time.time;
            float holdDuration = Time.time - pressStartTime;

            if (holdDuration < dotThreshold)
            {
                OnDotDetected();
            }
            else
            {
                OnDashDetected();
            }
        }

        // Detect Gaps (End of a letter sequence)
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

    void OnDotDetected()
    {
        currentLetterSequence += ".";
        Debug.Log("Current Sequence: " + currentLetterSequence + " (Dot)");
    }

    void OnDashDetected()
    {
        currentLetterSequence += "-";
        Debug.Log("Current Sequence: " + currentLetterSequence + " (Dash)");
    }

    void ProcessCompletedSequence()
    {
        if (string.IsNullOrEmpty(currentLetterSequence)) return;

        // 1. Send the sequence to our new decoder dictionary
        char translatedLetter = MorseDecoder.Translate(currentLetterSequence);
        
        // 2. Log out both the code and the real letter!
        Debug.Log($"[Morse] {currentLetterSequence}  =>  [Decoded] {translatedLetter}");
        
        // 🚀 NEW: Send the decoded letter to the Word Tracker component!
        if (wordTracker != null)
        {
            wordTracker.ReceiveLetter(translatedLetter);
        }
        else
        {
            Debug.LogWarning("WordTracker reference is missing on the MorseInputManager component!");
        }
        
        // Reset for the next letter
        currentLetterSequence = "";
    }
}