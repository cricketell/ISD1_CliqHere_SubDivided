using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // the inventory script that keeps track of what I have
    public InventoryManager inventoryManager;

    // set this to "Pipe" or "Valve" depending on which item this is
    public string itemName = "Pipe";

    void OnMouseDown()
    {
        // tell the inventory I picked this up
        inventoryManager.PickUpItem(itemName);

        // remove the item from the world since I grabbed it
        gameObject.SetActive(false);
    }
}