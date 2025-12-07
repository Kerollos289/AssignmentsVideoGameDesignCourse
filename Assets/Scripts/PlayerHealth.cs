using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 3;
    public int currentHP;

    [Header("UI")]
    public Image[] hearts; // assign 3 heart images
    public GameObject losePanel; // canvas panel that appears on death

    void Start()
    {
        currentHP = maxHP;
        UpdateHeartsUI();
        losePanel.SetActive(false);
    }

    public void TakeDamage()
    {
        currentHP--;

        UpdateHeartsUI();

        if (currentHP <= 0)
            Die();
    }

    public void Heal()
    {
        if (currentHP < maxHP)
        {
            currentHP++;
            UpdateHeartsUI();
        }
    }

    void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
            hearts[i].enabled = (i < currentHP);
    }

    void Die()
    {
        Time.timeScale = 0f;
        losePanel.SetActive(true);

        // Unlock and show mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // called by UI buttons
    public void RestartLevel()
    {
        Debug.Log("RestartLevel() was called!");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
