using UnityEngine;

public class FruitTarget : MonoBehaviour
{
    public string ingredientName = "Pomme";
    public int scoreValue = 10;
    public GameObject explosionPrefab; // Prefab du système de particules (VFX d'explosion)
    public AudioClip explosionSound;    // Son d'écrasement de fruit

    public void Slice(Vector3 sliceDirection, Vector3 contactPoint, Vector3 sliceVelocity)
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.currentMode == GameMode.Defouloir)
            {
                GameManager.Instance.AddScore(scoreValue);
            }
            else if (GameManager.Instance.currentMode == GameMode.Recette)
            {
                GameManager.Instance.AddScore(scoreValue);
            }
        }

        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        }

        if (explosionSound != null)
        {
            // Create a temporary object to play the sound at the explosion location
            // so it doesn't get cut off when the fruit is destroyed
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        Destroy(gameObject);
    }
}
