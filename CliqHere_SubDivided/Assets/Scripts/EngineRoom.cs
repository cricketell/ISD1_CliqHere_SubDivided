using UnityEngine;

public class EngineRoomWall : MonoBehaviour
{

    public InventoryManager inventoryManager;

    public GameObject needLaserCutterUI;

    private bool _isOpen = false;

    private float _hintTimer = 0f;
    private float _hintDuration = 5f;
    private bool _hintShowing = false;

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

        if (!inventoryManager.HasGun())
        {

            ShowHint();
            return;

        }

        OpenWall();

    }

    public void HideHint()
    {

        if (needLaserCutterUI != null) needLaserCutterUI.SetActive(false);
        _hintShowing = false;
        _hintTimer = 0f;

    }

    void ShowHint()
    {

        //hides any other hint that might already be showing
        HintManager.HideAll();

        if (needLaserCutterUI != null) needLaserCutterUI.SetActive(true);
        _hintShowing = true;
        _hintTimer = 0f;
        HintManager.Register(this);

    }

    void OpenWall()
    {

        _isOpen = true;

        HideHint();
        inventoryManager.UseGun();
        gameObject.SetActive(false);

    }

}