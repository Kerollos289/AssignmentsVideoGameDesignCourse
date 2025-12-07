using UnityEngine;
using TMPro;

public class Test1 : MonoBehaviour
{
    [Header("Cameras")]
    public Camera badGuyCamera;
    public Camera playerCamera;
    public Camera mainCamera;

    [Header("UI")]
    public TMP_Text dialogueText;
    public GameObject interactionPrompt;
    public GameObject winCanvas;
    public GameObject loseCanvas;
    public PauseMenu2 pauseMenu;


    [Header("Pills")]
    public GameObject redPill;
    public GameObject bluePill;

    [Header("Bad Guy Eyes")]
    public GameObject eye1;
    public GameObject eye2;
    public GameObject eye3;
    public GameObject eye4;

    [Header("Settings")]
    public float dialogueDuration = 3f;

    private bool cutsceneFinished = false;
    private bool pillChosen = false;

    private void Start()
    {
        // Start with cutscene cameras
        badGuyCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(false);

        interactionPrompt.SetActive(false);
        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);

        StartCoroutine(RunCutscene());
    }

    private void SwitchTo(Camera cam)
    {
        badGuyCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(false);

        cam.gameObject.SetActive(true);
    }

    private System.Collections.IEnumerator RunCutscene()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        DialogueSystem.instance.group.alpha = 1;

        // --- Dialogue sequence ---
        yield return ShowLine("I was expecting you", badGuyCamera);
        yield return ShowLine("You gave my brother that pill, you killed him and turned him into a zombie", playerCamera);
        yield return ShowLine("You detectives snoop around things that do not concern you", badGuyCamera);
        yield return ShowLine("You are a murderer.", playerCamera);
        yield return ShowLine("I am just following orders", badGuyCamera);
        yield return ShowLine("Orders from who?", playerCamera);
        yield return ShowLine("HAHA you really think I am going to tell you that", badGuyCamera);
        yield return ShowLine("Let's play red pill, blue pill", badGuyCamera);
        yield return ShowLine("What is that?", playerCamera);
        yield return ShowLine("Two pills. One does nothing, the other kills the consumer.", badGuyCamera);
        yield return ShowLine("What's in it for me?", playerCamera);
        yield return ShowLine("If you win before I take the pill, I'll tell you what you need.", badGuyCamera);
        yield return ShowLine("Deal.", playerCamera);

        // --- Cutscene ends, enable main camera ---
        dialogueText.text = "";
        DialogueSystem.instance.group.alpha = 0;
        SwitchTo(mainCamera);

        cutsceneFinished = true;
        interactionPrompt.SetActive(true);
    }

    private void Update()
    {

        //if (cutsceneFinished && !pillChosen && !pauseMenu.isPaused)
        //{
        //    // Free look
        //    float mouseX = Input.GetAxis("Mouse X");
        //    float mouseY = Input.GetAxis("Mouse Y");

        //    mainCamera.transform.Rotate(-mouseY, mouseX, 0);
        //}

        if (cutsceneFinished && !pillChosen)
        {
            CheckPillSelection();
        }

        if (cutsceneFinished)
        {
            // Free look
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            mainCamera.transform.Rotate(-mouseY, mouseX, 0);
        }
    }

    private void CheckPillSelection()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 15f))
        {
            if (hit.collider.gameObject == redPill || hit.collider.gameObject == bluePill)
            {
                interactionPrompt.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    pillChosen = true;
                    interactionPrompt.SetActive(false);

                    if (hit.collider.gameObject == redPill)
                        StartCoroutine(RedPillSequence());
                    else
                        StartCoroutine(BluePillSequence());
                }
            }
            else
            {
                interactionPrompt.SetActive(false);
            }
        }
        else
        {
            interactionPrompt.SetActive(false);
        }
    }

    private System.Collections.IEnumerator RedPillSequence()
    {
        redPill.SetActive(false);
        bluePill.SetActive(false);
        DialogueSystem.instance.group.alpha = 1;
        // Player dialogue
        yield return ShowLine("Player: Now tell me everything!", mainCamera);

        // Bad guy eye swap
        eye1.SetActive(false);
        eye2.SetActive(false);
        eye3.SetActive(true);
        eye4.SetActive(true);

        // Bad guy dies
        yield return ShowLine("Bad Guy: Yeah... I lied.", mainCamera);

        // Show win canvas
        winCanvas.SetActive(true);

        dialogueText.text = "";
        DialogueSystem.instance.group.alpha = 0;
    }

    private System.Collections.IEnumerator BluePillSequence()
    {
        Debug.Log("Entered in Function");
        redPill.SetActive(false);
        bluePill.SetActive(false);
        DialogueSystem.instance.group.alpha = 1;

        // Lose dialogue
        yield return ShowLine("Player: ...", mainCamera);
        yield return ShowLine("Bad Guy: HAHA, too bad.", mainCamera);

        // Show lose canvas
        DialogueSystem.instance.group.alpha = 0;
        loseCanvas.SetActive(true);

        dialogueText.text = "";
        
    }

    private System.Collections.IEnumerator ShowLine(string line, Camera cam)
    {
        SwitchTo(cam);
        dialogueText.text = line;
        yield return new WaitForSeconds(dialogueDuration);
    }
}
