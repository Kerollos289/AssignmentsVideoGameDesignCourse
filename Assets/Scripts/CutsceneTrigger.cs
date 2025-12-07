using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (other.CompareTag("Player"))
        {
            triggered = true;
            CutsceneManager.instance.StartCutscene();
        }
    }

}
