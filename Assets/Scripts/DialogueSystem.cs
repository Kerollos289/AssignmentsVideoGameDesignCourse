using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;

    public TextMeshProUGUI dialogueText;
    public CanvasGroup group;

    void Awake()
    {
        instance = this;
        group.alpha = 0;  // Only hide at start
    }

    public IEnumerator ShowDialogue(string message)
    {
        Debug.Log("Dialogue triggered: " + message);

        // Show UI
        group.alpha = 1;
        dialogueText.text = "";

        foreach (char c in message)
        {
            dialogueText.text += c;
            yield return new WaitForSecondsRealtime(0.03f);
        }

        yield return new WaitForSecondsRealtime(1.2f);

        // Hide UI
        //group.alpha = 0;
    }
}
