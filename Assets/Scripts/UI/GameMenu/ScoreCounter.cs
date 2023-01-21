using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreCounterText;
    [SerializeField] private GameEvent _onCoinPickUp;

    private int _Score;

    private void Start()
    {
        _onCoinPickUp.AddListener(CoinPickUp);
        _scoreCounterText.text = _Score.ToString();
    }

    private void OnDestroy()
    {
        _onCoinPickUp.RemoveListener(CoinPickUp);
    }

    private void CoinPickUp()
    {
        _Score++;
        _scoreCounterText.text = _Score.ToString();
    }
}
