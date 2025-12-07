using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseGameManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject playerPanel;   //  Add this in Inspector (your HUD / player UI)

    public PlayerController player;

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

            if (playerPanel != null)
                playerPanel.SetActive(true); // ?? Show player HUD again

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (player != null) player.canControl = true;
        }
        else // pause
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);

            if (playerPanel != null)
                playerPanel.SetActive(false); //  Hide player HUD on pause

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (player != null) player.canControl = false;
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("MainMenu");
    }
}
