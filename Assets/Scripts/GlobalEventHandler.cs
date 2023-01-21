using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalEventHandler : MonoBehaviour
{
    [Header("Global Events")]
    [SerializeField] private GameEvent _onPlayerDeaded;

    private void Start()
    {
        _onPlayerDeaded.AddListener(PlayerDeded);
    }

    private void OnDestroy()
    {
        _onPlayerDeaded.RemoveListener(PlayerDeded);
    }

    private void PlayerDeded()
    {
        StartCoroutine(RestartGameWithTimer());
    }

    IEnumerator RestartGameWithTimer()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneTransition.SwitchToScene(SceneManager.GetActiveScene().buildIndex);
    }
}
