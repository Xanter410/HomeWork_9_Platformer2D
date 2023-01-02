using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerDetectZone : MonoBehaviour
{
    private bool _isPlayerDetected;
    private Vector2 _playerPosition;

    private void Start()
    {
        _isPlayerDetected = false;
    }

    public bool TryGetPlayer(out Vector2 playerPosition)
    {
        playerPosition = _playerPosition;
        return _isPlayerDetected;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<PlayerController>(out _))
        {
            _isPlayerDetected = true;
            _playerPosition = col.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.TryGetComponent<PlayerController>(out _))
        {
            _isPlayerDetected = false;
            _playerPosition = Vector2.zero;
        }
    }
}
