using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainPanel;
    public GameObject settingsPanel;
    public GameObject exitConfirmPanel;

    private InputAction backAction;

    private void Awake()
    {
        // Setup Back input
        backAction = new InputAction(type: InputActionType.Button);
        backAction.AddBinding("<Keyboard>/escape");
        backAction.AddBinding("<Gamepad>/b");

        backAction.performed += OnBackPressed;
    }

    private void OnEnable()
    {
        backAction.Enable();
    }

    private void OnDisable()
    {
        backAction.Disable();
    }

    private void OnDestroy()
    {
        // SAFE removal — using a method, not a lambda
        backAction.performed -= OnBackPressed;

        // NO need to dispose — Unity handles it when GameObject dies
        // backAction.Dispose(); ← REMOVED
    }

    private void Start()
    {
        ShowMainMenu();
    }

    // ---------------------------------------------------
    // PANEL MANAGEMENT
    // ---------------------------------------------------
    private void ShowMainMenu()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
        exitConfirmPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
        exitConfirmPanel.SetActive(false);
    }

    public void OpenExitConfirm()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(false);
        exitConfirmPanel.SetActive(true);
    }

    // ---------------------------------------------------
    // BUTTON FUNCTIONS
    // ---------------------------------------------------
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnExitPressed()
    {
        OpenExitConfirm();
    }

    public void ConfirmExitYes()
    {
        Application.Quit();
    }

    public void ConfirmExitNo()
    {
        ShowMainMenu();
    }

    // ---------------------------------------------------
    // BACK BUTTON LOGIC
    // ---------------------------------------------------
    private void OnBackPressed(InputAction.CallbackContext ctx)
    {
        HandleBack();
    }

    private void HandleBack()
    {
        if (settingsPanel.activeSelf)
        {
            ShowMainMenu();
            return;
        }

        if (exitConfirmPanel.activeSelf)
        {
            ShowMainMenu();
            return;
        }

        OpenExitConfirm();
    }
}
