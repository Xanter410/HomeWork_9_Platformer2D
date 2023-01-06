using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Global Events")]
    [SerializeField] private GameEvent _onCoinPickUp;
    [SerializeField] private GameEvent _onPlayerDeaded;

    private void Start()
    {
        _onCoinPickUp.AddListener(CoinPickUp);
        _onPlayerDeaded.AddListener(PlayerDeded);
    }

    private void OnDestroy()
    {
        _onCoinPickUp.RemoveListener(CoinPickUp);
        _onPlayerDeaded.RemoveListener(PlayerDeded);
    }

    private void CoinPickUp()
    {
        Debug.Log("Монета была подобрана");
    }

    private void PlayerDeded()
    {
        Debug.Log("Персонаж погиб");
    }
}
