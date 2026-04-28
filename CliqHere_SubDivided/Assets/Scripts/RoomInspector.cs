using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomInspector : MonoBehaviour
{

    public string roomName = "Crew Room";

    public Camera mainOrbitalCamera;
    public Camera roomCamera;

    public GameObject escPromptObject;
    public GameObject roomNameTextObject;

    [Range(0f, 2f)]
    public float transitionDuration = 0.4f;

    private static RoomInspector s_currentRoom;
    private bool _isInspecting = false;

    private MonoBehaviour _orbitController;
    private Collider _myCollider;

    void Awake()
    {

        _myCollider = GetComponent<Collider>();

        if (mainOrbitalCamera != null)
        {

            _orbitController =
                mainOrbitalCamera.GetComponent("OrbitCamera") as MonoBehaviour ??
                mainOrbitalCamera.GetComponent("CameraOrbit") as MonoBehaviour ??
                mainOrbitalCamera.GetComponent("OrbitController") as MonoBehaviour ??
                mainOrbitalCamera.GetComponent("MouseOrbit") as MonoBehaviour;

        }

        SetUIVisible(false);

        if (roomCamera != null)
            roomCamera.gameObject.SetActive(false);

    }

    void Update()
    {

        if (!_isInspecting) return;

        // If a note is open ESC closes the note not the room
        if (ReadableNote.IsNoteOpen) return;

        if ((roomName != "Bridge") && Input.GetKeyDown(KeyCode.Escape))
            ExitRoom();

    }

    void OnMouseDown()
    {

        if (s_currentRoom != null) return;
        EnterRoom();

    }

    public void EnterRoom()
    {

        if (_isInspecting) return;

        _isInspecting = true;
        s_currentRoom = this;

        if (_orbitController != null) _orbitController.enabled = false;

        if (mainOrbitalCamera != null)
        {

            //strip MainCamera tag so OnMouseDown stops using this camera
            mainOrbitalCamera.tag = "Untagged";
            mainOrbitalCamera.enabled = false;

        }

        if (roomCamera != null)
        {

            roomCamera.gameObject.SetActive(true);
            roomCamera.enabled = true;
            //give the room camera the MainCamera tag so clicks work inside the room
            roomCamera.tag = "MainCamera";

        }

        SetUIVisible(true);

        if (roomNameTextObject != null)
        {

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

        if (_myCollider != null) _myCollider.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void ExitRoom()
    {

        if (!_isInspecting) return;

        _isInspecting = false;
        s_currentRoom = null;

        if (roomCamera != null)
        {

            //remove MainCamera tag from room camera before turning it off
            roomCamera.tag = "Untagged";
            roomCamera.enabled = false;
            roomCamera.gameObject.SetActive(false);

        }

        if (mainOrbitalCamera != null)
        {

            //give MainCamera tag back to the orbital camera
            mainOrbitalCamera.tag = "MainCamera";
            mainOrbitalCamera.enabled = true;

        }

        if (_orbitController != null) _orbitController.enabled = true;

        if (_myCollider != null) _myCollider.enabled = true;

        SetUIVisible(false);

    }

    private void SetUIVisible(bool visible)
    {

        if (escPromptObject != null) escPromptObject.SetActive(visible);
        if (roomNameTextObject != null) roomNameTextObject.SetActive(visible);

    }

    void OnDestroy()
    {

        if (_isInspecting) ExitRoom();

    }

}