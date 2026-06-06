using UnityEngine;
using TMPro;

public class Typer : MonoBehaviour
{
    public WordBank wordBank = null;
    public TextMeshProUGUI wordOutputBackground;
    public TextMeshProUGUI wordOutputForeground;
    public TextMeshProUGUI morseOutput;
    public MorseInputManager morseInputManager;
    public CorrectLetterFx correctLetterFx;
    public ScreenShake screenShake;

    private string remainingWord = string.Empty;
    private string currentWord;

    private void Start()
    {
        SetCurrentWord();
    }

    private void SetCurrentWord()
    {
        currentWord = wordBank.GetWord();
        morseInputManager?.ClearTypingDisplay();
        SetRemainingWord(currentWord);
    }

    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        UpdateWordDisplays();
    }

    private void UpdateWordDisplays()
    {
        wordOutputBackground.text = currentWord;
        wordOutputForeground.text = currentWord;

        if (morseOutput != null)
            morseOutput.text = MorseDecoder.EncodeWord(remainingWord);

        wordOutputForeground.ForceMeshUpdate();
        int revealedCount = currentWord.Length - remainingWord.Length;
        HideRevealedForegroundLetters(revealedCount);
    }

    /// <summary>
    /// Makes completed letters transparent on the white layer so the green word shows through underneath.
    /// Both layers keep the full word string so glyph positions stay aligned.
    /// </summary>
    private void HideRevealedForegroundLetters(int revealedCount)
    {
        TMP_TextInfo textInfo = wordOutputForeground.textInfo;

        int visibleIndex = 0;
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible)
                continue;

            byte alpha = visibleIndex < revealedCount ? (byte)0 : (byte)255;
            visibleIndex++;

            int materialIndex = charInfo.materialReferenceIndex;
            Color32[] vertexColors = textInfo.meshInfo[materialIndex].colors32;
            int vertexIndex = charInfo.vertexIndex;
            for (int v = 0; v < 4; v++)
                vertexColors[vertexIndex + v].a = alpha;

            textInfo.meshInfo[materialIndex].colors32 = vertexColors;
        }

        wordOutputForeground.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }

    /// <summary>
    /// Called by MorseInputManager when a dot/dash sequence is decoded into a letter.
    /// </summary>
    public void ReceiveMorseLetter(char letter)
    {
        if (letter == MorseDecoder.InvalidLetter)
        {
            screenShake?.ShakeHorizontal();
            return;
        }

        EnterLetter(char.ToLowerInvariant(letter).ToString());
    }

    private void EnterLetter(string typedLetter)
    {
        if (IsCorrectLetter(typedLetter))
        {
            correctLetterFx?.Play();
            RemoveLetter();

            if (IsWordComplete())
            {
                wordBank?.AdvanceLevel();
                SetCurrentWord();
            }
        }
        else
        {
            screenShake?.ShakeHorizontal();
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
