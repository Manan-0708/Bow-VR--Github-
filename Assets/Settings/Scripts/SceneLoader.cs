using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void LoadGameWithTutorial()
    {
        SceneManager.LoadScene("MainGame");
    }
}
