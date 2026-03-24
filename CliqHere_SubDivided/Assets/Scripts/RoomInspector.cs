using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomInspector : MonoBehaviour
{
    [Header("Room Identity")]
    [Tooltip("Friendly name shown in the UI when entering this room.")]
    public string roomName = "Crew Room";

    [Header("Cameras")]
    [Tooltip("The main orbital camera GameObject (will be disabled while inspecting).")]
    public Camera mainOrbitalCamera;

    [Tooltip("The fixed inspection camera for THIS room.")]
    public Camera roomCamera;

    [Header("UI")]
    [Tooltip("The GameObject that contains the 'Press ESC to exit' prompt.")]
    public GameObject escPromptObject;

    [Tooltip("(Optional) TextMeshProUGUI or Text that shows the room name.")]
    public GameObject roomNameTextObject;

    [Header("Transition")]
    [Tooltip("How fast the camera blends in (set to 0 for instant).")]
    [Range(0f, 2f)]
    public float transitionDuration = 0.4f;

    // this keeps track of which room is currently open so we cant open two at once
    private static RoomInspector s_currentRoom;
    private bool _isInspecting = false;
    private float _transitionTimer = 0f;

    // this stores the orbit script so we can turn it on and off
    private MonoBehaviour _orbitController;

    // the collider on this empty object so we can disable it while inside the room
    private Collider _myCollider;

    void Awake()
    {
        // grab the collider on this object so we can turn it off when inside the room
        _myCollider = GetComponent<Collider>();

        // here we look for the orbit camera script by trying different common names
        // if your script has a different name just add it to this list
        if (mainOrbitalCamera != null)
        {
            _orbitController =
                mainOrbitalCamera.GetComponent("OrbitCamera") as MonoBehaviour ??
                mainOrbitalCamera.GetComponent("CameraOrbit") as MonoBehaviour ??
                mainOrbitalCamera.GetComponent("OrbitController") as MonoBehaviour ??
                mainOrbitalCamera.GetComponent("MouseOrbit") as MonoBehaviour;
        }

        // make sure the UI and room camera are hidden when the game starts
        SetUIVisible(false);

        if (roomCamera != null)
            roomCamera.gameObject.SetActive(false);
    }

    void Update()
    {
        // no need to check anything if we are not inside a room
        if (!_isInspecting) return;

        // player presses ESC so we leave the room
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitRoom();
        }
    }

    // this fires when the player clicks on the trigger collider of this room
    void OnMouseDown()
    {
        // if the player is already inside a room do nothing
        if (s_currentRoom != null) return;

        EnterRoom();
    }

    public void EnterRoom()
    {
        if (_isInspecting) return;

        _isInspecting = true;
        s_currentRoom = this;

        // turn off the orbital camera so it stops rotating around the sub
        if (_orbitController != null) _orbitController.enabled = false;
        if (mainOrbitalCamera != null) mainOrbitalCamera.enabled = false;

        // turn on the fixed room camera so we see inside the room
        if (roomCamera != null)
        {
            roomCamera.gameObject.SetActive(true);
            roomCamera.enabled = true;
        }

        // show the ESC prompt and room name on screen
        SetUIVisible(true);

        // update the room name text on the UI to match this room
        if (roomNameTextObject != null)
        {
            // we check for TextMeshPro first and fall back to the old Unity Text
            var tmp = roomNameTextObject.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                tmp.text = roomName;
            }
            else
            {
                var legacyText = roomNameTextObject.GetComponent<Text>();
                if (legacyText != null) legacyText.text = roomName;
            }
        }

        // disable the trigger collider so the hatch can be clicked without this blocking it
        if (_myCollider != null) _myCollider.enabled = false;

        // make sure the cursor is visible so the player can see where they click
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log($"[RoomInspector] Entered room: {roomName}");
    }

    public void ExitRoom()
    {
        if (!_isInspecting) return;

        _isInspecting = false;
        s_currentRoom = null;

        // hide the room camera since we are going back to the main view
        if (roomCamera != null)
        {
            roomCamera.enabled = false;
            roomCamera.gameObject.SetActive(false);
        }

        // bring the orbital camera back so the player can rotate around the sub again
        if (mainOrbitalCamera != null) mainOrbitalCamera.enabled = true;
        if (_orbitController != null) _orbitController.enabled = true;

        // bring the collider back so the room can be clicked again next time
        if (_myCollider != null) _myCollider.enabled = true;

        // hide the UI since we left the room
        SetUIVisible(false);

        Debug.Log($"[RoomInspector] Exited room: {roomName}");
    }

    // this just turns the ESC prompt and room name on or off together
    private void SetUIVisible(bool visible)
    {
        if (escPromptObject != null) escPromptObject.SetActive(visible);
        if (roomNameTextObject != null) roomNameTextObject.SetActive(visible);
    }

    // if something destroys this object while we are inside the room exit cleanly
    void OnDestroy()
    {
        if (_isInspecting) ExitRoom();
    }
}