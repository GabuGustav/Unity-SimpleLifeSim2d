using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class MobileBackManager : MonoBehaviour
{
    public static MobileBackManager Instance;

    // Stack of UI panels
    private Stack<GameObject> panelStack = new Stack<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        bool pressed =
            (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame) ||
            (Keyboard.current != null && Keyboard.current.backspaceKey.wasPressedThisFrame) ||
            (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame);

        if (pressed)
            HandleQuitOrBack();
    }

    // Called by UI to register opening a panel
    public void PushPanel(GameObject panel)
    {
        panelStack.Push(panel);
    }

    // Called by UI to remove panel
    public void PopPanel()
    {
        if (panelStack.Count > 0)
        {
            GameObject top = panelStack.Pop();
            top.SetActive(false);
        }
    }

    private void HandleQuitOrBack()
    {
        // 1. If a panel is open → go back in UI
        if (panelStack.Count > 0)
        {
            PopPanel();
            return;
        }

        // 2. If no panel open → fallback to scene logic
        string scene = SceneManager.GetActiveScene().name;

        if (scene == "GameScene")
        {
            SceneManager.LoadScene("Main Menu");
        }
        else
        {
            Application.Quit();
        }
    }
}
