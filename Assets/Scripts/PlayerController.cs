using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public bool canControl = true;
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;

    [Header("Camera Settings")]
    public Camera playerCamera;

    [Header("Collider")]
    private float xRotation = 0f;
    private Rigidbody rb;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (!canControl) return;
        LookAround();
    }

    void FixedUpdate()
    {
        if (!canControl) return;
        MovePlayer();
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A/D
        float vertical = Input.GetAxis("Vertical");     // W/S
        vertical *= -1f; // keep your direction flip
        Debug.Log($"Horizontal: {horizontal}, Vertical: {vertical}");

        //Vector3 moveDirection = (transform.forward * vertical + transform.right * horizontal).normalized;
        Vector3 moveDirection = transform.forward * horizontal + transform.right * vertical;
        // Physics-based movement
        Vector3 newPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            // Optional: play sound or add score here
            Debug.Log("Collected: " + other.name);

            Destroy(other.gameObject); // remove the collectible
        }
    }

}
