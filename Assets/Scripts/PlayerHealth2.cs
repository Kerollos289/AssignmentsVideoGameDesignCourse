using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth2 : MonoBehaviour
{
    public int maxHP = 3;
    public int currentHP;

    [Header("UI")]
    public Image[] hearts;
    public GameObject losePanel;
    public GameObject playerPanel;

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
        // Stop all enemies
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            enemy.SetActive(false);

        // Stop player
        GetComponent<Collider>().enabled = false;
        if (GetComponent<PlayerController>() != null)
            GetComponent<PlayerController>().enabled = false;

        Time.timeScale = 0f;

        // UI changes
        if (playerPanel != null)
            playerPanel.SetActive(false);

        losePanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
