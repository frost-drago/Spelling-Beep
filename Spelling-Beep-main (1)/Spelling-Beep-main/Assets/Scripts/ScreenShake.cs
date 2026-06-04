using System.Collections;
using UnityEngine;

/// <summary>
/// Shakes UI horizontally. Attach to a child panel under the Canvas (not the Canvas root).
/// Screen Space Overlay canvases reset the root transform, so shaking a child is required.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class ScreenShake : MonoBehaviour
{
    [SerializeField] private float duration = 0.15f;
    [SerializeField] private float magnitude = 30f;

    private RectTransform rectTransform;
    private Vector3 restLocalPosition;
    private Coroutine shakeRoutine;

    private void Awake()
    {
        rectTransform = (RectTransform)transform;
        restLocalPosition = rectTransform.localPosition;
    }

    public void ShakeHorizontal()
    {
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        restLocalPosition = rectTransform.localPosition;
        shakeRoutine = StartCoroutine(ShakeRoutine());
    }

    private IEnumerator ShakeRoutine()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-magnitude, magnitude);
            rectTransform.localPosition = restLocalPosition + new Vector3(offsetX, 0f, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rectTransform.localPosition = restLocalPosition;
        shakeRoutine = null;
    }
}
