using UnityEngine;
using UnityEngine.InputSystem;

public class DisableControl : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    private void OnEnable()
    {
        if (_playerInput != null)
            _playerInput.enabled = false;
    }
    private void OnDisable()
    {
        if (_playerInput != null)
            _playerInput.enabled = true;
    }
}
