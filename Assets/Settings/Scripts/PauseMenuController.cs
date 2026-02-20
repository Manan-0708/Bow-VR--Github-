using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private string leaderboardSceneName = "LeaderboardScene";
    [SerializeField] private string mainMenuSceneName = "Basic Main Menu";

    private const string PauseSceneName = "EscapeScene";

    // Hook these to the UI Buttons in EscapeScene
    public void OnResume()
    {
        StartCoroutine(ClosePauseAndResume());
    }

    public void OnLeaderboard()
    {
        StartCoroutine(OpenSceneFromPause(leaderboardSceneName));
    }

    public void OnMainMenu()
    {
        StartCoroutine(OpenSceneFromPause(mainMenuSceneName));
    }

    private IEnumerator ClosePauseAndResume()
    {
        var unload = SceneManager.UnloadSceneAsync(PauseSceneName);
        while (unload != null && !unload.isDone)
            yield return null;

        Time.timeScale = 1f;
    }

    private IEnumerator OpenSceneFromPause(string sceneToOpen)
    {
        // Unload pause menu first
        var unload = SceneManager.UnloadSceneAsync(PauseSceneName);
        while (unload != null && !unload.isDone)
            yield return null;

        // Ensure game unpaused for target scene
        Time.timeScale = 1f;

        var load = SceneManager.LoadSceneAsync(sceneToOpen, LoadSceneMode.Single);
        while (!load.isDone)
            yield return null;
    }
}
