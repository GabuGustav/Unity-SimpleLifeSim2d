using UnityEngine;

public class KeepScreenAwake : MonoBehaviour
{
    private void Awake()
    {
        // Prevent the phone from sleeping while the game is running
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void OnApplicationQuit()
    {
        // Optional: reset to system default when quitting
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
    }
}
