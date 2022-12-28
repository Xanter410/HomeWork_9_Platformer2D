using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterController
{
    private HealthPoint healthPoint;

    public void OnMoveInputHandler(InputAction.CallbackContext context)
    {
        _onMoveInput?.Invoke(context.ReadValue<float>());
    }

    public void OnJumpInputHandler(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0)
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
