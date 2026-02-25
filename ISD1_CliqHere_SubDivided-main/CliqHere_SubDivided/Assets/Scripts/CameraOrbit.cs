using UnityEngine;


public class CameraOrbit : MonoBehaviour
{
    [Header("Target to orbit around")]
    // Drag any scene object here — camera will orbit around it
    public Transform orbitTarget;

    [Header("Rotation Speed")]
    // How fast the camera rotates when you move the mouse
    public float rotationSpeed = 4f;

    [Header("Zoom Settings")]
    public float zoomSpeed = 5f;
    public float minZoomDistance = 2f;
    public float maxZoomDistance = 100f;

    // ── Private state ────────────────────────────────────────
    private float currentYaw;
    private float currentPitch;
    private float currentDistance;

    // ─────────────────────────────────────────────────────────
    void Start()
    {
       

        // Starting rotation
        currentPitch = 18.204f;    // X rotation — how much the camera tilts up/down
        currentYaw = -129.412f;  // Y rotation — which direction it faces left/right

        
        // We calculate the distance from this position to the orbit target
        Vector3 startPos = new Vector3(109.3f, 53f, 69.2f);
        Vector3 targetPos = GetTargetPosition();
        currentDistance = Vector3.Distance(startPos, targetPos);

        // Clamp just in case the distance is out of range
        currentDistance = Mathf.Clamp(currentDistance, minZoomDistance, maxZoomDistance);

        // Apply immediately so the camera snaps to the right spot on frame 1
        ApplyCameraPosition();
    }

    // ─────────────────────────────────────────────────────────
    void LateUpdate()
    {
        // LateUpdate is best for cameras — runs after everything else moves
        HandleRotation();
        HandleZoom();
        ApplyCameraPosition();
    }

    // ─────────────────────────────────────────────────────────
    // Rotates the camera while RIGHT MOUSE BUTTON is held
    void HandleRotation()
    {
        if (!Input.GetMouseButton(1)) return; // 1 = right mouse button

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        currentYaw += mouseX * rotationSpeed;
        currentPitch -= mouseY * rotationSpeed;

        // Clamp so the camera can't flip upside down
        currentPitch = Mathf.Clamp(currentPitch, -85f, 85f);
    }

    // ─────────────────────────────────────────────────────────
    // Scroll wheel zooms in and out
    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scroll) > 0.001f)
        {
            currentDistance -= scroll * zoomSpeed;
            currentDistance = Mathf.Clamp(currentDistance, minZoomDistance, maxZoomDistance);
        }
    }

    // ─────────────────────────────────────────────────────────
    // Moves the camera to the correct position and makes it look at the target
    void ApplyCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
        Vector3 targetPos = GetTargetPosition();

        transform.position = targetPos + rotation * new Vector3(0f, 0f, -currentDistance);
        transform.LookAt(targetPos);
    }

    // ─────────────────────────────────────────────────────────
    // Returns the orbit target position, or world zero if nothing is assigned
    Vector3 GetTargetPosition()
    {
        if (orbitTarget != null)
            return orbitTarget.position;

        return Vector3.zero;
    }
}