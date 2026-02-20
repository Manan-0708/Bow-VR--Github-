using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public enum ButtonType { Start, Tutorial, Quit }
    public ButtonType buttonType;

    public void OnPressed()
    {
        Debug.Log("Pressed: " + buttonType);

        switch (buttonType)
        {
            case ButtonType.Start:
                SceneLoader.Instance.LoadGame(false);
                break;

            case ButtonType.Tutorial:
                SceneLoader.Instance.LoadGame(true);
                break;

            case ButtonType.Quit:
                Application.Quit();
                break;
        }
    }
}