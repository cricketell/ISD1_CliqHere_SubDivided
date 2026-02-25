using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject cameraToControl;
    public GameObject cameraToDisable;

    private void OnMouseDown()
    {

        cameraToControl.SetActive(true);
        cameraToDisable.SetActive(false);

        Invoke("ReturnMainCamera", 2);

    }

    private void ReturnMainCamera()
    {

        cameraToControl.SetActive(false);
        cameraToDisable.SetActive(true);

    }

}
