using UnityEngine;

public class HatchDoor : MonoBehaviour
{
   
    public InventoryManager inventoryManager;

   
    public GameObject needValveUI;

   
    public Camera labRoomCamera;

    
    public Camera mainOrbitalCamera;

    
    public GameObject labRoomWall;

    
    private MonoBehaviour _orbitController;

    
    private bool _isOpen = false;

    
    private float _hintTimer = 0f;

    
    private float _hintDuration = 5f;

    
    private bool _hintShowing = false;

    void Awake()
    {
        // look for the orbit script on the main camera by common names
        if (mainOrbitalCamera != null)
        {
            _orbitController =
                mainOrbitalCamera.GetComponent("OrbitCamera") as MonoBehaviour ??
                mainOrbitalCamera.GetComponent("CameraOrbit") as MonoBehaviour ??
                mainOrbitalCamera.GetComponent("OrbitController") as MonoBehaviour ??
                mainOrbitalCamera.GetComponent("MouseOrbit") as MonoBehaviour;
        }

        // make sure the lab camera is off at the start
        if (labRoomCamera != null)
            labRoomCamera.gameObject.SetActive(false);
    }

    void Update()
    {
        // count down and hide the hint after 5 seconds
        if (_hintShowing)
        {
            _hintTimer += Time.deltaTime;
            if (_hintTimer >= _hintDuration)
            {
                if (needValveUI != null) needValveUI.SetActive(false);
                _hintShowing = false;
                _hintTimer = 0f;
            }
        }
    }

    void OnMouseDown()
    {
        // hatch already open so do nothing
        if (_isOpen) return;

        // I dont have the valve so show the hint
        if (!inventoryManager.HasValve())
        {
            ShowHint();
            return;
        }   

        OpenHatch();
    }

    void ShowHint()
    {
        // show the "find a valve" message and start the countdown
        if (needValveUI != null) needValveUI.SetActive(true);
        _hintShowing = true;
        _hintTimer = 0f;
    }

    void OpenHatch()
    {
        _isOpen = true;

        // hide the hint if it was still showing
        if (needValveUI != null) needValveUI.SetActive(false);

        // remove the valve from inventory and hide its icon
        inventoryManager.UseValve();

        // hide the hatch itself
        gameObject.SetActive(false);

        // the lab wall disappears automatically when the hatch opens
        if (labRoomWall != null)
            labRoomWall.SetActive(false);

        // switch off the orbital camera and turn on the lab room camera
        if (_orbitController != null) _orbitController.enabled = false;
        if (mainOrbitalCamera != null) mainOrbitalCamera.enabled = false;

        if (labRoomCamera != null)
        {
            labRoomCamera.gameObject.SetActive(true);
            labRoomCamera.enabled = true;
        }
    }
}