using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class AirSystem : MonoBehaviour
{
    [Header("Air Settings")]
    // Total time in seconds before the diver dies. 60 = one minute.
    public float maxAir = 60f;

    // How much air is added after surfacing
    public float airRefillAmount = 10f;

    [Header("Surface Settings")]
    // How long the surface screen stays up (seconds)
    public float surfaceDuration = 2f;

    [Header("UI References")]
    // The blue bar image — MUST be set to Image Type: Filled in Inspector
    public Image airBarFill;

    // The small "Air" label next to the bar
    public TMP_Text airLabel;

    // Full-screen red image that fades in on death
    public Image deathOverlay;

    // The "press to restart" text shown after death
    public TMP_Text deathText;

    // Full-screen (or large) image shown when the diver surfaces — assign in Inspector
    public Image surfaceOverlay;

    // Text shown on the surface screen e.g. "Surfacing for air..."
    public TMP_Text surfaceText;

    [Header("Death Overlay Settings")]
    // How fast the red screen fades in (higher = faster)
    public float fadeInSpeed = 0.5f;

    // How opaque the red screen gets (0 = invisible, 1 = fully red)
    [Range(0f, 1f)]
    public float maxRedAlpha = 0.75f;

    // Internal tracking — don't touch these
    private float currentAir;
    private bool isDead = false;
    private bool isSurfacing = false;
    private float surfaceTimer = 0f;


    void Start()
    {
        // Fill the air tank to max when the game starts
        currentAir = maxAir;

        // Make sure death screen and surface screen are hidden at start
        SetDeathUIVisible(false);
        SetSurfaceUIVisible(false);

        // Set the bar to full immediately
        RefreshBar();
    }


    void Update()
    {
        // While dead — show death screen and wait for restart input
        if (isDead)
        {
            FadeInRedScreen();
            WaitForRestartInput();
            return;
        }

        // While surfacing — freeze the timer and count down the surface screen
        if (isSurfacing)
        {
            surfaceTimer -= Time.deltaTime;

            if (surfaceTimer <= 0f)
            {
                // Surface time is up — refill air and return to diving
                currentAir = Mathf.Min(currentAir + airRefillAmount, maxAir);
                isSurfacing = false;
                SetSurfaceUIVisible(false);
                RefreshBar();
            }

            // Don't drain air or accept space input while surfacing
            return;
        }

        // Press Space to surface (only while alive and not already surfacing)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSurfacing = true;
            surfaceTimer = surfaceDuration;
            SetSurfaceUIVisible(true);
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
    void RefreshBar()
    {
        if (airBarFill != null)
        {
            airBarFill.fillAmount = currentAir / maxAir;
        }

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


    // Shows or hides the death UI
    void SetDeathUIVisible(bool visible)
    {
        if (deathOverlay != null)
        {
            deathOverlay.gameObject.SetActive(visible);

            if (visible)
            {
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


    // Shows or hides the surface UI
    void SetSurfaceUIVisible(bool visible)
    {
        if (surfaceOverlay != null)
        {
            surfaceOverlay.gameObject.SetActive(visible);
        }

        if (surfaceText != null)
        {
            surfaceText.gameObject.SetActive(visible);
            if (visible)
            {
                surfaceText.text = "Diving up to the surface...";
            }
        }
    }
}

