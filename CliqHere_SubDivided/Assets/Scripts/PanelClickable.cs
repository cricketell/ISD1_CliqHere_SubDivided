using UnityEngine;

public class PanelClickable : MonoBehaviour
{
    // Drag the PanelManager object here
    [SerializeField] private SequentialPanelInteraction manager;
    [SerializeField] private bool isTopPanel = true;

    void OnMouseDown()
    {
        if (isTopPanel)
            manager.OnTopPanelClicked();
        else
            manager.OnBottomPanelClicked();
    }
}