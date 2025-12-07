//using UnityEngine;
//using System.Collections;

//public class SecondSceneIntro : MonoBehaviour
//{
//    [Header("Cameras")]
//    public Camera cinematicCamera;     // first fly-around camera
//    public Camera cutscene1Camera;     // camera focusing on player
//    public PlayerCameraController playerCam; // to re-enable real gameplay camera

//    [Header("Player")]
//    public PlayerController player;
//    public float rotateSpeed = 3f;

//    void Start()
//    {
//        StartCoroutine(CutsceneSequence());
//    }

//    private IEnumerator CutsceneSequence()
//    {
//        // Disable gameplay
//        player.canControl = false;
//        playerCam.enabled = false;

//        // Unlock cursor (can hide if needed)
//        Cursor.lockState = CursorLockMode.None;
//        Cursor.visible = true;

//        // 1? Start with cinematic camera
//        cinematicCamera.gameObject.SetActive(true);
//        cutscene1Camera.gameObject.SetActive(false);

//        // Optional movement animation (simple rotation)
//        float rotateTime = 2f;
//        float elapsed = 0f;

//        while (elapsed < rotateTime)
//        {
//            elapsed += Time.deltaTime;
//            cinematicCamera.transform.RotateAround(player.transform.position, Vector3.up, 20 * Time.deltaTime);
//            yield return null;
//        }

//        // 2? Switch to cutscene camera 1
//        cinematicCamera.gameObject.SetActive(false);
//        cutscene1Camera.gameObject.SetActive(true);

//        yield return new WaitForSeconds(1f);

//        // 3? Dialogue sequence
//        yield return DialogueSystem.instance.ShowDialogue("What is this place...");
//        yield return DialogueSystem.instance.ShowDialogue("Wasn't I just in a maze?");
//        yield return DialogueSystem.instance.ShowDialogue("What is this anomaly?");

//        // 4? Player turn 180 degrees
//        Quaternion startRot = player.transform.rotation;
//        Quaternion rotated = Quaternion.Euler(0, player.transform.eulerAngles.y + 180f, 0);

//        float t = 0f;
//        while (t < 1f)
//        {
//            t += Time.deltaTime * rotateSpeed;
//            player.transform.rotation = Quaternion.Slerp(startRot, rotated, t);
//            yield return null;
//        }

//        yield return new WaitForSeconds(0.5f);

//        // 5? Turn back to original rotation
//        t = 0f;
//        while (t < 1f)
//        {
//            t += Time.deltaTime * rotateSpeed;
//            player.transform.rotation = Quaternion.Slerp(rotated, startRot, t);
//            yield return null;
//        }

//        yield return new WaitForSeconds(0.5f);

//        // 6? Final dialogue
//        yield return DialogueSystem.instance.ShowDialogue("I must find someone and ask him what is going on...");

//        // 7? Re-enable gameplay
//        cutscene1Camera.gameObject.SetActive(false);
//        playerCam.enabled = true;
//        player.canControl = true;

//        // Lock cursor again
//        Cursor.lockState = CursorLockMode.Locked;
//        Cursor.visible = false;
//    }
//}



using UnityEngine;
using System.Collections;

public class SecondSceneIntro : MonoBehaviour
{
    [Header("Cameras")]
    public Camera cinematicCamera;
    public Camera cutscene1Camera;
    public PlayerCameraController playerCam;

    [Header("Player")]
    public PlayerController player;
    public float rotateSpeed = 3f;

    [Header("UI")]
    public GameObject uiRoot; // assign your UI canvas here

    void Start()
    {
        // Hide UI at scene start
        if (uiRoot != null) uiRoot.SetActive(false);

        StartCoroutine(CutsceneSequence());
    }

    private IEnumerator CutsceneSequence()
    {
        // Disable gameplay
        player.canControl = false;
        playerCam.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // ---- 1. Cinematic camera ----
        cinematicCamera.gameObject.SetActive(true);
        cutscene1Camera.gameObject.SetActive(false);

        float rotateTime = 2f;
        float elapsed = 0f;

        while (elapsed < rotateTime)
        {
            elapsed += Time.deltaTime;
            cinematicCamera.transform.RotateAround(player.transform.position, Vector3.up, 20 * Time.deltaTime);
            yield return null;
        }

        // ---- 2. Switch to cutscene camera ----
        cinematicCamera.gameObject.SetActive(false);
        cutscene1Camera.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        // ---- 3. Dialogue ----
        yield return DialogueSystem.instance.ShowDialogue("What is this place...");
        yield return DialogueSystem.instance.ShowDialogue("Wasn't I just in a maze?");
        yield return DialogueSystem.instance.ShowDialogue("What is this anomaly?");

        // ---- 4. Player rotates ----
        Quaternion startRot = player.transform.rotation;
        Quaternion rotated = Quaternion.Euler(0, player.transform.eulerAngles.y + 180f, 0);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * rotateSpeed;
            player.transform.rotation = Quaternion.Slerp(startRot, rotated, t);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        // ---- 5. Turn back ----
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * rotateSpeed;
            player.transform.rotation = Quaternion.Slerp(rotated, startRot, t);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        // ---- 6. Final dialogue ----
        yield return DialogueSystem.instance.ShowDialogue("I must find someone and ask him what is going on...");

        // ---- 7. Re-enable UI & gameplay ----
        if (uiRoot != null) uiRoot.SetActive(true);

        cutscene1Camera.gameObject.SetActive(false);
        playerCam.enabled = true;
        player.canControl = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        DialogueSystem.instance.group.alpha = 0;

    }
}
