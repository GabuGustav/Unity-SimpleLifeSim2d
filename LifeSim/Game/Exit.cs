using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MobileBackManager : MonoBehaviour
{
    void Update()
    {
        // Escape on keyboard (PC)
        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            HandleQuitOrBack();
        }

        // Android Back button
        if (Keyboard.current != null &&
            Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            HandleQuitOrBack();
        }

        // Gamepad B / Circle
        if (Gamepad.current != null &&
            Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            HandleQuitOrBack();
        }
    }

    private void HandleQuitOrBack()
    {
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
