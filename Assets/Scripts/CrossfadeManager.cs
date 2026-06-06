using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossfadeManager : MonoBehaviour
{
    public static CrossfadeManager Instance { get; private set; }
    public Texture2D CapturedScreenshot { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Keep this alive across scenes
    }

    public void LoadSceneWithCrossfade(string sceneName)
    {
        StartCoroutine(CaptureAndLoad(sceneName));
    }

    private IEnumerator CaptureAndLoad(string sceneName)
    {
        // Wait until the end of the frame so everything is completely rendered
        yield return new WaitForEndOfFrame();

        // Take a snapshot of the exact screen resolution
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        CapturedScreenshot = screenshot;

        // Load the You Win scene
        SceneManager.LoadScene(sceneName);
    }

    public void ClearScreenshot()
    {
        if (CapturedScreenshot != null)
        {
            Destroy(CapturedScreenshot);
            CapturedScreenshot = null;
        }
    }
}