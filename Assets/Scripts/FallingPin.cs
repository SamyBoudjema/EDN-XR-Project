using UnityEngine;

/// <summary>
/// Détecte si une quille est tombée en comparant son inclinaison
/// par rapport à la verticale (Vector3.up).
/// À attacher sur chaque prefab de quille (Pin).
/// </summary>
public class FallingPin : MonoBehaviour
{
    [Tooltip("Angle en degrés au-delà duquel la quille est considérée comme tombée")]
    public float fallAngleThreshold = 45f;

    [Tooltip("État de la quille : true si elle est tombée")]
    public bool isFallen = false;

    [Tooltip("Effet de particules de célébration (bonus)")]
    public ParticleSystem celebrationEffect;

    void Update()
    {
        // Calculer l'angle entre le haut de la quille et le haut du monde
        float angle = Vector3.Angle(transform.up, Vector3.up);

        // Si l'angle dépasse le seuil et que la quille n'est pas encore marquée comme tombée
        if (angle > fallAngleThreshold && !isFallen)
        {
            isFallen = true;

            // Bonus : déclencher l'effet de particules si configuré
            if (celebrationEffect != null)
            {
                celebrationEffect.Play();
            }
        }

        // Debug : décommenter la ligne suivante pour faire disparaître la quille quand elle tombe
        // gameObject.SetActive(!isFallen);
    }
}
