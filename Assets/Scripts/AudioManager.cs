using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        StartCoroutine(PlaySFXCoroutine(clip, volume));

    }

    IEnumerator PlaySFXCoroutine(AudioClip clip, float volume = 1f)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        yield return new WaitForSeconds(clip.length * 2);

        Destroy(audioSource);
    }

}
