using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Global Events")]
    [SerializeField] private GameEvent _onCoinPickUp;
    [SerializeField] private GameEvent _onPlayerDeaded;
    [SerializeField] private GameEvent _onLevelCompleted;

    private void Start()
    {
        _onCoinPickUp.AddListener(CoinPickUp);
        _onPlayerDeaded.AddListener(PlayerDeded);
        _onLevelCompleted.AddListener(LevelCompleted);
    }

    private void OnDestroy()
    {
        _onCoinPickUp.RemoveListener(CoinPickUp);
        _onPlayerDeaded.RemoveListener(PlayerDeded);
        _onLevelCompleted.RemoveListener(LevelCompleted);
    }

    private void CoinPickUp()
    {
        Debug.Log("Монета была подобрана");
    }

    private void PlayerDeded()
    {
        Debug.Log("Персонаж погиб");
        StartCoroutine(RestartGameWithTimer());
    }

    private void LevelCompleted()
    {
        Debug.Log("Уровень завершен");
    }

    IEnumerator RestartGameWithTimer()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneTransition.SwitchToScene(SceneManager.GetActiveScene().buildIndex);
    }
}
