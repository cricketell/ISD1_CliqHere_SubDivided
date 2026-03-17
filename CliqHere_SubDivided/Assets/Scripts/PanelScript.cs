using System.Collections;
using UnityEngine;

public class SequentialPanelInteraction : MonoBehaviour
{
    [Header("Panels - Assign in Order")]
    [SerializeField] private GameObject topPanel;
    [SerializeField] private GameObject bottomPanel;

    [Header("Settings")]
    [SerializeField] private float fadeSpeed = 2f; // set to 0 for instant disappear

    private bool topPanelRemoved = false;

    void Start()
    {
        // Make sure bottom panel is not interactable at start
        // (optional visual cue - you can tint it)
        SetBottomPanelLocked(true);
    }

    // Call this from the TOP panel's OnClick or attach to it
    public void OnTopPanelClicked()
    {
        if (topPanelRemoved) return;

        topPanelRemoved = true;
        StartCoroutine(FadeOutAndDisable(topPanel));

        // Unlock the bottom panel
        SetBottomPanelLocked(false);
    }

    // Call this from the BOTTOM panel's OnClick or attach to it
    public void OnBottomPanelClicked()
    {
        if (!topPanelRemoved)
        {
            Debug.Log("Press the top panel first!");
            return;
        }

        StartCoroutine(FadeOutAndDisable(bottomPanel));
    }

    private void SetBottomPanelLocked(bool locked)
    {
        // Optional: visually indicate locked state by tinting
        Renderer rend = bottomPanel.GetComponent<Renderer>();
        if (rend != null)
        {
            Color c = rend.material.color;
            c.a = locked ? 0.4f : 1f;
            rend.material.color = c;
        }
    }

    private IEnumerator FadeOutAndDisable(GameObject panel)
    {
        Renderer rend = panel.GetComponent<Renderer>();

        if (rend != null && fadeSpeed > 0)
        {
            Color c = rend.material.color;
            while (c.a > 0f)
            {
                c.a -= Time.deltaTime * fadeSpeed;
                rend.material.color = c;
                yield return null;
            }
        }

        panel.SetActive(false);
    }
}