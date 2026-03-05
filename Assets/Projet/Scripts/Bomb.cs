using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float timePenalty = 10f;
    public GameObject explosionVFX;

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

        Destroy(gameObject);
    }
}
