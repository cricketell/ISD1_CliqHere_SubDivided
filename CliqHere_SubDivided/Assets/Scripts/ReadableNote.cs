using UnityEngine;
using TMPro;

public class ReadableNote : MonoBehaviour
{
    [Header("Note Content")]
    [TextArea(5, 20)]
    public string noteText;

    [Header("UI References")]
    public GameObject notePanel;
    public TextMeshProUGUI noteTextUI;
    public TextMeshProUGUI escHintText;

    private static ReadableNote _currentOpen;

    public static bool IsNoteOpen => _currentOpen != null;

    void Start()
    {
        if (notePanel != null) notePanel.SetActive(false);
    }

    void Update()
    {
        if (_currentOpen == this && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
            CloseNote();
    }

    void OnMouseDown()
    {
        if (_currentOpen != null && _currentOpen != this)
            _currentOpen.CloseNote();

        OpenNote();
    }

    void OpenNote()
    {
        _currentOpen = this;
        if (notePanel != null) notePanel.SetActive(true);
        if (noteTextUI != null) noteTextUI.text = noteText;
        if (escHintText != null) escHintText.text = "Press ENTER to close";
    }

    public void CloseNote()
    {
        _currentOpen = null;
        if (notePanel != null) notePanel.SetActive(false);
    }
}