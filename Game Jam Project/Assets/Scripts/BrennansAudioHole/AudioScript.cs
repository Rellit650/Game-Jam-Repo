using System.Collections;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    private float endVol;
    /// <summary>
    ///  A utility script to allow dynamic transitions between bgm clips
    ///  Similar in nature to how FMOD tends to output, but with a much simpler backend
    /// </summary>
    /// <param name="source">Audio source to play clip from.</param>
    /// <param name="clip">Audio clip to play.</param>
    /// <param name="volume">Volume the clip should be played at [0.0, 1.0]. Set negative to maintain prior volume.</param>
    /// <param name="otherSource">The second source to use (only necessary for crossfading). Default: null</param>
    /// <param name="shouldFade">Should the transition crossfade between two clips or full start/stop. Default: false</param>
    /// <param name="fadeTime">Time the crossfade should take. Default: 1.0f</param>
    public void SwitchAudioTrackToVariant(AudioSource source, AudioClip clip, float volume, AudioSource otherSource = null, bool shouldFade = false, float fadeTime = 1.0f)
    {
        // If invalid source
        if (source == null)
        {
            Debug.LogError("Invalid Primary Audio Source");
            return;
        }
        
        // If invalid clip
        if (clip == null)
        {
            source.Stop();
            Debug.LogWarning("Invalid Audio Clip, Stopping Audio in Source");
            return;
        }

        // Handling based on fade
        switch (shouldFade)
        {
            // If the tracks should crossfade
            case true:
                // If the other source doesn't exist, then we need a new one
                if (otherSource == null)
                {
                    Debug.LogWarning("Invalid Secondary Audio Source, Creating a New One");
                    Transform sTransform = source.transform;
                    otherSource = Instantiate(source, sTransform.position, sTransform.rotation);
                }
                // Lining up the prior track with the current
                otherSource.Stop();
                otherSource.clip = clip;
                otherSource.time = source.time;
                otherSource.volume = 0.0f;
                otherSource.Play();
                
                // Assigning the proper variables for the crossfade to properly function
                endVol = volume > 0.0f ? volume : source.volume;
                StartCoroutine(StartFade(source, fadeTime, 0.0f));
                StartCoroutine(StartFade(otherSource, fadeTime, endVol));
                break;
            // If the tracks should not crossfade
            case false:
                // Reassign the source's clip and start from the beginning
                source.Stop();
                source.clip = clip;
                if (volume > 0.0f)
                {
                    source.volume = volume;
                }
                source.Play();
                break;
        }
    }
    
    private static IEnumerator StartFade(AudioSource sourceToFade, float timeToFade, float endVol)
    {
        float currentTime = 0;
        float start = sourceToFade.volume;

        while (currentTime < timeToFade)
        {
            currentTime += Time.deltaTime;
            sourceToFade.volume = Mathf.Lerp(start, endVol, currentTime / timeToFade);
            yield return null;
        }
    }
}
