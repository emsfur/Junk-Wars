using System.Collections;
using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{
    public AudioSource windSource;
    public AudioSource musicSource;

    public float windFadeInDuration = 10f;
    public float windHoldDuration = 8f;
    public float windFadeOutDuration = 3f;

    public float musicFadeInDuration = 5f;

    void Start()
    {
        windSource.volume = 0;
        musicSource.volume = 0;

        StartCoroutine(PlayAmbientSequence());
    }

    IEnumerator PlayAmbientSequence()
    {
        // Fade in wind
        windSource.Play();
        yield return StartCoroutine(FadeAudioSource(windSource, 0f, 0.5f, windFadeInDuration));

        // Hold wind
        yield return new WaitForSeconds(windHoldDuration);

        // Fade out wind and fade in music at the same time
        musicSource.Play();
        yield return StartCoroutine(FadeBothAudioSources(
            windSource, 0.5f, 0f,
            musicSource, 0f, 0.2f,
            Mathf.Max(windFadeOutDuration, musicFadeInDuration) // sync over longest time
        ));

        // Stop wind after fading out
        windSource.Stop();
    }

    IEnumerator FadeAudioSource(AudioSource source, float startVol, float endVol, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float normalizedTime = t / duration;
            source.volume = Mathf.Lerp(startVol, endVol, normalizedTime);
            yield return null;
        }
        source.volume = endVol;
    }

    IEnumerator FadeBothAudioSources(AudioSource sourceA, float startA, float endA,
                                      AudioSource sourceB, float startB, float endB,
                                      float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float n = t / duration;
            sourceA.volume = Mathf.Lerp(startA, endA, n);
            sourceB.volume = Mathf.Lerp(startB, endB, n);
            yield return null;
        }
        sourceA.volume = endA;
        sourceB.volume = endB;
    }
}
