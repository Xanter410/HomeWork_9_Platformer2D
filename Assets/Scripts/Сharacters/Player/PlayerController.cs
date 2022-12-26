using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterController
{
    public void OnMoveInputHandler(InputAction.CallbackContext context)
    {
        _onMoveInput?.Invoke(context.ReadValue<Vector2>());
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
}
