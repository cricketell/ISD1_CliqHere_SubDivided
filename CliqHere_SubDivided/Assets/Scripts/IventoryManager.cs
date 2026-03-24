using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // the pipe icon in my UI canvas
    public GameObject pipeIconUI;

    // the valve icon in my UI canvas
    public GameObject valveIconUI;

    // remembers what I currently have in my pocket
    private bool _hasPipe = false;
    private bool _hasValve = false;

    void Start()
    {
        // both icons should be hidden at the start
        if (pipeIconUI != null) pipeIconUI.SetActive(false);
        if (valveIconUI != null) valveIconUI.SetActive(false);
    }

    // called when I click on an item in the world
    public void PickUpItem(string itemName)
    {
        if (itemName == "Pipe")
        {
            _hasPipe = true;
            if (pipeIconUI != null) pipeIconUI.SetActive(true);
        }

        if (itemName == "Valve")
        {
            _hasValve = true;
            if (valveIconUI != null) valveIconUI.SetActive(true);
        }
    }

    // crew room wall checks this before it lets me in
    public bool HasPipe()
    {
        return _hasPipe;
    }

    // hatch checks this before it opens
    public bool HasValve()
    {
        return _hasValve;
    }

    // crew room wall calls this after opening so pipe icon disappears
    public void UsePipe()
    {
        _hasPipe = false;
        if (pipeIconUI != null) pipeIconUI.SetActive(false);
    }

    // hatch calls this after opening so valve icon disappears
    public void UseValve()
    {
        _hasValve = false;
        if (valveIconUI != null) valveIconUI.SetActive(false);
    }
}