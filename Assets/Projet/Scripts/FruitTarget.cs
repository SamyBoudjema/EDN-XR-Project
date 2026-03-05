using UnityEngine;

public class FruitTarget : MonoBehaviour
{
    public string ingredientName = "Pomme";
    public int scoreValue = 10;
    public GameObject slicedPrefab; // Prefab with both halves
    public float separationForce = 5f;

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

        if (slicedPrefab != null)
        {
            GameObject slicedObj = Instantiate(slicedPrefab, transform.position, transform.rotation);
            
            Rigidbody[] rbs = slicedObj.GetComponentsInChildren<Rigidbody>();
            
            // To properly split objects, we determine which side of the local center they are on
            foreach (Rigidbody rb in rbs)
            {
                Vector3 direction = (rb.transform.position - transform.position).normalized;
                if (direction == Vector3.zero) direction = Random.insideUnitSphere.normalized;
                
                rb.AddForce(direction * separationForce, ForceMode.Impulse);
                rb.AddTorque(Random.insideUnitSphere * separationForce, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }
}
