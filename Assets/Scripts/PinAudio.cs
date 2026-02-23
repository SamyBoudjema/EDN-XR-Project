using UnityEngine;

/// <summary>
/// Joue un son lorsqu'une quille est percutée par un autre objet.
/// À attacher sur chaque quille avec un composant AudioSource configuré.
/// Bonus TP2.
/// </summary>
public class PinAudio : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Récupérer le composant AudioSource attaché à la quille
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Jouer le son uniquement s'il n'est pas déjà en cours de lecture
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
