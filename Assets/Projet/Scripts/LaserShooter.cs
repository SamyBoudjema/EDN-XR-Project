using System.Collections;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(LineRenderer))]
public class LaserShooter : MonoBehaviour
{
    [Header("Laser Settings")]
    public XRNode controllerNode = XRNode.RightHand;
    public float laserRange = 100f;
    public float laserDuration = 0.1f;    // Combien de temps le laser reste visible
    public float fakeSliceForce = 15f;    // Force envoyée au fruit coupé pour séparer les morceaux

    private LineRenderer lineRenderer;
    private bool previousTriggerState = false;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        
        // Ajuster l'épaisseur du laser si ce n'est pas déjà fait dans l'éditeur
        if (lineRenderer.startWidth == 1f)
        {
            lineRenderer.startWidth = 0.02f;
            lineRenderer.endWidth = 0.02f;
        }
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);
        if (device.isValid)
        {
            // Vérifie si la gâchette principale ("Trigger") est pressée
            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue))
            {
                // On s'assure de ne tirer qu'une seule fois au moment de l'appui
                if (triggerValue && !previousTriggerState)
                {
                    ShootLaser();
                }
                previousTriggerState = triggerValue;
            }
        }
    }

    void ShootLaser()
    {
        // Active l'affichage du laser
        StartCoroutine(ShowLaserRoutine());

        RaycastHit hit;
        Vector3 rayStart = transform.position;
        Vector3 rayDir = transform.forward;
        
        // Par défaut, le laser tire jusqu'à sa portée maximale si on ne touche rien
        Vector3 hitPosition = rayStart + rayDir * laserRange;

        if (Physics.Raycast(rayStart, rayDir, out hit, laserRange))
        {
            hitPosition = hit.point;

            FruitTarget fruit = hit.collider.GetComponent<FruitTarget>();
            if (fruit != null)
            {
                // On envoie la direction du tir comme force de coupe avec une force virtuelle élevée
                fruit.Slice(rayDir, hitPosition, rayDir * fakeSliceForce);
            }

            Bomb bomb = hit.collider.GetComponent<Bomb>();
            if (bomb != null)
            {
                bomb.Slice();
            }
        }

        // Met à jour la position de la ligne (départ -> impact)
        lineRenderer.SetPosition(0, rayStart);
        lineRenderer.SetPosition(1, hitPosition);
    }

    private IEnumerator ShowLaserRoutine()
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        lineRenderer.enabled = false;
    }
}
