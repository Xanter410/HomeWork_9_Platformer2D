using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject SettingsMenu;

    public void StartGame(int SceneIndex)
    {
        SceneTransition.SwitchToScene(SceneIndex);
    }

    public void ShowSettingsMenu()
    {
        MainMenu.SetActive(false); 
        SettingsMenu.SetActive(true);
    }
    public void CloseSettingsMenu()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }

    public void CloseAplication()
    {
        Application.Quit();
    }
}
