using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{

    public GameObject deathPanel;
    public TextMeshProUGUI deathText;
    public TextMeshProUGUI subtitleText;
    public Button restartButton;

    //all other UI canvases that should hide when the player dies
    public GameObject[] uiToHideOnDeath;

    public float fadeDuration = 1.5f;

    private CanvasGroup canvasGroup;

    void Awake()
    {

        canvasGroup = deathPanel.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = deathPanel.AddComponent<CanvasGroup>();

        deathPanel.SetActive(false);
        canvasGroup.alpha = 0f;

    }

    void Start()
    {

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartScene);

    }

    public void ShowDeathScreen()
    {

        //hides any hints that are still showing
        HintManager.HideAll();

        //hides all other UI panels assigned in the inspector
        foreach (GameObject ui in uiToHideOnDeath)
        {

            if (ui != null) ui.SetActive(false);

        }

        deathPanel.SetActive(true);
        StartCoroutine(FadeIn());

    }

    private IEnumerator FadeIn()
    {

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {

            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;

        }

        canvasGroup.alpha = 1f;
        Time.timeScale = 0f;

    }

    private void RestartScene()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}