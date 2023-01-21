using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [Header("Menu Panels")]
    [SerializeField] private GameObject _generalMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _scoreTableMenu;

    [Header("Global Events")]
    [SerializeField] private GameEvent _onLevelCompleted;

    private bool _isActiveMenu = false;
    private bool _ActiveGeneralMenu
    {
        set
        {
            _generalMenu.SetActive(value);
        }

    }
    private bool _ActiveSettingsMenu
    {
        set
        {
            _settingsMenu.SetActive(value);
        }

    }
    private bool _ActiveScoreTableMenu
    {
        set
        {
            _scoreTableMenu.SetActive(value);
        }

    }

    private void Start()
    {
        SetActiveGameMenu(_isActiveMenu);
        _onLevelCompleted.AddListener(ShowScoreTableMenu);
    }

    private void OnDestroy()
    {
        _onLevelCompleted.RemoveListener(ShowScoreTableMenu);
    }

    public void ShowScoreTableMenu()
    {
        SetActiveGameMenu(true);
        _ActiveScoreTableMenu = true;
        _ActiveGeneralMenu = false;
        _ActiveSettingsMenu = false;
    }

    public void ShowMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _isActiveMenu = _isActiveMenu ? false : true;
            SetActiveGameMenu(_isActiveMenu);
        }
    }
    public void ContinueButton()
    {
        _ActiveGeneralMenu = true;
        _ActiveSettingsMenu = false;
        SetActiveGameMenu(false);
    }
    public void SettingsButton()
    {
        _ActiveGeneralMenu = false;
        _ActiveSettingsMenu = true;
    }

    public void ExitButton()
    {
        //SetActiveGameMenu(false);

        Time.timeScale = 1;
        SceneTransition.SwitchToScene(0);
    }

    public void NextLevelButton()
    {
        //SetActiveGameMenu(false);
        Time.timeScale = 1;
        var NextSceneIndexBuild = SceneManager.GetActiveScene().buildIndex + 1;

        if (SceneManager.sceneCountInBuildSettings > NextSceneIndexBuild)
        {
            SceneTransition.SwitchToScene(NextSceneIndexBuild);
        }
        else
        {
            SceneTransition.SwitchToScene(0);
        }
    }

    public void RestartCurrentLevel()
    {
        //SetActiveGameMenu(false);
        Time.timeScale = 1;
        SceneTransition.SwitchToScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackSettingsButton()
    {
        _ActiveGeneralMenu = true;
        _ActiveSettingsMenu = false;
    }

    private void SetActiveGameMenu(bool switchActiveMenu)
    {
        gameObject.SetActive(switchActiveMenu);

        if (switchActiveMenu)
        {
            Time.timeScale = 0;
        }
        else
        {
            _ActiveGeneralMenu = true;
            _ActiveSettingsMenu = false;
            _ActiveScoreTableMenu = false;
            Time.timeScale = 1;
        }
    }
}
