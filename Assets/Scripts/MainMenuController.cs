using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] int firstLevelBuildIndex = 1;

    void Update()
    {
        if (WasConfirmPressed())
            TryStart();
    }

    static bool WasConfirmPressed()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.spaceKey.wasPressedThisFrame
                || keyboard.enterKey.wasPressedThisFrame
                || keyboard.numpadEnterKey.wasPressedThisFrame)
                return true;
        }

        var pad = Gamepad.current;
        if (pad != null && pad.buttonSouth.wasPressedThisFrame)
            return true;

        return false;
    }

    void TryStart()
    {
        if (firstLevelBuildIndex < 0 || firstLevelBuildIndex >= SceneManager.sceneCountInBuildSettings)
            return;
        SceneManager.LoadScene(firstLevelBuildIndex);
    }

    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 420, 90), GUIContent.none);
        GUI.Label(new Rect(20, 20, 400, 28), "Main Menu");
        GUI.Label(new Rect(20, 50, 400, 40), "Space / Enter — or gamepad A / Cross to start (Level 1)");
    }
}
