using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip somClique;

    public void TocarSom()
    {
        audioSource.PlayOneShot(somClique);
    }
}
