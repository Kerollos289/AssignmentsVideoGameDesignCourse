using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public GameObject loadingScreen;   // assign your loading panel here
    public float loadDelay = 2f;       // how long the loading screen shows before moving levels

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return; // avoid double trigger

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered portal!");
            triggered = true;
            StartCoroutine(LoadNextLevel());
        }
    }

    private System.Collections.IEnumerator LoadNextLevel()
    {
        // Freeze game
        Time.timeScale = 0f;

        // Show loading screen
        if (loadingScreen != null)
            loadingScreen.SetActive(true);

        // Unlock mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Wait (unscaled time because game is frozen)
        yield return new WaitForSecondsRealtime(loadDelay);

        // Restore time
        Time.timeScale = 1f;

        // Load next scene
        //int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        //SceneManager.LoadScene(nextScene);
        SceneManager.LoadScene("Level3");
    }
}
