using UnityEngine;

public class BridgeRoom : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public RoomInspector roomInspector;
    public GameObject crewRoomText;
    public GameObject bridgeText;
    public GameObject escText;

    //public GameObject needValveUI;

    public Camera bridgeRoomCamera;
    public Camera mainOrbitalCamera;

    private MonoBehaviour _orbitController;

    private bool _isOpen = false;

    private float _hintTimer = 0f;
    private float _hintDuration = 5f;
    private bool _hintShowing = false;

    Animator animator;

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

        if (bridgeRoomCamera != null)
            bridgeRoomCamera.gameObject.SetActive(false);

    }

    void Start()
    {

        animator = GetComponent<Animator>();

    }

    void Update()
    {

        /*if (_hintShowing)
        {

            _hintTimer += Time.deltaTime;

            if (_hintTimer >= _hintDuration)
            {

                HideHint();

            }

        }*/

    }

    void OnMouseDown()
    {

        if (_isOpen) return;

        if (!inventoryManager.HasKey())
        {

            //ShowHint();
            return;

        }

        HatchAnimation();

    }

    /*public void HideHint()
    {

        if (needValveUI != null) needValveUI.SetActive(false);
        _hintShowing = false;
        _hintTimer = 0f;

    }*/

    /*void ShowHint()
    {

        HintManager.HideAll();

        if (needValveUI != null) needValveUI.SetActive(true);
        _hintShowing = true;
        _hintTimer = 0f;
        HintManager.Register(this);

    }*/

    void OpenHatch()
    {

        _isOpen = true;

        //HideHint();
        inventoryManager.UseKey();

        roomInspector.roomName = "Bridge";

        crewRoomText.SetActive(false);
        bridgeText.SetActive(true);
        escText.SetActive(false);

        if (_orbitController != null) _orbitController.enabled = false;

        if (mainOrbitalCamera != null)
        {

            //remove MainCamera tag so raycasts stop using it
            mainOrbitalCamera.tag = "Untagged";
            mainOrbitalCamera.enabled = false;

        }

        if (bridgeRoomCamera != null)
        {

            bridgeRoomCamera.gameObject.SetActive(true);
            bridgeRoomCamera.enabled = true;
            //give bridge camera the MainCamera tag so OnMouseDown works through it
            bridgeRoomCamera.tag = "MainCamera";

        }

    }

    void HatchAnimation()
    {

        animator.SetBool("IsOpen", true);

        Invoke("OpenHatch", 2.1f);

    }
}
