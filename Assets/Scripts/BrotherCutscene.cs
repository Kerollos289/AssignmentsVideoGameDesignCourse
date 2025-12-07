
//using System.Collections;
//using UnityEngine;
//using TMPro;

//public class BrotherCutscene : MonoBehaviour
//{
//    [Header("References")]
//    public Transform player;
//    public Transform brother;

//    public GameObject zombie;
//    public GameObject portal;

//    [Header("Brother Eyes")]
//    public GameObject eye1;
//    public GameObject eye2;
//    public GameObject eye3;
//    public GameObject eye4;

//    [Header("UI")]
//    public TMP_Text dialogueText;
//    public TMP_Text objectiveText;

//    [Header("Settings")]
//    public float triggerDistance = 3f;

//    private bool cutsceneStarted = false;

//    void Update()
//    {
//        if (!cutsceneStarted)
//        {
//            float dist = Vector3.Distance(player.position, brother.position);
//            if (dist <= triggerDistance)
//            {
//                StartCoroutine(StartCutscene());
//                cutsceneStarted = true;
//            }
//        }
//    }

//    IEnumerator StartCutscene()
//    {
//        // Make sure dialogue UI is visible again
//        DialogueSystem.instance.group.alpha = 1;

//        // Freeze player
//        PlayerController pm = player.GetComponent<PlayerController>();
//        if (pm != null) pm.enabled = false;

//        // Make brother face the player (with rotation offset fix)
//        Vector3 dir = (player.position - brother.position).normalized;
//        dir.y = 0;
//        brother.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(0, 90f, -45);

//        // ---- Dialogue Sequence ----
//        yield return ShowDialogue("Player: Hey, long time no see. What happened to you?", 3f);
//        yield return ShowDialogue("Brother: Hey... I don't feel so good.", 3f);
//        yield return ShowDialogue("Brother: I don't know if the one behind this is a magician, high-tech, or something else...", 4f);
//        yield return ShowDialogue("Player: A detective went missing... I didn’t think it was you. Come on, I'll help us escape!", 4f);
//        yield return ShowDialogue("Brother: It is no use... I think my time has come...", 3f);
//        yield return ShowDialogue("Player: NOO!", 2.5f);

//        // --- Switch Eyes to X Eyes ---
//        eye1.SetActive(false);
//        eye2.SetActive(false);
//        eye3.SetActive(true);
//        eye4.SetActive(true);

//        yield return ShowDialogue("Player: I can't believe you died...", 3f);

//        // --- Brother disappears, zombie appears ---
//        //brother.gameObject.SetActive(false);
//        //Debug.Log("test1 started");
//        //zombie.SetActive(true);
//        //Debug.Log("test2");
//        //yield return ShowDialogue("Player: Oh no, he turned into a zombie! I have to get out of here!", 3f);
//        dialogueText.gameObject.SetActive(true);
//        dialogueText.transform.parent.gameObject.SetActive(true);
//        DialogueSystem.instance.group.alpha = 1;
//        brother.gameObject.SetActive(false);
//        zombie.SetActive(true);
//        Debug.Log("test2");

//        yield return ShowDialogue("Player: Oh no, he turned into a zombie! I have to get out of here!", 3f);


//        // --- Activate portal ---
//        portal.SetActive(true);

//        // --- Update objective ---
//        if (objectiveText != null)
//            objectiveText.text = "Find the portal and escape!";

//        // Unfreeze player
//        if (pm != null) pm.enabled = true;

//        // Hide dialogue text
//        dialogueText.text = "";
//        DialogueSystem.instance.group.alpha = 0;
//    }

//    IEnumerator ShowDialogue(string text, float duration)
//    {
//        if (dialogueText != null)
//            dialogueText.text = text;

//        yield return new WaitForSeconds(duration);
//    }
//}



using System.Collections;
using UnityEngine;
using TMPro;

public class BrotherCutscene : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform brother;

    public GameObject zombie;
    public GameObject portal;

    [Header("Brother Eyes")]
    public GameObject eye1;
    public GameObject eye2;
    public GameObject eye3;
    public GameObject eye4;

    [Header("UI")]
    public TMP_Text dialogueText;
    public TMP_Text objectiveText;

    [Header("Settings")]
    public float triggerDistance = 3f;

    private bool cutsceneStarted = false;

    void Update()
    {
        if (!cutsceneStarted)
        {
            float dist = Vector3.Distance(player.position, brother.position);
            if (dist <= triggerDistance)
            {
                cutsceneStarted = true;
                StartCoroutine(StartCutscene());
            }
        }
    }

    IEnumerator StartCutscene()
    {
        // Ensure dialogue UI is visible
        DialogueSystem.instance.group.alpha = 1;

        // Freeze player movement
        PlayerController pm = player.GetComponent<PlayerController>();
        if (pm != null) pm.enabled = false;

        // Brother faces player
        Vector3 dir = (player.position - brother.position).normalized;
        dir.y = 0;
        brother.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(0, 90f, -45);

        // ---- Dialogue Sequence ----
        yield return ShowDialogue("Player: Hey, long time no see. What happened to you?", 3f);
        yield return ShowDialogue("Brother: Hey... I don't feel so good.", 3f);
        yield return ShowDialogue("Brother: I don't know if the one behind this is a magician, high-tech, or something else...", 4f);
        yield return ShowDialogue("Player: A detective went missing... I didn’t think it was you. Come on, I'll help us escape!", 4f);
        yield return ShowDialogue("Brother: It is no use... I think my time has come...", 3f);
        yield return ShowDialogue("Player: NOO!", 2.5f);

        // --- Switch Eyes to X Eyes ---
        eye1.SetActive(false);
        eye2.SetActive(false);
        eye3.SetActive(true);
        eye4.SetActive(true);

        yield return ShowDialogue("Player: I can't believe you died...", 3f);

        // --- Brother disappears, zombie appears (idle) ---
        brother.gameObject.SetActive(false);
        zombie.SetActive(true);

        // ensure the zombie AI stays idle during the cutscene
        ZombieAI2 ai = zombie.GetComponent<ZombieAI2>();
        if (ai != null)
            ai.cutsceneActive = true;

        yield return ShowDialogue("Player: Oh no, he turned into a zombie! I have to get out of here!", 3f);

        // --- Activate portal ---
        portal.SetActive(true);

        // --- Update objective ---
        if (objectiveText != null)
            objectiveText.text = "Find the portal and escape!";

        // --- End cutscene, zombie starts chasing ---
        if (ai != null)
            ai.cutsceneActive = false;

        // Unfreeze player
        if (pm != null) pm.enabled = true;

        // Hide dialogue
        dialogueText.text = "";
        DialogueSystem.instance.group.alpha = 0;
    }

    IEnumerator ShowDialogue(string text, float duration)
    {
        if (dialogueText != null)
            dialogueText.text = text;

        yield return new WaitForSeconds(duration);
    }
}
