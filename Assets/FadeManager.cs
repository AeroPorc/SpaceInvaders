using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance { get; private set; }
    [SerializeField] private Volume volume;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float maxIntensity;
    [SerializeField] private float minIntensity;
    private Vignette vignette;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (volume == null)
        {
            Debug.LogError("Volume non assigné !");
            return;
        }

        if (volume.profile.TryGet(out vignette))
        {
            vignette.intensity.overrideState = true;
        }
        else
        {
            Debug.LogError("Aucun effet Vignette trouvé dans le Volume !");
        }
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeVignetteSequence(minIntensity, maxIntensity));
    }

    private IEnumerator FadeVignetteSequence(float maxIntensity, float minIntensity)
    {
        float halfDuration = fadeDuration / 2f;

        yield return StartCoroutine(FadeVignette(maxIntensity, minIntensity, halfDuration));

        yield return StartCoroutine(FadeVignette(minIntensity, maxIntensity, halfDuration));
    }

    private IEnumerator FadeVignette(float startValue, float endValue, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            vignette.intensity.value = Mathf.Lerp(startValue, endValue, t);
            yield return null;
        }

        Debug.Log($"Fade Vignette from {startValue} to {endValue} done !");
        vignette.intensity.value = endValue;
    }
}