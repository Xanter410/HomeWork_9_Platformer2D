using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : CharacterController
{
    private HealthPoint healthPoint;

    protected UnityAction<bool> _onJumpAndDownInput;
    public event UnityAction<bool> OnJumpAndDownInput
    {
        add { _onJumpAndDownInput += value; }
        remove { _onJumpAndDownInput -= value; }
    }

    public void OnMoveInputHandler(InputAction.CallbackContext context)
    {
        _onMoveInput?.Invoke(context.ReadValue<float>());
    }

    public void OnJumpInputHandler(InputAction.CallbackContext context)
    {
        if (_onJumpAndDownInput != null && context.ReadValue<float>() < 0)
        {
            _onJumpAndDownInput?.Invoke(true);
        }
        else if (context.ReadValue<float>() != 0)
        {
            _onJumpInput?.Invoke(true);
        }
    }

    public void OnHitInputHandler(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0)
        {
            _onHitInput?.Invoke(true);
        }
    }

    private void Start()
    {
        if(TryGetComponent(out healthPoint))
        {
            healthPoint.OnDeaded += DeadPlayer;
        }
    }

    private void DeadPlayer()
    {
        Debug.Log("Персонаж погиб");
        //healthPoint.enabled = false;
        //this.enabled = false;
    }
}
