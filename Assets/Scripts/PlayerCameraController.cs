using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [Header("Cameras")]
    public Camera firstPersonCam;
    public Camera thirdPersonCam;
    public Camera cinematicCam;

    [Header("Settings")]
    public KeyCode switchToFirstPersonKey = KeyCode.Alpha1;
    public KeyCode switchToThirdPersonKey = KeyCode.Alpha2;
    public KeyCode switchToCinematicKey = KeyCode.Alpha3;

    void Start()
    {
        // Start in first-person view
        SetActiveCamera(firstPersonCam);
    }

    void Update()
    {
        if (Input.GetKeyDown(switchToFirstPersonKey))
            SetActiveCamera(firstPersonCam);
        else if (Input.GetKeyDown(switchToThirdPersonKey))
            SetActiveCamera(thirdPersonCam);
        else if (Input.GetKeyDown(switchToCinematicKey))
            SetActiveCamera(cinematicCam);
    }

    //void SetActiveCamera(Camera activeCam)
    //{
    //    firstPersonCam.gameObject.SetActive(false);
    //    thirdPersonCam.gameObject.SetActive(false);
    //    cinematicCam.gameObject.SetActive(false);

    //    activeCam.gameObject.SetActive(true);
    //}
    void SetActiveCamera(Camera activeCam)
    {
        firstPersonCam.gameObject.SetActive(false);
        thirdPersonCam.gameObject.SetActive(false);
        cinematicCam.gameObject.SetActive(false);

        activeCam.gameObject.SetActive(true);

        // Handle player control toggle
        PlayerController player = GetComponent<PlayerController>();
        if (player != null)
        {
            // Disable control only in cinematic view
            player.canControl = (activeCam != cinematicCam);
        }
    }

}
