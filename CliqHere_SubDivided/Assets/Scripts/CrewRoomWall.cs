using UnityEngine;

public class CrewRoomWall : MonoBehaviour
{
   
    public InventoryManager inventoryManager;

    
    public GameObject needPipeUI;

    
    private bool _isOpen = false;

    
    private float _hintTimer = 0f;

    
    private float _hintDuration = 5f;

    
    private bool _hintShowing = false;

    void Update()
    {
        // if the hint is on screen count down and hide it after 5 seconds
        if (_hintShowing)
        {
            _hintTimer += Time.deltaTime;
            if (_hintTimer >= _hintDuration)
            {
                if (needPipeUI != null) needPipeUI.SetActive(false);
                _hintShowing = false;
                _hintTimer = 0f;
            }
        }
    }

    void OnMouseDown()
    {
        // wall is already gone so do nothing
        if (_isOpen) return;

        // I dont have the pipe yet so show the hint
        if (!inventoryManager.HasPipe())
        {
            ShowHint();
            return;
        }

        OpenWall();
    }

    void ShowHint()
    {
        // show the "you need a pipe" message and start the timer
        if (needPipeUI != null) needPipeUI.SetActive(true);
        _hintShowing = true;
        _hintTimer = 0f;
    }

    void OpenWall()
    {
        _isOpen = true;

        // hide the hint if it was showing when I opened the wall
        if (needPipeUI != null) needPipeUI.SetActive(false);

        // remove the pipe from inventory and hide its icon
        inventoryManager.UsePipe();

        // make the wall disappear
        gameObject.SetActive(false);
    }
}