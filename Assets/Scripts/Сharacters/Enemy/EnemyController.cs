using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : CharacterController
{
    private HealthPoint _healthPoint;
    private MonoBehaviour[] _Components;

    protected UnityAction<bool> _onDashInput;

    public event UnityAction<bool> OnDashInput
    {
        add { _onDashInput += value; }
        remove { _onDashInput -= value; }
    }

    private void Awake()
    {
        if (TryGetComponent(out _healthPoint))
        {
            _healthPoint.OnDeaded += DeadEnemy;
        }
    }

    public void OnMoveInputHandler(float value)
    {
        _onMoveInput?.Invoke(value);
    }

    public void OnJumpInputHandler()
    {
        _onJumpInput?.Invoke(true);
    }

    public void OnAttackInputHandler()
    {
        _onHitInput?.Invoke(true);
    }

    public void OnDashInputHandler()
    {
        _onDashInput?.Invoke(true);
    }

    private void Start()
    {
        if (TryGetComponent(out _healthPoint))
        {
            _healthPoint.OnDeaded += DeadEnemy;
        }
    }

    private void DeadEnemy()
    {
        StartCoroutine(DeleteEnemy());

        gameObject.layer = 6; // "EnemyNoCollision"
         
        if (TryGetComponent(out AIController AI))
        {
            AI.enabled = false;
        }
    }

    IEnumerator DeleteEnemy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
