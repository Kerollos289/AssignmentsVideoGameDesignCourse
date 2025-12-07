using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public PlayerController player; // assign your player in inspector


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape pressed!");
            TogglePause();
        }
    }

    public void TogglePause()
    {
        bool paused = Time.timeScale == 0;

        if (paused) // resume
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (player != null) player.canControl = true; // allow movement
        }
        else // pause
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (player != null) player.canControl = false; // disable movement
        }
    }

    public void ResumeGame() => TogglePause();

    public void Restart()
    {
        Debug.Log("restarting");
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None; // show cursor in menu
        Cursor.visible = true;

        SceneManager.LoadScene("MainMenu");
    }
}
