using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float timePenalty = 10f;
    public GameObject explosionVFX;
    public AudioClip explosionSound;

    public void Slice()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddTimePenalty(timePenalty);
        }

        if (explosionVFX != null)
        {
            Instantiate(explosionVFX, transform.position, Quaternion.identity);
        }

        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        Destroy(gameObject);
    }
}
