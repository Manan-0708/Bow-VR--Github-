using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseInputListener : MonoBehaviour
{
    [SerializeField] private InputActionReference leftMenuButton; // bind to left controller "menu" action
    private bool wasPressed = false;
    private const string PauseSceneName = "EscapeScene";

    private void OnEnable()
    {
        if (leftMenuButton != null)
            leftMenuButton.action.Enable();
    }

    private void OnDisable()
    {
        if (leftMenuButton != null)
            leftMenuButton.action.Disable();
    }

    private void Update()
    {
        // Editor quick test
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
            return;
        }

        if (leftMenuButton == null) return;

        bool isPressed = leftMenuButton.action.ReadValue<float>() > 0.5f;

        if (isPressed && !wasPressed)
            TogglePauseMenu();

        wasPressed = isPressed;
    }

    private void TogglePauseMenu()
    {
        if (IsPauseSceneLoaded())
            StartCoroutine(UnloadPauseScene());
        else
            StartCoroutine(LoadPauseScene());
    }

    private bool IsPauseSceneLoaded()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
            if (SceneManager.GetSceneAt(i).name == PauseSceneName)
                return true;
        return false;
    }

    private IEnumerator LoadPauseScene()
    {
        // freeze gameplay
        Time.timeScale = 0f;

        var loadOp = SceneManager.LoadSceneAsync(PauseSceneName, LoadSceneMode.Additive);
        while (!loadOp.isDone)
            yield return null;
    }

    private IEnumerator UnloadPauseScene()
    {
        var unloadOp = SceneManager.UnloadSceneAsync(PauseSceneName);
        while (unloadOp != null && !unloadOp.isDone)
            yield return null;

        // resume gameplay
        Time.timeScale = 1f;
    }
}