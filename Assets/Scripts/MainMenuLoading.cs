using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenuLoading : MonoBehaviour
{
    [Header("UI")]
    public GameObject loadingPanel; // Assign your image/panel in Inspector
    public float loadingDuration = 3f; // seconds

    void Start()
    {
        if (loadingPanel != null)
            loadingPanel.SetActive(false);
    }

    // Call this to start loading Level1
    public void LoadLevel1()
    {
        StartCoroutine(ShowLoadingAndLoad());
    }

    private IEnumerator ShowLoadingAndLoad()
    {
        if (loadingPanel != null)
            loadingPanel.SetActive(true);

        // Wait for the duration
        yield return new WaitForSecondsRealtime(loadingDuration);

        // Load Level1
        SceneManager.LoadScene("Level1");
    }
}
