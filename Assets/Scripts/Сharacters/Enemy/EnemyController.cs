using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : CharacterController
{
    private HealthPoint healthPoint;
    private AIController AIController;

    protected UnityAction<bool> _onDashInput;

    public event UnityAction<bool> OnDashInput
    {
        add { _onDashInput += value; }
        remove { _onDashInput -= value; }
    }

    private void Awake()
    {
        if (TryGetComponent(out healthPoint))
        {
            healthPoint.OnDeaded += DeadEnemy;
        }
        if (!TryGetComponent(out AIController))
        {
            AIController = null;
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
        if (TryGetComponent(out healthPoint))
        {
            healthPoint.OnDeaded += DeadEnemy;
        }
    }

    private void DeadEnemy()
    {
        StartCoroutine(DeleteEnemy());
        AIController.enabled = false;
        healthPoint.enabled = false;
    }

    IEnumerator DeleteEnemy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
