using UnityEngine;

public class PauseMenu2 : MonoBehaviour
{
    [Header("UI")]
    public GameObject pausePanel;

    [Header("Camera")]
    public Camera mainCamera;

    [Header("Settings")]
    public float mouseSensitivity = 2f;
    public float minY = -80f;
    public float maxY = 80f;

    public bool isPaused = false;
    private float rotX = 0f;
    private float rotY = 0f;

    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 euler = mainCamera.transform.eulerAngles;
        rotX = euler.x;
        rotY = euler.y;
    }

    void Update()
    {
        // Toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();

        // Only rotate camera if not paused
        if (!isPaused)
            RotateCamera();
    }

    void RotateCamera()
    {
        // Read raw mouse input (not affected by Time.timeScale)
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        rotY += mouseX;
        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, minY, maxY);

        mainCamera.transform.rotation = Quaternion.Euler(rotX, rotY, 0f);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // stops physics and animations
            if (pausePanel != null) pausePanel.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f;
            if (pausePanel != null) pausePanel.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ResumeGame()
    {
        if (isPaused)
            TogglePause();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
