using UnityEngine;

public class Slicer : MonoBehaviour
{
    public float minVelocityForSlice = 2.0f;
    private Vector3 previousPosition;
    private Vector3 currentVelocity;

    void Start()
    {
        previousPosition = transform.position;
    }

    void Update()
    {
        Vector3 newVelocity = (transform.position - previousPosition) / Time.deltaTime;
        currentVelocity = Vector3.Lerp(currentVelocity, newVelocity, 0.5f); // Smoothing
        previousPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentVelocity.magnitude >= minVelocityForSlice)
        {
            Vector3 sliceDirection = currentVelocity.normalized;
            Vector3 contactPoint = other.ClosestPoint(transform.position);

            FruitTarget fruit = other.GetComponent<FruitTarget>();
            if (fruit != null)
            {
                fruit.Slice(sliceDirection, contactPoint, currentVelocity);
            }

            Bomb bomb = other.GetComponent<Bomb>();
            if (bomb != null)
            {
                bomb.Slice();
            }
        }
    }
}
