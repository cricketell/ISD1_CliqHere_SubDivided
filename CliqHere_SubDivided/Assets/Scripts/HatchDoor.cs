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

        if (mainOrbitalCamera != null)
        {

            _orbitController =
                mainOrbitalCamera.GetComponent("OrbitCamera") as MonoBehaviour ??
                mainOrbitalCamera.GetComponent("CameraOrbit") as MonoBehaviour ??
                mainOrbitalCamera.GetComponent("OrbitController") as MonoBehaviour ??
                mainOrbitalCamera.GetComponent("MouseOrbit") as MonoBehaviour;

        }

        if (labRoomCamera != null)
            labRoomCamera.gameObject.SetActive(false);

    }

    void Update()
    {

        if (_hintShowing)
        {

            _hintTimer += Time.deltaTime;

            if (_hintTimer >= _hintDuration)
            {

                HideHint();

            }

        }

    }

    void OnMouseDown()
    {

        if (_isOpen) return;

        if (!inventoryManager.HasValve())
        {

            ShowHint();
            return;

        }

        OpenHatch();

    }

    public void HideHint()
    {

        if (needValveUI != null) needValveUI.SetActive(false);
        _hintShowing = false;
        _hintTimer = 0f;

    }

    void ShowHint()
    {

        HintManager.HideAll();

        if (needValveUI != null) needValveUI.SetActive(true);
        _hintShowing = true;
        _hintTimer = 0f;
        HintManager.Register(this);

    }

    void OpenHatch()
    {

        _isOpen = true;

        HideHint();
        inventoryManager.UseValve();
        gameObject.SetActive(false);

        if (labRoomWall != null)
            labRoomWall.SetActive(false);

        if (_orbitController != null) _orbitController.enabled = false;

        if (mainOrbitalCamera != null)
        {

            //remove MainCamera tag so raycasts stop using it
            mainOrbitalCamera.tag = "Untagged";
            mainOrbitalCamera.enabled = false;

        }

        if (labRoomCamera != null)
        {

            labRoomCamera.gameObject.SetActive(true);
            labRoomCamera.enabled = true;
            //give lab camera the MainCamera tag so OnMouseDown works through it
            labRoomCamera.tag = "MainCamera";

        }

    }

}