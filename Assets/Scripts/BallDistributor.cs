using UnityEngine;
using System.Collections;

/// <summary>
/// Simule un système de retour de boule de bowling réaliste.
/// La boule apparaît depuis un point bas (machine de retour) et remonte
/// en suivant une courbe de Bézier jusqu'à la position de repos du joueur.
/// À attacher sur un GameObject vide "BallReturnMachine".
/// </summary>
public class BallDistributor : MonoBehaviour
{
    [Header("Références")]
    [Tooltip("Le prefab ou l'objet boule à distribuer")]
    public GameObject ballPrefab;

    [Tooltip("Point de départ de l'animation (sous la piste, dans la machine)")]
    public Transform spawnPoint;

    [Tooltip("Point intermédiaire haut de la courbe (sommet de la rampe)")]
    public Transform curveTopPoint;

    [Tooltip("Position finale où la boule attend le joueur")]
    public Transform restPoint;

    [Header("Animation")]
    [Tooltip("Durée de l'animation de retour en secondes")]
    public float animationDuration = 2.5f;

    [Tooltip("Courbe d'animation pour ajuster la vitesse (ease in/out)")]
    public AnimationCurve movementCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    [Header("Audio")]
    [Tooltip("Son mécanique du retour de boule")]
    public AudioSource returnSound;

    [Tooltip("Son quand la boule arrive à destination")]
    public AudioSource arrivalSound;

    // Référence vers la boule active
    private GameObject currentBall;
    private bool isAnimating = false;

    void Start()
    {
        // Distribuer une première boule au démarrage
        DistributeBall();
    }

    /// <summary>
    /// Lance l'animation de distribution d'une nouvelle boule.
    /// Appelée au démarrage et peut être rappelée après chaque lancer.
    /// </summary>
    public void DistributeBall()
    {
        if (isAnimating) return;

        StartCoroutine(AnimateBallReturn());
    }

    /// <summary>
    /// Coroutine qui anime le retour de la boule le long d'une courbe de Bézier quadratique.
    /// </summary>
    private IEnumerator AnimateBallReturn()
    {
        isAnimating = true;

        // Créer ou repositionner la boule
        if (currentBall == null && ballPrefab != null)
        {
            currentBall = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
        }
        else if (currentBall != null)
        {
            currentBall.transform.position = spawnPoint.position;
            currentBall.SetActive(true);
        }

        if (currentBall == null)
        {
            isAnimating = false;
            yield break;
        }

        // Désactiver la physique pendant l'animation
        Rigidbody rb = currentBall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Jouer le son de retour
        if (returnSound != null)
        {
            returnSound.Play();
        }

        // Animation le long de la courbe de Bézier quadratique
        float elapsed = 0f;
        Vector3 p0 = spawnPoint.position;
        Vector3 p1 = curveTopPoint != null ? curveTopPoint.position : (spawnPoint.position + restPoint.position) / 2f + Vector3.up * 0.5f;
        Vector3 p2 = restPoint.position;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / animationDuration);

            // Appliquer la courbe d'animation pour un mouvement plus naturel
            float curvedT = movementCurve.Evaluate(t);

            // Courbe de Bézier quadratique : B(t) = (1-t)²P0 + 2(1-t)tP1 + t²P2
            Vector3 position = QuadraticBezier(p0, p1, p2, curvedT);
            currentBall.transform.position = position;

            // Rotation de la boule pendant le déplacement (effet de roulement)
            currentBall.transform.Rotate(Vector3.right, 360f * Time.deltaTime / animationDuration, Space.World);

            yield return null;
        }

        // Position finale exacte
        currentBall.transform.position = restPoint.position;

        // Réactiver la physique
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Son d'arrivée
        if (arrivalSound != null)
        {
            arrivalSound.Play();
        }

        isAnimating = false;
    }

    /// <summary>
    /// Calcule un point sur une courbe de Bézier quadratique.
    /// </summary>
    private Vector3 QuadraticBezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        float u = 1f - t;
        return u * u * p0 + 2f * u * t * p1 + t * t * p2;
    }

    /// <summary>
    /// Méthode publique pour redistribuer une boule (appelable depuis d'autres scripts ou events).
    /// </summary>
    public void OnBallLost()
    {
        // Petite attente avant de redistribuer
        StartCoroutine(DelayedDistribute(1.5f));
    }

    private IEnumerator DelayedDistribute(float delay)
    {
        yield return new WaitForSeconds(delay);
        DistributeBall();
    }
}
