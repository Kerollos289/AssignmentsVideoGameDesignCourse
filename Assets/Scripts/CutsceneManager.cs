using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager instance;

    [Header("Cameras")]
    public Camera cutsceneCamera;

    [Header("Player")]
    public PlayerController player;
    public PlayerCameraController playerCamController;

    [Header("UI")]
    public GameObject loadingScreen;

    [Header("Zombie")]
    public GameObject zombie;

    [Header("Door")]
    public GameObject door;

    void Awake()
    {
        instance = this;
        cutsceneCamera.gameObject.SetActive(false);
        loadingScreen.SetActive(false);
    }

    public void StartCutscene()
    {
        StartCoroutine(CutsceneSequence());
    }

    private IEnumerator CutsceneSequence()
    {
        Debug.Log("Cutscene started");

        if (zombie != null)
        {
            //Destroy(zombie);
            zombie.SetActive(false);
        }

        // 1? Disable player control
        player.canControl = false;

        // Unlock cursor so UI/dialogue can show
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // 2? Disable all player cameras
        if (playerCamController != null)
        {
            playerCamController.firstPersonCam.gameObject.SetActive(false);
            playerCamController.thirdPersonCam.gameObject.SetActive(false);
            playerCamController.cinematicCam.gameObject.SetActive(false);
        }

        // 3? Activate cutscene camera
        cutsceneCamera.gameObject.SetActive(true);
        cutsceneCamera.transform.parent = null; // detach from player
        

        // 4? Play dialogue sequence
        yield return DialogueSystem.instance.ShowDialogue("What was that...");
        yield return DialogueSystem.instance.ShowDialogue("How what was that maze, and what was that zombie...");
        yield return DialogueSystem.instance.ShowDialogue("How did all this happen...");
        yield return DialogueSystem.instance.ShowDialogue("I must reenter to figure out what is wrong...");
        yield return DialogueSystem.instance.ShowDialogue("I must find out.");

        // 5? Rotate player 180 degrees
        Quaternion targetRot = Quaternion.Euler(0, player.transform.eulerAngles.y + 180f, 0);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRot, t);
            yield return null;
        }

        // 6? Move player forward automatically
        float moveTime = 5f;
        float timer = 0f;
        Rigidbody rb = player.GetComponent<Rigidbody>();
        while (timer < moveTime)
        {
            timer += Time.deltaTime;
            //rb.MovePosition(rb.position + player.transform.forward * 1.5f * Time.deltaTime);
            rb.MovePosition(rb.position + Vector3.left * 0.5f * Time.deltaTime);
            yield return null;
        }
        // close door
        //Animator doorAnim = door.GetComponent<Animator>();
        //doorAnim.SetTrigger("CloseDoor");

        // 7? Loading screen
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(5f);
        //loadingScreen.SetActive(false);
        // 8? Load next scene
        SceneManager.LoadScene("Level2");
    }
}
