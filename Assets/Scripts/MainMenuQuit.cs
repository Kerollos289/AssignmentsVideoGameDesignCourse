using UnityEngine;

public class MainMenuQuit : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in editor
#else
        Application.Quit(); // Quits the game in build
#endif
    }
}
