using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ICharacterAction
{
    void Run();
}

public class CharacterController : MonoBehaviour
{
    private readonly List<ICharacterAction> _actions = new List<ICharacterAction>();

    protected UnityAction<float> _onMoveInput;
    protected UnityAction<bool> _onJumpInput;
    protected UnityAction<bool> _onHitInput;

    public event UnityAction<float> OnMoveInput
    {
        add { _onMoveInput += value; }
        remove { _onMoveInput -= value; }
    }
    public event UnityAction<bool> OnJumpInput
    {
        add { _onJumpInput += value; }
        remove { _onJumpInput -= value; }
    }
    public event UnityAction<bool> OnHitImput
    {
        add { _onHitInput += value; }
        remove { _onHitInput -= value; }
    }

    public void Register(ICharacterAction action)
    {
        if (!_actions.Contains(action))
        {
            _actions.Add(action);
        }
    }

    public void UnRegister(ICharacterAction action)
    {
        if (_actions.Contains(action))
        {
            _actions.Remove(action);
        }
    }

    private void FixedUpdate()
    {
        foreach (var action in _actions)
        {
            action.Run();
        }
    }
}
