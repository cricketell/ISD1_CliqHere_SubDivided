using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class AirSystem : MonoBehaviour
{
    [Header("Air Settings")]
    // Total time in seconds before the diver dies. 60 = one minute.
    public float maxAir = 60f;

    [Header("UI References")]
    // The blue bar image — MUST be set to Image Type: Filled in Inspector
    public Image airBarFill;

    // The small "Air" label next to the bar
    public TMP_Text airLabel;

    // Full-screen red image that fades in on death
    public Image deathOverlay;

    // The "press to restart" text shown after death
    public TMP_Text deathText;

    [Header("Death Overlay Settings")]
    // How fast the red screen fades in (higher = faster)
    public float fadeInSpeed = 0.5f;

    // How opaque the red screen gets (0 = invisible, 1 = fully red)
    [Range(0f, 1f)]
    public float maxRedAlpha = 0.75f;

    // Internal tracking — don't touch these
    private float currentAir;
    private bool isDead = false;

    
    void Start()
    {
        // Fill the air tank to max when the game starts
        currentAir = maxAir;

        // Make sure death screen is hidden at start
        SetDeathUIVisible(false);

        // Set the bar to full immediately
        RefreshBar();
    }

    
    void Update()
    {
        // While dead — just show the death screen and wait for input
        if (isDead)
        {
            FadeInRedScreen();
            WaitForRestartInput();
            return;
        }

        // Drain air over time. Time.deltaTime is the time since last frame.
        currentAir -= Time.deltaTime;

        // Make sure air doesn't go below zero
        if (currentAir <= 0f)
        {
            currentAir = 0f;
            Die();
        }

        // Update the bar every frame so it visually shrinks
        RefreshBar();
    }

    
    // Updates the blue fill bar.
    // fillAmount goes from 1.0 (full) to 0.0 (empty) — Unity handles the shrinking.
    void RefreshBar()
    {
        if (airBarFill != null)
        {
            // This is what actually moves the bar — currentAir divided by max gives a 0-1 value
            airBarFill.fillAmount = currentAir / maxAir;
        }

        // Just show "Air"
        if (airLabel != null)
        {
            airLabel.text = "Air";
        }
    }

    
    // Called once when air reaches zero
    void Die()
    {
        isDead = true;
        SetDeathUIVisible(true);
    }

    
    // Gradually makes the red overlay more visible
    void FadeInRedScreen()
    {
        if (deathOverlay == null) return;

        Color c = deathOverlay.color;
        c.a = Mathf.MoveTowards(c.a, maxRedAlpha, fadeInSpeed * Time.deltaTime);
        deathOverlay.color = c;
    }

    
    // Listens for any key or mouse click to restart the game
    void WaitForRestartInput()
    {
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    
    // Shows or hides the death UI. When showing, starts alpha at 0 for smooth fade.
    void SetDeathUIVisible(bool visible)
    {
        if (deathOverlay != null)
        {
            deathOverlay.gameObject.SetActive(visible);

            if (visible)
            {
                // Start fully transparent so FadeInRedScreen can animate it
                Color c = deathOverlay.color;
                c.a = 0f;
                deathOverlay.color = c;
            }
        }

        if (deathText != null)
        {
            deathText.gameObject.SetActive(visible);
        }
    }
}