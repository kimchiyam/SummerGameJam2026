using UnityEngine;

public class ClickSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void PlaySound()
    {
        audioSource.Play();
    }
}
