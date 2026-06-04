using UnityEngine;

public class CorrectLetterFx : MonoBehaviour
{
    [SerializeField] private GameObject fxPrefab;

    public void Play()
    {
        if (fxPrefab == null)
            return;

        GameObject instance = Instantiate(fxPrefab, transform.position, transform.rotation);
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
}
