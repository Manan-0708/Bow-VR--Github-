using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MenuButton : MonoBehaviour
{
    public enum ButtonType { Start, Tutorial, Quit }
    public ButtonType buttonType;

    public void OnPressed()
    {
        switch (buttonType)
        {
            case ButtonType.Start:
                SceneLoader.Instance.LoadGame();
                break;

            case ButtonType.Tutorial:
                SceneLoader.Instance.LoadGameWithTutorial();
                break;

            case ButtonType.Quit:
                Application.Quit();
                break;
        }
    }
}
