using UnityEngine;

public class CinematicCameraOrbit : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;

    [Header("Orbit Settings")]
    public float rotationSpeed = 20f;   // how fast to orbit
    public float height = 3f;           // how high above the target
    public float distance = 6f;         // how far behind the target
    public bool orbitClockwise = true;  // direction

    private float currentAngle = 0f;

    void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("CinematicCameraOrbit: No target assigned!");
            enabled = false;
            return;
        }

        // Initialize camera position just behind the player
        currentAngle = 0f;
        UpdateCameraPosition();
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Update angle smoothly each frame
        float direction = orbitClockwise ? 1f : -1f;
        currentAngle += rotationSpeed * direction * Time.deltaTime;

        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        // Calculate offset around the player based on currentAngle
        float radians = currentAngle * Mathf.Deg2Rad;

        // Keep camera behind and slightly above player
        Vector3 offset = new Vector3(
            Mathf.Sin(radians) * distance,
            height,
            Mathf.Cos(radians) * distance
        );

        transform.position = target.position + offset;

        // Smoothly look at the target
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3f);
    }
}
