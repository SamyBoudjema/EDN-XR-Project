using UnityEngine;

/// <summary>
/// Détecte si une quille est tombée en comparant son inclinaison
/// par rapport à son orientation initiale.
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

    // Direction "haut" initiale de la quille au démarrage
    private Vector3 initialUp;

    void Start()
    {
        // Mémoriser l'orientation initiale de la quille
        initialUp = transform.up;
    }

    void Update()
    {
        // Calculer l'angle entre l'orientation actuelle et l'orientation initiale
        float angle = Vector3.Angle(transform.up, initialUp);

        // Si l'angle dépasse le seuil et que la quille n'est pas encore marquée comme tombée
        if (angle > fallAngleThreshold && !isFallen)
        {
            isFallen = true;

            // Bonus : déclencher l'effet de particules une seule fois
            if (celebrationEffect != null)
            {
                var main = celebrationEffect.main;
                main.loop = false; // Désactiver la boucle
                celebrationEffect.Play();
            }
        }

        // Debug : décommenter la ligne suivante pour faire disparaître la quille quand elle tombe
        // gameObject.SetActive(!isFallen);
    }
}
