using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWinScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private GameObject confettiPrefab;

    private void Start()
    {
        if (titleText != null)
            titleText.text = "You Win";

        PlayConfetti();
    }

    private void PlayConfetti()
    {
        if (confettiPrefab == null)
            return;

        GameObject instance = Instantiate(confettiPrefab, new Vector3(0f, 1f, 0f), Quaternion.identity);
        float destroyDelay = PlayAllParticleSystems(instance);
        Destroy(instance, destroyDelay);
    }

    private static float PlayAllParticleSystems(GameObject root)
    {
        float destroyDelay = 2f;
        ParticleSystem[] particleSystems = root.GetComponentsInChildren<ParticleSystem>(true);

        foreach (ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.Clear(true);
            particleSystem.Play(true);

            ParticleSystem.MainModule main = particleSystem.main;
            float lifetime = main.startLifetime.mode == ParticleSystemCurveMode.TwoConstants
                ? main.startLifetime.constantMax
                : main.startLifetime.constant;
            destroyDelay = Mathf.Max(destroyDelay, main.duration + lifetime);
        }

        return destroyDelay + 0.25f;
    }

    public void ContinueEndless()
    {
        GameSettings.PrepareEndlessContinue();
        SceneManager.LoadScene("SampleScene");
    }

    public void BackToMainMenu(){
        SceneManager.LoadScene("StartMenu");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
