using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public AudioSource source,
                       newSource;
    public float newVolume;
    public AudioClip clip;
    public void OnClick(GameObject obj)
    {
        obj.GetComponent<AudioScript>().SwitchAudioTrackToVariant(source, clip, newVolume, newSource, true, 2.0f);
    }
}
