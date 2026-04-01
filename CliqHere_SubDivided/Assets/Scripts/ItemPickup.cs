using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    public InventoryManager inventoryManager;
    public string itemName = "Pipe";

    void Update()
    {

        if (!Input.GetMouseButtonDown(0)) return;

        Camera cam = Camera.main;

        //if Camera.main is null find any active camera manually
        if (cam == null)
        {
            foreach (Camera c in Camera.allCameras)
            {
                if (c.enabled && c.gameObject.activeInHierarchy)
                {
                    cam = c;
                    break;
                }
            }
        }

        if (cam == null) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, 1000f);

        foreach (RaycastHit hit in hits)
        {

            if (hit.collider.gameObject == gameObject)
            {

                inventoryManager.PickUpItem(itemName);
                gameObject.SetActive(false);
                return;

            }

        }

    }

}