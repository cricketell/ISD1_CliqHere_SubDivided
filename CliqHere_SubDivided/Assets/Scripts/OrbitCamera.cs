using UnityEngine;

// attach to MainCamera, drag Box010(1) into target slot
public class OrbitCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Orbit")]
    public float orbitSpeed = 5f;

    [Header("Zoom")]
    public float zoomSpeed = 3f;
    public float minDistance = 2f;
    public float maxDistance = 50f;

    private float _yaw;
    private float _pitch;
    private float _distance;

    void Start()
    {
        if (target == null) { Debug.LogWarning("no target set!"); return; }

        // my start position from editor
        transform.position = new Vector3(54f, 41.44f, 42.6f);
        // calculate distance and pitch from position, but force yaw to front
        Vector3 offset = transform.position - target.position;
        _distance = offset.magnitude;
        _yaw = 230f; // hardcode facing front, atan2 was pointing backwards
        _pitch = Mathf.Asin(Mathf.Clamp(offset.y / _distance, -1f, 1f)) * Mathf.Rad2Deg;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // RMB to orbit
        if (Input.GetMouseButton(1))
        {
            _yaw += Input.GetAxis("Mouse X") * orbitSpeed;
            _pitch -= Input.GetAxis("Mouse Y") * orbitSpeed;
            _pitch = Mathf.Clamp(_pitch, -80f, 80f);
        }

        // scroll zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
            _distance = Mathf.Clamp(_distance - scroll * zoomSpeed * _distance * 0.3f, minDistance, maxDistance);

        // move camera
        Quaternion rot = Quaternion.Euler(_pitch, _yaw, 0f);
        transform.position = target.position + rot * new Vector3(0f, 0f, -_distance);
        transform.LookAt(target.position);
    }
}